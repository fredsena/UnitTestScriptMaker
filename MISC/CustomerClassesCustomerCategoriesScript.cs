using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class TestClassesTestCategoriesScript
    {
        [Fact]
        public void DbScriptByTemplate_TestClassesTestCategories()
        {
            string filename = "Insert_TestClassesTestCategories2_DbScriptByTemplate_15-07-2021";
            string fileToSearch = @"C:\TestClassesTestCategories2\_Class_To_Category_Relationship_Data2.csv";

            var builder = new StringBuilder();

            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();
            builder.AppendLine("DELETE FROM TestClassesTestCategories");
            builder.AppendLine();

            string path = @"C:\TestClassesTestCategories2\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TestClassesTestCategories(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                //#BUCode# //#ClassCode#  #ClassCodeDesc#      //#CategoryCode#   // #CategoryName#
                builder.AppendLine(GetTestClassesTestCategories_INSERT_INTO()
                    .Replace("#BUCode#", data[0].Trim())
                    .Replace("#ClassCode#", data[1].Trim())
                    .Replace("#ClassCodeDesc#", data[2].Trim().Replace("'", "''"))
                    .Replace("#CategoryCode#", data[3].Trim())
                    //.Replace("#CategoryName#", data[4].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );
            }

            builder.AppendLine(
                "PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were INSERTED with TestClasses and TestCategories'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTestClassesTestCategories_INSERT_INTO()
        {
            return @"-- #number#. BUCode = #BUCode# | ClassCode = #ClassCode#  | ClassCodeDesc = #ClassCodeDesc# | CategoryCode = #CategoryCode#  
BEGIN
	BEGIN TRY		
		INSERT INTO [dbo].[TestClassesTestCategories] ([TestClassId], [TestCategoryId]) VALUES ((select top 1 id from TestClasses where code ='#ClassCode#' and LTRIM(RTRIM([Name])) = '#ClassCodeDesc#'  and BusinessUnitId = (Select id from table where code = '#BUCode#') AND id in (select TestClassId from BusinessTableTestClasses)), (select top 1 id from TestCategories where code = '#CategoryCode#' and BusinessUnitId = (Select id from table where code = '#BUCode#')))
	END TRY
	BEGIN CATCH
		PRINT 'FAILED: | TestClassId: #ClassCode# | TestCategoryId: #CategoryCode# | ClassCodeDesc: #ClassCodeDesc# | BU: #BUCode# | were NOT inserted in the TestClassesTestCategories table due to: ' + ERROR_MESSAGE()
	END CATCH
END
GO";
        }

        /*
        public string GetTestClassesTestCategories_INSERT_INTO()
        {
            return @"-- #number#. BUCode = #BUCode# | ClassCode = #ClassCode#  | ClassCodeDesc = #ClassCodeDesc# | CategoryCode = #CategoryCode#  | CategoryName = #CategoryName#
BEGIN
	BEGIN TRY		
		INSERT INTO [dbo].[TestClassesTestCategories] ([TestClassId], [TestCategoryId]) VALUES ((select top 1 id from TestClasses where code ='#ClassCode#' and LTRIM(RTRIM([Name])) = '#ClassCodeDesc#'  and BusinessUnitId = (Select id from table where code = '#BUCode#')), (select top 1 id from TestCategories where code = '#CategoryCode#' and LTRIM(RTRIM([Name])) = '#CategoryName#' and BusinessUnitId = (Select id from table where code = '#BUCode#')))		
	END TRY
	BEGIN CATCH
		PRINT 'FAILED: | TestClassId: #ClassCode# | TestCategoryId: #CategoryCode# | ClassCodeDesc: #ClassCodeDesc# | CategoryName: #CategoryName# | BU: #BUCode# | were NOT inserted in the TestClassesTestCategories table due to: ' + ERROR_MESSAGE()
	END CATCH
END
GO";
        }

        */

        public string GetDataFromFile_for_TestClassesTestCategories(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.GetEncoding("ISO-8859-1"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Fact]
        public void DbScriptByTemplate_Validation_TestClassesTestCategories()
        {
            string filename = "Validation_TestClassesTestCategories2_DbScriptByTemplate_15-07-2021";
            string fileToSearch = @"C:\TestClassesTestCategories2\_Class_To_Category_Relationship_Data2.csv";

            var builder = new StringBuilder();

            builder.AppendLine("USE [DcDb]");
            builder.AppendLine();
            builder.AppendLine("DECLARE @COUNTER INT = 0");

            string path = @"C:\TestClassesTestCategories2\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_TestClassesTestCategories(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetTestClassesTestCategories_VALIDATION()
                    .Replace("#BUCode#", data[0].Trim())
                    .Replace("#ClassCode#", data[1].Trim())
                    .Replace("#ClassCodeDesc#", data[2].Trim().Replace("'", "''"))
                    .Replace("#CategoryCode#", data[3].Trim())
                    //.Replace("#CategoryName#", data[4].Trim())
                    .Replace("#number#", (i + 1).ToString())
                );
                builder.AppendLine();
            }

            builder.AppendLine(GetTrailerValidation());

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTrailerValidation()
        {
            return @"IF @COUNTER = 0	
	PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were successfully INSERTED with TestClasses and TestCategories'	
ELSE
	PRINT 'FAILED: There are ' +  CAST(@COUNTER AS VARCHAR(15)) +  ' TestClassId and TestCategoryId NOT inserted in the TestClassesTestCategories table' 
";
        }
        /*
        public string GetTestClassesTestCategories_VALIDATION()
        {
            return @"IF NOT EXISTS (
	SELECT TOP 1 1 FROM [dbo].[TestClassesTestCategories]
	WHERE TestClassId = (select top 1 id from TestClasses where code ='#ClassCode#' and LTRIM(RTRIM([Name])) = '#ClassCodeDesc#'  and BusinessUnitId = (Select id from table where code = '#BUCode#'))
	AND TestCategoryId = (select top 1 id from TestCategories where code = '#CategoryCode#' and LTRIM(RTRIM([Name])) = '#CategoryName#' and BusinessUnitId = (Select id from table where code = '#BUCode#')) )	
	BEGIN
		SET @COUNTER = @COUNTER+1
		PRINT 'FAILED: | TestClassId: #ClassCode# | TestCategoryId: #CategoryCode# | ClassCodeDesc: #ClassCodeDesc# | CategoryName: #CategoryName# | BU: #BUCode# | were NOT inserted in the TestClassesTestCategories table'
	END";
        }
        */

        public string GetTestClassesTestCategories_VALIDATION()
        {
            return @"-- #number#. BUCode = #BUCode# | ClassCode = #ClassCode#  | ClassCodeDesc = #ClassCodeDesc# | CategoryCode = #CategoryCode#
IF NOT EXISTS (
	SELECT TOP 1 1 FROM [dbo].[TestClassesTestCategories]
	WHERE TestClassId = (select top 1 id from TestClasses where code ='#ClassCode#' and LTRIM(RTRIM([Name])) = '#ClassCodeDesc#'  and BusinessUnitId = (Select id from table where code = '#BUCode#') AND id in (select TestClassId from BusinessTableTestClasses))
	AND TestCategoryId = (select top 1 id from TestCategories where code = '#CategoryCode#' and BusinessUnitId = (Select id from table where code = '#BUCode#')) )	
	BEGIN
		SET @COUNTER = @COUNTER+1
		PRINT 'FAILED: | TestClassId: #ClassCode# | TestCategoryId: #CategoryCode# | ClassCodeDesc: #ClassCodeDesc# | BU: #BUCode# | were NOT inserted in the TestClassesTestCategories table'
	END";
        }
    }
}
