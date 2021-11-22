using PhoneLogProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneLogProcessor.Logic
{
    public class DataLogic : IDataLogic
    {
        //MEMBERS
        private List<Country> Countries;
        private List<Area> Areas;
        private List<CallData> CallData;
        private List<ProcessedCallData> ProcessedCallData;
        private IEnumerable<int> countryLengths;
        private IEnumerable<int> areaLengths;
        private readonly IFileHandler fileHandler = new FileHandler();

        //PUBLICS

        /// <summary>
        /// A 3 fájlból (area.txt, country.txt, input.txt) való adatok beolvasása és eltárolása.
        /// </summary>
        /// <param name="path">A 3 fájlt tartalmazó mappa elérési útvonala.</param>
        public void LoadDataFromFiles(string path)
        {
            var result = fileHandler.FileReading(path);
            Areas = result.areas;
            Countries = result.countries;
            CallData = result.callData;
        }

        /// <summary>
        /// Műveletek végrehajtás és az eredmény eltárolása.
        /// </summary>
        public void Process()
        {
            countryLengths = Countries.Select(x => x.CountryId.ToString().Length).Distinct();
            areaLengths = Areas.Select(x => x.DistrictId.ToString().Length).Distinct();
            ProcessedCallData = GeneratedData();
        }

        /// <summary>
        /// Az eredmény fájlba való kiíratása (output.txt)
        /// </summary>
        /// <param name="path">Mappa elérési útvonala, ahova az output.txt el fog tárolódni.</param>
        public void WriteDataToFile(string path)
        {
            fileHandler.FileWriting(path, ProcessedCallData);
        }

        //PRIVATES

        /// <summary>
        /// Adatok legenerálása a bejövő adatok alapján.
        /// </summary>
        /// <returns>Output.txt fájlba mentendő adatok listája.</returns>
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

        /// <summary>
        /// A hívott fél telefonszáma alapján az ország lekérése történik.
        /// Visszatérési érték egy Tuple, melynek adatai:
        /// - az ország összes adata
        /// - az ország hívókódjának a hossza
        /// </summary>
        /// <param name="data">Hívott fél telefonszámát tartalmazó input adat</param>
        /// <returns></returns>
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

        /// <summary>
        /// A GetCoutry visszatérési értéke és a hívott fél telefonszáma alapján a körzet lekérése történik.
        /// Visszatérési érték a megtalált Körzet adatai lesznek
        /// </summary>
        /// <param name="countryTuple">Ország adatai, ország hívókódjának hossza</param>
        /// <param name="data">Hívott fél telefonszámát tartalmazó input adat</param>
        /// <returns></returns>
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
