using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSV_Reader.HelperClasses
{
    public class Helper
    {
        public static void saveFileAsTxt(string content)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputTxt.txt";
            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(save.OpenFile()))
                {
                    writer.WriteLine(content);
                    writer.Dispose();

                    MessageBox.Show("saved!", "Info");
                }
            }
        }

        // in work !!
        public static void saveFileAsHtml(string content)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputHtml.html";
            save.Filter = "html File | *.html";

            if (save.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(save.OpenFile()))
                {
                    writer.WriteLine(content);
                    writer.Dispose();

                    MessageBox.Show("saved!", "Info");
                }
            }
        }

        public static async void startProgressBar(ProgressBar progressBar)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar.Value++;
                await Task.Delay(150);
                if (progressBar.Value == 100) break;
            }
        }

        public static void fillProgressBar(ProgressBar progressBar)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar.Value++;
            }
            MessageBox.Show("Process done!", "CSV to txt");           
        }

        public static void openFileAsTxt(string selectedFile)
        {
            if (File.Exists(selectedFile))
            {
                Process.Start("notepad.exe", selectedFile);
            }
            else MessageBox.Show("Not a File", "Error");
        }

        public static string readText(string selectedFile)
        {
            string content;
            try
            {
                using (StreamReader sr = new StreamReader(selectedFile))
                {
                    content = sr.ReadToEnd();
                    content.Split(',');
                    sr.Dispose();
                    sr.Close();
                    return content;
                }
            }
            catch(ArgumentNullException e)
            {
                MessageBox.Show("No file selected", "Error");
            }
            return null;
        }

        public static async void startCount(string selectedFile ,string content)
        {
            Task taskRead = new Task(() => content = Helper.readText(selectedFile));
            taskRead.Start();
            await taskRead;
            taskRead.Dispose();
            WordCounter.countWords(content);
        }

        public static void fillListBox(ListBox listBox, Dictionary<string, int> dic)
        {
            listBox.Items.Clear();
            foreach (var word in dic)
            {
                listBox.Items.Add($"{word.Key,-22}\t{word.Value,10}");
            }
            
        }

    }
}
