using PhoneLogProcessor.Logic;
using System;

namespace PhoneLogProcess.Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLogic dataLogic = new DataLogic();
            dataLogic.LoadDataFromFiles(@"C:\Users\Matthew\OneDrive\Asztali gép\Konasoft beadandó");
            dataLogic.Process();
            dataLogic.WriteDataToFile(@"C:\Users\Matthew\OneDrive\Asztali gép\Konasoft beadandó\Megoldas");
        }
    }
}
