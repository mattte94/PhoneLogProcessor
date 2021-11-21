using PhoneLogProcessor.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneLogProcess.UI.ViewModel
{
    public class PhoneLogProcessVM : ViewModelBase
    {
        private readonly IDataLogic logic;
        private string inputPath;
        private string outputPath;

        public DelegateCommand StartProcessCommand { get; set; }
        public DelegateCommand SelectInputDirectoryCommand { get; set; }
        public DelegateCommand SelectOutputDirectoryCommand { get; set; }

        public PhoneLogProcessVM(IDataLogic logic)
        {
            this.logic = logic;
        }

        protected override void InitializeCommands()
        {
            StartProcessCommand = new DelegateCommand(StartProcessFunction);
            SelectInputDirectoryCommand = new DelegateCommand(SelectInputDirectoryFunction);
            SelectOutputDirectoryCommand = new DelegateCommand(SelectOutputDirectoryFunction);
        }

        private void SelectOutputDirectoryFunction(object obj)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    outputPath = fbd.SelectedPath;
            }
        }

        private void SelectInputDirectoryFunction(object obj)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    inputPath = fbd.SelectedPath;
            }
        }

        private void StartProcessFunction(object obj)
        {
            if (string.IsNullOrEmpty(inputPath))
            {
                MessageBox.Show("Input directory path is empty!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("OutPut directory path is empty!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                logic.LoadDataFromFiles(inputPath);
                logic.Process();
                logic.WriteDataToFile(outputPath);
                MessageBox.Show("Data process finished.", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
