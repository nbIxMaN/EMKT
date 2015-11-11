using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.EMKServise;
using System.Collections.Generic;

namespace PixServiseTests.MedRecords_Tests
{
    [TestFixture]
    public class SickListTest: Data
    {
        //--------------------------- AmbCase
        [Test]
        public void AddAmbCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList()
                };
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList
                };
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb = (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
                caseAmb= (new SetData()).MinCaseAmbSetForClose();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //--------------------------- StatCase
        [Test]
        public void AddStatCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList()
                };
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList
                };
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseMinSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinSickList(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseFullSick()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat = (new SetData()).MinCaseStatSetForClose();
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.sickList,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

    }
}
