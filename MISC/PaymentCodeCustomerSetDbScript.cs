using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class PaymentCodeTestSetDbScript
    {
        public string GetPaymentCodeTestSet_VALIDATION()
        {
            //#UserType#
            //#PaymentCode#
            //#Code#

            return @"if(EXISTS(SELECT TOP 1 * FROM PaymentCodeTestSet pccs 
			inner join TestSets cs on pccs.TestSetId= cs.id 
			inner join paymentcode pc on pccs.paymentcodeid = pc.id 
	WHERE pc.paymentcode = '#PaymentCode#' and cs.Code = '#Code#'))	
	PRINT 'SUCCESS: Missing TestSet code: #Code# for PaymentCode: #PaymentCode# was successfully inserted into the PaymentCodeTestSet table'				
ELSE
	PRINT 'FAILED: Missing TestSet code: #Code# for PaymentCode: #PaymentCode# was NOT inserted into the PaymentCodeTestSet table '";
        }

        [Fact]
        public void GenerateDbScriptByTemplate_Validation_TestClassesTestCategories()
        {
            //#UserType#
            //#PaymentCode#
            //#Code#

            string filename = "Validation_PaymentCodeTestSet_DbScriptByTemplate_31-05-2021";
            string fileToSearch = @"C:\PaymentCodeTestSet\PaymentCodeTestSet_data.txt";

            var builder = new StringBuilder();

            string path = @"C:\PaymentCodeTestSet\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_PaymentCodeTestSet(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;


                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                builder.AppendLine(GetPaymentCodeTestSet_VALIDATION()
                    .Replace("#UserType#", data[0].Trim())
                    .Replace("#PaymentCode#", data[1].Trim())
                    .Replace("#Code#", data[2].Trim()));
                builder.AppendLine();
            }

            //builder.AppendLine(GetTrailerValidation());

            builder.AppendLine();

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetPaymentCodeTestSet_INSERT_INTO()
        {
            return @"if(NOT EXISTS(SELECT TOP 1 * FROM PaymentCodeTestSet pccs 
			inner join TestSets cs on pccs.TestSetId= cs.id 
			inner join paymentcode pc on pccs.paymentcodeid = pc.id 
	WHERE pc.paymentcode = '#PaymentCode#' and cs.Code = '#Code#'))
	BEGIN
		BEGIN TRY	
			INSERT INTO PaymentCodeTestSet(PaymentCodeId, TestSetId) 
			values (
			(select PaymentCodeId from UserTypePaymentCodes where PaymentCodeId = (select id from PaymentCode where PaymentCode = '#PaymentCode#') and UserTypeId = (select id from UserTypes where UserType = '#UserType#') ),
			(SELECT id from TestSets where Code = '#Code#'))
			
			PRINT 'SUCCESS: Missing TestSet code: #Code# for PaymentCode: #PaymentCode# was successfully inserted into the PaymentCodeTestSet table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Missing TestSet code: #Code# for PaymentCode: #PaymentCode# was NOT inserted into the PaymentCodeTestSet table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: TestSet code: #Code# for PaymentCode: #PaymentCode# was ALREADY inserted into the PaymentCodeTestSet table'
GO";
        }

        [Fact]
        public void GenerateDbScriptByTemplate_PaymentCodeTestSet_GetLocalInputData()
        {
            //#UserType#
            //#PaymentCode#
            //#Code#


            string filename = "Insert_PaymentCodeTestSet_DbScriptByTemplate_31-05-2021";
            string fileToSearch = @"C:\PaymentCodeTestSet\PaymentCodeTestSet_data.txt";

            var builder = new StringBuilder();
            //builder.AppendLine(GetHeader());

            builder.AppendLine("USE [DcDb]");

            string path = @"C:\PaymentCodeTestSet\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile_for_PaymentCodeTestSet(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //int counter = 0;

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                //if (counter == 100)
                //{
                //    counter = 0;
                //    builder.AppendLine("GO");
                //}

                builder.AppendLine(GetPaymentCodeTestSet_INSERT_INTO()
                    .Replace("#UserType#", data[0].Trim())
                    .Replace("#PaymentCode#", data[1].Trim())
                    .Replace("#Code#", data[2].Trim()));

                builder.AppendLine();

                //counter++;
            }

            //builder.Append(GetTrailer());

            builder.AppendLine(
                "PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were INSERTED with TestClasses and TestCategories'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        [Fact]
        public void GenerateDbScriptByTemplate_PayCodesTestSets()
        {
            string filename = "Insert_PayCodesTestSets_DbScriptByTemplate2";

            var builder = new StringBuilder();

            string path = @"C:\2021-0602\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row = GetInputData().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                builder.AppendLine();

                builder.Append(GetTemplate()
                    .Replace("#PaymentCode#", data[0].Trim())
                    .Replace("#TestSet#", data[1].Trim())
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
                return File.ReadAllText(filePath, Encoding.GetEncoding("ISO-8859-1"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetTemplate()
        {
            return @"
-- #number#.  #PaymentCode# #TestSet#
IF (NOT EXISTS ( 
	SELECT COUNT(PCCS.TestSetId) qtd
	FROM PaymentCodeTestSet PCCS
	WHERE PCCS.TestSetId in (Select id from TestSets where code = '#TestSet#')
	GROUP BY PCCS.TestSetId ))

	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[PaymentCodeTestSet] ([PaymentCodeId] ,[TestSetId])
			SELECT (select id from PaymentCode where PaymentCode = '#PaymentCode#') as PaymentCodeId, (Select id from TestSets where code = '#TestSet#') as TestSetId

			PRINT 'SUCCESS: TestSet code: #TestSet# for PaymentCode: #PaymentCode# was successfully INSERTED on the PaymentCodeTestSet table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: TestSet code: #TestSet# for PaymentCode: #PaymentCode# was NOT INSERTED on PaymentCodeTestSet table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF (EXISTS ( SELECT COUNT(PCCS.TestSetId) qtd
			FROM PaymentCodeTestSet PCCS
			WHERE PCCS.TestSetId in (Select id from TestSets where code = '#TestSet#')
			GROUP BY PCCS.TestSetId
			HAVING COUNT(PCCS.TestSetId) = 1 ))

			BEGIN
				BEGIN TRY	
					UPDATE PaymentCodeTestSet SET PaymentCodeId = (SELECT id from paymentcode where paymentcode = '#PaymentCode#')
					WHERE TestSetId = (SELECT id from TestSets where Code = '#TestSet#')
			
					PRINT 'SUCCESS: TestSet code: #TestSet# for PaymentCode: #PaymentCode# was successfully updated on the PaymentCodeTestSet table'
				END TRY
				BEGIN CATCH
					PRINT 'FAILED: TestSet code: #TestSet# for PaymentCode: #PaymentCode# was NOT updated on PaymentCodeTestSet table due to: ' + ERROR_MESSAGE()
				END CATCH
			END
		ELSE
			PRINT 'FAILED: TestSet code: #TestSet# for PaymentCode: #PaymentCode# was was NOT updated on the PaymentCodeTestSet table'				
	END";
        }

        public string GetInputData()
        {
            return @"AECOM;aerel01f
ATCNR;atdhs1f
ATCOM;atbsdt1f
ATCOM;atrel01f
AUCNR;audhs1f
AUCOM;aubsd1f
AUCOM;aurel1f";

        }

        [Fact]
        public void TrimEnd_Test()
        {
            string sentence = "The dog had a bone, a ball, and other toys.";
            char[] charsToTrim = { ',', '.', ' ' };
            string[] words = sentence.Split();
            var result = sentence.TrimEnd(charsToTrim);
            var trimmedText = string.Empty;
            foreach (string word in words)
            {
                Console.WriteLine(word.TrimEnd(charsToTrim));
                trimmedText += word.TrimEnd(charsToTrim);
            }
            Assert.NotEmpty(trimmedText);
        }
    }

}
