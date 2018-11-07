using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektTPA.Lib.Model
{
    public abstract class Model
    {
        public string Name { get; set; }

        protected Model(string name)
        {
            Name = name;
        }
    }
}
