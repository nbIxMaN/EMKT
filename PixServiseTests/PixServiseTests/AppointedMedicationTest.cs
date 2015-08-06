﻿using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    [TestFixture]
    public class AppointedMedicationTest: Data
    {
        //--------------------------- AmbCase
        [Test]
        public void AddAmbCaseWithAppointedMedicationFull()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();

                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication,
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseAmb.Steps = new StepAmb[]
                {
                    stepAmb
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                client.AddStepToCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new MedRecord[]
                {
                    (new  SetData()).MinAppointedMedication()
                };

                client.AddStepToCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", caseAmb.IdPatientMis, caseAmb.IdCaseMis, stepAmb);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();

                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication,
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinAppointedMedication()
                };

                caseStat.Steps = new StepStat[]
                {
                    stepStat
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    MedRecordData.appointedMedication
                };

                client.AddStepToCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new MedRecord[]
                {
                    (new  SetData()).MinAppointedMedication()
                };

                client.AddStepToCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", caseStat.IdPatientMis, caseStat.IdCaseMis, stepStat);
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
