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
    class AddStepToCaseTest: Data
    {
        [Test]
        public void AddAmbStep_CloseCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase(Data.globalGuid, caseAmb);

                StepAmb stepAmb = (new SetData()).MinStepAmbSet();

                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            Assert.IsTrue(Global.errors1.Contains(" - Случай обслуживания закрыт"), "Случай обслуживания был добавлен");
        }

        //------------------------------AmbStep
        [Test]
        public void AddAmbStep_OtherIdMin()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseAmb);

                StepAmb stepAmb = (new SetData()).MinOtherStepAmbSet();
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbStep_OtherIdFull()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseAmb);

                StepAmb stepAmb = CaseAmbData.otherStep;            
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinAppointedMedication(),
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    (new SetData()).MinLaboratoryReport()
                };
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbStep_SameIdMin()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseAmb);

                StepAmb stepAmb = (new SetData()).MinOtherStepAmbSet();
                stepAmb.IdStepMis = CaseAmbData.step.IdStepMis;
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbStep_SameIdFull()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseAmb);

                StepAmb stepAmb = CaseAmbData.otherStep;
                stepAmb.IdStepMis = CaseAmbData.step.IdStepMis;
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinAppointedMedication(),
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    (new SetData()).MinLaboratoryReport()
                };
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }



        //------------------------------StatStep
        [Test]
        public void AddStatStep_OtherIdMin()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseStat);

                StepStat stepStat = (new SetData()).MinOtherStepStatSet();
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatStep_OtherIdFull()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseStat);

                StepStat stepStat = CaseStatData.otherStep;
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinAppointedMedication(),
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    (new SetData()).MinLaboratoryReport()
                };
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatStep_SameIdMin()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseStat);

                StepStat stepStat = (new SetData()).MinOtherStepStatSet();
                stepStat.IdStepMis = CaseStatData.step.IdStepMis;
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatStep_SameIdFull()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Data.globalGuid, caseStat);

                StepStat stepStat = CaseStatData.otherStep;
                stepStat.IdStepMis = CaseStatData.step.IdStepMis;
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinService(),
                    (new SetData()).MinAppointedMedication(),
                    (new SetData()).MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    (new SetData()).MinLaboratoryReport()
                };
                EmkClient.AddStepToCase(Data.globalGuid, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
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
