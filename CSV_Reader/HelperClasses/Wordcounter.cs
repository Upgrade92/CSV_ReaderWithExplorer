using System.Collections.Generic;
using System.Windows;

namespace CSV_Reader.HelperClasses
{
    public class WordCounter
    {
        public string Word { get; set; }
        public int Frequency { get; set; }

        public WordCounter(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public static string[] contentToArray(string content)
        {        
            char[] sep = { ' ', ',', '!', '?', '"', '\'', '*', '-', '.', '\r' ,'\n','\b' };
            string[] result = content.Split(sep);

            return result;
        }


        public static void countWords(string txt)
        {
            int count = 0;
            char[] sep = { ' ', ',', '!', '?', '"', '\'', '*', '-', '.', '\r', '\n', '\b' };
            string[] words = txt.Split(sep);
            foreach (string word in words) count++;
            MessageBox.Show(words.Length.ToString());
            countFrequency(words);
        }


        public static Dictionary<string,int> countFrequency(string[] words)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (!(counts.ContainsKey(word)))
                {
                    counts.Add(word, 1);
                }
                else counts[word]++;
            }            
            return counts;
        }

    }
}
