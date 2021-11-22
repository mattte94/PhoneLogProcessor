using System.Threading.Tasks;

namespace PhoneLogProcessor.Logic
{
    public interface IDataLogic
    {
        void LoadDataFromFiles(string path);
        void Process();
        Task WriteDataToFileAsync(string path);
    }
}