using System;
using NUnit.Framework;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;
using System.Collections.Generic;


namespace PixServiseTests
{
    [TestFixture]
    public class PatientTests : Data
    {
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
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
            client.UpdatePatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
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
            client.UpdatePatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient2);
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
            PatientDto patient = (new SetData()).PatientSet();
            PatientDto forSearch = new PatientDto();
            forSearch.FamilyName = PatientData.Patient.FamilyName;
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
            var patents = client.GetPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Fed);
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
            PatientDto patient = (new SetData()).PatientSet();
            PatientDto forSearch = new PatientDto();
            forSearch.FamilyName = PatientData.Patient.GivenName;
            forSearch.GivenName = PatientData.Patient.FamilyName;
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
            var patents = client.GetPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Fed);
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
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
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
            client.GetPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", forSearch, SourceType.Reg);
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void FindPatientByIdMIS()
        {
            TestPixServiceClient client = new TestPixServiceClient();
            PatientDto patient = (new SetData()).PatientSet();
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", patient);
            PatientDto find = new PatientDto();
            find.IdPatientMIS = patient.IdPatientMIS;
            client.GetPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.118", find, SourceType.Reg);
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
            client.AddPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.230", patient);
            client.UpdatePatient(Global.GUID, "1.2.643.5.1.13.3.25.78.230", patient);
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
            client.GetPatient(Global.GUID, "1.2.643.5.1.13.3.25.78.230", patient2, SourceType.Reg);
        }
        [Test]
        public void AddStatMedRecDeathInfo_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase(Global.GUID, caseStat);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseStat.IdPatientMis, MedRecordData.deathInfo, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddStatMedRecSickList_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase(Global.GUID, caseStat);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseStat.IdPatientMis, MedRecordData.sickList, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedAppointedMedication_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.appointedMedication, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbMedRecClinicDiagnosis_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.clinicMainDiagnosis, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecConsultNote_OpenCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.consultNote, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecDiagnosis_OpenCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.diagnosis, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecDishargeSummary_OpenCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.dischargeSummary, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecLabRep_OpenCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.LaboratoryReport, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecReferral_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.referral, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddAmbMedRecTfomfsInfo_OpenCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseAmb);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, MedRecordData.tfomsInfo, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddStatMedAppointedMedication_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase(Global.GUID, caseStat);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseStat.IdPatientMis, MedRecordData.appointedMedication, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddStatMedRecAnatomDiagnosis_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase(Global.GUID, caseStat);
                EmkClient.AddMedRecord(Global.GUID, Data.idlpu, caseStat.IdPatientMis, MedRecordData.anatomopathologicalClinicMainDiagnosis, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddDeathInfo()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.IdCaseResult = 6;
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    MedRecordData.deathInfo
                };
                EmkClient.AddCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
