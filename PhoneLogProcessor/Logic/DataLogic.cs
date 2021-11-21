using PhoneLogProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneLogProcessor.Logic
{
    public class DataLogic
    {
        private List<Country> Countries;
        private List<Area> Areas;
        private List<CallData> CallData;
        private List<ProcessedCallData> ProcessedCallData;
        private IEnumerable<int> countryLengths;
        private IEnumerable<int> areaLengths;
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
            countryLengths = Countries.Select(x => x.CountryId.ToString().Length).Distinct();
            areaLengths = Areas.Select(x => x.DistrictId.ToString().Length).Distinct();
            ProcessedCallData = GeneratedData();
        }
        public void WriteDataToFile(string path)
        {
            fileHandler.FileWriting(path, ProcessedCallData);
        }

        private List<ProcessedCallData> GeneratedData()
        {
            var list = new List<ProcessedCallData>();

            foreach (var item in CallData)
            {
                var x = GetCountry(item);
                var y = GetArea(x, item);

                if (list.Any(r => r.CountryId == x.country.CountryId && r.DistrictId == y.DistrictId))
                    list.Single(r => r.CountryId == x.country.CountryId && r.DistrictId == y.DistrictId).CallDuring += item.CallDuration;
                else
                    list.Add(new ProcessedCallData()
                    {
                        CountryName = x.country.Name,
                        DistrictName = y.Name,
                        CountryId = x.country.CountryId,
                        DistrictId = y.DistrictId,
                        CallDuring = item.CallDuration
                    });
            }

            return (List<ProcessedCallData>)list.OrderByDescending(x => x.CountryName).ThenBy(y => y.DistrictName).ToList();
        }

        private (Country country, int length) GetCountry(CallData data)
        {
            string number = data.CalledPersonPhoneNumber.Trim('+');

            foreach (var length in countryLengths)
            {
                string numberCountry = number.Substring(0, length);

                if (Countries.Any(x => x.CountryId.ToString() == numberCountry))
                    return (Countries.Single(x => x.CountryId.ToString() == numberCountry), length);
            }

            throw new Exception();
        }

        private Area GetArea((Country country, int length) countryTuple, CallData data)
        {
            var areas = Areas.Where(x => countryTuple.country.CountryId == x.CountryId);
            string number = data.CalledPersonPhoneNumber.Trim('+').Substring(countryTuple.length);

            foreach (var areaLength in areaLengths)
            {
                string numberArea = number.Substring(0, areaLength);

                if (areas.Any(x => x.DistrictId.ToString() == numberArea))
                    return areas.Single(x => x.DistrictId.ToString() == numberArea);
            }

            throw new Exception();
        }
    }
}
