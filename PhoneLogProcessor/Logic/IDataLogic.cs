namespace PhoneLogProcessor.Logic
{
    public interface IDataLogic
    {
        public void LoadDataFromFiles(string path);
        public void Process();
        public void WriteDataToFile(string path);
    }
}