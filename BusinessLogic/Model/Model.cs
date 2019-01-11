namespace BusinessLogic.Model
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
