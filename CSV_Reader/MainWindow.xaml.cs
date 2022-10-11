using CSV_Reader.HelperClasses;
using CSV_Reader.otherClasses;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CSV_Reader
{
    /// <summary>
    /// source : https://medium.com/@mikependon/designing-a-wpf-treeview-file-explorer-565a3f13f6f2
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string selectedFile;
        private static string content;
        private static string converted;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFileSystemObjects();
            xD();
        }

        private void InitializeFileSystemObjects()
        {
            var drives = DriveInfo.GetDrives();
            DriveInfo.GetDrives().ToList().ForEach(drive =>
            {
                var fileSystemObject = new FileSystemObjectInfo(drive);
                fileSystemObject.BeforeExplore += FileSystemObject_BeforeExplore;
                fileSystemObject.AfterExplore += FileSystemObject_AfterExplore;
                treeView.Items.Add(fileSystemObject);
            });
        }

        private void FileSystemObject_AfterExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void FileSystemObject_BeforeExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Wait;
        }

        private async void buttonConvert_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Value = 0;
            if (File.Exists(selectedFile))
            {
                Helper.startProgressBar(progressBar);

                Task taskRead = new Task(() => content = Helper.readText(selectedFile));
                taskRead.Start();
                await taskRead;
                taskRead.Dispose();

                Task taskCSV = new Task(() => converted = CSVHelper.replaceCommas(content));
                taskCSV.Start();
                await taskCSV;
                taskCSV.Dispose();
                WordCounter.countWords(converted);
                Helper.fillProgressBar(progressBar);
            }
            else MessageBox.Show("Not a File", "Error");
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (content == null && converted == null) 
            { 
                MessageBox.Show("No File selected", "Error"); 
            }
            if (content != null && converted == null)
            {
                if (progressBar.Value == 100)
                {
                    Helper.saveFileAsTxt(content);
                    progressBar.Value = 0;
                }
                else MessageBox.Show("Please wait for finishing the converting Process", "Warning");
            }

            if (content != null && converted != null)
            {
                if (progressBar.Value == 100)
                {
                    Helper.saveFileAsTxt(converted);
                    progressBar.Value = 0;
                }
                else MessageBox.Show("Please wait for finishing the converting Process", "Warning");
            }
        }

        private void buttonClearTB_Click(object sender, RoutedEventArgs e)
        {
            textBoxFilePath.Text = "";
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedFile = ((CSV_Reader.otherClasses.FileSystemObjectInfo)treeView.SelectedItem).FileSystemInfo.FullName;
            textBoxFilePath.Text = selectedFile ;

        }

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(selectedFile))
            {
                Helper.openFileAsTxt(selectedFile);
            }
        } 
        private async void xD()
        {
            int x = 0, y = 0, z = 0;
            while (1 == 1) {
                for (int i = 0; i < 255; i++) {                    
                    x+=1; y += 1; z +=1;
                    var brush = new SolidColorBrush(Color.FromArgb(150, (byte)x, (byte)y, (byte)z));
                    window.Background = brush;
                    await Task.Delay(1);
                }
                for (int i = 0; i < 255; i++)
                {
                    x -= 1; y -= 1; z -= 1;
                    var brush = new SolidColorBrush(Color.FromArgb(150, (byte)x, (byte)y, (byte)z));
                    window.Background = brush;
                    await Task.Delay(1);
                }
            }
        }

        private async void buttonCount_Click(object sender, RoutedEventArgs e)
        {
            Helper.startProgressBar(progressBar);
            Task taskRead = new Task(() => content = Helper.readText(selectedFile));
            taskRead.Start();
            await taskRead;
            taskRead.Dispose();
            
            Task taskCount = new Task(() => WordCounter.countWords(content));
            taskCount.Start();
            await taskCount;
            taskCount.Dispose();
            Helper.fillListBox(listBoxWordFreq, WordCounter.countFrequency(WordCounter.contentToArray(content)));
            Helper.fillProgressBar(progressBar);

        }

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (progressBar.Value == 0) textBlockProgressBarPercentage.Text = "";
            textBlockProgressBarPercentage.Text = progressBar.Value.ToString()+"%";
        }

        private async void buttonConv_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Value = 0;
            if (File.Exists(selectedFile))
            {
                Helper.startProgressBar(progressBar);

                Task taskCSV = new Task(() => content = CSVHelper.GetHtmlFromCsvFile(selectedFile));
                taskCSV.Start();
                await taskCSV;
                taskCSV.Dispose();
                WordCounter.countWords(content);
                Helper.fillProgressBar(progressBar);
            }
            else MessageBox.Show("Not a File", "Error");
        }

        private void buttonSaveHtml_Click(object sender, RoutedEventArgs e)
        {
            if (content == null && converted == null)
            {
                MessageBox.Show("No File selected", "Error");
            }
            if (content != null && converted == null)
            {
                if (progressBar.Value == 100)
                {
                    Helper.saveFileAsHtml(content);
                    progressBar.Value = 0;
                }
                else MessageBox.Show("Please wait for finishing the converting Process", "Warning");
            }

            if (content != null && converted != null)
            {
                if (progressBar.Value == 100)
                {
                    Helper.saveFileAsHtml(converted);
                    progressBar.Value = 0;
                }
                else MessageBox.Show("Please wait for finishing the converting Process", "Warning");
            }
        }
    }
}
