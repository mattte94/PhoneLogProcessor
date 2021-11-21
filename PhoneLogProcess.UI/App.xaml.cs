using PhoneLogProcess.UI.ViewModel;
using PhoneLogProcessor.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PhoneLogProcess.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataLogic logic;
        private PhoneLogProcessVM phoneLogProcessVM;

        public App()
        {
            logic = new DataLogic();
            phoneLogProcessVM = new PhoneLogProcessVM(logic);

            MainWindow = new MainWindow()
            {
                DataContext = phoneLogProcessVM
            };
            MainWindow.Show();
        }
    }
}
