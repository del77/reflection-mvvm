using System;
using System.ComponentModel.Composition;
using System.IO;
using ViewModel.Interfaces;

namespace ViewModelTests
{
    [Export(typeof(IPathProvider))]
    class PathProvider : IPathProvider
    {
        public string GetPath() => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\test.dll"));
    }
}