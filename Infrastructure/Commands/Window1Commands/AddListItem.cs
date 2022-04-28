using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestNeuro.Services;
using WpfApp2.Infrastructure.Commands.Base;

namespace TestNeuro.Infrastructure.Commands.Window1Commands
{
    internal class AddListItem : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var ListItems = parameter as ObservableCollection<string>;
            ListItems.Add($"Значение {ListItems.Count + 1}");
        }
    }

    internal class RemoveListItem : Command
    {
        public override bool CanExecute(object parameter)
        {
            var values = parameter as object[];
            if (parameter == null) return false;
            else if(values[1] == null) return false;
            else return true;
        }

        public override void Execute(object parameter)
        {
            var values = parameter as object[];
            var ListItems = values[0] as ObservableCollection<string>;
            var SelectedListItem = values[1] as string;
            
            ListItems.Remove(SelectedListItem);
        }
    }

    internal class MoveListItenUp : Command
    {
        public override bool CanExecute(object parameter)
        {
            var values = parameter as object[];
            if (parameter == null) return false;
            else if (values[1] == null) return false;
            else return true;
        }

        public override void Execute(object parameter)
        {
            var values = parameter as object[];
            var ListItems = values[0] as ObservableCollection<string>;
            var SelectedListItem = values[1] as string;

            int currentIndex = ListItems.IndexOf(SelectedListItem);
            if(currentIndex == 0) return;
            ListItems.Move(currentIndex, currentIndex - 1);
        }
    }

    internal class MoveListItenDown : Command
    {
        public override bool CanExecute(object parameter)
        {
            var values = parameter as object[];
            if (parameter == null) return false;
            else if (values[1] == null) return false;
            else return true;
        }

        public override void Execute(object parameter)
        {
            var values = parameter as object[];
            var ListItems = values[0] as ObservableCollection<string>;
            var SelectedListItem = values[1] as string;

            int currentIndex = ListItems.IndexOf(SelectedListItem);
            if (currentIndex == ListItems.Count - 1) return;
            ListItems.Move(currentIndex, currentIndex + 1);
        }
    }

    internal class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            WindowService.getInstance().CloseListEditor();
        }
    }

    class Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
