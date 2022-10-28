using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class FindReplaceBatchTest
    {
        public string GetBuDbVariableName(string XYZ)
        {

            switch (XYZ)
            {
                case "11":
                    return "@XYZ11";
                case "1212":
                    return "@XYZ1212";
                case "1222":
                    return "@XYZ1222";
                case "1224":
                    return "@XYZ1224";
                case "1313":
                    return "@XYZ1313";
                case "1401":
                    return "@XYZ1401";
                case "1717":
                    return "@XYZ1717";
                case "1841":
                    return "@XYZ1841";
                case "1919":
                    return "@XYZ1919";
                case "202":
                    return "@XYZ202";
                case "2121":
                    return "@XYZ2121";
                case "2323":
                    return "@XYZ2323";
                case "2727":
                    return "@XYZ2727";
                case "2828":
                    return "@XYZ2828";
                case "2929":
                    return "@XYZ2929";
                case "3131":
                    return "@XYZ3131";
                case "3232":
                    return "@XYZ3232";
                case "3434":
                    return "@XYZ3434";
                case "3535":
                    return "@XYZ3535";
                case "3838":
                    return "@XYZ3838";
                case "4046":
                    return "@XYZ4046";
                case "4065":
                    return "@XYZ4065";
                case "4075":
                    return "@XYZ4075";
                case "439":
                    return "@XYZ439";
                case "4444":
                    return "@XYZ4444";
                case "4545":
                    return "@XYZ4545";
                case "5000":
                    return "@XYZ5000";
                case "5102":
                    return "@XYZ5102";
                case "531":
                    return "@XYZ531";
                case "5455":
                    return "@XYZ5455";
                case "546":
                    return "@XYZ546";
                case "547":
                    return "@XYZ547";
                case "551":
                    return "@XYZ551";
                case "552":
                    return "@XYZ552";
                case "572":
                    return "@XYZ572";
                case "584":
                    return "@XYZ584";
                case "592":
                    return "@XYZ592";
                case "5959":
                    return "@XYZ5959";
                case "6161":
                    return "@XYZ6161";
                case "6969": 
                    return "@XYZ6969";
                case "707":
                    return "@XYZ707";
                case "7460":
                    return "@XYZ7460";
                case "7465":
                    return "@XYZ7465";
                case "808":
                    return "@XYZ808";
                case "8270":
                    return "@XYZ8270";
                case "909":
                    return "@XYZ909";
                default:
                    return "";
            }
        }

        public string GetHeaderValidation()
        {
            return @"USE [DcDb]
DECLARE @COUNTER INT = 0
DECLARE @XYZ11	   UNIQUEIDENTIFIER
DECLARE @XYZ1212  UNIQUEIDENTIFIER
DECLARE @XYZ1222  UNIQUEIDENTIFIER
DECLARE @XYZ1224  UNIQUEIDENTIFIER
DECLARE @XYZ1313  UNIQUEIDENTIFIER
DECLARE @XYZ1401  UNIQUEIDENTIFIER
DECLARE @XYZ1717  UNIQUEIDENTIFIER
DECLARE @XYZ1841  UNIQUEIDENTIFIER
DECLARE @XYZ1919  UNIQUEIDENTIFIER
DECLARE @XYZ202   UNIQUEIDENTIFIER
DECLARE @XYZ2121  UNIQUEIDENTIFIER
DECLARE @XYZ2323  UNIQUEIDENTIFIER
DECLARE @XYZ2727  UNIQUEIDENTIFIER
DECLARE @XYZ2828  UNIQUEIDENTIFIER
DECLARE @XYZ2929  UNIQUEIDENTIFIER
DECLARE @XYZ3131  UNIQUEIDENTIFIER
DECLARE @XYZ3232  UNIQUEIDENTIFIER
DECLARE @XYZ3434  UNIQUEIDENTIFIER
DECLARE @XYZ3535  UNIQUEIDENTIFIER
DECLARE @XYZ3838  UNIQUEIDENTIFIER
DECLARE @XYZ4046  UNIQUEIDENTIFIER
DECLARE @XYZ4065  UNIQUEIDENTIFIER
DECLARE @XYZ4075  UNIQUEIDENTIFIER
DECLARE @XYZ439   UNIQUEIDENTIFIER
DECLARE @XYZ4444  UNIQUEIDENTIFIER
DECLARE @XYZ4545  UNIQUEIDENTIFIER
DECLARE @XYZ5000  UNIQUEIDENTIFIER
DECLARE @XYZ5102  UNIQUEIDENTIFIER
DECLARE @XYZ531   UNIQUEIDENTIFIER
DECLARE @XYZ5455  UNIQUEIDENTIFIER
DECLARE @XYZ546   UNIQUEIDENTIFIER
DECLARE @XYZ547   UNIQUEIDENTIFIER
DECLARE @XYZ551   UNIQUEIDENTIFIER
DECLARE @XYZ552   UNIQUEIDENTIFIER
DECLARE @XYZ572   UNIQUEIDENTIFIER
DECLARE @XYZ584   UNIQUEIDENTIFIER
DECLARE @XYZ592   UNIQUEIDENTIFIER
DECLARE @XYZ5959  UNIQUEIDENTIFIER
DECLARE @XYZ6161  UNIQUEIDENTIFIER
DECLARE @XYZ6969  UNIQUEIDENTIFIER
DECLARE @XYZ707   UNIQUEIDENTIFIER
DECLARE @XYZ7460  UNIQUEIDENTIFIER
DECLARE @XYZ7465  UNIQUEIDENTIFIER
DECLARE @XYZ808   UNIQUEIDENTIFIER
DECLARE @XYZ8270  UNIQUEIDENTIFIER
DECLARE @XYZ909   UNIQUEIDENTIFIER

Select @XYZ11	   = id from table where code = '11'	
Select @XYZ1212   = id from table where code = '1212'
Select @XYZ1222   = id from table where code = '1222'
Select @XYZ1224   = id from table where code = '1224'
Select @XYZ1313   = id from table where code = '1313'
Select @XYZ1401   = id from table where code = '1401'
Select @XYZ1717   = id from table where code = '1717'
Select @XYZ1841   = id from table where code = '1841'
Select @XYZ1919   = id from table where code = '1919'
Select @XYZ202    = id from table where code = '202'
Select @XYZ2121   = id from table where code = '2121'
Select @XYZ2323   = id from table where code = '2323'
Select @XYZ2727   = id from table where code = '2727'
Select @XYZ2828   = id from table where code = '2828'
Select @XYZ2929   = id from table where code = '2929'
Select @XYZ3131   = id from table where code = '3131'
Select @XYZ3232   = id from table where code = '3232'
Select @XYZ3434   = id from table where code = '3434'
Select @XYZ3535   = id from table where code = '3535'
Select @XYZ3838   = id from table where code = '3838'
Select @XYZ4046   = id from table where code = '4046'
Select @XYZ4065   = id from table where code = '4065'
Select @XYZ4075   = id from table where code = '4075'
Select @XYZ439    = id from table where code = '439'
Select @XYZ4444   = id from table where code = '4444'
Select @XYZ4545   = id from table where code = '4545'
Select @XYZ5000   = id from table where code = '5000'
Select @XYZ5102   = id from table where code = '5102'
Select @XYZ531    = id from table where code = '531'
Select @XYZ5455   = id from table where code = '5455'
Select @XYZ546    = id from table where code = '546'
Select @XYZ547    = id from table where code = '547'
Select @XYZ551    = id from table where code = '551'
Select @XYZ552    = id from table where code = '552'
Select @XYZ572    = id from table where code = '572'
Select @XYZ584    = id from table where code = '584'
Select @XYZ592    = id from table where code = '592'
Select @XYZ5959   = id from table where code = '5959'
Select @XYZ6161   = id from table where code = '6161'
Select @XYZ6969   = id from table where code = '6969'
Select @XYZ707    = id from table where code = '707'
Select @XYZ7460   = id from table where code = '7460'
Select @XYZ7465   = id from table where code = '7465'
Select @XYZ808    = id from table where code = '808'
Select @XYZ8270   = id from table where code = '8270'
Select @XYZ909    = id from table where code = '909'";
        }

        public string GetTrailerValidation()
        {
            return @"IF @COUNTER = 0	
	PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were successfully INSERTED with TestClasses and TestCategories'	
ELSE
	PRINT 'FAILED: There are ' +  CAST(@COUNTER AS VARCHAR(15)) +  ' TestClassId and TestCategoryId NOT inserted in the TestClassesTestCategories table' 
";
        }

        public string GetTestClassesTestCategories_VALIDATION()
        {
            return @"IF (NOT EXISTS ( 
	SELECT TOP 1 1 
	FROM TestClassesTestCategories CCCC
	WHERE CCCC.TestClassId = (select top 1 id from TestClasses where code ='#ClassCode#' and BusinessUnitId = #BUCode#)
	AND CCCC.TestCategoryId = (select top 1 id from TestCategories where code = '#CategoryCode#' and BusinessUnitId = #BUCode#)))
	BEGIN
		SET @COUNTER = @COUNTER+1
		PRINT 'FAILED: TestClassId: #ClassCode# and TestCategoryId #CategoryCode# were NOT inserted in the TestClassesTestCategories table' 
	END";
        }

        [Fact]
        public void GenerateDbScriptByTemplate_Validation_TestClassesTestCategories()
        {
            //#ClassCode#
            //#CategoryCode#
            //#BUCode#

            string filename = "Validation_TestClassesTestCategories_DbScriptByTemplate";
            string fileToSearch = @"C:\TestClassesTestCategories\_Class_To_Category_Relationship_Data.csv";

            var builder = new StringBuilder();
            builder.AppendLine(GetHeaderValidation());

            builder.AppendLine();

            string path = @"C:\TestClassesTestCategories\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDatafromFile_for_TestClassesTestCategories(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;


                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                builder.AppendLine(GetTestClassesTestCategories_VALIDATION()
                    .Replace("#ClassCode#", data[0].Trim())
                    .Replace("#CategoryCode#", data[1].Trim())
                    .Replace("#BUCode#", GetBuDbVariableName(data[2].Trim())));
                builder.AppendLine();
            }

            builder.AppendLine(GetTrailerValidation());

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetHeader()
        {
            return @"IF (NOT EXISTS ( SELECT COUNT(*) qtd FROM TestClassesTestCategories HAVING COUNT(*) > 0))
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[TestClassesTestCategories] ([TestClassId], [TestCategoryId])";
        }

        public string GetTrailer()
        {
            return @"			PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were successfully INSERTED with TestClasses and TestCategories'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Data for NEW TestClassesTestCategories Table has FAILED due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN				
		PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table already inserted'		
	END";
        }

        //#ClassCode#
        //#CategoryCode#
        //#BUCode#
        public string GetTestClassesTestCategories_SELECT_INSERT()
        {
            return @"			SELECT (select top 1 id from TestClasses where code ='#ClassCode#' and BusinessUnitId = (Select id from table where code = '11')) as TestClassId,  (select top 1 id from TestCategories where code = '#CategoryCode#' and BusinessUnitId = (Select id from table where code = '#BUCode#')) as TestCategoryId  UNION";
        }

        public string GetTestClassesTestCategories_INSERT_INTO()
        {
            return @"BEGIN
	BEGIN TRY		
		INSERT INTO [dbo].[TestClassesTestCategories] ([TestClassId], [TestCategoryId]) VALUES ((select top 1 id from TestClasses where code ='#ClassCode#' and BusinessUnitId = (Select id from table where code = '#BUCode#')), (select top 1 id from TestCategories where code = '#CategoryCode#' and BusinessUnitId = (Select id from table where code = '#BUCode#')))		
	END TRY
	BEGIN CATCH
		PRINT 'FAILED: TestClassId: #ClassCode# and TestCategoryId: #CategoryCode# for bu: #BUCode# were NOT inserted in the TestClassesTestCategories table due to: ' + ERROR_MESSAGE()
	END CATCH
END
GO";
        }

        [Fact]
        public void GenerateDbScriptByTemplate_TestClassesTestCategories()
        {
            //#ClassCode#
            //#CategoryCode#
            //#BUCode#

            string filename = "Insert_TestClassesTestCategories_DbScriptByTemplate_25-05-2021";
            string fileToSearch = @"C:\TestClassesTestCategories\_Class_To_Category_Relationship_Data.csv";

            var builder = new StringBuilder();
            //builder.AppendLine(GetHeader());

            builder.AppendLine("USE [DcDb]");

            string path = @"C:\TestClassesTestCategories\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row = 
                GetDatafromFile_for_TestClassesTestCategories(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            //int counter = 0;

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] { "," }, StringSplitOptions.None);

                //if (counter == 100)
                //{
                //    counter = 0;
                //    builder.AppendLine("GO");
                //}

                builder.AppendLine(GetTestClassesTestCategories_INSERT_INTO()
                    .Replace("#ClassCode#", data[0].Trim())
                    .Replace("#CategoryCode#", data[1].Trim())
                    .Replace("#BUCode#", data[2].Trim()));

                //builder.AppendLine("GO");

                //counter++;
            }

            //builder.Append(GetTrailer());

            builder.AppendLine(
                "PRINT 'SUCCESS: Data for NEW TestClassesTestCategories Table were INSERTED with TestClasses and TestCategories'");

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetDatafromFile_for_TestClassesTestCategories(string filePath)
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
        public void GenerateDbScriptByTemplate_PayCodesTestSets()
        {
            string filename = "Insert_PayCodesTestSets_DbScriptByTemplate2";

            //string fileToSearch = @"C:\Users\fred_sena\DataFile.txt";
            
            var builder = new StringBuilder();
            //builder.Append(GetDatafromFile(fileToSearch));

            string path = @"C:\2021-0602\";

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row = GetInputData().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                string[] data = row[i].Split(new[] {","}, StringSplitOptions.None);

                builder.AppendLine();

                builder.Append(GetTemplate()
                    .Replace("#PaymentCode#", data[0].Trim())
                    .Replace("#TestSet#", data[1].Trim())
                    .Replace("#number#", (i+1).ToString()));

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetTemplate2()
        {
            return @"SELECT (select id from PaymentCode where PaymentCode = '#PaymentCode#') as PaymentCodeId, (Select id from TestSets where code = '#TestSet#') as TestSetId UNION";
        }

        public string GetInputData2()
        {
            return @"I2CNR,i3dhs1f
I2CNR,indhs1f
REL,g_369604
ATCNR,atdhs1f
I2COM,i3bsd1f
I2COM,inrel1f
I2COM,inbsd1f
I2COM,i3rel1f
G500,usglob1f
G500,usdglob1f
APO,usapo1f
APO,usdapo1f
HKCOM,hkbsd1f
HKCOM,hkrel1f
HKCNR,hkdhs1f
NLCOM,nlbsdt1f
NLCOM,nlrel01f
DHS,mxdhs1f
DHS,usdhs1f
DHS,usdarb1f
TWCNR,twdhs1f
CARB,uscarb1f
APJCHP01,nzdhs1g
APJCHP01,jpdhs1g
APJCHP01,inrel1g
APJCHP01,sgbsd1g
APJCHP01,jprel1g
APJCHP01,indhs1g
APJCHP01,mydhs1g
APJCHP01,hkrel1g
APJCHP01,idrel1g
APJCHP01,idbsd1g
APJCHP01,i3bsd1g
APJCHP01,sgdhs1g
APJCHP01,hkdhs1g
APJCHP01,jpbsd1g
APJCHP01,thdhs1g
APJCHP01,thbsd1g
APJCHP01,cndhs1g
APJCHP01,cnrel1g
APJCHP01,hkbsd1g
APJCHP01,nzrel1g
APJCHP01,krrel1g
APJCHP01,mybsd1g
APJCHP01,cnbsd1g
APJCHP01,threl1g
APJCHP01,sgrel1g
APJCHP01,iddhs1g
APJCHP01,aurel1g
APJCHP01,i3rel1g
APJCHP01,i3dhs1g
APJCHP01,nzbsd1g
APJCHP01,myrel1g
APJCHP01,krbsd1g
APJCHP01,aubsd1g
APJCHP01,inbsd1g
APJCHP01,audhs1g
APJCHP01,krdhs1g
AUCNR,audhs1f
IC,usic1f
IC,usdic1f
WPK,uswpk1f
WPK,usdwpk1f
FICNR,fidhs1f
I1CNR,i1dhs1f
XYZCHP01,rurel01g
XYZCHP01,uarel01g
XYZCHP01,grrel01g
XYZCHP01,trrel01g
XYZCHP01,aerel01g
XYZCHP01,hurel01g
XYZCHP01,ilrel01g
RUCOM,rurel01f
MKT,ca3mp1f
MKT,ca3mr1f
TRCOM,trrel01f
EPK,usepk1f
EPK,usdepk1f
GRCOM,grrel01f
LA,uslata1f
LA,uslatb1f
LA,usdlata1f
LA,usdlatb1f
LA,g_20
COM,caint1f
COM,castg1f
COM,cagcp1f
COM,cagcc1f
COM,cagcd1f
COM,cacmb1f
COM,cacmbp1f
COM,mxbiz1f
COM,cacpu1f
COM,caccm1f
COM,g_70711
COM,cacgc1f
NLCNR,nldhs1f
USCHP01,usle1f
USCHP01,usmb1f
USCHP01,usglob1f
USCHP01,usdhs1f
USCHP01,usbsd1f
USCHP01,usmpp1f
USCHP01,uschat1f
USCHP01,usdarb1f
USCHP01,uslata1f
USCHP01,uslatb1f
USCHP01,usreta1f
USCHP01,usretb1f
USCHP01,usfeda1f
USCHP01,usfedb1f
USCHP01,uscarb1f
USCHP01,usic1f
USCHP01,usuc1f
USCHP01,uswc1f
USCHP01,uscc1f
USCHP01,uspec1f
USCHP01,usac1f
USCHP01,uswpk1f
USCHP01,uscpk1f
USCHP01,usepk1f
USCHP01,usapk1f
USCHP01,uswpo1f
USCHP01,uscpo1f
USCHP01,usepo1f
USCHP01,usapo1f
USCHP01,isipo1f
USCHP01,usintr1f
USCHP01,uspilot1f
USCHP01,usdmb1f
USCHP01,usdglob1f
USCHP01,usdbsd1f
USCHP01,usdchat1f
USCHP01,usdlata1f
USCHP01,usdlatb1f
USCHP01,usdfeda1f
USCHP01,usdfedb1f
USCHP01,usdic1f
USCHP01,usduc1f
USCHP01,usdwc1f
USCHP01,usdcc1f
USCHP01,usdpec1f
USCHP01,usdac1f
USCHP01,usdwpk1f
USCHP01,usdcpk1f
USCHP01,usdepk1f
USCHP01,usdapk1f
USCHP01,usdwpo1f
USCHP01,usdcpo1f
USCHP01,usdepo1f
USCHP01,usdapo1f
USCHP01,isdipo1f
USCHP01,usdintr1f
USCHP01,usppisc1f
USCHP01,g_20
PCA,usmb1f
PCA,usdmb1f
DICOM,sxbsd1f
DICOM,sxrel1f
SGCOM,sgrel1f
SGCOM,sgbsd1f
AUCOM,aurel1f
AUCOM,aubsd1f
NOCNR,nodhs1f
GBCNR,ukdhs1f
LEA,usle1f
SECOM,serel01f
SECOM,sebsdt1f
KRCNR,krdhs1f
IPO,isipo1f
IPO,isdipo1f
TWCOM,twbsd1f
TWCOM,twrel1f
JPCOM,jpbsd1f
JPCOM,jprel1f
UC,usuc1f
UC,usduc1f
CON,campp1f
CON,ca3mp1f
CON,ca3mr1f
CON,caccn1f
ITCOM,itrel01f
ITCOM,itbsdt1f
INT,usintr1f
INT,uspilot1f
INT,usdintr1f
THCOM,threl1f
THCOM,thbsd1f
FICOM,fibsdt1f
FICOM,firel01f
FRCNR,frdhs1f
CHAT,uschat1f
CHAT,usdchat1f
SECNR,sedhs1f
CC,uscc1f
CC,usdcc1f
FED,usfeda1f
FED,usfedb1f
FED,usdfeda1f
FED,usdfedb1f
DECOM,debsdt1f
DECOM,derel01f
THCNR,thdhs1f
CPO,uscpo1f
CPO,usdcpo1f
ATCOM,atrel01f
ATCOM,atbsdt1f
AC,usac1f
AC,usdac1f
LUCOM,lurel01f
WPO,uswpo1f
WPO,usdwpo1f
CNCNR,cndhs1f
BECNR,bedhs1f
DKCNR,dkdhs1f
ZACOM,zarel01f
IEREN,ieren11f
IEREN,ieren21f
IEREN,ieren01f
BECOM,bebsdt1f
BECOM,berel01f
APK,usapk1f
APK,usdapk1f
ILCOM,ilrel01f
CHCOM,chbsdt1f
CHCOM,chrel01f
GBCOM,ukpub01f
GBCOM,ukpad01f
GBCOM,ukcbg01f
GBCOM,ukbsdt1f
IDCNR,iddhs1f
SACOM,sarel01f
PLCOM,plrel01f
FRCOM,frbsdt1f
FRCOM,frrel01f
CHCNR,chdhs1f
XYZCHP03,sarel01g
MYCOM,myrel1f
MYCOM,mybsd1f
ITCNR,itdhs1f
DICNR,sxdhs1f
EDCOM,edrel01f
CPK,uscpk1f
CPK,usdcpk1f
CNCOM,cnbsd1f
CNCOM,cnrel1f
EPO,usepo1f
EPO,usdepo1f
MYCNR,mydhs1f
SGCNR,sgdhs1f
I3COM,i3rel1f
KRCOM,krbsd1f
KRCOM,krrel1f
SKCOM,skrel01f
ESCOM,esrel01f
ESCOM,esbsdt1f
HUCOM,hurel01f
NZCNR,nzdhs1f
SMB,cacsb1f
IECOM,iebsdt1f
IECOM,ierel01f
DKCOM,dkrel01f
DKCOM,dkbsdt1f
RET,careu1f
RET,usreta1f
RET,usretb1f
RET,cacrt1f
CACHP01,caint1f
CACHP01,castg1f
CACHP01,cagcp1f
CACHP01,cagcc1f
CACHP01,cagcd1f
CACHP01,cacmb1f
CACHP01,careu1f
CACHP01,campp1f
CACHP01,cacmbp1f
CACHP01,ca3mp1f
CACHP01,ca3mr1f
CACHP01,cacpu1f
CACHP01,caccm1f
CACHP01,cacsb1f
CACHP01,g_70711
CACHP01,cacrt1f
CACHP01,cacgc1f
CACHP01,caccn1f
IECNR,iedhs1f
APJCHP02,sxrel1g
APJCHP02,twrel1g
APJCHP02,twdhs1g
APJCHP02,i1bsd1g
APJCHP02,sxdhs1g
APJCHP02,i1dhs1g
APJCHP02,sxbsd1g
APJCHP02,twbsd1g
APJCHP02,i1rel1g
UKREN,ukren41f
UKREN,ukren31f
UKREN,ukren61f
UKREN,ukren51f
UKREN,ukren01f
UKREN,ukren71f
UKREN,ukren11f
UKREN,ukren21f
CZCOM,czrel01f
PTCOM,ptrel01f
MPP,campp1f
MPP,usmpp1f
WC,uswc1f
WC,usdwc1f
JPCNR,jpdhs1f
DECNR,dedhs1f
BSD,mxbsdt1f
BSD,usbsd1f
BSD,usdbsd1f
BSD,usppisc1f
BSD,cacsb1f
EC,uspec1f
EC,usdpec1f
UACOM,uarel01f
AECOM,aerel01f
NZCOM,nzbsd1f
NZCOM,nzrel1f
ESCNR,esdhs1f
NOCOM,norel01f
NOCOM,nobsdt1f
I1COM,i1rel1f
I1COM,i1bsd1f
IDCOM,idbsd1f
IDCOM,idrel1f
";
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
AUCOM;aurel1f
BECNR;bedhs1f
BECOM;bebsdt1f
BECOM;berel01f
CHCNR;chdhs1f
CHCOM;chbsdt1f
CHCOM;chrel01f
CNCNR;cndhs1f
CNCOM;cnbsd1f
CNCOM;cnrel1f
CZCOM;czrel01f
DECNR;dedhs1f
DICNR;sxdhs1f
DICOM;sxbsd1f
DICOM;sxrel1f
DKCNR;dkdhs1f
DKCOM;dkbsdt1f
DKCOM;dkrel01f
EDCOM;edrel01f
ESCNR;esdhs1f
ESCOM;esbsdt1f
ESCOM;esrel01f
FICNR;fidhs1f
FICOM;fibsdt1f
FICOM;firel01f
FRCNR;frdhs1f
GBCNR;ukdhs1f
GRCOM;grrel01f
HKCNR;hkdhs1f
HKCOM;hkbsd1f
HKCOM;hkrel1f
HUCOM;hurel01f
I1CNR;i1dhs1f
I1COM;i1bsd1f
I1COM;i1rel1f
I2CNR;i3dhs1f
I2CNR;indhs1f
I2COM;i3bsd1f
I2COM;i3rel1f
I2COM;inbsd1f
I2COM;inrel1f
I3COM;i3rel1f
IDCNR;iddhs1f
IDCOM;idbsd1f
IDCOM;idrel1f
IECNR;iedhs1f
IECOM;iebsdt1f
IECOM;ierel01f
ILCOM;ilrel01f
ITCNR;itdhs1f
ITCOM;itbsdt1f
ITCOM;itrel01f
JPCNR;jpdhs1f
JPCOM;jpbsd1f
JPCOM;jprel1f
KRCNR;krdhs1f
KRCOM;krbsd1f
KRCOM;krrel1f
LUCOM;lurel01f
MYCNR;mydhs1f
MYCOM;mybsd1f
MYCOM;myrel1f
NLCNR;nldhs1f
NLCOM;nlbsdt1f
NLCOM;nlrel01f
NOCNR;nodhs1f
NOCOM;nobsdt1f
NOCOM;norel01f
NZCNR;nzdhs1f
NZCOM;nzbsd1f
NZCOM;nzrel1f
PLCOM;plrel01f
PTCOM;ptrel01f
RUCOM;rurel01f
SACOM;sarel01f
SECNR;sedhs1f
SECOM;sebsdt1f
SECOM;serel01f
SGCNR;sgdhs1f
SGCOM;sgbsd1f
SGCOM;sgrel1f
SKCOM;skrel01f
THCNR;thdhs1f
THCOM;thbsd1f
THCOM;threl1f
TRCOM;trrel01f
TWCNR;twdhs1f
TWCOM;twbsd1f
TWCOM;twrel1f
UACOM;uarel01f
ZACOM;zarel01f";

        }
        public string GetDatafromFile(string filePath)
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
        public void test()
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
