using System;
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
    class AddCaseTest : Data
    {
        [Test]
        public void AddMinAmbCase()
        {
            //TestPixServiceClient PixClient = new TestPixServiceClient();
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);
            }
            //TestEmkServiceClient EmkClient = new TestEmkServiceClient();
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                /*  caseAmb.MedRecords = new MedRecord[]
                  {
                      new DispensaryOne
                      {
                          CreationDate = new DateTime(2010, 11, 2),
                          Header = "ДОКУМЕНТ",
                          IsGuested = true,
                          HasExtraResearchRefferal = true,
                          IsUnderObservation = true,
                          HasExpertCareRefferal = true,
                          HasPrescribeCure = true,
                          HasHealthResortRefferal = true,
                          HasSecondStageRefferal = false,
                          Author = caseAmb.DoctorInCharge,
                          HealthGroup = new HealthGroup
                          {
                              Doctor = caseAmb.DoctorInCharge,
                              HealthGroupInfo = new HealthGroupInfo
                              {
                                  IdHealthGroup = 1,
                                  Date = new DateTime(2010, 11, 2),
                              }
                          }
                      }
                  };*/
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
            TestPixServiceClient c = new TestPixServiceClient();

            PatientDto patient = (new SetData()).PatientSet();
            c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);

            TestEmkServiceClient client = new TestEmkServiceClient();
            CaseStat caseStat = (new SetData()).MinCaseStatSet();
            client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }



        [Test]
        public void AddFullAmbCase()
        {
            TestPixServiceClient c = new TestPixServiceClient();

            PatientDto patient = (new SetData()).PatientSet();
            c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);

            TestEmkServiceClient client = new TestEmkServiceClient();
            CaseAmb caseAmb = (new SetData()).FullCaseAmbSet();
            client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullStatCase()
        {
            TestPixServiceClient c = new TestPixServiceClient();

            PatientDto patient = (new SetData()).PatientSet();
            c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.118", patient);

            TestEmkServiceClient client = new TestEmkServiceClient();
            CaseStat caseStat = (new SetData()).FullCaseStatSet();
            client.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
