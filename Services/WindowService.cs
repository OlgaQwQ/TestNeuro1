using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNeuro.Models;
using TestNeuro.Services.Interfaces;
using TestNeuro.Views.Windows;

namespace TestNeuro.Services
{
    internal class WindowService : IWindowService
    {
        private WindowService() { }

        private Window1 editor;

        private static WindowService Instance;

        public static WindowService getInstance()
        {
            if (Instance == null)
                Instance = new WindowService();
            return Instance;
        }

        public void EditList(object parameter)
        {
            editor = new Window1(parameter as Parameter);
            
            editor.Show();
        }

        public void CloseListEditor()
        {
            if(editor != null) editor.Close();
        }
    }
}
