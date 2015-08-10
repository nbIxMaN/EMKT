using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestEmkServiceClient : IDisposable
    {
        private EmkServiceClient client = new EmkServiceClient();
        private bool disposed = false; 
        public void getErrors(object obj)
        {
            Array errors = obj as Array;
            if (errors != null)
            {
                foreach(RequestFault i in errors)
                {
                    Global.errors1.Add(i.PropertyName + " - " + i.Message);
                    getErrors(i.Errors);
                }
            }
        }
        private void CaseWork(string guid, CaseBase c, string method)
        {
            try
            {
                CaseAmb ca = c as CaseAmb;
                if ((object)ca != null)
                {
                    TestAmbCase example = null;
                    switch (method)
                    {
                        case "AddCase":
                            client.AddCase(guid, ca);
                            example = new TestAmbCase(guid, ca);
                            break;
                        case "CreateCase":
                            client.CreateCase(guid, ca);
                            example = new TestAmbCase(guid, ca);
                            break;
                        case "CloseCase":
                            string patientId = TestPerson.GetPersonId(guid, ca.IdLpu, ca.IdPatientMis);
                            example = TestAmbCase.BuildAmbCaseFromDataBaseData(guid, ca.IdLpu, ca.IdCaseMis, patientId);
                            client.CloseCase(guid, ca);
                            example.ChangeUpdateAmbCase(guid, ca);
                            break;
                        case "UpdateCase":
                            client.UpdateCase(guid, ca);
                            example = new TestAmbCase(guid, ca);
                            break;
                    }
                    if (!example.CheckCaseInDataBase())
                    {
                        Global.errors1.AddRange(Global.errors2);
                        Global.errors1.Add("Несовпадение");
                    }
                }
                CaseStat cs = c as CaseStat;
                if ((object)cs != null)
                {
                    TestStatCase example = null;
                    switch (method)
                    {
                        case "AddCase":
                            client.AddCase(guid, cs);
                            example = new TestStatCase(guid, cs);
                            break;
                        case "CreateCase":
                            client.CreateCase(guid, cs);
                            example = new TestStatCase(guid, cs);
                            break;
                        case "CloseCase":
                            client.CloseCase(guid, cs);
                            string patientId = TestPerson.GetPersonId(guid, cs.IdLpu, cs.IdPatientMis);
                            example = TestStatCase.BuildAmbCaseFromDataBaseData(guid, cs.IdLpu, cs.IdCaseMis, patientId);
                            example.ChangeUpdateStatCase(guid, cs);
                            break;
                        case "UpdateCase":
                            client.UpdateCase(guid, cs);
                            example = new TestStatCase(guid, cs);
                            break;
                    }
                    if (!example.CheckCaseInDataBase())
                    {
                        Global.errors1.AddRange(Global.errors2);
                        Global.errors1.Add("Несовпадение");
                    }
                }
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault[]> e)
            {
                getErrors(e.Detail);
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault> e)
            {
                Global.errors1.Add(e.Detail.PropertyName + " - " + e.Detail.Message);
            }
        }

        public void AddCase(string guid, CaseBase c)
        {
            CaseWork(guid, c, "AddCase");
        }
        public void CreateCase(string guid, CaseBase c)
        {
            CaseWork(guid, c, "CreateCase");
        }
        public void CloseCase(string guid, CaseBase c)
        {
            CaseWork(guid, c, "CloseCase");
        }
        public void UpdateCase(string guid, CaseBase c)
        {
            CaseWork(guid, c, "UpdateCase");
        }
        public void AddStepToCase(string guid, string idLpu, string idPatientMis, string idCaseMis, StepBase s)
        {
            try
            {
                StepAmb sa = s as StepAmb;
                if ((object)sa != null)
                {
                    client.AddStepToCase(guid, idLpu, idPatientMis, idCaseMis ,s);
                    TestAmbStep example = new TestAmbStep(sa, idLpu);
                    if (!example.CheckStepInDataBase(guid, idPatientMis, idLpu, idCaseMis))
                    {
                        Global.errors1.AddRange(Global.errors2);
                    }
                }
                StepStat ss = s as StepStat;
                if ((object)ss != null)
                {
                    client.AddStepToCase(guid, idLpu, idPatientMis, idCaseMis, s);
                    TestStatStep example = new TestStatStep(ss, idLpu);
                    if (!example.CheckStepInDataBase(guid, idPatientMis, idLpu, idCaseMis))
                        Global.errors1.AddRange(Global.errors2);
                }
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault[]> e)
            {
                getErrors(e.Detail);
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault> e)
            {
                Global.errors1.Add(e.Detail.PropertyName + " - " + e.Detail.Message);
            }
        }
        public void AddMedRecord(string guid, string idLpu, string idPatientMis, MedRecord mr, string idCaseMis = "")
        {
            try
            {
                client.AddMedRecord(guid, idLpu, idPatientMis, idCaseMis, mr);
                TestMasterCase tmc = TestMasterCase.BuildTestMasterCase(guid, idLpu, idPatientMis, idCaseMis);
                if (!tmc.CheckDocumentInCase(mr, idLpu))
                    Global.errors1.Add("Не добавлен");
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault[]> e)
            {
                getErrors(e.Detail);
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.EMKServise.RequestFault> e)
            {
                Global.errors1.Add(e.Detail.PropertyName + " - " + e.Detail.Message);
            }
        }
        //public ReferralTupleDto[] GetReferralList(string guid, string idLpu, byte idReferralType, DateTime startDate, DateTime endDate)
        //{
        //    var a = client.GetReferralList(guid, idLpu, idReferralType, startDate, endDate);
        //}
        ~TestEmkServiceClient()
        {
            client.Close();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                client.Close();
                disposed = true;
            }
        }
    }
}
