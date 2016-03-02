namespace DataLoading
{
    public interface IDataIO
    {
        string ReadData(string name);
        void WriteData(string name, string data);
        void ClearData(string name);
    }
}
