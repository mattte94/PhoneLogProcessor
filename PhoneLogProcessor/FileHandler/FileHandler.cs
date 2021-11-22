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

        //PUBLICS

        /// <summary>
        /// 3 bemeneti fájl (area.txt, country.txt, input.txt) beolvasása.
        /// </summary>
        /// <param name="path">A 3 bemeneti fájlt tartalmazó mappa elérési útvonala.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Output.txt fájlba történő adatok mentése.
        /// </summary>
        /// <param name="path">Output.txt fájlt tartalmazó mappa elérési útvonala.</param>
        /// <param name="processCallData">Output.txt fájlba írandó adatok.</param>
        /// <returns></returns>
        public async Task FileWritingAsync(string path, List<ProcessedCallData> processCallData)
        {
            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException();

            if (!Directory.Exists(path))
                throw new FileNotFoundException();

            if (File.Exists(Path.Combine(path, OUTPUT)))
                throw new Exception("Output.txt already exists!");

            using StreamWriter file = new(Path.Combine(path, OUTPUT));

            foreach (var line in processCallData)
            {
                await file.WriteLineAsync(line.ToString());
            }
        }

        //PRIVATES

        /// <summary>
        /// Area.txt fájl adatainak beolvasása és eltárolása.
        /// </summary>
        /// <param name="path">Area.txt fájlt tartalmazó mappa elérési útvonala.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Country.txt fájl adatainak beolvasása és eltárolása.
        /// </summary>
        /// <param name="path">Country.txt fájlt tartalmazó mappa elérési útvonala.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Input.txt fájl adatainak beolvasása és eltárolása.
        /// </summary>
        /// <param name="path">Input.txt fájlt tartalmazó mappa elérési útvonala.</param>
        /// <returns></returns>
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
    }
}
