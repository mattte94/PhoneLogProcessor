using PhoneLogProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneLogProcessor.Logic
{
    public class DataLogic
    {
        private List<Country> Countries;
        private List<Area> Areas;
        private List<CallData> CallData;
        private readonly List<ProcessedCallData> ProcessedCallData = new List<ProcessedCallData>();
        private readonly IFileHandler fileHandler = new FileHandler();

        public void LoadDataFromFiles(string path)
        {
            var result = fileHandler.FileReading(path);
            Areas = result.areas;
            Countries = result.countries;
            CallData = result.callData;
        }

        public void Process()
        {
            ProcessedCallData.Add(new ProcessedCallData()
            {
                CallDuring = 2,
                CountryId = 3,
                CountryName = "Blabla",
                DistrictId = 4,
                DistrictName = "Nyet"
            });
        }
        public void WriteDataToFile(string path)
        {
            fileHandler.FileWriting(path, ProcessedCallData);
        }
    }
}
