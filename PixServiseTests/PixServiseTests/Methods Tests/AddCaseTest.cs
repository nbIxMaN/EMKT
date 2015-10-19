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
    public class AddCaseTest : Data
    {
        [Test]
        public void AddMinAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                //caseStat.PrehospitalDefects = new List<byte>() { 1, 2 };
               
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void _AddCaseForMiac()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                patient.IdPatientMIS = "56c46538-cff7-4991-9777-2cc7eca05f21";
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.186", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.IdLpu = "1.2.643.5.1.13.3.25.78.186";
                caseStat.IdPatientMis = "56c46538-cff7-4991-9777-2cc7eca05f21";
                caseStat.IdCaseMis = "158903";
                caseStat.OpenDate = new DateTime(2015, 10, 12);
                caseStat.CloseDate = new DateTime(2015, 10, 13);
                caseStat.HistoryNumber = "23030";
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                caseStat.Steps = new StepStat[]
                {(new SetData()).MinStepStatSet()};
                caseStat.Steps[0].MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };
                EmkClient.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);

            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinDispCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseDispSet();
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).FullCaseAmbSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    set.MinClinicMainDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.diagnosis,
                    set.MinRefferal(),
                    set.MinSickList(),
                    set.MinDischargeSummary(),
                    set.MinLaboratoryReport(),
                    set.MinConsultNote()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReport(),
                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).FullCaseStatSet();
                SetData set = new SetData();
                caseStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    MedRecordData.deathInfo,
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    MedRecordData.referral,   
                    MedRecordData.sickList,
                    set.MinDischargeSummary(),
                    set.MinLaboratoryReport(),
                    set.MinConsultNote()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReport(),
                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullDispCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).FullCaseDispSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    set.MinDispensaryOne(),
                    set.MinLaboratoryReport(),
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReport(),
                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
