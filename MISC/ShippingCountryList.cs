using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class TESTTableList
    {
        [Fact]
        public void DbScriptByTemplate_TESTTableList()
        {
            string[] TableCodeList = { "AD", "AE", "AL", "AM", "AO", "AS", "AT", "AU", "AX", "AZ", "BA", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BW", "BY", "CA", "CD", "CG", "CH", "CI", "CM", "CN", "CR", "CV", "CY", "CZ", "DE", "DJ", "DK", "DM", "DZ", "EE", "EG", "ER", "ES", "ET", "FI", "FO", "FR", "GA", "UK", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GP", "GR", "GT", "GW", "GY", "HK", "HM", "HN", "HR", "HT", "HU", "IE", "IL", "IM", "IN", "IQ", "IS", "IT", "JE", "JO", "JP", "KE", "KG", "KM", "KW", "KZ", "LB", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY", "MA", "MC", "MD", "ME", "MG", "MH", "MK", "ML", "MO", "MQ", "MR", "MT", "MU", "MW", "MY", "MZ", "NA", "NC", "NE", "NG", "NI", "NL", "NO", "NP", "OM", "PF", "PH", "PL", "PN", "PT", "PW", "QA", "RE", "RO", "RS", "RU", "RW", "SA", "SB", "SC", "SE", "SG", "SH", "SI", "SK", "SL", "SM", "SN", "SR", "SS", "ST", "SZ", "TD", "TF", "TG", "TJ", "TL", "TM", "TN", "TR", "TT", "TZ", "UA", "UG", "UM", "US", "UZ", "VA", "VC", "VU", "YE", "YT", "ZA", "ZM", "ZW" };

            string path = @"C:\BillingTESTMappings\";
            string filename = "GenerateScript_Map_TEST_Countries_21-10-2021_TEST";
            string fileToSearch = @"C:\BillingTESTMappings\Map_TEST_Countries.csv";

            var builder = new StringBuilder();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //BU_Code	XYZ	BU	TYPE	Table_CODE	Table

            builder.AppendLine("TESTTableList");

            foreach (var Table in TableCodeList)
            {
                builder.Append(Table+"|");
                GetTableBuCodes(row, Table, ref builder);
                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        private void GetTableBuCodes(string[] row, string TableCode, ref StringBuilder builder)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[4].Trim() == TableCode)
                {
                    builder.Append("'" + data[0] + "',");
                }
            }
        }

        public string GetDataFromFile(string filePath)
        {
            try
            {
                //return File.ReadAllText(filePath, Encoding.GetEncoding("ISO-8859-1"));
                return File.ReadAllText(filePath, Encoding.GetEncoding("UTF-16"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

