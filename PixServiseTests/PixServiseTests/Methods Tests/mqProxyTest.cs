using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PixServiseTests.EMKServise;
using PixServiseTests.PixServise;

namespace PixServiseTests.Methods_Tests
{
    [TestFixture]
    class mqProxyTest : Data
    {
        [Test]
        public void TestForConsultNote()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                patient.IdPatientMIS = "TestForMq";
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
            caseAmb.MedRecords = new List<MedRecord>
            {
                    MedRecordData.consultNote,
                    (new SetData()).MinClinicMainDiagnosis()
            };
            caseAmb.IdCaseMis = "TestForMq";
            caseAmb.IdPatientMis = "TestForMq";
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            using (TestMqProxyClient client = new TestMqProxyClient())
            {
                client.GetResultDocument("1.2.643.5.1.13.3.25.78.118", caseAmb.IdCaseMis, 3);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void TestDishargeSummary()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
            caseAmb.MedRecords = new List<MedRecord>
            {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
            };
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            using (TestMqProxyClient client = new TestMqProxyClient())
            {
                client.GetResultDocument("1.2.643.5.1.13.3.25.78.118", caseAmb.IdCaseMis, 1);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
