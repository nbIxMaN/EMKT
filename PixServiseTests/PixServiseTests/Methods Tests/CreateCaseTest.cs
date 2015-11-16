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
    class CreateCaseTest: Data
    {
        [Test]
        public void CreateMinAmbCase()
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
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                EmkClient.CreateCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateFullAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).FullCaseAmbSetForCreate();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
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
                EmkClient.CreateCase(Global.GUID, caseAmb);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateFullStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).FullCaseStatSetForCreate();
                SetData set = new SetData();
                caseStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,   
                    MedRecordData.sickList,
                    set.MinLaboratoryReport()
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
                EmkClient.CreateCase(Global.GUID, caseStat);
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
