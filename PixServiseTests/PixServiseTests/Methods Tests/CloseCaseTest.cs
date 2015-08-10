using System;
using NUnit.Framework;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;

namespace PixServiseTests.Methods_Tests
{
    [TestFixture]
    public class CloseCaseTest : Data
    {
        ///-----------------------------AmbCase
        [Test]
        public void CloseAmbCaseMin()
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
                CaseAmb CaseAmbClose = (new SetData()).MinCaseAmbSetForClose();
                EmkClient.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", CaseAmbClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseAmbCaseFull()
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
                CaseAmb CaseAmbClose = (new SetData()).FullCaseAmbSetForClose();

                EmkClient.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", CaseAmbClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        ///-----------------------------StatCase
        [Test]
        public void CloseStatCaseMin()
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
                CaseStat CaseStatClose = (new SetData()).MinCaseStatSetForClose();
                EmkClient.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", CaseStatClose);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void CloseStatCaseFull()
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
                CaseStat CaseStatClose = (new SetData()).FullCaseStatSetForClose();
                EmkClient.CloseCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", CaseStatClose);
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
