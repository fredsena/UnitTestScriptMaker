using Xunit;

namespace UnitTestProject
{
	public class CustClasses
	{
		static string path = @"C:\DATA-SCRIPTS\CustClassesCustCategoriesHK\";
		string fileToSearch = path + "CustClassesCustCategoriesHK.csv";
		string[] list = { "#CCCode#",	"#CCName#",	"#BSCompanyNumber#" };

		[Fact]
		public void Script_DEPLOY_CustClassesCustCategoriesHK()
		{
			string filename = "DEPLOY_CustClassesCustCategoriesHK_19-JAN-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				Get_CustClassesCustCategoriesHK_INSERT(), ";", "DEPLOY", GetInitialScript());
		}

		public string Get_CustClassesCustCategoriesHK_INSERT()
		{
			return @"-- #number#.  #CCCode#	#CCName#  #BSCompanyNumber#
IF (NOT EXISTS ( Select TOP 1 1 from TestClasses where [Name] = '#CCName#' AND Code = '#CCCode#' AND [BusinessUnitId] = @BU_8242 ))
	BEGIN
		BEGIN TRY

			INSERT INTO [dbo].[TestClasses] ([Id],[Name],[Code],[TestCategoryId],[BusinessUnitId])
				 VALUES( NEWID() ,'#CCName#','#CCCode#',null ,@BU_8242)

			DECLARE @CustClass_#CCCode# as UNIQUEIDENTIFIER
			Select @CustClass_#CCCode#=id from TestClasses where [Name] = '#CCName#' AND Code = '#CCCode#' AND [BusinessUnitId] = @BU_8242

			INSERT INTO [dbo].[BusinessTableTestClasses] ([BusinessTableId],[TestClassId] ,[IsDefault])
			SELECT id as BusinessTableId, @CustClass_#CCCode# as TestClassId, 1 as [IsDefault] FROM Table 
			WHERE BusinessUnitId = @BU_8242	AND LTRIM(RTRIM(CompanyNumber)) = '#BSCompanyNumber#'

			INSERT INTO [dbo].[TestClassesTestCategories] ([TestClassId],[TestCategoryId])
			SELECT @CustClass_#CCCode# as TestClassId, id as TestCategoryId FROM TestCategories WHERE BusinessUnitId = @BU_8242

			PRINT 'SUCCESS: #number#. TestCat code: #CCCode# for CompanyNumber: #BSCompanyNumber# were successfully INSERTED into BusinessTableTestClasses and [TestClassesTestCategories] tables for BU=8242'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. TestCat code: #CCCode# for CompanyNumber: #BSCompanyNumber# were NOT INSERTED into BusinessTableTestClasses and [TestClassesTestCategories] tables for BU=8242 due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF (EXISTS ( Select TOP 1 1 from TestClasses where [Name] = '#CCName#' AND Code = '#CCCode#' AND [BusinessUnitId] = @BU_8242 ))		
			PRINT 'SUCCESS: #number#. TestCat code: #CCCode# for CompanyNumber: #BSCompanyNumber# were ALREADY INSERTED into BusinessTableTestClasses and [TestClassesTestCategories] tables for BU=8242'				
		ELSE
			PRINT 'FAILED: SUCCESS: #number#. TestCat code: #CCCode# for CompanyNumber: #BSCompanyNumber# were NOT INSERTED into BusinessTableTestClasses and [TestClassesTestCategories] tables for BU=8242'
	END
";
		}

		//[Fact]
		public void Script_Validation_CustClassesCustCategoriesHK()
		{
			string filename = "VALIDATION_New_Test_PayCodeCustSet_Map_19-JAN-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				CustClassesCustCategoriesHK_VALIDATION(), ";", "VALIDATION", GetInitialScript());
		}

		public string CustClassesCustCategoriesHK_VALIDATION()
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

		public string Get_CustClassesCustCategoriesHK_Rollback()
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

		//[Fact]
		public void Script_Rollback_CustClassesCustCategoriesHK()
		{
			string filename = "ROLLBACK_Get_CustClassesCustCategoriesHK_19-JAN-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				Get_CustClassesCustCategoriesHK_Rollback(), ";", "ROLLBACK", GetInitialScript());
		}

		public string GetInitialScript()
		{
			return @"DECLARE @BU_8242 as UNIQUEIDENTIFIER
SELECT @BU_8242=id from table where code = '8242'
";
		}

	}
}




