using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.EMKServise;
using System.Collections.Generic;

namespace PixServiseTests
{
    [TestFixture]
    public class UnitTest1 : Data
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
            patient.Documents = new PixServise.DocumentDto[]
            {
                new PixServise.DocumentDto()
                {
                    DocN = "123-123-123-12",
                    ProviderName = "ПФР",
                    IdDocumentType = 223
                }
            };
            client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            client.UpdatePatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
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
            patient.Documents = (new SetData()).PatientSet().Documents;
            client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            //patient.FamilyName = "Сидоров";
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
            client.UpdatePatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
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
        //[Test]
        //public void AddNewMedicalCase()
        //{
        //    TestPixServiceClient client = new TestPixServiceClient();
        //    PatientDto patient = new PatientDto();
        //    patient.FamilyName = "Легенда";
        //    patient.GivenName = "Легенда";
        //    patient.BirthDate = new DateTime(1983, 01, 07);
        //    patient.Sex = 1;
        //    patient.IdPatientMIS = "1123123123";
        //    PixServise.DocumentDto document = new PixServise.DocumentDto();
        //    document.IdDocumentType = 14;
        //    document.DocS = "1311";
        //    document.DocN = "113131";
        //    document.ProviderName = "УФМС";
        //    patient.Documents = new PixServise.DocumentDto[] { document };
        //    client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
        //    PatientDto patient2 = new PatientDto();
        //    patient2.FamilyName = "Легенда";
        //    patient2.GivenName = "Легенда";
        //    patient2.BirthDate = new DateTime(1983, 01, 07);
        //    patient2.Sex = 1;
        //    patient2.IdPatientMIS = "1234567890987";
        //    PixServise.DocumentDto document2 = new PixServise.DocumentDto();
        //    document2.IdDocumentType = 14;
        //    document2.DocS = "1311";
        //    document2.DocN = "113131";
        //    document2.ProviderName = "УФМС";
        //    patient2.Documents = new PixServise.DocumentDto[] { document2 };
        //    client.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.29.1", patient2);
        //    TestPatient a = TestPatient.BuildPatientFromDataBaseData("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient.IdPatientMIS);
        //    TestPatient b = TestPatient.BuildPatientFromDataBaseData("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.29.1", patient2.IdPatientMIS);
        //    if (a.patient.IdGlobal != b.patient.IdGlobal)
        //        Global.errors1.Add("Создан новый пациент");
        //    if (Global.errors == "")
        //        Assert.Pass();
        //    else
        //        Assert.Fail(Global.errors);
        //}

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
            job.OgrnCode = "0100000000000"; // некорректный код
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
        [Test]
        public void MiacTest()
        {
            TestMiacClient c = new TestMiacClient();
            c.GetCasesByPeriod("5C04E58B-07C0-421C-804A-CD774685AEA2", new DateTime(2014, 06, 01), new DateTime(2014, 06, 10));
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        //[Test]
        //public void AddMinAmbCase()
        //{
        //    using (TestPixServiceClient PixClient = new TestPixServiceClient())
        //    {
        //        PatientDto patient = (new SetData()).PatientSet();
        //        PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
        //    }
        //    using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
        //    {
        //        CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
        //        SetData set = new SetData();
        //        caseAmb.MedRecords = new MedRecord[]
        //        {
        //            set.MinClinicMainDiagnosis(),
        //            MedRecordData.referral,
        //            MedRecordData.referral,
        //            MedRecordData.referral,
        //            MedRecordData.referral,
        //            MedRecordData.referral,
        //            MedRecordData.referral,
        //            MedRecordData.referral
        //        };
        //        EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
        //        EmkClient.GetReferralList("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb.IdLpu, 1, new DateTime(2015, 08, 18), new DateTime(2015, 08, 20));

        //    }
        //    if (Global.errors == "")
        //        Assert.Pass();
        //    else
        //        Assert.Fail(Global.errors);
        //}
        //[Test]
        //public void aaaa()
        //{
        //    TestEmkServiceClient c = new TestEmkServiceClient();
        //    c.GetReferralList("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", 1,
        //        new DateTime(2010, 02, 01), new DateTime(2010, 02, 03));
        //}
        private LaboratoryReport GetOnkomarkers()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 04),
                Attachment = Data.SetAttachment("Ифа.Онкомаркеры.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Онкомаркеры"
            };
        }
        private LaboratoryReport GetGemotology()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 02),
                Attachment = Data.SetAttachment("Гематология.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Гематология"
            };
        }
        private LaboratoryReport GetBlood()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 05),
                Attachment = Data.SetAttachment("Общийанализкрови.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Общий анализ крови"
            };
        }
        private DischargeSummary GetEpic()
        {
            return new DischargeSummary()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 03),
                Attachment = Data.SetAttachment("эпикриз.html", null, "text/html"),
                Header = "Эпикриз"
            };
        }

        private ConsultNote GetConsult()
        {
            return new ConsultNote()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 08),
                Attachment = Data.SetAttachment("консультация.pdf", null, "application/pdf"),
                Header = "Консультация"
            };
        }

        [Test]
        public void TTT()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto p = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.3.64", p);
            }
            using (TestEmkServiceClient c = new TestEmkServiceClient())
            {
                CaseAmb Case = (new SetData()).MinCaseAmbSet();
                Case.IdLpu = "1.2.643.5.1.13.3.25.3.64";
                c.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", Case);
            }
        }
        [Test]
        public void _AddCaseForFedUploader()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                patient.MiddleName = "Викторович";
                patient.GivenName = "Евгений";
                patient.FamilyName = "Эторцев";
                patient.IdPatientMIS = "ForUploaderMIS4";
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.3.64", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.IdLpu = "1.2.643.5.1.13.3.25.3.64";
                caseAmb.IdPatientMis = "ForUploaderMIS4";
                caseAmb.Authenticator.Doctor = new MedicalStaff()
                {
                    Person = new PersonWithIdentity()
                    {
                        IdPersonMis = "DoctorForUpload2",
                        Birthdate = new DateTime(1987, 06, 11),
                        HumanName = new HumanName()
                        {
                            FamilyName = "Александров",
                            MiddleName = "Игнатьевич",
                            GivenName = "Борис"
                        },
                        Sex = 1
                    },
                    IdPosition = 74,
                    IdSpeciality = 29
                };
                caseAmb.Author = caseAmb.Authenticator;
                caseAmb.DoctorInCharge = caseAmb.Authenticator.Doctor;
                var x = MedRecordData.clinicMainDiagnosis;
                x.Doctor = caseAmb.DoctorInCharge;
                caseAmb.MedRecords = new List<MedRecord>
                {
                    //set.MinService(),
                    //set.MinTfomsInfo(),
                    //set.MinDiagnosis(),
                    //set.MinClinicMainDiagnosis(),
                    x,
                    //MedRecordData.diagnosis
                    //set.MinRefferal(),
                    //set.MinSickList(),
                    //set.MinDischargeSummary(),
                    //set.MinLaboratoryReport(),
                    //set.MinConsultNote()
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void _HashTest()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto p = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", p);
            }
            using (TestEmkServiceClient c = new TestEmkServiceClient())
            {
                CaseAmb p = (new SetData()).FullCaseAmbSet();
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                var x = GetConsult();
                x.Attachment.Hash = new byte[] { 1, 2, 3, 4, 5 };
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    //GetEpic(),
                    //GetConsult()
                    x
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers(),
                    MedRecordData.appointedMedication,
                    MedRecordData.service
                };
                c.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
                string caseMis = p.IdCaseMis;
                p = (new SetData()).FullCaseAmbSet();
                p.IdCaseMis = caseMis;
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    //GetConsult()
                    x
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers()
                };
                c.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void _AddAmbCase()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto p = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", p);
            }
            using (TestEmkServiceClient c = new TestEmkServiceClient())
            {
                CaseAmb p = (new SetData()).FullCaseAmbSet();
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    //GetEpic(),
                    GetConsult()
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers(),
                    MedRecordData.appointedMedication,
                    MedRecordData.service
                };
                c.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
                string caseMis = p.IdCaseMis;
                p = (new SetData()).FullCaseAmbSet();
                p.IdCaseMis = caseMis;
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    GetConsult()
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers()
                };
                c.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void _UpdateAmbCase()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto p = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", p);
            }
            using (TestEmkServiceClient c = new TestEmkServiceClient())
            {
                CaseAmb p = (new SetData()).FullCaseAmbSet();
                p.Guardian = null;
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis
                };
                c.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
                string caseMis = p.IdCaseMis;
                p = (new SetData()).FullCaseAmbSet();
                p.IdCaseMis = caseMis;
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    GetConsult()
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers()
                };
                c.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void _CreateStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).FullCaseStatSetForCreate();
                caseStat.Guardian = null;
                caseStat.OpenDate = new DateTime(2014, 06, 01);
                SetData set = new SetData();
                StepStat stepStat = (new SetData()).MinStepStatSet();
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                caseStat.Steps[0].DateStart = new DateTime(2014, 06, 01);
                caseStat.Steps[0].DateEnd = new DateTime(2014, 06, 04);
                EmkClient.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                StepStat s = CaseStatData.otherStep;
                s.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication,
                    MedRecordData.service
                };
                s.DateStart = new DateTime(2014, 06, 05);
                s.DateEnd = new DateTime(2014, 06, 10);
                EmkClient.AddStepToCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat.IdLpu, caseStat.IdPatientMis, caseStat.IdCaseMis, s);
                caseStat = (new SetData()).FullCaseStatSetForClose();
                caseStat.Guardian = null;
                caseStat.CloseDate = new DateTime(2014, 06, 10);
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    GetEpic()
                };
                EmkClient.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        private DispensaryOne GetDispanseryOne()
        {
            var d = MedRecordData.dispensaryOne;
            d.Attachment = null;
            d.CreationDate = new DateTime(2014, 06, 04);
            return d;
        }

        [Test]
        public void AddDicpCase()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).FullCaseDispSet();
                caseDisp.Guardian = null;
                caseDisp.OpenDate = new DateTime(2014, 06, 01);
                caseDisp.CloseDate = new DateTime(2014, 06, 10);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    GetDispanseryOne(),
                    MedRecordData.clinicMainDiagnosis
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void TestDoc()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).FullCaseDispSetWithDoctorWithoutSNILS();
                caseDisp.Guardian = null;
                caseDisp.OpenDate = new DateTime(2014, 06, 01);
                caseDisp.CloseDate = new DateTime(2014, 06, 10);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOneWithoutSnils,
                    MedRecordData.clinicMainDiagnosisWithOutSnils
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp = (new SetData()).FullCaseDispSetWithDoctorWithSNILS();
                caseDisp.Guardian = null;
                caseDisp.OpenDate = new DateTime(2014, 06, 01);
                caseDisp.CloseDate = new DateTime(2014, 06, 10);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOneWithSnils,
                    MedRecordData.clinicMainDiagnosisWithSnils
                };
                caseDisp.IdCaseMis = System.DateTime.Now.ToString();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
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