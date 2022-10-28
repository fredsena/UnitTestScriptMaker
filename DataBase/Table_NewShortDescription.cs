using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class Table_NewShortDescription
    {
        [Fact]
        public void DEPLOY_Table_New_ShortDescription()
        {
            string path = @"C:\DATA-SCRIPTS\DcDb\";
            string filename = "DEPLOY_Table_New_ShortDescription_21-10-2022";
            string fileToSearch = @"C:\DATA-SCRIPTS\DcDb\Table_New_ShortDescription.csv";

            //string[] list = { "#RegionCode#","#BusinessUnit#","#BusinessUnitDesc#","#Level3Table#","#CompanyNumber#","#CompanyNumberDesc#","#ShortDesc#","#NewShortDesc#","#Check#" };            

            var builder = new StringBuilder();

            builder.AppendLine("--DEPLOY");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", "439"));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var businessUnit = "439";

            for (int i = 0; i < row.Length; i++)
            {
                //     0               1                    2                   3
                //"#RegionCode#","#BusinessUnit#","#BusinessUnitDesc#","#Level3Table#",
                
                //        4                   5                   6               7          8
                //"#CompanyNumber#","#CompanyNumberDesc#","#ShortDesc#","#NewShortDesc#","#Check#" 

                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[1].Trim() != businessUnit)
                {
                    builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", data[1].Trim()));
                    builder.AppendLine();
                    businessUnit = data[1].Trim();
                }

                builder.AppendLine(Get_Table_New_ShortDescriptions_INSERT()
                    .Replace("#RegionCode#", data[0].Trim())
                    .Replace("#BusinessUnit#", data[1].Trim())
                    .Replace("#Level3Table#", data[3].Trim())
                    .Replace("#CompanyNumber#", data[4].Trim())
                    .Replace("#CompanyNumberDesc#", data[5].Trim())
                    .Replace("#ShortDesc#", data[6].Trim())
                    .Replace("#NewShortDesc#", data[7].Trim())
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
            return @"DECLARE @BusinessUnitId_#BusinessUnit# UNIQUEIDENTIFIER
SELECT @BusinessUnitId_#BusinessUnit#=id from table where code = '#BusinessUnit#'";
        }        

        public string Get_Table_New_ShortDescriptions_INSERT()
        {
            return @"-- #number#. RegionCode = #RegionCode# | BusinessUnit = #BusinessUnit# | CompanyNumber = #CompanyNumber# | CompanyNumberDesc = #CompanyNumberDesc# | NewShortDesc = #NewShortDesc# |
IF (EXISTS (
	SELECT TOP 1 1 FROM Table
	WHERE Level3Table = '#Level3Table#' 
	AND ShortDescription = '#ShortDesc#'
	AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
    AND CompanyNumber = '#CompanyNumber#'
	AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
	BEGIN			
		UPDATE Table SET ShortDescription = '#NewShortDesc#'
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#ShortDesc#'
        AND CompanyNumber = '#CompanyNumber#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND [Description] = LTRIM(RTRIM('#CompanyNumberDesc#'))

		PRINT 'SUCCESS: #number#. ShortDescription for RegionCode: #RegionCode#,  BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#| was successfully Updated with value: #NewShortDesc# on Table table'
	END
ELSE
	BEGIN
	IF (EXISTS (
		SELECT TOP 1 1 FROM Table
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#NewShortDesc#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND CompanyNumber = '#CompanyNumber#'
		AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
		PRINT 'SUCCESS: #number#. ShortDescription for RegionCode: #RegionCode#,  BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#| was ALREADY Updated with value: #NewShortDesc# on Table table'
	ELSE
	IF (NOT EXISTS (
		SELECT TOP 1 1 FROM Table
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#ShortDesc#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND CompanyNumber = '#CompanyNumber#'
		AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
		PRINT 'WARNING: #number#. The BusinessTable BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#|, ShortDescription: #ShortDesc# was NOT FOUND for updating ShortDescription (#NewShortDesc#) on Table table'
	END
";
        }

        [Fact]
        public void VALIDATION_Table_New_ShortDescription()
        {
            string path = @"C:\DATA-SCRIPTS\DcDb\";
            string filename = "VALIDATION_Table_New_ShortDescription_21-10-2022";
            string fileToSearch = @"C:\DATA-SCRIPTS\DcDb\Table_New_ShortDescription.csv";

            var builder = new StringBuilder();

            builder.AppendLine("--VALIDATION");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", "439"));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var businessUnit = "439";

            for (int i = 0; i < row.Length; i++)
            {
                //     0               1                    2                   3
                //"#RegionCode#","#BusinessUnit#","#BusinessUnitDesc#","#Level3Table#",

                //        4                   5                   6               7          8
                //"#CompanyNumber#","#CompanyNumberDesc#","#ShortDesc#","#NewShortDesc#","#Check#" 

                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[1].Trim() != businessUnit)
                {
                    builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", data[1].Trim()));
                    builder.AppendLine();
                    businessUnit = data[1].Trim();
                }

                builder.AppendLine(Get_Table_New_ShortDescriptions_VALIDATION()
                    .Replace("#RegionCode#", data[0].Trim())
                    .Replace("#BusinessUnit#", data[1].Trim())
                    .Replace("#Level3Table#", data[3].Trim())
                    .Replace("#CompanyNumber#", data[4].Trim())
                    .Replace("#CompanyNumberDesc#", data[5].Trim())
                    .Replace("#ShortDesc#", data[6].Trim())
                    .Replace("#NewShortDesc#", data[7].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string Get_Table_New_ShortDescriptions_VALIDATION()
        {
            return @"-- #number#. RegionCode = #RegionCode# | BusinessUnit = #BusinessUnit# | CompanyNumber = #CompanyNumber# | CompanyNumberDesc = #CompanyNumberDesc# | NewShortDesc = #NewShortDesc# |
IF (EXISTS (
	SELECT TOP 1 1 FROM Table
	WHERE Level3Table = '#Level3Table#' 
	AND ShortDescription = '#NewShortDesc#'
	AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
    AND CompanyNumber = '#CompanyNumber#'
	AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
	BEGIN			
		PRINT 'SUCCESS: #number#. ShortDescription for RegionCode: #RegionCode#,  BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#| was successfully Updated with value: #NewShortDesc# on Table table'
	END
ELSE
	BEGIN
	IF (NOT EXISTS (
		SELECT TOP 1 1 FROM Table
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#ShortDesc#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND CompanyNumber = '#CompanyNumber#'
		AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
		PRINT 'WARNING: #number#. The BusinessTable BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#|, ShortDescription: #ShortDesc# was NOT FOUND for updating ShortDescription (#NewShortDesc#) on Table table'
	ELSE
	IF (NOT EXISTS (
		SELECT TOP 1 1 FROM Table
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#NewShortDesc#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND CompanyNumber = '#CompanyNumber#'
		AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
		PRINT 'FAILED: #number#. ShortDescription for RegionCode: #RegionCode#,  BU: #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#| was NOT Updated with value: #NewShortDesc# on Table table'	
    END
";
        }

        public string GetDataFromFile(string filePath)
        {
            try
            {                
                return File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Fact]
        public void ROLLBACK_Table_New_ShortDescription()
        {
            string path = @"C:\DATA-SCRIPTS\DcDb\";
            string filename = "ROLLBACK_Table_New_ShortDescription_21-10-2022";
            string fileToSearch = @"C:\DATA-SCRIPTS\DcDb\Table_New_ShortDescription.csv";

            var builder = new StringBuilder();

            builder.AppendLine("--ROLLBACK");
            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();

            builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", "439"));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var businessUnit = "439";

            for (int i = 0; i < row.Length; i++)
            {
                //     0               1                    2                   3
                //"#RegionCode#","#BusinessUnit#","#BusinessUnitDesc#","#Level3Table#",

                //        4                   5                   6               7          8
                //"#CompanyNumber#","#CompanyNumberDesc#","#ShortDesc#","#NewShortDesc#","#Check#" 

                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                if (data[1].Trim() != businessUnit)
                {
                    builder.AppendLine(GetSearchVariables().Replace("#BusinessUnit#", data[1].Trim()));
                    builder.AppendLine();
                    businessUnit = data[1].Trim();
                }

                builder.AppendLine(Get_Table_New_ShortDescriptions_ROLLBACK()
                    .Replace("#RegionCode#", data[0].Trim())
                    .Replace("#BusinessUnit#", data[1].Trim())
                    .Replace("#Level3Table#", data[3].Trim())
                    .Replace("#CompanyNumber#", data[4].Trim())
                    .Replace("#CompanyNumberDesc#", data[5].Trim())
                    .Replace("#ShortDesc#", data[6].Trim())
                    .Replace("#NewShortDesc#", data[7].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string Get_Table_New_ShortDescriptions_ROLLBACK()
        {
            return @"-- #number#. RegionCode = #RegionCode# | BusinessUnit = #BusinessUnit# | CompanyNumber = #CompanyNumber# | CompanyNumberDesc = #CompanyNumberDesc# | NewShortDesc = #NewShortDesc# |
IF (EXISTS (
	SELECT TOP 1 1 FROM Table
	WHERE Level3Table = '#Level3Table#' 
	AND ShortDescription = '#NewShortDesc#'
	AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
    AND CompanyNumber = '#CompanyNumber#'
	AND Description = LTRIM(RTRIM('#CompanyNumberDesc#')) ) )
	BEGIN			
		UPDATE Table SET ShortDescription = '#ShortDesc#'
		WHERE Level3Table = '#Level3Table#' 
		AND ShortDescription = '#NewShortDesc#'
        AND CompanyNumber = '#CompanyNumber#'
		AND BusinessUnitId = @BusinessUnitId_#BusinessUnit#
		AND [Description] = LTRIM(RTRIM('#CompanyNumberDesc#'))

		PRINT 'SUCCESS: #number#. ShortDescription for RegionCode: #RegionCode#,  BU: #BusinessUnit#, Company: |#CompanyNumberDesc#| was successfully Rolled back with value: #ShortDesc# on Table table'
	END
ELSE
	PRINT 'SUCCESS: #number#. ShortDescription for RegionCode: #RegionCode#, BU #BusinessUnit#, CompanyNumber: #CompanyNumber#, Company: |#CompanyNumberDesc#| was ALREADY Rolled back with value: #ShortDesc# on Table table'
";
        }
    }
}

