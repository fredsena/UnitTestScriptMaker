using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class BusinessSegTableRegTEST
    {
        [Fact]
        public void DbScriptByTemplate_BusinessSegTableRegTEST()
        {
            string path = @"C:\BillingTESTMappings\";
            string filename = "DEPLOY_Script_Map_TEST_Countries_22-10-2021";
            string fileToSearch = @"C:\BillingTESTMappings\Script_Mapped_TEST_Countries.txt";

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

                string[] data = row[i].Split(new[] { "|" }, StringSplitOptions.None);

                builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                builder.AppendLine();

                var TableList = data[1].TrimEnd(',');

                var TableListDoubleQuoted = TableList.Replace("'", "");

                builder.AppendLine(GetScript_Map_TEST_Countries_INSERT()
                    .Replace("#TableCode#", data[0].Trim())
                    .Replace("#TableList#", TableList)
                    .Replace("#TableListDoubleQuoted#", TableListDoubleQuoted)
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

        public string GetScript_Map_TEST_Countries_INSERT()
        {
            return @"-- #number#
BEGIN
	BEGIN TRY
		INSERT INTO BusinessTableTableRegion (TableRegionId, BusinessTableId, AddressPurposeType, [Default])
		SELECT DISTINCT @TableRegionId_#TableCode# as TableRegionId, BSG.id as BusinessTableId, 'TEST' as AddressPurposeType, 0 as [Default]
		FROM BusinessTableTableRegion BSCR
			INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
			INNER JOIN table BU on BU.id = BSG.BusinessUnitId
			INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
		WHERE RTRIM(LTRIM(BU.Name)) IN (#TableList#)
		AND BSCR.BusinessTableId NOT IN (
					SELECT DISTINCT BSG.id FROM BusinessTableTableRegion BSCR
						INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
						INNER JOIN table BU on BU.id = BSG.BusinessUnitId
						INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
					WHERE RTRIM(LTRIM(BU.Name)) IN (#TableList#)
					AND BSCR.TableRegionId = @TableRegionId_#TableCode#
                    AND BSCR.AddressPurposeType = 'TEST')

		PRINT 'SUCCESS: ##number#: TableCode: #TableCode# TEST to Countries: (#TableListDoubleQuoted#) were successfully mapped/inserted into BusinessTableTableRegion table'
	END TRY
	BEGIN CATCH
		PRINT 'FAILED: ##number#: TableCode: #TableCode# TEST to Countries: (#TableListDoubleQuoted#) were NOT mapped/inserted into BusinessTableTableRegion due to: ' + ERROR_MESSAGE()
	END CATCH
END
";
        }

        [Fact]
        public void ScriptValidation_Map_TEST_Countries()
        {
            string path = @"C:\BillingTESTMappings\";
            string filename = "VALIDATE_Script_Map_TEST_Countries_22-10-2021";
            string fileToSearch = @"C:\BillingTESTMappings\Script_Mapped_TEST_Countries.txt";

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

                string[] data = row[i].Split(new[] { "|" }, StringSplitOptions.None);

                builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                builder.AppendLine();

                var TableList = data[1].TrimEnd(',');

                var TableListDoubleQuoted = TableList.Replace("'", "");

                builder.AppendLine(Map_TEST_Countries_VALIDATION()
                    .Replace("#TableCode#", data[0].Trim())
                    .Replace("#TableList#", TableList)
                    .Replace("#TableListDoubleQuoted#", TableListDoubleQuoted)
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string Map_TEST_Countries_VALIDATION()
        {
            return @"-- #number#
IF ( EXISTS ( 
	SELECT * FROM BusinessTableTableRegion 
	WHERE TableRegionId = @TableRegionId_#TableCode# 
		AND AddressPurposeType = 'TEST'
		AND [Default] = 0
		AND BusinessTableId IN (
				SELECT DISTINCT BSG.id FROM BusinessTableTableRegion BSCR
				INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
				INNER JOIN table BU on BU.id = BSG.BusinessUnitId
				INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
				WHERE RTRIM(LTRIM(BU.Name)) IN (#TableList#) ) ))
	PRINT 'SUCCESS: ##number#: TableCode: #TableCode# TEST to Countries: (#TableListDoubleQuoted#) were successfully mapped/inserted into BusinessTableTableRegion table'
ELSE
	PRINT 'FAILED: ##number#: TableCode: #TableCode# TEST to Countries: (#TableListDoubleQuoted#) were NOT mapped/inserted into BusinessTableTableRegion'
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



