
using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class TimezoneTableRegion
    {
        public string GetTimezoneTableRegion_ROLLBACK()
        {
            return @"DECLARE @bucode#BU# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC# as INT
SELECT @bucode#BU#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#.  #CC# #BU# #Iana#  #VV# #CN#
IF ( EXISTS ( 
	SELECT TOP 1 1 from TimezoneTableRegion 
	WHERE TableRegionId = @TableCode#CC#
	AND BusinessUnitId = @bucode#BU# ))

	BEGIN
		BEGIN TRY	
		UPDATE [dbo].[TimeZoneTableRegion]
			SET [Value] = '#VV#' ,[IanaTimeZone] = '#Iana#' WHERE TableRegionId = @TableCode#CC#  AND BusinessUnitId = @bucode#BU#	
			PRINT 'SUCCESS: Table code: #CC# with IanaTimeZone: #Iana# was successfully Rolled back on TimeZoneTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT Rolled back on TimeZoneTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
";
        }

        [Fact]
        public void GenerateDbScript_Rollback_TimezoneTableRegion()
        {
            string path = @"C:\TimezoneTableRegion\";
            string filename = "Rollback_TimezoneTableRegion_10-09-2021_TEST";
            string fileToSearch = @"C:\TimezoneTableRegion\timezones_prod.csv";

            string[] list = { "#VV#", "#Iana#", "#CN#", "#CC#", "#BU#" };

            new ScriptHelper().GenerateFile(path,filename,
                fileToSearch, "USE [DcDb]",list, 
                GetTimezoneTableRegion_ROLLBACK(),",");
        }

        public string GetTimezoneTableRegion_VALIDATION()
        {
            return @"DECLARE @bucode#BU# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC# as INT
SELECT @bucode#BU#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#.  #CC# '#BU#' #Iana#  #VV#
IF ( EXISTS ( 
	SELECT TOP 1 1 from TimezoneTableRegion 
	WHERE TableRegionId = @TableCode#CC#
	AND BusinessUnitId = @bucode#BU# 
	AND [IanaTimeZone] = '#Iana#'))

	BEGIN
		PRINT 'SUCCESS: Table code: #CC# with IanaTimeZone: #Iana# was successfully updated into TimeZoneTableRegion table'
	END
ELSE
	BEGIN	
		PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT updated on TimeZoneTableRegion table'				
	END
";
        }

        [Fact]
        public void GenerateDbScript_Validation_TimezoneTableRegion()
        {
            string filename = "Validation_TimezoneTableRegion_09-09-2021";
            string fileToSearch = @"C:\TimezoneTableRegion\TimezoneTableRegion_data.csv";

            var builder = new StringBuilder();

            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            string path = @"C:\TimezoneTableRegion\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_PaymentCodeTestSet(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetTimezoneTableRegion_VALIDATION()
                    .Replace("#BU#", data[0].Trim())
                    .Replace("#CC#", data[1].Trim())
                    .Replace("#Iana#", data[2].Trim())
                    .Replace("#VV#", data[3].Trim())
                    .Replace("#number#", (i + 1).ToString()));

                builder.AppendLine();
            }

            builder.AppendLine();

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        [Fact]
        public void GenerateDbScript_TimezoneTableRegion()
        {
            //#BU#
            //#CC#
            //#Iana#
            //#VV#

            string path = @"C:\TimezoneTableRegion\";
            string filename = "TimezoneTableRegion_DbScript_09-09-2021";
            string fileToSearch = @"C:\TimezoneTableRegion\TimezoneTableRegion_data.csv";
            
            var builder = new StringBuilder();

            builder.AppendLine("USE [DcDb]");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_PaymentCodeTestSet(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetTemplate()
                    .Replace("#BU#", data[0].Trim())
                    .Replace("#CC#", data[1].Trim())
                    .Replace("#Iana#", data[2].Trim())
                    .Replace("#VV#", data[3].Trim())
                    .Replace("#number#", (i + 1).ToString()));

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetDataFromFile_for_PaymentCodeTestSet(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.GetEncoding("UTF-16"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetTemplate()
        {
            return @"DECLARE @bucode#BU# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC# as INT
SELECT @bucode#BU#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#.  #CC# '#BU#' #Iana#  #VV#
IF ( NOT EXISTS ( 
	SELECT TOP 1 1 from TimezoneTableRegion 
	WHERE TableRegionId = @TableCode#CC#
	AND BusinessUnitId = @bucode#BU# ))

	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[TimeZoneTableRegion] ([Value] ,[IsDefault] ,[TableRegionId] ,[BusinessUnitId] ,[IanaTimeZone])
			VALUES ('#VV#',1, @TableCode#CC#, @bucode#BU#, '#Iana#')

			PRINT 'SUCCESS: Table code: #CC# with IanaTimeZone: #Iana# was successfully INSERTED into TimeZoneTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT INSERTED into the TimeZoneTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF ( EXISTS ( 
		SELECT TOP 1 1 from TimezoneTableRegion 
		WHERE TableRegionId = @TableCode#CC#
		AND BusinessUnitId = @bucode#BU# ))

			BEGIN
				BEGIN TRY	
				UPDATE [dbo].[TimeZoneTableRegion]
					SET [Value] = '#VV#' ,[IanaTimeZone] = '#Iana#'
					WHERE TableRegionId = @TableCode#CC#
					AND BusinessUnitId = @bucode#BU#
			
					PRINT 'SUCCESS: Table code: #CC# with IanaTimeZone: #Iana# was successfully updated on TimeZoneTableRegion table'
				END TRY
				BEGIN CATCH
					PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT updated on TimeZoneTableRegion table due to: ' + ERROR_MESSAGE()
				END CATCH
			END
		ELSE
			PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT updated on TimeZoneTableRegion table'				
	END
";
        }

    }
}
