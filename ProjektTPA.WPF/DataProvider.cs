using System.ComponentModel.Composition;
using Microsoft.Win32;
using ProjektTPA.Lib.Utility;
using ProjektTPA.Lib.ViewModel;

namespace ProjektTPA.WPF
{
    [Export(typeof(IDataProvider))]
    public class DataProvider : IDataProvider
    {
        public string GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll|" + "XML File (*.xml)| *.xml"
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