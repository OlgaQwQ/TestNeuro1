using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace TestNeuro.Models
{
    [Serializable]
    public class Parameter
    {
        public string Name { get; set; }
                     
        public int SelectedTypeIndex { get; set; }
                
        public ObservableCollection<string> ListItems { get; set; }

    }

    public class TypeList
    {
        public string TypeName { get; set; }

        public bool IsListNeeded { get; set; }

        public TypeList(string TypeName, bool IsListNeeded)
        {
            this.TypeName = TypeName;
            this.IsListNeeded = IsListNeeded;
        }
    }

}
