using System.Linq;
using System.Text;

namespace CSV_Reader.HelperClasses
{
    public class CSVHelper 
    {
        public static string replaceCommas(string content)
        {                            
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string s in content.Split(','))
            {
                string replacement = $"{s.Replace("\"",""),50}";
                stringBuilder.Append(s.Replace(s, replacement));
            }
            return stringBuilder.ToString();          
        }

        public static string GetHtmlFromCsvFile(string csvFile)
        {
            return GetHtmlFromCsv(System.IO.File.ReadAllLines(csvFile));
        }


        public static string GetHtmlFromCsv(string[] csvLines)
        {
            var headerLine = csvLines[0];
            var columns = headerLine.Split(',').Select((v, i) => new { ColumnName = v, ColumnIndex = i });

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table align=\"center\" border=\"1\">");
            sb.AppendLine("<thead>");
            sb.AppendLine("</thead>");

            foreach (var column in columns)
            {
                sb.Append("<th>" + column.ColumnName + "</th>");
            }

            sb.AppendLine("<tbody>");

            var dataLines = csvLines.Skip(1);
            dataLines.ToList().ForEach(line => {
                var data = line.Split(',');
                sb.AppendLine("<tr>");
                foreach (var column in columns)
                {
                    sb.AppendLine("<td style=\"text-align:center\">" + data[column.ColumnIndex] + "</td>");
                }
                sb.AppendLine("</tr>");
            });

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}
