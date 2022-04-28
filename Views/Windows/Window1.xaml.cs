using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestNeuro.Models;
using TestNeuro.ViewModels;

namespace TestNeuro.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Parameter _Parameter { get; set; }

        public string SelectedListItem { get; set; }

        public Window1(Parameter parameter)
        {
            _Parameter = parameter;
            InitializeComponent();
        }

    }
}
