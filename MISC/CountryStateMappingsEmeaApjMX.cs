using System;
using System.IO;
using System.Text;
using Xunit;
namespace UnitTestProject
{
    public class TableStateMappingsXYZApjMX
    {
        [Fact]
        public void DbScriptByTemplate_TableStateMappings()
        {
            //#Region#,
            //#TableCode#,
            //TableName,
            //#RegionName#,
            //#RegionCode2#

            string filename = "Insert_TableStateMappings_DbScriptByTemplate_16-07-2021";
            string fileToSearch = @"C:\TableStateMappings\TableStateMappingsTest-MX.csv";

            var builder = new StringBuilder();
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();
            builder.AppendLine(GetInitialDeleteRows());

            string path = @"C:\TableStateMappings\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TableStateMappings(fileToSearch)
                    .Replace("\"BONAIRE, SINT EUSTATIUS AND SABA\"", "BONAIRE SINT EUSTATIUS AND SABA")
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                //#Region#,
                //#TableCode#,
                //TableName,
                //#RegionName#,
                //#RegionCode2#

                builder.AppendLine(GetTableStateMappings_INSERT_INTO()
                    .Replace("#Region#", data[0].Trim())
                    .Replace("#TableCode#", data[1].Trim())
                    .Replace("#RegionName#", data[3].Trim().Replace("'", "''"))
                    .Replace("#RegionCode2#", data[4].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            builder.AppendLine(
                "PRINT 'SUCCESS: Region Codes XYZ/JP/AP were successfully inserted on StateTableRegion table for replacement with new data'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTableStateMappings_INSERT_INTO()
        {
            //Region,TableCode, TableName,          RegionName, RegionCode2 
            //XYZ,    AE,        UNITED ARAB EMIRATES, AJMAN,      AJ

            //#Region#,
            //#TableCode#,
            //TableName,
            //#RegionName#,
            //#RegionCode2#

            return @"-- #number#. RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = #RegionName# | TableCode = #TableCode#
IF (NOT EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#RegionCode2#'
		AND LTRIM(RTRIM(StateName)) = N'#RegionName#'
		AND TableRegionId = (select top 1 id from TableRegion where RegionCode = '#Region#' AND TableCode = '#TableCode#') ) )
	BEGIN
		BEGIN TRY	
			INSERT INTO StateTableRegion (StateCode, StateName, TableRegionId) 
			SELECT '#RegionCode2#' as StateCode, N'#RegionName#' as StateName, (select top 1 id from TableRegion where RegionCode = '#Region#' AND TableCode = '#TableCode#' ) as TableRegionId			
			PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | TableCode = #TableCode# | were successfully inserted on StateTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | TableCode = #TableCode# | were NOT inserted on StateTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | TableCode = #TableCode# | were ALREADY inserted on StateTableRegion table'
GO";
        }

        public string GetDataFromFile_for_TableStateMappings(string filePath)
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
            string filename = "Validation_TableStateMappings_DbScriptByTemplate_16-07-2021";
            string fileToSearch = @"C:\TableStateMappings\TableStateMappingsTest-MX.csv";

            var builder = new StringBuilder();
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            string path = @"C:\TableStateMappings\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TableStateMappings(fileToSearch)
                    .Replace("\"BONAIRE, SINT EUSTATIUS AND SABA\"", "BONAIRE SINT EUSTATIUS AND SABA")
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                builder.AppendLine(TableStateMappings_VALIDATION()
                    .Replace("#Region#", data[0].Trim())
                    .Replace("#TableCode#", data[1].Trim())
                    .Replace("#RegionName#", data[3].Trim().Replace("'", "''"))
                    .Replace("#RegionCode2#", data[4].Trim())
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
            return @"-- #number#. RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = #RegionName# | TableCode = #TableCode#
IF (EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#RegionCode2#'
		AND LTRIM(RTRIM(StateName)) = N'#RegionName#'
		AND TableRegionId = (select top 1 id from TableRegion where RegionCode = '#Region#' AND TableCode = '#TableCode#') ) )	
	PRINT 'SUCCESS: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | TableCode = #TableCode# | were successfully inserted on StateTableRegion table'
ELSE
	PRINT 'FAILED: RegionCode = #Region# | StateCode = #RegionCode2#  | StateName = ' + N'#RegionName#' + ' | TableCode = #TableCode# | were NOT inserted on StateTableRegion table' 
GO";
        }
    }
}
