using System.ComponentModel.Composition;
using Microsoft.Win32;
using ViewModel.Interfaces;

namespace ProjektTPA.WPF
{
    [Export(typeof(IPathProvider))]
    public class WPFPathProvider : IPathProvider
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