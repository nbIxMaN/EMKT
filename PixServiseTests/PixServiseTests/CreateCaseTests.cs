﻿using System;
using NUnit.Framework;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    [TestFixture]
    class CreateCaseTests : Data
    {
        [Test]
        public void CreateMinAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                EmkClient.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateMinStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                caseAmb.MedRecords = new MedRecord[]
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
                    set.MinDischargeSummary(),
                    set.MinLaboratoryReportSet(),
                    set.MinConsultNote()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReportSet(),
                };
                caseAmb.Steps = new StepAmb[]
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
                caseStat.MedRecords = new MedRecord[]
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
                    set.MinLaboratoryReportSet(),
                    set.MinConsultNote()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReportSet(),
                };
                caseStat.Steps = new StepStat[]
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

        [TearDown]
        public void Clear()
        {
            Global.errors3.Clear();
            Global.errors2.Clear();
            Global.errors1.Clear();
        }
    }
}
