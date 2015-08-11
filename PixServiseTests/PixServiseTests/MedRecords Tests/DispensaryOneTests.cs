using System;
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
    public class DispensaryOneTests : Data
    {
        [Test]
        public void AddDispCaseWithMinDispanseryOne()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinDispensaryOne(),
                    MedRecordData.dispensaryOne
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    MedRecordData.dispensaryOne
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinDispensaryOne()
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    MedRecordData.dispensaryOne
                };
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinDispensaryOne()
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinDispensaryOne(),
                    (new SetData()).MinTfomsInfo()
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                caseDisp.MedRecords = new MedRecord[]
                {
                    MedRecordData.dispensaryOne
                };
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new MedRecord[]
                {
                    MedRecordData.dispensaryOne,
                    (new SetData()).MinTfomsInfo()
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new MedRecord[]
                {
                    (new SetData()).MinDispensaryOne(),
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                CaseAmb caseDisp = (new SetData()).MinCaseDispSet();
                client.CreateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseDisp);
                caseDisp.MedRecords = new MedRecord[]
                {
                    MedRecordData.dispensaryOne
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
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
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
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
