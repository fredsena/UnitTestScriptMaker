using Xunit;

namespace UnitTestProject
{
	public class New_CodeSet_Map
	{
		static string path = @"C:\DATA-SCRIPTS\PaymentCodeTestSet\";
		string fileToSearch = path + "New-Test-PayCodeCustSet-Map.csv";
		string[] list = { "#PaymentCode#", "#TestSet#" };

		[Fact]
		public void Script_DEPLOY_New_Test_PayCodeCustSet_Map()
		{			
			string filename = "DEPLOY_New_Test_PayCodeCustSet_Map_02-DEC-2021";		

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				GetNew_Test_PayCodeCustSet_Map_INSERT(), ";", "DEPLOY", GetInitialScript());
		}

		public string GetNew_Test_PayCodeCustSet_Map_INSERT()
		{
			return @"select @TestSetId=id from TestSets where RTRIM(LTRIM(code)) = '#TestSet#'

-- #number#.  #PaymentCode# #TestSet#
IF (NOT EXISTS ( SELECT TOP 1 1 FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId AND PaymentCodeId = #PaymentCode# ))
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[PaymentCodeTestSet] ([PaymentCodeId] ,[TestSetId])
			SELECT #PaymentCode# as PaymentCodeId, @TestSetId as TestSetId
			PRINT 'SUCCESS: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was successfully INSERTED into PaymentCodeTestSet table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was NOT INSERTED into PaymentCodeTestSet table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF (EXISTS ( SELECT TOP 1 1 FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId  AND PaymentCodeId = #PaymentCode# ))		
			PRINT 'SUCCESS: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was ALREADY inserted into PaymentCodeTestSet table'				
		ELSE
			PRINT 'FAILED: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was was NOT inserted into PaymentCodeTestSet table'
	END
";
		}

		[Fact]
		public void ScriptValidation_New_Test_PayCodeCustSet_Map()
		{			
			string filename = "VALIDATION_New_Test_PayCodeCustSet_Map_02-DEC-2021";			

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				New_Test_PayCodeCustSet_Map_VALIDATION(), ";", "VALIDATION", GetInitialScript());
		}

		public string New_Test_PayCodeCustSet_Map_VALIDATION()
		{
			return @"select @TestSetId=id from TestSets where RTRIM(LTRIM(code)) = '#TestSet#'

-- #number#.  #PaymentCode# #TestSet#
BEGIN	
	IF (EXISTS ( SELECT TOP 1 1 FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId  AND PaymentCodeId = #PaymentCode# ))		
		PRINT 'SUCCESS: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was ALREADY inserted into PaymentCodeTestSet table'				
	ELSE
		PRINT 'FAILED: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was was NOT inserted into PaymentCodeTestSet table'
END
";
		}

		public string GetNew_Test_PayCodeCustSet_Map_Rollback()
		{
			return @"select @TestSetId=id from TestSets where RTRIM(LTRIM(code)) = '#TestSet#'

-- #number#.  #PaymentCode# #TestSet#
IF (EXISTS ( SELECT TOP 1 1 FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId AND PaymentCodeId = #PaymentCode# ))
	BEGIN
		BEGIN TRY
			DELETE FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId AND PaymentCodeId = #PaymentCode#
			PRINT 'SUCCESS: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was rolled back from PaymentCodeTestSet table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was NOT rolled back from PaymentCodeTestSet table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF (NOT EXISTS ( SELECT TOP 1 1 FROM PaymentCodeTestSet WHERE TestSetId = @TestSetId  AND PaymentCodeId = #PaymentCode# ))		
			PRINT 'SUCCESS: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was ALREADY rolled back from PaymentCodeTestSet table'				
		ELSE
			PRINT 'FAILED: #number#. TestSet code: #TestSet# for PaymentCode: #PaymentCode# was was NOT rolled back from PaymentCodeTestSet table'
	END
";
		}

		[Fact]
		public void ScriptRollback_LangCultContryRegion()
		{			
			string filename = "ROLLBACK_New_Test_PayCodeCustSet_Map_02-DEC-2021";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				GetNew_Test_PayCodeCustSet_Map_Rollback(), ";", "ROLLBACK", GetInitialScript());
		}

		public string GetInitialScript()
		{
			return @"DECLARE @APJCHP01 UNIQUEIDENTIFIER
DECLARE @APJCHP02 UNIQUEIDENTIFIER
DECLARE @XYZCHP01 UNIQUEIDENTIFIER
DECLARE @XYZCHP02 UNIQUEIDENTIFIER
DECLARE @XYZCHP03 UNIQUEIDENTIFIER
DECLARE @TestSetId UNIQUEIDENTIFIER
select @APJCHP01=id from PaymentCode where PaymentCode = 'APJCHP01'
select @APJCHP02=id from PaymentCode where PaymentCode = 'APJCHP02'
select @XYZCHP01=id from PaymentCode where PaymentCode = 'XYZCHP01'
select @XYZCHP02=id from PaymentCode where PaymentCode = 'XYZCHP02'
select @XYZCHP03=id from PaymentCode where PaymentCode = 'XYZCHP03'
";
		}

	}
}



