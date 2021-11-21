using PhoneLogProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogProcessor
{
    internal class FileHandler : IFileHandler
    {
        //MEMBERS
        private const string AREA = "area.txt";
        private const string COUNTRY = "country.txt";
        private const string INPUT = "input.txt";
        private const string OUTPUT = "output.txt";
        private const string OUTPUT_BACK = "output_back";

        //PUBLICS
        public (List<Area>, List<Country>, List<CallData>) FileReading(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException();

            if (!Directory.Exists(path))
                throw new FileNotFoundException();

            if (!File.Exists(Path.Combine(path, AREA)))
                throw new FileNotFoundException(AREA);

            if (!File.Exists(Path.Combine(path, COUNTRY)))
                throw new FileNotFoundException(COUNTRY);

            if (!File.Exists(Path.Combine(path, INPUT)))
                throw new FileNotFoundException(INPUT);

            return (ReadArea(Path.Combine(path, AREA)), ReadCountry(Path.Combine(path, COUNTRY)), ReadCallData(Path.Combine(path, INPUT)));
        }

        public async Task FileWriting(string path, List<ProcessedCallData> processCallData)
        {
            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException();

            if (!Directory.Exists(path))
                throw new FileNotFoundException();

            if (File.Exists(Path.Combine(path, OUTPUT)))
                CreateBackUp(path);

            using StreamWriter file = new(Path.Combine(path, OUTPUT));

            foreach (var line in processCallData)
            {
                await file.WriteLineAsync(line.ToString());
            }
        }

        //PRIVATES

        private List<Area> ReadArea(string path)
        {
            List<Area> readedAreaList = new List<Area>();

            foreach (string line in File.ReadLines(path, Encoding.Latin1))
            {
                string[] columns = line.Split('\t');

                if (columns.Length != 3)
                    throw new InvalidDataException();

                int.TryParse(columns[0], out int countryId);
                int.TryParse(columns[1], out int districtId);

                readedAreaList.Add(new Area(countryId, districtId, columns[2]));
            }

            return readedAreaList;
        }

        private List<Country> ReadCountry(string path)
        {
            List<Country> readedCountryList = new List<Country>();

            foreach (string line in File.ReadLines(path, Encoding.Latin1))
            {
                string[] columns = line.Split('\t');

                if (columns.Length != 2)
                    throw new InvalidDataException();

                int.TryParse(columns[0], out int countryId);

                readedCountryList.Add(new Country(countryId, columns[1]));
            }

            return readedCountryList;
        }

        private List<CallData> ReadCallData(string path)
        {
            List<CallData> readedCallDataList = new List<CallData>();

            foreach (string line in File.ReadLines(path))
            {
                string[] columns = line.Split('\t');

                if (columns.Length != 5)
                    throw new InvalidDataException();

                DateTime callDate = DateTime.Parse(columns[0]);
                DateTime callTime = DateTime.Parse(columns[1]);
                int.TryParse(columns[4], out int callDuration);

                readedCallDataList.Add(new CallData(callDate, callTime, columns[2], columns[3], callDuration));
            }

            return readedCallDataList;
        }

        private void CreateBackUp(string path) => File.Move(Path.Combine(path, OUTPUT), Path.Combine(path, $"{OUTPUT_BACK}_{File.GetCreationTime(Path.Combine(path, OUTPUT)):HH_mm_ss}.txt"));
    }
}
