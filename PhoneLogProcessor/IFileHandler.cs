using PhoneLogProcessor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneLogProcessor
{
    internal interface IFileHandler
    {
        (List<Area> areas, List<Country> countries, List<CallData> callData) FileReading(string path);
        Task FileWriting(string path, List<ProcessedCallData> processCallData);
    }
}