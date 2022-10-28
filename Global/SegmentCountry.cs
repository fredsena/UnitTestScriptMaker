using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject.Global_Info
{
    public class SegmentCountry
    {
        [Fact]
        public void DEPLOY_BusinessTableTableRegion_Billing()
        {
            string path = @"C:\DATA-SCRIPTS\BillingTESTMappings\";
            string filename = "DEPLOY_Script_BusinessTableTableRegion_Billing_10-10-2022";
            string fileToSearch = @"C:\DATA-SCRIPTS\BillingTESTMappings\BusinessTableTableRegion4081.txt";

            var builder = new StringBuilder();

            builder.AppendLine("--DEPLOY");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                builder.AppendLine();

                var TableList = data[1].TrimEnd(',');

                //var TableListDoubleQuoted = TableList.Replace("'", "");

                builder.AppendLine(GetScript_Map_Billing_Countries_INSERT()
                    .Replace("#TableCode#", data[0].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        private string GetSearchVariables()
        {
            return @"DECLARE @TableRegionId_#TableCode# INT
SELECT TOP 1 @TableRegionId_#TableCode#=id from TableRegion where TableCode = '#TableCode#'";
        }

        public string GetScript_Map_Billing_Countries_INSERT()
        {
            return @"-- #number#
INSERT INTO BusinessTableTableRegion (TableRegionId, BusinessTableId, AddressPurposeType, [Default])
SELECT DISTINCT  @TableRegionId_#TableCode# as TableRegionId, BSG.id as BusinessTableId, 'Billing' as AddressPurposeType, 0 as [Default]
FROM Table BSG	
	INNER JOIN table BU on BU.id = BSG.BusinessUnitId
WHERE RTRIM(LTRIM(BU.Name)) IN ('C2')
AND BSG.id NOT IN (
			SELECT DISTINCT BSG.id FROM BusinessTableTableRegion BSCR
				INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
				INNER JOIN table BU on BU.id = BSG.BusinessUnitId
				INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
			WHERE RTRIM(LTRIM(BU.Name)) IN ('C2')
			AND BSCR.TableRegionId = @TableRegionId_#TableCode#
			AND BSCR.AddressPurposeType = 'Billing')

PRINT 'SUCCESS: ##number#: TableCode: #TableCode# billing to Countries: (C2) were successfully mapped/inserted into BusinessTableTableRegion table'

";
        }

        [Fact]
        public void ScriptValidation_Map_Billing_Countries()
        {
            string path = @"C:\DATA-SCRIPTS\BillingTESTMappings\";
            string filename = "VALIDATE_Script_BusinessTableTableRegion4081_Billing_10-10-2022";
            string fileToSearch = @"C:\DATA-SCRIPTS\BillingTESTMappings\BusinessTableTableRegion4081.txt";

            var builder = new StringBuilder();

            builder.AppendLine("--VALIDATION");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                builder.AppendLine();

                //var TableList = data[1].TrimEnd(',');
                //var TableListDoubleQuoted = TableList.Replace("'", "");

                builder.AppendLine(Map_Billing_Countries_VALIDATION()
                    .Replace("#TableCode#", data[0].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string Map_Billing_Countries_VALIDATION()
        {
            return @"-- #number#
IF ( EXISTS ( 
	SELECT * FROM BusinessTableTableRegion 
	WHERE TableRegionId = @TableRegionId_#TableCode# 
		AND AddressPurposeType = 'Billing'
		AND [Default] = 0
		AND BusinessTableId IN (
			SELECT DISTINCT BSG.id FROM BusinessTableTableRegion BSCR
				INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
				INNER JOIN table BU on BU.id = BSG.BusinessUnitId
				INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
			WHERE RTRIM(LTRIM(BU.Name)) IN ('C2')
			AND BSCR.TableRegionId = @TableRegionId_#TableCode#
			AND BSCR.AddressPurposeType = 'Billing' ) ))
	PRINT 'SUCCESS: ##number#: TableCode: #TableCode# billing to Table: (C2) were successfully mapped/inserted into BusinessTableTableRegion table'
ELSE
	PRINT 'FAILED: ##number#: TableCode: #TableCode# billing to Table: (C2) were NOT mapped/inserted into BusinessTableTableRegion'
";
        }

        public string GetDataFromFile(string filePath)
        {
            try
            {
                //return File.ReadAllText(filePath, Encoding.GetEncoding("ISO-8859-1"));
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



