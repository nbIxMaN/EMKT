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
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
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
                client.AddCase(Data.globalGuid, caseAmb);
            }
            using (TestMqProxyClient client = new TestMqProxyClient())
            {
                client.GetResultDocument(Data.globalGuid, Data.idlpu, caseAmb.IdCaseMis, 3);
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
                c.AddPatient(Data.globalGuid, Data.idlpu, patient);
            }
            CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
            caseAmb.MedRecords = new List<MedRecord>
            {
                    MedRecordData.dischargeSummary,
                    (new SetData()).MinClinicMainDiagnosis()
            };
            using (TestEmkServiceClient client = new TestEmkServiceClient())
            {
                client.AddCase(Data.globalGuid, caseAmb);
            }
            using (TestMqProxyClient client = new TestMqProxyClient())
            {
                client.GetResultDocument(Data.globalGuid, Data.idlpu, caseAmb.IdCaseMis, 1);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
