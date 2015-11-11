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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb = (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb = (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDischargeSummary(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                MedRecord r = (new SetData()).MinDischargeSummary();
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                client.AddMedRecord("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb.IdLpu, caseAmb.IdPatientMis, r, caseAmb.IdCaseMis);
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
                c.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                MedRecord r = MedRecordData.dischargeSummary;
                client.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                client.AddMedRecord("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb.IdLpu, caseAmb.IdPatientMis, r, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
