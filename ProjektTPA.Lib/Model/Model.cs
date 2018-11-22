using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ProjektTPA.Lib.Model
{
    [DataContract(IsReference = true)]
    public abstract class Model
    {
        [DataMember]
        public string Name { get; set; }

        protected Model(string name)
        {
            Name = name;
        }
    }
}
