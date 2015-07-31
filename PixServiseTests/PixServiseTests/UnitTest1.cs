using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    /* [TestFixture]
     public class UnitTest1
     {
         [Test]
         public void AddEmptyPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);


         }
         [Test]
         public void SearchEmptyPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             var test = client.GetPatient("8CDE415D-FAB7-4809-AA37-8CDD70B1B46C", "1.2.643.5.1.13.3.25.78.118", patient, SourceType.Fed);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddMinPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PixServiceClient c = new PixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             patient = new PatientDto();
             patient.IdPatientMIS = "123456789010";
             client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient, SourceType.Reg);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void UpdateMinPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             patient.FamilyName = "Сидоров";
             client.UpdatePatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void UpdatePatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "АЛЕКСЕЙ";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "12345678900029";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 14;
             document.DocS = "1311";
             document.DocN = "113131";
             document.ProviderName = "УФМС";
             patient.Documents = new PixServise.DocumentDto[] { document };
             PixServise.AddressDto address = new PixServise.AddressDto();
             address.IdAddressType = 1;
             address.StringAddress = "ТУТ";
             patient.Addresses = new PixServise.AddressDto[] { address };
             ContactDto cont = new ContactDto();
             cont.IdContactType = 1;
             cont.ContactValue = "89519435454";
             ContactDto cont2 = new ContactDto();
             cont2.IdContactType = 1;
             cont2.ContactValue = "89519435455";
             patient.Contacts = new ContactDto[] { cont, cont2 };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             PatientDto patient2 = new PatientDto();
             PixServise.DocumentDto document2 = new PixServise.DocumentDto();
             document2.IdDocumentType = 14;
             document2.DocS = "1311";
             document2.DocN = "113131";
             document2.ProviderName = "УФМС";
             patient2.Documents = new PixServise.DocumentDto[] { document2 };
             PixServise.AddressDto address2 = new PixServise.AddressDto();
             address2.IdAddressType = 1;
             address2.StringAddress = "ТУТ";
             patient2.FamilyName = "Сидоров";
             patient2.Addresses = new PixServise.AddressDto[] { address2 };
             ContactDto cont3 = new ContactDto();
             cont3.IdContactType = 1;
             cont3.ContactValue = "89519435456";
             patient2.Contacts = new ContactDto[] { cont3 };
             patient2.IdPatientMIS = patient.IdPatientMIS;
             client.UpdatePatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient2);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void FindByFamily()
         {
             //System.Collections.ArrayList exeptions;
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Павел";
             patient.GivenName = "Петров";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PatientDto forSearch = new PatientDto();
             forSearch.FamilyName = "Павел";
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             var patents = client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Fed);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void FindByFamilyAndName()
         {
             //System.Collections.ArrayList exeptions;
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Павел";
             patient.GivenName = "Петров";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PatientDto forSearch = new PatientDto();
             forSearch.FamilyName = "Павел";
             forSearch.GivenName = "Петров";
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             var patents = client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Fed);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void SearchPatientByMinParametrAndDocument()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 14;
             document.DocS = "1234";
             document.DocN = "123456";
             document.ProviderName = "УФМС";
             patient.Documents = new PixServise.DocumentDto[] { document };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             PatientDto forSearch = new PatientDto();
             forSearch.FamilyName = "Жукин";
             forSearch.GivenName = "Дмитрий";
             forSearch.BirthDate = new DateTime(1983, 01, 07);
             forSearch.Sex = 1;
             PixServise.DocumentDto forSearchD = new PixServise.DocumentDto();
             forSearchD.IdDocumentType = 14;
             forSearchD.DocS = "1234";
             forSearchD.DocN = "123456";
             forSearch.Documents = new PixServise.DocumentDto[] { document };
             client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Reg);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddPatientWithPassport()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Легенда";
             patient.GivenName = "Легенда";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "1123123123";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 14;
             document.DocS = "1311";
             document.DocN = "113131";
             document.ProviderName = "УФМС";
             patient.Documents = new PixServise.DocumentDto[] { document };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddPatientWithAddress()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PixServise.AddressDto address = new PixServise.AddressDto();
             address.StringAddress = "Улица Ленина 47";
             address.IdAddressType = 1;
             PixServise.AddressDto address2 = new PixServise.AddressDto();
             address2.StringAddress = "Улица Партизанская 47";
             address2.IdAddressType = 2;
             patient.Addresses = new PixServise.AddressDto[] { address, address2 };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddPatientWithContacts()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             ContactDto contact = new ContactDto();
             contact.IdContactType = 1;
             contact.ContactValue = "89626959434";
             ContactDto contact2 = new ContactDto();
             contact2.IdContactType = 1;
             contact2.ContactValue = "89525959544";
             patient.Contacts = new ContactDto[] { contact, contact2 };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddPatientWithJob()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PixServise.JobDto job = new PixServise.JobDto();
             job.CompanyName = "OOO 'МИГ'";
             patient.Job = job;
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void PatientWithRealationship()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PatientDto patient2 = new PatientDto();
             patient2.FamilyName = "Петров";
             patient2.GivenName = "Денис";
             patient2.BirthDate = new DateTime(1999, 02, 03);
             patient2.Sex = 1;
             patient2.IdPatientMIS = "098765432111";
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient2);
             ContactPersonDto pers = new ContactPersonDto();
             pers.ContactList = patient2.Contacts;
             pers.FamilyName = patient2.FamilyName;
             pers.GivenName = patient2.GivenName;
             pers.IdPersonMis = patient2.IdPatientMIS;
             pers.IdRelationType = 2;
             pers.MiddleName = patient2.MiddleName;
             patient.ContactPerson = pers;
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddPatientWithPrivilege()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "123456789010";
             PrivilegeDto privilege = new PrivilegeDto();
             privilege.DateStart = new DateTime(1993, 01, 02);
             privilege.DateEnd = new DateTime(2020, 01, 02);
             privilege.IdPrivilegeType = 10;
             patient.Privilege = privilege;
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void AddNewMedicalCase()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Легенда";
             patient.GivenName = "Легенда";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "1123123123";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 14;
             document.DocS = "1311";
             document.DocN = "113131";
             document.ProviderName = "УФМС";
             patient.Documents = new PixServise.DocumentDto[] { document };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             PatientDto patient2 = new PatientDto();
             patient2.FamilyName = "Легенда";
             patient2.GivenName = "Легенда";
             patient2.BirthDate = new DateTime(1983, 01, 07);
             patient2.Sex = 1;
             patient2.IdPatientMIS = "1234567890987";
             PixServise.DocumentDto document2 = new PixServise.DocumentDto();
             document2.IdDocumentType = 14;
             document2.DocS = "1311";
             document2.DocN = "113131";
             document2.ProviderName = "УФМС";
             patient2.Documents = new PixServise.DocumentDto[] { document2 };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.29.1", patient2);
             TestPatient a = TestPatient.BuildPatientFromDataBaseData("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient.IdPatientMIS);
             TestPatient b = TestPatient.BuildPatientFromDataBaseData("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.29.1", patient2.IdPatientMIS);
             if (a.patient.IdGlobal != b.patient.IdGlobal)
                 Global.errors1.Add("Создан новый пациент");
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }

         [Test]
         public void AddFullPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.DeathTime = new DateTime(2020, 02, 15);
             patient.FamilyName = "Жукин";
             patient.GivenName = "Дмитрий";
             patient.IdBloodType = 1;
             patient.IdLivingAreaType = 1;
             patient.IdPatientMIS = "123456789010";
             patient.MiddleName = "Артемович";
             patient.Sex = 1;
             patient.SocialGroup = 2;
             patient.SocialStatus = "2.11";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 14;
             document.DocS = "1311";
             document.DocN = "113131";
             document.ExpiredDate = new DateTime(1999, 11, 12);
             document.IssuedDate = new DateTime(2012, 11, 12);
             document.ProviderName = "УФМС";
             document.RegionCode = "1221";
             patient.Documents = new PixServise.DocumentDto[] { document };
             PixServise.AddressDto address = new PixServise.AddressDto();
             address.IdAddressType = 1;
             address.StringAddress = "Улица Ленина 47";
             address.Street = "01000001000000100";
             address.Building = "1";
             address.City = "0100000000000";
             address.Appartment = "1";
             address.PostalCode = 1;
             address.GeoData = "1";
             PixServise.AddressDto address2 = new PixServise.AddressDto();
             address2.IdAddressType = 2;
             address2.StringAddress = "Улица Партизанская 47";
             address2.Street = "01000001000000100";
             address2.Building = "1";
             address2.City = "0100000000000";
             address2.Appartment = "1";
             address2.PostalCode = 1;
             address2.GeoData = "1";
             patient.Addresses = new PixServise.AddressDto[] { address, address2 };
             BirthPlaceDto birthplace = new BirthPlaceDto();
             birthplace.City = "Тутинск";
             birthplace.Country = "маленькая";
             birthplace.Region = "БОЛЬШОЙ";
             patient.BirthPlace = birthplace;
             ContactDto contact = new ContactDto();
             contact.IdContactType = 1;
             contact.ContactValue = "89626959434";
             ContactDto contact2 = new ContactDto();
             contact2.IdContactType = 1;
             contact2.ContactValue = "89525959544";
             patient.Contacts = new ContactDto[] { contact, contact2 };
             PixServise.JobDto job = new PixServise.JobDto();
             job.OgrnCode = "0100000000000";
             job.CompanyName = "OOO 'МИГ'";
             job.Sphere = "Я";
             job.Position = "Я";
             job.DateStart = new DateTime(2003, 1, 1);
             job.DateEnd = new DateTime(2004, 1, 1);
             patient.Job = job;
             PrivilegeDto privilege = new PrivilegeDto();
             privilege.DateStart = new DateTime(1993, 01, 02);
             privilege.DateEnd = new DateTime(2020, 01, 02);
             privilege.IdPrivilegeType = 10;
             patient.Privilege = privilege;
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         //[Test]
         //public void FindPatientBySnilsInFed()
         //{
         //    TestPixServiceClient client = new TestPixServiceClient();
         //    PatientDto patient = new PatientDto();
         //    patient.FamilyName = "Павел";
         //    patient.GivenName = "Петров";
         //    patient.BirthDate = new DateTime(1983, 01, 07);
         //    patient.Sex = 1;
         //    patient.IdPatientMIS = "1123123123";
         //    DocumentDto document = new DocumentDto();
         //    document.IdDocumentType = 223;
         //    document.DocN = "123-456-789 45";
         //    document.ProviderName = "Снилс";
         //    patient.Documents = new DocumentDto[] { document };
         //    client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
         //    PatientDto find = new PatientDto();
         //    find.FamilyName = "Павел";
         //    //find.GivenName = "Легенда";
         //    DocumentDto document2 = new DocumentDto();
         //    document2.IdDocumentType = 223;
         //    document2.DocN = "123-456-789 45";
         //    document2.ProviderName = "Снилс";
         //    find.Documents = new DocumentDto[] { document2 };
         //    client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", find, SourceType.Fed);
         //    if (Global.errors == "")
         //        Assert.Pass();
         //    else
         //        Assert.Fail(Global.errors);
         //}
         [Test]
         public void FindPatientBySnilsInReg()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Легенда";
             patient.GivenName = "Легенда";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "1123123123";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 223;
             document.DocN = "123-456-789 45";
             document.ProviderName = "Снилс";
             patient.Documents = new PixServise.DocumentDto[] { document };
             PatientDto find = new PatientDto();
             find.FamilyName = "Легенда";
             find.GivenName = "Легенда";
             find.BirthDate = new DateTime(1983, 01, 07);
             find.Documents = new PixServise.DocumentDto[] { document };
             client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", find, SourceType.Reg);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void FindPatientByIdMIS()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.FamilyName = "Легенда";
             patient.GivenName = "Легенда";
             patient.BirthDate = new DateTime(1983, 01, 07);
             patient.Sex = 1;
             patient.IdPatientMIS = "1123123123";
             PixServise.DocumentDto document = new PixServise.DocumentDto();
             document.IdDocumentType = 223;
             document.DocN = "123-456-789 45";
             document.ProviderName = "Снилс";
             patient.Documents = new PixServise.DocumentDto[] { document };
             client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
             PatientDto find = new PatientDto();
             find.IdPatientMIS = "1123123123";
             client.GetPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", find, SourceType.Reg);
             if (Global.errors == "")
                 Assert.Pass();
             else
                 Assert.Fail(Global.errors);
         }
         [Test]
         public void FindMultidocumentPatient()
         {
             TestPixServiceClient client = new TestPixServiceClient();
             PatientDto patient = new PatientDto();
             patient.Addresses = new PixServise.AddressDto[]
             {
                 new PixServise.AddressDto
                 {
                     IdAddressType = 1,
                     StringAddress = "Россия, г.Санкт-Петербург, р-н.Центральный, пер.Дегтярный, д.1/8, кв.82"
                 }
             };
             patient.BirthDate = new DateTime(1976, 07, 19);
             patient.BirthPlace = new BirthPlaceDto
             {
                 City = "г. СПБ",
                 Country = "г. СПБ",
                 Region = "г. СПБ"
             };
             patient.Contacts = new ContactDto[]
             {
                 new ContactDto
                 {
                     ContactValue = "274-26-75",
                     IdContactType = 1
                 }
             };
             patient.Documents = new PixServise.DocumentDto[]
             {
                 new PixServise.DocumentDto
                 {
                     DocN = "993820",
                     DocS = "40 02",
                     IdDocumentType = 14,
                     IssuedDate = new DateTime(2002, 09, 06),
                     ProviderName = "76 о/м СПб"
                 },
                 new PixServise.DocumentDto
                 {
                     DocN = "7852320830001562",
                     DocS = "ЕП",
                     IdDocumentType = 228,
                     IdProvider = "78008",
                     IssuedDate = new DateTime(2014, 05, 03),
                     ProviderName = "САНКТ-ПЕТЕРБУРГСКИЙ ФИЛИАЛ ОАО 'РОСНО-МС'"
                 },
                 new PixServise.DocumentDto
                 {
                     DocN = "148-841-391 96",
                     IdDocumentType = 223,
                     ProviderName = "ПФР"
                 }
             };
             patient.FamilyName = "Трескунов";
             patient.GivenName = "Роман";
             patient.IdLivingAreaType = 1;
             patient.Job = new PixServise.JobDto
             {
                 CompanyName = "Не работает",
             };
             patient.SocialStatus = "2.4";
             patient.IdPatientMIS = "2312312312399";
             patient.Sex = 1;
             client.AddPatient("5C04E58B-07C0-421C-804A-CD774685AEA2", "1.2.643.5.1.13.3.25.78.230", patient);
             client.UpdatePatient("5C04E58B-07C0-421C-804A-CD774685AEA2", "1.2.643.5.1.13.3.25.78.230", patient);
             PatientDto patient2 = new PatientDto();
             patient2.Documents = new PixServise.DocumentDto[]
             {
                 new PixServise.DocumentDto
                 {
                     DocN = "7852320830001562",
                     DocS = "ЕП",
                     IdDocumentType = 228
                 }
             };
             patient2.FamilyName = "Трескунов";
             patient2.GivenName = "Роман";
             patient2.BirthDate = new DateTime(1976, 07, 19);
             client.GetPatient("5C04E58B-07C0-421C-804A-CD774685AEA2", "1.2.643.5.1.13.3.25.78.230", patient2, SourceType.Reg);
         }
         [TearDown]
         public void Clear()
         {
             Global.errors3.Clear();
             Global.errors2.Clear();
             Global.errors1.Clear();
         }
     }*/

    [TestFixture]
    public class UnitTest2
    {
        /*  [Test]
          public void EMKAddMinAmbCase()
          {
              TestPixServiceClient c = new TestPixServiceClient();
              PatientDto patient = new PatientDto();
              patient.FamilyName = "Жукин";
              patient.GivenName = "Дмитрий";
              patient.BirthDate = new DateTime(1983, 01, 07);
              patient.Sex = 1;
              patient.IdPatientMIS = "123456789010";
              c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
              TestEmkServiceClient client = new TestEmkServiceClient();
              client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", new CaseAmb
                  {
                      OpenDate = new DateTime(2010, 10, 10),
                      CloseDate = new DateTime(2010, 10, 14),
                      HistoryNumber = "1000121",
                      IdCaseMis = "123412341234",
                      IdPaymentType = 1,
                      Confidentiality = 1,
                      DoctorConfidentiality = 1,
                      CuratorConfidentiality = 1,
                      IdLpu = "1.2.643.5.1.13.3.25.78.118",
                      IdCaseResult = 1,
                      Comment = "КОММЕНТ",
                      IdPatientMis = patient.IdPatientMIS,
                      IdCaseType = 2,
                      DoctorInCharge = new MedicalStaff
                      {
                          IdSpeciality = 29,
                          IdPosition = 72,
                          Person = new PersonWithIdentity
                          {
                              IdPersonMis = "123123123",
                              HumanName = new HumanName
                              {
                                  FamilyName = "Лукин",
                                  GivenName = "Василий"
                              }
                          }
                      },
                      Authenticator = new Participant
                      {
                          Doctor = new MedicalStaff
                          {
                              IdSpeciality = 29,
                              IdPosition = 72,
                              Person = new PersonWithIdentity
                              {
                                  IdPersonMis = "123123123",
                                  HumanName = new HumanName
                                  {
                                      FamilyName = "Лукин",
                                      GivenName = "Василий"
                                  }
                              }
                          }
                      },
                      Author = new Participant
                      {
                          Doctor = new MedicalStaff
                          {
                              IdSpeciality = 29,
                              IdPosition = 72,
                              Person = new PersonWithIdentity
                              {
                                  IdPersonMis = "123123123",
                                  HumanName = new HumanName
                                  {
                                      FamilyName = "Лукин",
                                      GivenName = "Василий"
                                  }
                              }
                          }
                      },
                      Steps = new StepAmb[]
                      {
                          new StepAmb
                          {
                              IdStepMis = "12341234",
                              DateStart = new DateTime(2010, 10, 10),
                              DateEnd = new DateTime(2010, 10, 14),
                              IdVisitPlace = 1,
                              IdVisitPurpose = 1,
                              IdPaymentType = 1,
                              Doctor = new MedicalStaff
                              {
                                  IdSpeciality = 29,
                                  IdPosition = 72,
                                  Person = new PersonWithIdentity
                                  {
                                      IdPersonMis = "123123123",
                                      HumanName = new HumanName
                                      {
                                          FamilyName = "Лукин",
                                          GivenName = "Василий"
                                      }
                                  }
                              }
                          }
                      }
                  });
                  if (Global.errors == "")
                      Assert.Pass();
                  else
                      Assert.Fail(Global.errors);
              }
       
         [Test]
          public void EMKAddMinStatCase()
          {
              TestPixServiceClient c = new TestPixServiceClient();
              PatientDto patient = new PatientDto();
              patient.FamilyName = "Жукин";
              patient.GivenName = "Дмитрий";
              patient.BirthDate = new DateTime(1983, 01, 07);
              patient.Sex = 1;
              patient.IdPatientMIS = "12930193123";
              c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
              TestEmkServiceClient client = new TestEmkServiceClient();
              CaseStat ca = new CaseStat();
              ca.OpenDate = new DateTime(2013, 04, 24);
              ca.CloseDate = new DateTime(2013, 04, 27);
              ca.HistoryNumber = "19344";
              ca.IdCaseMis = "130059240420132617_1I";
              ca.IdCaseAidType = 3;
              ca.IdPaymentType = 1;
              ca.Confidentiality = 1;
              ca.CuratorConfidentiality = 1;
              ca.DoctorConfidentiality = 1;
              ca.IdLpu = "1.2.643.5.1.13.3.25.78.118";
              ca.IdCaseResult = 6;
              ca.Comment = ".";
              ca.DoctorInCharge = new MedicalStaff
              {
                  IdSpeciality = 29,
                  IdPosition = 72,
                  Person = new PersonWithIdentity
                  {
                      IdPersonMis = "123123123",
                      HumanName = new HumanName
                      {
                          FamilyName = "Лукин",
                          GivenName = "Василий"
                      }
                  }
              };
              ca.Authenticator = new Participant
              {
                  Doctor = new MedicalStaff
                  {
                      IdSpeciality = 29,
                      IdPosition = 72,
                      Person = new PersonWithIdentity
                      {
                          IdPersonMis = "123123123",
                          HumanName = new HumanName
                          {
                              FamilyName = "Лукин",
                              GivenName = "Василий"
                          }
                      }
                  }
              };
              ca.Author = new Participant
              {
                  Doctor = new MedicalStaff
                  {
                      IdSpeciality = 29,
                      IdPosition = 72,
                      Person = new PersonWithIdentity
                      {
                          IdPersonMis = "123123123",
                          HumanName = new HumanName
                          {
                              FamilyName = "Лукин",
                              GivenName = "Василий"
                          }
                      }
                  }
              };
              ca.IdPatientMis = patient.IdPatientMIS;
              ca.IdPatientConditionOnAdmission = 2;
              ca.IdTypeFromDiseaseStart = 3;
              ca.IdRepetition = 1;
              ca.HospitalizationOrder = 2;
              ca.Steps = new StepStat[]
              {
                  new StepStat
                  {
                      DateStart = new DateTime(2013, 04, 24),
                      DateEnd = new DateTime(2013, 04, 27),
                      Comment = "Поступление. Выбытие. Клиническое отделение",
                      IdPaymentType = 1,
                      IdStepMis = "130059240420132618F",
                      HospitalDepartmentName = "11 6 ХИРУРГИЧЕСКОЕ",
                      IdHospitalDepartment = "0028",
                      WardNumber = ".",
                      BedNumber = ".",
                      BedProfile = 18,
                      DaySpend = 3,
                      Doctor = new MedicalStaff
                      {
                          IdSpeciality = 29,
                          IdPosition = 72,
                          Person = new PersonWithIdentity
                          {
                              IdPersonMis = "123123123",
                              HumanName = new HumanName
                              {
                                  FamilyName = "Лукин",
                                  GivenName = "Василий"
                              }
                          }
                      },
                  }
              };
              ca.HospResult = 1;
              ca.IdHospChannel = 2;
              ca.MedRecords = new MedRecord[]
              {
                  new TfomsInfo
                  {
                      Count = 5,
                      IdTfomsType = 291170,
                      Tariff = 712.11M
                  },
                  new SickList
                          {
                              CreationDate = new DateTime(2013, 04, 24),
                              Header = "Я ХЭДЭР",
                              Author = new MedicalStaff
                              {
                                  IdSpeciality = 29,
                                  IdPosition = 72,
                                  Person = new PersonWithIdentity
                                  {
                                      IdPersonMis = "123123123",
                                      HumanName = new HumanName
                                      {
                                          FamilyName = "Лукин",
                                          GivenName = "Василий"
                                      }
                                  }
                              },
                              SickListInfo = new SickListInfo
                              {
                                  Number = "123",
                                  DateStart = new DateTime(2013, 04, 24),
                                  DateEnd = new DateTime(2013, 04, 27)
                              }
                          }
              };
              client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", ca);
              if (Global.errors == "")
                  Assert.Pass();
              else
                  Assert.Fail(Global.errors);
          }
         */

        [Test]
        public void AddMinAmbCase()
        {
            TestPixServiceClient c = new TestPixServiceClient();
            PatientDto patient = new PatientDto();
            patient.FamilyName = "Жукин";
            patient.GivenName = "Дмитрий";
            patient.BirthDate = new DateTime(1983, 01, 07);
            patient.Sex = 1;
            patient.IdPatientMIS = "123456789010";
            c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            TestEmkServiceClient client = new TestEmkServiceClient();
            client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", new CaseAmb
            {
                OpenDate = new DateTime(2010, 10, 10),
                CloseDate = new DateTime(2010, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "123412341234",
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = "1.2.643.5.1.13.3.25.78.118",
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = patient.IdPatientMIS,
                IdCaseType = 2,
                DoctorInCharge = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий"
                        }
                    }
                },
                Authenticator = new Participant
                {
                    Doctor = new MedicalStaff
                    {
                        IdSpeciality = 29,
                        IdPosition = 72,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = "123123123",
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий"
                            }
                        }
                    }
                },
                Author = new Participant
                {
                    Doctor = new MedicalStaff
                    {
                        IdSpeciality = 29,
                        IdPosition = 72,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = "123123123",
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий"
                            }
                        }
                    }
                },
                Steps = new StepAmb[]
                    {
                        new StepAmb
                        {
                            DateStart = new DateTime(2010, 10, 10),
                            DateEnd = new DateTime(2010, 10, 14),
                            IdStepMis = "12341234",
                            IdPaymentType = 1,
                            IdVisitPlace = 1,
                            IdVisitPurpose = 1,
                            Doctor = new MedicalStaff
                            {
                                IdSpeciality = 29,
                                IdPosition = 72,
                                Person = new PersonWithIdentity
                                {
                                    IdPersonMis = "123123123",
                                    HumanName = new HumanName
                                    {
                                        FamilyName = "Лукин",
                                        GivenName = "Василий"
                                    }
                                }
                            }
                        }
                    }
            });
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinStatCase()
        {
            TestPixServiceClient c = new TestPixServiceClient();
            PatientDto patient = new PatientDto();
            patient.FamilyName = "Жукин";
            patient.GivenName = "Дмитрий";
            patient.BirthDate = new DateTime(1983, 01, 07);
            patient.Sex = 1;
            patient.IdPatientMIS = "12930193123";
            c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            TestEmkServiceClient client = new TestEmkServiceClient();
            CaseStat ca = new CaseStat();
            ca.OpenDate = new DateTime(2013, 04, 24);
            ca.CloseDate = new DateTime(2013, 04, 27);
            ca.HistoryNumber = "19344";
            ca.IdCaseMis = "130059240420132617_1I";
            ca.IdPaymentType = 1;
            ca.Confidentiality = 1;
            ca.DoctorConfidentiality = 1;
            ca.CuratorConfidentiality = 1;
            ca.IdLpu = "1.2.643.5.1.13.3.25.78.118";
            ca.IdCaseResult = 6;
            ca.Comment = ".";
            ca.IdPatientMis = patient.IdPatientMIS;
            ca.IdTypeFromDiseaseStart = 1;
            ca.IdRepetition = 1;
            ca.HospResult = 1;
            ca.IdHospChannel = 2;
            ca.DoctorInCharge = new MedicalStaff
            {
                IdSpeciality = 29,
                IdPosition = 72,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "123123123",
                    HumanName = new HumanName
                    {
                        FamilyName = "Лукин",
                        GivenName = "Василий"
                    }
                }
            };
            ca.Authenticator = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий"
                        }
                    }
                }
            };
            ca.Author = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий"
                        }
                    }
                }
            };

            ca.Steps = new StepStat[]
            {
                new StepStat
                {
                    DateStart = new DateTime(2013, 04, 24),
                    DateEnd = new DateTime(2013, 04, 27),
                    IdStepMis = "130059240420132618F",
                    IdPaymentType = 1,
                    HospitalDepartmentName = "11 6 ХИРУРГИЧЕСКОЕ",
                    IdHospitalDepartment = "0028",
                    BedProfile = 18,
                    DaySpend = 3,
                    Doctor = new MedicalStaff
                    {
                        IdSpeciality = 29,
                        IdPosition = 72,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = "123123123",
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий"
                            }
                        }
                    },
                }
            };
            client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", ca);
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [TearDown]
        public void Clear()
        {
            Global.errors3.Clear();
            Global.errors2.Clear();
            Global.errors1.Clear();
        }
    }
}