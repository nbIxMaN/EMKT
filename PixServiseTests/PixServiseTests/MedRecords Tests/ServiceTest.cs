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
    public class ServiceTest : Data
    {
        //--------------------------- AmbCase

        [Test]
        public void AddAmbCaseWithServiceFull()
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
                    MedRecordData.service,
                    (new SetData()).MinClinicMainDiagnosis()
                };

                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()

                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.AddCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void CreateAmbCaseWithServiceFull()
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
                     MedRecordData.service
                 };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()

                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.CreateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseWithServiceMin()
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
                     (new SetData()).MinService()
                 };
                client.CreateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithServiceFull()
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
                    MedRecordData.service,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()

                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.UpdateCase(Data.globalGuid, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void CloseAmbCaseWithServiceFull()
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
                    MedRecordData.service,
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
        public void CloseAmbCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void AddStepToCaseAmbCaseWithServiceFull()
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
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service
                };
                client.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseAmbCaseWithServiceMin()
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
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService()
                };
                client.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


        [Test]
        public void AddMedRecord_ToCase_AmbCaseWithServicetMin()
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

                MedRecord medRecord = (new SetData()).MinService();

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, medRecord, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMedRecord_ToCase_AmbCaseWithServiceFull()
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

                MedRecord medRecord = MedRecordData.service;

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, medRecord, caseAmb.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //--------------------------- StatCase
        [Test]
        public void AddStatCaseWithServiceFull()
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
                    MedRecordData.service,
                    (new SetData()).MinClinicMainDiagnosis()
                };

                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()

                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.AddCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void CreateStatCaseWithServiceFull()
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
                     MedRecordData.service
                 };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()

                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.CreateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseWithServiceMin()
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
                     (new SetData()).MinService()
                 };

                client.CreateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithServiceFull()
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
                    MedRecordData.service,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    (new SetData()).MinService()
                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.UpdateCase(Data.globalGuid, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void CloseStatCaseWithServiceFull()
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
                    MedRecordData.service,
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
        public void CloseStatCaseWithServiceMin()
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
                    (new SetData()).MinService(),
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
        public void AddStepToCaseStatCaseWithServiceFull()
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
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service
                };
                client.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseStatCaseWithServiceMin()
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
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService()
                };
                client.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMedRecord_ToCase_StatCaseWithServiceMin()
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

                MedRecord medRecord = (new SetData()).MinService();

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, medRecord, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMedRecord_ToCase_StatCaseWithServiceFull()
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

                MedRecord medRecord = MedRecordData.service;

                client.AddMedRecord(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, medRecord, caseStat.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
