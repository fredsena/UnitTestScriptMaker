using Xunit;

namespace UnitTestProject
{
	public class DateTimeFormatAdd
	{
		static string path = @"C:\DATA-SCRIPTS\DateTimeFormat\";
		string fileToSearch = path + "DateTimeFormatAdd.csv";
		string[] list = { "#TableCode#", "#LanguageCode#" };

		[Fact]
		public void Script_DEPLOY_DateTimeFormatAdd()
		{
			string filename = "DEPLOY_DateTimeFormatAdd_01-FEB-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				Get_DateTimeFormatAdd_INSERT(), ";", "DEPLOY", GetInitialScript());
		}

		public string Get_DateTimeFormatAdd_INSERT()
		{
			return @"-- #number#. 
-- #TableCode# #LanguageCode# 
IF ( NOT EXISTS ( SELECT TOP 1 1 from DateTimeFormat WHERE TableCode = '#TableCode#' AND LanguageCode = '#LanguageCode#' ))
	BEGIN
		BEGIN TRY

			SELECT TOP 1 @longDateFormat = LongDateFormat, @shortDateFormat = ShortDateFormat, @shortTimeFormat=ShortTimeFormat  
			from DateTimeFormat where TableCode = '#TableCode#'

			INSERT INTO [dbo].[DateTimeFormat] ([TableCode],[LanguageCode],[LongDateFormat],[ShortDateFormat],[ShortTimeFormat])
			VALUES ('#TableCode#','#LanguageCode#', @longDateFormat, @shortDateFormat, @shortTimeFormat)

			PRINT 'SUCCESS: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were successfully INSERTED into DateTimeFormat table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were NOT INSERTED into the DateTimeFormat table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF ( EXISTS ( SELECT TOP 1 1 from DateTimeFormat WHERE TableCode = '#TableCode#' AND LanguageCode = '#LanguageCode#'))			
			PRINT 'SUCCESS: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were ALREADY INSERTED into DateTimeFormat table'
		ELSE
			PRINT 'FAILED: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were NOT INSERTED into the DateTimeFormat'
	END
";
		}

		[Fact]
		public void Script_Validation_DateTimeFormatAdd()
		{
			string filename = "VALIDATION_DateTimeFormatAdd_01-FEB-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				DateTimeFormatAdd_VALIDATION(), ";", "VALIDATION", GetInitialScript());
		}

		public string DateTimeFormatAdd_VALIDATION()
		{
			return @"SELECT TOP 1 @longDateFormat = LongDateFormat, @shortDateFormat = ShortDateFormat, @shortTimeFormat=ShortTimeFormat  
from DateTimeFormat where TableCode = '#TableCode#'

IF (EXISTS ( SELECT TOP 1 1 from DateTimeFormat 
	WHERE TableCode = '#TableCode#' AND LanguageCode = '#LanguageCode#' 
	AND LongDateFormat = @longDateFormat AND ShortDateFormat = @shortDateFormat AND ShortTimeFormat = @shortTimeFormat ))
	PRINT 'SUCCESS: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were successfully INSERTED into DateTimeFormat table'
ELSE
	PRINT 'FAILED: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were NOT INSERTED into the DateTimeFormat'
";
		}

		public string Get_DateTimeFormatAdd_Rollback()
		{
			return @"-- #number#. 
-- #TableCode# #LanguageCode# 
IF ( EXISTS ( SELECT TOP 1 1 from DateTimeFormat WHERE TableCode = '#TableCode#' AND LanguageCode = '#LanguageCode#' ))
	BEGIN
		BEGIN TRY
			DELETE FROM [dbo].[DateTimeFormat] WHERE [TableCode] = '#TableCode#' AND [LanguageCode] = '#LanguageCode#'
			PRINT 'SUCCESS: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were successfully rolled back (deleted) from DateTimeFormat table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were NOT rolled back (deleted) from DateTimeFormat table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF ( NOT EXISTS ( SELECT TOP 1 1 from DateTimeFormat WHERE TableCode = '#TableCode#' AND LanguageCode = '#LanguageCode#'))			
			PRINT 'SUCCESS: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were ALREADY rolled back (deleted) from DateTimeFormat table'
		ELSE
			PRINT 'FAILED: #number#. TableCode: #TableCode# | LanguageCode: #LanguageCode# were NOT rolled back (deleted) from DateTimeFormat'
	END
";
		}

		[Fact]
		public void Script_Rollback_DateTimeFormatAdd()
		{
			string filename = "ROLLBACK_DateTimeFormatAdd_01-FEB-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				Get_DateTimeFormatAdd_Rollback(), ";", "ROLLBACK", GetInitialScript());
		}

		public string GetInitialScript()
		{
			return @"DECLARE @longDateFormat as VARCHAR(25)
DECLARE @shortDateFormat as NVARCHAR(155)
DECLARE @shortTimeFormat as NVARCHAR(155)
";
		}

	}
}
