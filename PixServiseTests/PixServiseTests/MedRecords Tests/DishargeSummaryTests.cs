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
    class DishargeSummaryTests : Data
    {
        [Test]
        public void AddAmbCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase(Data.globalGuid, caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase(Data.globalGuid, caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase(Data.globalGuid, caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase(Data.globalGuid, caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase(Data.globalGuid, caseAmb);
                caseAmb = (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase(Data.globalGuid, caseAmb);
                caseAmb = (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseWithMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase(Data.globalGuid, caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseWithMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase(Data.globalGuid, caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                MedRecord r = (new SetData()).MinDischargeSummary();
                client.AddCase(Data.globalGuid, caseAmb);
                client.AddMedRecord(Data.globalGuid, caseAmb.IdLpu, caseAmb.IdPatientMis, r, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMaxDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                MedRecord r = MedRecordData.dischargeSummary;
                client.AddCase(Data.globalGuid, caseAmb);
                client.AddMedRecord(Data.globalGuid, caseAmb.IdLpu, caseAmb.IdPatientMis, r, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
