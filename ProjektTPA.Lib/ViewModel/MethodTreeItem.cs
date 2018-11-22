using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ProjektTPA.Lib.Model;
using ProjektTPA.Lib.Model.Enums;

namespace ProjektTPA.Lib.ViewModel
{
    public class MethodTreeItem : TreeViewItem
    {
        public MethodModel MethodModel { get; set; }
        public override string Name { get; set; }
        public MethodTreeItem(MethodModel methodModel) : base(methodModel.Name)
        {
            MethodModel = methodModel;
            TreeType = TreeTypeEnum.Method;
            Children = PrepareChildrenInstance();
            ResolveFullName();
        }

        public sealed override ObservableCollection<TreeViewItem> PrepareChildrenInstance()
        {
            var ret = new ObservableCollection<TreeViewItem>();
            if((MethodModel.ReturnType != null && MethodModel.ReturnType.Name != "Void") || MethodModel.Parameters.Count() != 0 || MethodModel.Attributes.Count() != 0)
                ret.Add(null);
            return ret;
        }

        private void ResolveFullName()
        {
            if (MethodModel.Modifiers == null)
                return;
            StringBuilder builder = new StringBuilder();
            builder.Append(MethodModel.Modifiers.Item1);
            if (MethodModel.Modifiers.Item2 != AbstractEnum.NotAbstract)
                builder.Append(" ").Append(MethodModel.Modifiers.Item2);
            if (MethodModel.Modifiers.Item3 == StaticEnum.Static)
                builder.Append(" ").Append(MethodModel.Modifiers.Item3);
            if (MethodModel.Modifiers.Item4 == VirtualEnum.Virtual)
                builder.Append(" ").Append(VirtualEnum.Virtual);


            if(MethodModel.ReturnType != null)
                builder.Append(" ").Append(MethodModel.ReturnType.Name).Append(" ");
            builder.Append(Name).Append("(");

            for (int i = 0; i < MethodModel.Parameters.Count() - 1;i++)
            {
                var parm = MethodModel.Parameters.ElementAt(i);
                builder.Append(parm.TypeModel.Name).Append(" ").Append(parm.Name).Append(", ");
            }
            if(MethodModel.Parameters.LastOrDefault() != null)
                builder.Append(MethodModel.Parameters.Last().TypeModel.Name).Append(" ").Append(MethodModel.Parameters.Last().Name);


            builder.Append(")");
            

            Name = builder.ToString();
        }

        public override void BuildMyself()
        {
            base.BuildMyself();
            if(MethodModel.GenericArguments != null)
            {
                foreach (var genericArgument in MethodModel.GenericArguments)
                {
                    Children.Add(new TypeTreeItem(genericArgument));
                }
            }

            if(MethodModel.Parameters != null)
            {
                foreach (var parameter in MethodModel.Parameters)
                {
                    Children.Add(new ParameterTreeItem(parameter));
                }
            }

            if(MethodModel.Attributes != null)
            {
                foreach (var attribute in MethodModel.Attributes)
                {
                    Children.Add(new AttributeTreeItem(attribute));
                }
            }

            if(MethodModel.ReturnType != null)
            {
                Children.Add(new TypeTreeItem(MethodModel.ReturnType));
            }

        }
    }
}