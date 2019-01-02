using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DatabaseSerializer.Model;
using DtoLayer;
using MEF;

namespace DatabaseSerializer
{
    [Export(typeof(ISerializer))]
    public class DatabaseSerializer : ISerializer
    {
        
    }
}