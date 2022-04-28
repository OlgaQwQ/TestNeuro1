using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestNeuro.Models;
using TestNeuro.Views.Windows;
using WpfApp2.Infrastructure.Commands;
using WpfApp2.ViewModels.Base;


using System.IO;
using Newtonsoft.Json;
using TestNeuro.Services;

namespace TestNeuro.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        private ObservableCollection<Parameter> _parameters;

        public ObservableCollection<Parameter> Parameters
        {
            get => _parameters;
            set => Set(ref _parameters, value);
        }

        /*private ObservableCollection<string> _types;
        public ObservableCollection<string> Types
        {
            get => _types;
            set => Set(ref _types, value);
        }*/

        private Parameter _selectedParameter;

        public Parameter SelectedParameter
        {
            get => _selectedParameter;
            set => Set(ref _selectedParameter, value);
        }

        private TypeList _selectedListItem;

        public TypeList SelectedListItem
        {
            get => _selectedListItem;
            set => Set(ref _selectedListItem, value);
        }

        #endregion

        public readonly string FileName = "Parameters.json";

        private ObservableCollection<TypeList> _types = new ObservableCollection<TypeList>()
        {
            new TypeList("Простая строка", false),
            new TypeList("Строка с историей", false),
            new TypeList("Значение из списка", true),
            new TypeList("Набор значений из списка", true)
        };

        public ObservableCollection<TypeList> Types
        {
            get => _types;
            set => Set(ref _types, value);
        }


        #region Commands

        #region AddNewParameter

        public ICommand AddNewParameterCommand { get; }

        private bool CanAddNewParameterCommandExecute(object o) => true;

        private void OnAddNewParameterCommandExecute(object o)
        {
            Parameters.Add(new Parameter
            {
                Name = $"Параметр {Parameters.Count + 1}",
                SelectedTypeIndex = 0,
                ListItems = new ObservableCollection<string>()
            });
        }

        #endregion

        #region RemoveParameter

        public ICommand RemoveParameterCommand { get; }

        private bool CanRemoveParameterCommandExecute(object o)
        {
            if (SelectedParameter != null)
                return true;
            else return false;
        }

        private void OnRemoveParameterCommandExecute(object o) => Parameters.Remove(SelectedParameter);

        #endregion

        #region MoveParameterUp

        public ICommand MoveParameterUpCommand { get; }

        private bool CanMoveParameterUpCommandExecute(object o)
        {
            if (SelectedParameter != null)
                return true;
            else return false;
        }

        private void OnMoveParameterUpCommandExecute(object o)
        {
            int currentIndex = Parameters.IndexOf(SelectedParameter);
            if (currentIndex == 0)
                return;
            Parameters.Move(currentIndex, currentIndex - 1);
        }

        #endregion

        #region MoveParameterDown

        public ICommand MoveParameterDownCommand { get; }

        private bool CanMoveParameterDownCommandExecute(object o)
        {
            if (SelectedParameter != null)
                return true;
            else return false;
        }

        private void OnMoveParameterDownCommandExecute(object o)
        {
            int currentIndex = Parameters.IndexOf(SelectedParameter);
            if (currentIndex == Parameters.Count - 1)
                return;
            Parameters.Move(currentIndex, currentIndex + 1);
        }

        #endregion

        #region CloseApp

        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecute(object o) => true;

        private void OnCloseAppCommandExecute(object o)
        {
            Application.Current.Shutdown();
        }

        #endregion
                
        #region OpenListWindow

        public ICommand OpenListWindowCommand { get; }

        private bool CanOpenListWindowCommandExecute(object o)
        {                        
            if (o == null)
                return false;
            else
            {
                var TypeIndex = (int)o;
                if (TypeIndex >= Types.Count) TypeIndex = Types.Count - 1;
                var TypeName = Types.ElementAt(TypeIndex);
                return TypeName.IsListNeeded;
            }
        }
        public void OnOpenListWindowCommandExecute(object o)
        {
            WindowService.getInstance().EditList(SelectedParameter);
        }

        #endregion        

        #region SaveChangesAndCloseApp

        public ICommand SaveChangesAndCloseAppCommand { get; }

        private bool CanSaveChangesAndCloseAppCommandExecute(object o) => true;

        private void OnSaveChangesAndCloseAppCommandExecute(object o)
        {
            File.Delete(FileName);
           // FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
           StreamWriter sw = new StreamWriter(new FileStream (FileName,FileMode.OpenOrCreate) , Encoding.Default);

           
            //JsonWriter writer = new JsonTextWriter(sw);
            var serializer = new Newtonsoft.Json.JsonSerializer();
      
            serializer.Serialize(sw,Parameters);
            sw.Flush(); 
            sw.Close();
            Application.Current.Shutdown();
        }

        #endregion
                
        #endregion

        public MainWindowViewModel()
        {
            if (File.Exists(FileName))
            {
                FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                var serializer = new Newtonsoft.Json.JsonSerializer();
                Parameters = serializer.Deserialize<ObservableCollection<Parameter>>(new JsonTextReader(new StringReader (textFromFile)));
                fs.Close();
            }
            else Parameters = new ObservableCollection<Parameter> { };
            
            foreach (Parameter parameter in Parameters)
            {
                if(parameter.SelectedTypeIndex >= Types.Count) parameter.SelectedTypeIndex = Types.Count - 1;
            }

            /*Types = new ObservableCollection<string>()
            {
                "Простая строка",
                "Строка с историей",
                "Значение из списка",
                "Набор значений из списка"
            };*/

            //SelectedListItem = Types.First().TypeName;
                                   
            #region Commands

            AddNewParameterCommand = new RelayCommand(OnAddNewParameterCommandExecute, CanAddNewParameterCommandExecute);
            RemoveParameterCommand = new RelayCommand(OnRemoveParameterCommandExecute, CanRemoveParameterCommandExecute);
            MoveParameterUpCommand = new RelayCommand(OnMoveParameterUpCommandExecute, CanMoveParameterUpCommandExecute);
            MoveParameterDownCommand = new RelayCommand(OnMoveParameterDownCommandExecute, CanMoveParameterDownCommandExecute);
            CloseAppCommand = new RelayCommand(OnCloseAppCommandExecute, CanCloseAppCommandExecute);
            OpenListWindowCommand = new RelayCommand(OnOpenListWindowCommandExecute, CanOpenListWindowCommandExecute);
            SaveChangesAndCloseAppCommand = new RelayCommand(OnSaveChangesAndCloseAppCommandExecute, CanSaveChangesAndCloseAppCommandExecute);

            #endregion
        }
    }
}
