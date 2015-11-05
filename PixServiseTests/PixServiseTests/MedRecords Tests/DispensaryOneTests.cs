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
    public class DispensaryOneTests : Data
    {
        [Test]
        public void AddDispCaseWithMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddDispCaseWithMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateDispCaseWithMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSetForCreate();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CreateDispCaseWithMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSetForCreate();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateDispCaseWithOutMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateDispCaseWithMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateDispCaseWithOutMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void UpdateDispCaseWithMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseDispCaseWithMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp = (new SetData()).MinCaseDispSetForClose();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseDispCaseWithMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSetForCreate();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp = (new SetData()).MinCaseDispSetForClose();
                caseDisp.MedRecords = new List<MedRecord>
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinClinicMainDiagnosis()
                };
                client.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                MedRecord r = (new SetData()).MinDispensaryOne();
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                client.AddMedRecord("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp.IdLpu, caseDisp.IdPatientMis, r, caseDisp.IdCaseMis);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMaxDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                MedRecord r = MedRecordData.dispensaryOne;
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                client.AddMedRecord("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp.IdLpu, caseDisp.IdPatientMis, r, caseDisp.IdCaseMis);
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
