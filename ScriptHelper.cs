using System;
using System.IO;
using System.Text;

namespace UnitTestProject
{
    public class ScriptHelper
    {
        public void GenerateFile(
            string path, string filename, string fileToSearch, 
            string databaseUseName, string [] rowList, string templateString, 
            string separator, string scriptTitle = "", string initialScript = "", string middleScript = "", string finalScript = "")
        {
            var builder = new StringBuilder();

            builder.AppendLine("--"+scriptTitle);
            builder.AppendLine(databaseUseName);
            builder.AppendLine(initialScript);
            builder.AppendLine();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { separator }, StringSplitOptions.None);

                var tempString = templateString;
                var sb = new StringBuilder(tempString);

                for (int j = 0; j < rowList.Length; j++)
                {
                    sb.Replace(rowList[j], data[j].Trim().Replace("'", "''"));
                }

                sb.Replace("#number#", (i + 1).ToString());

                builder.AppendLine(sb.ToString());

                if (!string.IsNullOrEmpty(middleScript)) 
                    builder.AppendLine(middleScript);
            }

            if (!string.IsNullOrEmpty(finalScript))
                builder.AppendLine(finalScript);

            builder.AppendLine();

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
        }

        private static string GetDataFromFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
