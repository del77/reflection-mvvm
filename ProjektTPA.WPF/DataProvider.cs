using Microsoft.Win32;
using ProjektTPA.Lib.Utility;
using ProjektTPA.Lib.ViewModel;

namespace ProjektTPA.WPF
{
    public class DataProvider : IDataProvider
    {
        public string GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            if (dialog.ShowDialog() == true)
            {
                return (dialog.FileName);
            }
            else
                return "";
        }
    }
}