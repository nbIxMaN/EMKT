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
    public class AppointedMedicationTest : Data
    {
        //--------------------------- AmbCase
        [Test]
        public void AddAmbCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication,
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.AddCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddAmbCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.AddCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.CreateCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateAmbCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.CreateCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase(Global.GUID, caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.UpdateCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateAmbCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase(Global.GUID, caseAmb);
                caseAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                client.UpdateCase(Global.GUID, caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseAmbCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase(Global.GUID, caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                client.AddStepToCase(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseAmbCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase(Global.GUID, caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    (new  SetData()).MinAppointedMedication()
                };

                client.AddStepToCase(Global.GUID, Data.idlpu, caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //--------------------------- StatCase
        [Test]
        public void AddStatCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication,
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.AddCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStatCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.AddCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.CreateCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateStatCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    MedRecordData.appointedMedication,
                };
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.CreateCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase(Global.GUID, caseStat);
                caseStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis(),
                    (new SetData()).MinAppointedMedication()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.UpdateCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateStatCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase(Global.GUID, caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                client.UpdateCase(Global.GUID, caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseStatCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase(Global.GUID, caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };

                client.AddStepToCase(Global.GUID, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddStepToCaseStatCaseWithAppointedMedicationMin()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient(Global.GUID, Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase(Global.GUID, caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    (new  SetData()).MinAppointedMedication()
                };

                client.AddStepToCase(Global.GUID, Data.idlpu, caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
