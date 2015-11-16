using System;
using NUnit.Framework;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;
using System.Collections.Generic;

namespace PixServiseTests.Methods_Tests
{
    [TestFixture]
    public class CloseCaseTest : Data
    {
        ///-----------------------------AmbCase
        [Test]
        public void CloseAmbCaseMin()
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
                
                CaseAmb CaseAmbClose = (new SetData()).MinCaseAmbSetForClose();
                EmkClient.CloseCase(Global.GUID, CaseAmbClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseFull()
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
               
                CaseAmb CaseAmbClose = (new SetData()).FullCaseAmbSetForClose();
                CaseAmbClose.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinTfomsInfo(),
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinLaboratoryReport(),
                    (new SetData()).MinConsultNote()
                };

                EmkClient.CloseCase(Global.GUID, CaseAmbClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        ///-----------------------------StatCase
        [Test]
        public void CloseStatCaseMin()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseStat);
               
                CaseStat CaseStatClose = (new SetData()).MinCaseStatSetForClose();
                EmkClient.CloseCase(Global.GUID, CaseStatClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseFull()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseStat);
               
                CaseStat CaseStatClose = (new SetData()).FullCaseStatSetForClose();
                //CaseStatClose.Guardian = null;
                CaseStatClose.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinTfomsInfo(),
                    MedRecordData.deathInfo,
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinLaboratoryReport(),
                    (new SetData()).MinConsultNote()
                };
                EmkClient.CloseCase(Global.GUID, CaseStatClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
