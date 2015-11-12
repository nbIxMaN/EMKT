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
    public class TfomsInfoTest: Data
    {
        //--------------------------- AmbCase

        [Test]
        public void AddAmbCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void AddAmbCaseWithTfomsInfotMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void CreateAmbCaseWithTfomsInfoFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                 {
                     MedRecordData.tfomsInfo
                 };

                client.CreateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseWithTfomsInfoMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                 {
                     (new SetData()).MinTfomsInfo()
                 };

                client.CreateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void UpdateAmbCaseWithTfomsInfoMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void CloseAmbCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void CloseAmbCaseWithTfomsInfoMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void AddMedRecord_ToCase_AmbCaseWithTfomsInfoMin()
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

                MedRecord medRecord = (new SetData()).MinTfomsInfo();

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, medRecord, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMedRecord_ToCase_AmbCaseWithTfomsInfoFull()
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

                MedRecord medRecord = MedRecordData.tfomsInfo;

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, medRecord, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //--------------------------- StatCase
        [Test]
        public void AddStatCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void AddStatCaseWithTfomsInfoMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void CreateStatCaseWithTfomsInfoFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                 {
                     MedRecordData.tfomsInfo
                 };
                client.CreateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseWithTfomsInfoMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                 {
                     (new SetData()).MinTfomsInfo()
                 };
                client.CreateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void UpdateStatCaseWithTfomsInfoMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void CloseStatCaseWithTfomsInfoFull()
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
                    MedRecordData.tfomsInfo,
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
        public void CloseStatCaseWithTfomsInfoMin()
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
                    (new SetData()).MinTfomsInfo(),
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
        public void AddMedRecord_ToCase_StatCaseWithTfomsInfoMin()
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

                MedRecord medRecord = (new SetData()).MinTfomsInfo();

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, medRecord, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


        [Test]
        public void AddMedRecord_ToCase_StatCaseWithTfomsInfoFull()
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

                MedRecord medRecord = MedRecordData.tfomsInfo;

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, medRecord, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
