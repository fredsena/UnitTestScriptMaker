using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject.JsonCSV
{
    public class ConvertJsonCSV2
    {
        static string path = @"C:\DATA-SCRIPTS\";
        string fileToWrite = path + "G4_BusinessConfig_EDGES2.csv";
        string fileToSearch = path + "Edges2.json";

        [Fact]
        public void Script_ConvertInconsistentJsonToCSV()
        {
            StreamReader r = new StreamReader(fileToSearch);
            string jsonString = r.ReadToEnd();
            r.Close();

            List<Rootobject> data = JsonConvert.DeserializeObject<List<Rootobject>>(jsonString);

            var builder = new StringBuilder();

            foreach (var item in data)
            {
                builder.AppendLine( $"{item.from};{item.to};{item.type};{item.value}");
            }
            
            File.WriteAllText(fileToWrite, builder.ToString().Substring(0, builder.Length));            
        }
    }


    public class Rootobject
    {        
        public string from { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public string value { get; set; }        
    }
}
