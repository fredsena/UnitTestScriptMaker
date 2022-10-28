using System;
using System.IO;
using System.Text;
using Xunit;
namespace UnitTestProject
{
    public class StateTableRegionBr
    {
        [Fact]
        public void DbScriptByTemplate_TableStateMappings()
        {

            string filename = "Insert_StateTableRegionBr_DbScriptByTemplate_31-08-2021";
            string fileToSearch = @"C:\TableStateMappings\Brazilian-States.txt";

            var builder = new StringBuilder();
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();
            builder.AppendLine(GetInitialDeleteRows());

            string path = @"C:\TableStateMappings\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TableStateMappings(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetTableStateMappings_INSERT_INTO()
                    .Replace("#State#", data[0].Trim())
                    .Replace("#StateName#", data[1].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            builder.AppendLine(
                "PRINT 'SUCCESS: Brazilian Region Codes were successfully inserted on StateTableRegion table for replacement with new data'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTableStateMappings_INSERT_INTO()
        {
            return @"-- #number#. State = #State# | StateName = #StateName# 
IF (NOT EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#State#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'
		AND TableRegionId = (select id from TableRegion where TableCode = 'BR' and TableDescription = 'Brazil') ) )
	BEGIN
		BEGIN TRY	
			INSERT INTO StateTableRegion (StateCode, StateName, TableRegionId) 
			SELECT '#State#' as StateCode, N'#StateName#' as StateName, (select id from TableRegion where TableCode = 'BR' and TableDescription = 'Brazil' ) as TableRegionId
			PRINT 'SUCCESS: StateCode = #State# | StateName = ' + N'#StateName#' + ' | were successfully inserted on StateTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: StateCode = #State# | StateName = ' + N'#StateName#' + ' | were NOT inserted on StateTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: StateCode = #State# | StateName = ' + N'#StateName#' + ' | were ALREADY inserted on StateTableRegion table'
GO
";      }

        public string GetDataFromFile_for_TableStateMappings(string filePath)
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

        public string GetInitialDeleteRows()
        {
            return @"IF ( EXISTS (	
	SELECT TOP 1 1 from StateTableRegion 
	where TableRegionId in (select id from TableRegion where TableCode = 'BR' and TableDescription = 'Brazil') ) )	
	BEGIN
		BEGIN TRY	
			DELETE from StateTableRegion where TableRegionId in (select id from TableRegion where TableCode = 'BR' and TableDescription = 'Brazil')			
			PRINT 'SUCCESS: Brazilian Region Codes successfully removed from StateTableRegion table for replacement with new data'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Brazilian Region Codes were NOT removed from StateTableRegion table (for replacement with new data) due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: Brazilian Region Codes were ALREADY removed from StateTableRegion table for replacement with new data'
GO
";
        }

        [Fact]
        public void ScriptValidation_StateTableRegionBr()
        {
            string filename = "Validation_StateTableRegionBr_DbScriptByTemplate_31-08-2021";
            string fileToSearch = @"C:\TableStateMappings\Brazilian-States.txt";

            var builder = new StringBuilder();
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            string path = @"C:\TableStateMappings\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TableStateMappings(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(TableStateMappings_VALIDATION()
                    .Replace("#State#", data[0].Trim())
                    .Replace("#StateName#", data[1].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );
                builder.AppendLine();
            }

            builder.AppendLine();

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string TableStateMappings_VALIDATION()
        {
            return @"-- #number#. State = #State# | StateName = #StateName# 
IF (EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#State#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'
		AND TableRegionId = (select id from TableRegion where TableCode = 'BR' and TableDescription = 'Brazil') ) )	
	PRINT 'SUCCESS: StateCode = #State# | StateName = ' + N'#StateName#' + ' | were successfully inserted on StateTableRegion table'
ELSE
	PRINT 'FAILED: StateCode = #State# | StateName = ' + N'#StateName#' + ' | were NOT inserted on StateTableRegion table ' 
GO";
        }
    }
}
