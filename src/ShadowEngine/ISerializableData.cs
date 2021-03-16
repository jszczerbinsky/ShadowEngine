namespace ShadowEngine
{
    public interface ISerializableData
    {
        void BeforeSerialization();
        void AfterDeserialization();
    }
}
