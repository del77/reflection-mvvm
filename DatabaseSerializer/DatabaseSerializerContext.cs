using System.ComponentModel.Composition;
using System.Data.Entity;
using DatabaseSerializer.Model;
using MEF;

namespace DatabaseSerializer
{
    public class DatabaseSerializerContext : DbContext
    {
        public DatabaseSerializerContext() : base("SerializingDatabase")
        {
            Database.SetInitializer<DatabaseSerializerContext>(new DropCreateDatabaseIfModelChanges<DatabaseSerializerContext>());
        }

        public DbSet<AssemblyDb> Assemblies { get; set; }
        public DbSet<NamespaceDb> Namespaces { get; set; }
        public DbSet<FieldDb> Fields { get; set; }
        public DbSet<MethodDb> Methods { get; set; }
        public DbSet<PropertyDb> Properties { get; set; }
        public DbSet<TypeDb> Types { get; set; }
    }
}