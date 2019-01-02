namespace MEF
{
    public interface ISerializer
    {
        void Serialize(object model);
        object Deserialize();
    }
}