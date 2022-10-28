using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class NewSTATETableRegions2
    {
        [Fact]
        public void DbScriptByTemplate_NewSTATETableRegions()
        {
            string path = @"C:\TableStateMappings\";
            string filename = "GenerateScript_NewSTATETableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\TableStateMappings\StateTableRegion_New States2.csv";

            //string[] list = { "#TableCode#", "#TableDescription#", "#StateName#", "#StateCode#" };
            
            var builder = new StringBuilder();

            builder.AppendLine("--DEPLOY");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            builder.AppendLine(GetSearchVariables().Replace("#TableCode#", "AD"));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var TableCode = "AD";

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[0].Trim() != TableCode)
                {
                    builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                    builder.AppendLine();
                    TableCode = data[0].Trim();
                }

                builder.AppendLine(GetNewSTATETableRegions_INSERT()
                    .Replace("#TableCode#", data[0].Trim().Replace("'", "''"))
                    .Replace("#TableDescription#", data[1].Trim().Replace("'", "''"))
                    .Replace("#StateName#", data[2].Trim().Replace("'", "''"))
                    .Replace("#StateCode#", data[3].Trim().Replace("'", "''"))
                    .Replace("#number#", (i + 1).ToString().Replace("'", "''"))
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

        public string GetNewSTATETableRegions_INSERT()
        {
            return @"-- #number#. StateCode = #StateCode# | StateName = #StateName# | TableCode = #TableCode# | TableDescription = #TableDescription# |
IF (NOT EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#StateCode#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'		
		AND TableRegionId = @TableRegionId_#TableCode# ) )
	BEGIN
		BEGIN TRY			
			INSERT INTO StateTableRegion (StateCode, StateName, TableRegionId) 
			SELECT '#StateCode#' as StateCode, N'#StateName#' as StateName, @TableRegionId_#TableCode# as TableRegionId
			PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were successfully inserted into StateTableRegion table'			
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were NOT inserted into StateTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were ALREADY inserted into StateTableRegion table'
";
        }


        [Fact]
        public void ScriptValidation_NewSTATETableRegions()
        {
            string path = @"C:\TableStateMappings\";
            string filename = "Validation_NewSTATETableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\TableStateMappings\StateTableRegion_New States2.csv";

            var builder = new StringBuilder();

            builder.AppendLine("--VALIDATION");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            builder.AppendLine(GetSearchVariables().Replace("#TableCode#", "AD"));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var TableCode = "AD";

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[0].Trim() != TableCode)
                {
                    builder.AppendLine(GetSearchVariables().Replace("#TableCode#", data[0].Trim()));
                    builder.AppendLine();
                    TableCode = data[0].Trim();
                }

                builder.AppendLine(NewSTATETableRegions_VALIDATION()
                    .Replace("#TableCode#", data[0].Trim().Replace("'", "''"))
                    .Replace("#TableDescription#", data[1].Trim().Replace("'", "''"))
                    .Replace("#StateName#", data[2].Trim().Replace("'", "''"))
                    .Replace("#StateCode#", data[3].Trim().Replace("'", "''"))
                    .Replace("#number#", (i + 1).ToString().Replace("'", "''"))
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string NewSTATETableRegions_VALIDATION()
        {
            return @"-- #number#. StateCode = #StateCode# | StateName = #StateName# | TableCode = #TableCode# | TableDescription = #TableDescription# |
IF ( EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#StateCode#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'		
		AND TableRegionId = @TableRegionId_#TableCode# ) )
	PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were successfully inserted into StateTableRegion table'			
ELSE
	PRINT 'FAILED: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were NOT inserted into StateTableRegion table '
";
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

