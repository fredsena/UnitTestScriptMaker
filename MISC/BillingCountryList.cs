using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class BillingTableList
    {
        [Fact]
        public void DbScriptByTemplate_BillingTableList()
        {
            string[]  TableCodeList  = { "AD", "AE", "AL", "AM", "AO", "AS", "AT", "AU", "AZ", "BA", "BB", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BM", "BR", "BS", "BW", "BY", "CA", "CD", "CG", "CH", "CI", "CL", "CM", "CN", "CY", "CZ", "DE", "DK", "DZ", "EE", "EG", "ES", "ET", "FI", "FJ", "FO", "FR", "GA", "UK", "GE", "GF", "GG", "GH", "GI", "GL", "GP", "GR", "GW", "GY", "HK", "HM", "HR", "HT", "HU", "IE", "IL", "IM", "IN", "IQ", "IS", "IT", "JE", "JO", "JP", "KE", "KG", "KR", "KW", "KY", "KZ", "LB", "LI", "LR", "LT", "LU", "LV", "LY", "MA", "MC", "MD", "ME", "MG", "MH", "MK", "ML", "MQ", "MR", "MT", "MU", "MX", "MY", "MZ", "NA", "NC", "NE", "NG", "NI", "NL", "NO", "NP", "NZ", "OM", "PA", "PE", "PF", "PH", "PL", "PT", "QA", "RE", "RO", "RS", "RU", "RW", "SA", "SB", "SC", "SE", "SG", "SI", "SK", "SL", "SM", "SN", "SZ", "TF", "TG", "TH", "TM", "TN", "TR", "TW", "TZ", "UA", "UG", "US", "UZ", "VA", "VU", "YE", "YT", "ZA", "ZM", "ZW" }; 

            string path = @"C:\BillingTESTMappings\";
            string filename = "GenerateScript_Map_Billing_Countries_21-10-2021_TEST";
            string fileToSearch = @"C:\BillingTESTMappings\Map_Billing_Countries.csv";
            
            var builder = new StringBuilder();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //BU_Code	XYZ	BU	TYPE	Table_CODE	Table

            builder.AppendLine("BillingTableList");

            foreach (var Table in TableCodeList)
            {
                builder.Append(Table + "|");
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
                    builder.Append("'"+data[0]+"',");
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
