using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;
using ViewModel.Enums;

namespace ViewModel.ViewModel
{
    public class TypeTreeItem : TreeViewItem
    {
        public TypeModel TypeModel { get; set; }
        public override string Name { get; set; }
        public TypeTreeItem(TypeModel typeModel) : base(typeModel.Name)
        {
            TypeModel = typeModel;
            ResolveFullName();
            Children = PrepareChildrenInstance();

        }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            
            if(!TypeModel.IsGeneric && TypeModel.Name != "Void" && TypeModel.Resolved)
                ret.Add(null);
            
            return ret;
        }

        private void ResolveFullName()
        {
            TreeType = TreeTypeEnum.Type;
            if (TypeModel.Modifiers == null)
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(TypeModel.Modifiers.Item1);
            if (TypeModel.Modifiers.Item2 == SealedEnum.Sealed)
            {
                builder.Append(" ").Append(TypeModel.Modifiers.Item2);
            }

            if (TypeModel.Modifiers.Item3 == AbstractEnum.Abstract)
            {
                builder.Append(" ").Append(AbstractEnum.Abstract);
            }

            builder.Append(" ").Append(TypeModel.TypeKind.ToString()).Append(" ");
            builder.Append(Name);
            if (TypeModel.ImplementedInterfaces.Any())
            {
                builder.Append(" : ");
                for (int i = 0; i < TypeModel.ImplementedInterfaces.Count() -1; i++)
                {
                    builder.Append(TypeModel.ImplementedInterfaces.ElementAt(i).Name).Append(", ");
                }


                builder.Append(TypeModel.ImplementedInterfaces.Last().Name);
                if (TypeModel.BaseType != null)
                    builder.Append(", ").Append(TypeModel.BaseType.Name);
            }
            else if (TypeModel.BaseType != null)
                builder.Append(" : ").Append(TypeModel.BaseType.Name);

            Name = builder.ToString();
            //builder.Append(TypeModel.Modifiers.Item2 == SealedEnum.Sealed ? buiSealedEnum.Sealed.ToString() : string.Empty);
            //builder.Append(TypeModel.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString() : string.Empty).Append(" class ");


        }

       

        public override void BuildMyself()
        {
            base.BuildMyself();
            if (TypeModel.Attributes != null)
            {
                foreach (var attribute in TypeModel.Attributes)
                {
                    Children.Add(new AttributeTreeItem(attribute));
                }
            }

            if (TypeModel.Constructors != null)
            {
                foreach (var constructor in TypeModel.Constructors)
                {
                    Children.Add(new MethodTreeItem(constructor));
                }
            }

            if(TypeModel.GenericArguments != null)
            {
                foreach (var argument in TypeModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(argument));
                }
            }

            if(TypeModel.ImplementedInterfaces != null)
            {
                foreach (var implementedInterface in TypeModel.ImplementedInterfaces)
                {
                    Children.Add(new TypeTreeItem(implementedInterface)); // ???
                }
            }

            if(TypeModel.Methods != null)
            {
                foreach (var method in TypeModel.Methods)
                {
                    Children.Add(new MethodTreeItem(method));
                }
            }

            if(TypeModel.NestedTypes != null)
            {
                foreach (var nested in TypeModel.NestedTypes)
                {
                    Children.Add(new TypeTreeItem(nested));
                }
            }

            if(TypeModel.Properties != null)
            {
                foreach (var property in TypeModel.Properties)
                {
                    Children.Add(new PropertyTreeItem(property));
                }
            }

            if(TypeModel.BaseType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.BaseType));
            }

            if(TypeModel.DeclaringType != null)
            {
                Children.Add(new TypeTreeItem(TypeModel.DeclaringType));
            }

            if (TypeModel.Fields != null)
            {
                foreach (var field in TypeModel.Fields)
                {
                    Children.Add(new FieldTreeItem(field));
                }
            }

        }
    }
}