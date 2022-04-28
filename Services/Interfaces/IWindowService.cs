using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNeuro.Services.Interfaces
{
    internal interface IWindowService
    {
        void EditList(object parameter);

        void CloseListEditor();
    }
}
