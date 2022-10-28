using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class TableStateMappingsJAPAN
    {
        [Fact]
        public void DbScriptByTemplate_TableStateMappings()
        {
            string filename = "Update_TableStateMappingsJAPAN_DbScriptByTemplate_28-07-2021";
            string fileToSearch = @"C:\TableStateMappings\TableStateMappings_Japan.txt";

            var builder = new StringBuilder();
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();
            //builder.AppendLine(GetInitialDeleteRows());

            string path = @"C:\TableStateMappings\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TableStateMappings(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                //RegionName'				LocalStateName		RegionCode2	
                //Hokkaido,		JP,JP,JAPAN,北海道,				01

                builder.AppendLine(GetTableStateMappings_UPDATE()
                    .Replace("#RegionName#", data[0].Trim().Replace("'", "''"))
                    .Replace("#Region#", data[1].Trim())
                    .Replace("#TableCode#", data[2].Trim())
                    .Replace("#LocalStateName#", data[4].Trim())
                    .Replace("#RegionCode2#", data[5].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            builder.AppendLine(
                "PRINT 'SUCCESS: Region Codes JP were successfully updated on StateTableRegion table for replacement with new data'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTableStateMappings_UPDATE()
        {
            //Region,TableCode, TableName,          RegionName, RegionCode2 
            //XYZ,    AE,        UNITED ARAB EMIRATES, AJMAN,      AJ

            //#Region#,
            //#TableCode#,
            //TableName,
            //#RegionName#,
            //#RegionCode2#

            return @"-- #number#. RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = #RegionName# | LocalStateName = #LocalStateName# | TableCode = #TableCode# | 
IF (NOT EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#RegionCode2#'
		AND LTRIM(RTRIM(StateName)) = N'#RegionName#'
		AND LTRIM(RTRIM(LocalStateName)) = N'#LocalStateName#'
		AND TableRegionId = (select top 1 id from TableRegion where RegionCode = 'JP' AND TableCode = 'JP') ) )
	BEGIN
		BEGIN TRY			
			UPDATE StateTableRegion SET StateName = N'#RegionName#', LocalStateName = N'#LocalStateName#'
			WHERE TableRegionId = (select top 1 id from TableRegion where RegionCode = 'JP' AND TableCode = 'JP')
			and StateCode = '#RegionCode2#'			
			PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | LocalStateName = ' + N'#LocalStateName#' + ' | TableCode = #TableCode# | were successfully updated on StateTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | LocalStateName = ' + N'#LocalStateName#' + ' | TableCode = #TableCode# | were NOT updated on StateTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | LocalStateName = ' + N'#LocalStateName#' + ' | TableCode = #TableCode# | were ALREADY updated on StateTableRegion table'
GO";
        }

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
	SELECT TOP 1 1 from StateTableRegion where Id in (
	Select scr.Id from StateTableRegion scr 
	Inner join TableRegion cr on scr.TableRegionId = cr.Id
	INNER JOIN tableTableRegion bcr on bcr.TableRegionId = cr.Id 
	INNER JOIN table bu on bu.id  = bcr.BusinessUnitId  
	where ( RTRIM(LTRIM(cr.RegionCode)) IN ('XYZ', 'JP', 'AP') OR cr.id IN  (select id from TableRegion where TableCode = 'MX') ) )
	))
	BEGIN
		BEGIN TRY	
			DELETE from StateTableRegion where Id in (
				Select scr.Id from StateTableRegion scr 
					Inner join TableRegion cr on scr.TableRegionId = cr.Id
					INNER JOIN tableTableRegion bcr on bcr.TableRegionId = cr.Id 
					INNER JOIN table bu on bu.id  = bcr.BusinessUnitId  
				WHERE ( RTRIM(LTRIM(cr.RegionCode)) IN ('XYZ', 'JP', 'AP') OR cr.id IN  (select id from TableRegion where TableCode = 'MX') ) )			
			PRINT 'SUCCESS: Region Codes XYZ/JP/AP successfully removed from StateTableRegion table for replacement with new data'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Region Codes XYZ/JP/AP were NOT removed from StateTableRegion table (for replacement with new data) due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: Region Codes XYZ/JP/AP was ALREADY removed from StateTableRegion table for replacement with new data'
GO
";
        }

        [Fact]
        public void ScriptValidation_TestClassesTestCategories()
        {
            string filename = "Validation_TableStateMappingsJAPAN_DbScriptByTemplate_28-07-2021";
            string fileToSearch = @"C:\TableStateMappings\TableStateMappings_Japan.txt";

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

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                builder.AppendLine(TableStateMappings_VALIDATION()
                    .Replace("#RegionName#", data[0].Trim().Replace("'", "''"))
                    .Replace("#Region#", data[1].Trim())
                    .Replace("#TableCode#", data[2].Trim())
                    .Replace("#LocalStateName#", data[4].Trim())
                    .Replace("#RegionCode2#", data[5].Trim())
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
            return @"-- #number#. RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = #RegionName# | LocalStateName = #LocalStateName# | TableCode = #TableCode#
IF (EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#RegionCode2#'
		AND LTRIM(RTRIM(StateName)) = N'#RegionName#'
		AND LTRIM(RTRIM(LocalStateName)) = N'#LocalStateName#'
		AND TableRegionId = (select top 1 id from TableRegion where RegionCode = 'JP' AND TableCode = 'JP') ) )	
	PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | LocalStateName = ' + N'#LocalStateName#' + ' | TableCode = #TableCode# | were successfully updated on StateTableRegion table'
ELSE
	PRINT 'FAILED: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | LocalStateName = ' + N'#LocalStateName#' + ' | TableCode = #TableCode# | were NOT updated on StateTableRegion table' 
GO";
        }
    }
}
