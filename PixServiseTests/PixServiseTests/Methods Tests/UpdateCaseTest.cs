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
    public class UpdateCaseTest : Data
    {
        //-----------------------------After AddCase
        [Test]
        public void UpdateMinAmbCase_AfterAdd()
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
                caseAmb.Comment = "23123123123123123123";
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateFullAmbCase_AfterAdd()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmbAdd = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmbAdd);
                CaseAmb caseAmb = (new SetData()).FullCaseAmbSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    MedRecordData.tfomsInfo,
                    MedRecordData.diagnosis,
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
                    MedRecordData.dischargeSummary,
                    MedRecordData.LaboratoryReport,
                    MedRecordData.consultNote
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                //stepAmb.MedRecords = new List<MedRecord>
                //{
                //    set.MinService(),
                //    set.MinAppointedMedication(),
                //    set.MinDiagnosis(),
                //    MedRecordData.clinicMainDiagnosis,
                //    MedRecordData.referral,
                //    set.MinLaboratoryReport(),
                //};
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };

                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateMinStatCase_AfterAdd()
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
                caseStat = (new SetData()).MinCaseStatSet();
                caseStat.DoctorInCharge = new MedicalStaff
                {
                    IdLpu = Data.idlpu,
                    IdSpeciality = 31,
                    IdPosition = 76,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "unknown1",
                        Sex = 2,
                        Birthdate = new DateTime(1976, 03, 09),
                        Documents = new List<IdentityDocument>
                    {
                        DocumentData.SNILS,
                    },
                        HumanName = new HumanName
                        {
                            FamilyName = "unknown1",
                            GivenName = "unknown1",
                            MiddleName = "unknown1",
                        },
                    }
                };
                caseStat.DoctorInCharge.Person.Documents[0].DocN = "11111111549";
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateFullStatCase_AfterAdd()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("5c04e58b-07c0-421c-804a-cd774685aea2", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStatAdd = (new SetData()).MinCaseStatSet();
                EmkClient.AddCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStatAdd);
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
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //-----------------------------After CloseCase
        [Test]
        public void UpdateMinAmbCase_AfterClose()
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
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);

                caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateFullAmbCase_AfterClose()
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
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmb);

                CaseAmb caseAmbUpdate = (new SetData()).FullCaseAmbSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
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
                    set.MinRefferal(),
                    set.MinLaboratoryReport(),
                };
                caseAmbUpdate.Steps = new List<StepAmb>
                {
                    stepAmb
                };

                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseAmbUpdate);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateMinStatCase_AfterClose()
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
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
                caseStat = (new SetData()).MinCaseStatSet();
                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateFullStatCase_AfterClose()
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
                EmkClient.CloseCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStat);

                CaseStat caseStatUpdate = (new SetData()).FullCaseStatSet();
                SetData set = new SetData();
                caseStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    MedRecordData.deathInfo,
                    set.MinDiagnosis(),
                    MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    set.MinRefferal(),   
                    set.MinSickList(),
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
                    set.MinRefferal(),
                    set.MinLaboratoryReport(),
                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };

                EmkClient.UpdateCase("5c04e58b-07c0-421c-804a-cd774685aea2", caseStatUpdate);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
