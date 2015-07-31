using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestEmkServiceClient
    {
        static public EmkServiceClient client = new EmkServiceClient();
        private void getErrors(object obj)
        {
            Array errors = obj as Array;
            if (errors != null)
            {
                foreach (RequestFault i in errors)
                {
                    Global.errors1.Add(i.PropertyName + " - " + i.Message);
                    getErrors(i.Errors);
                }
            }
        }
        public void AddCase(string guid, CaseBase c)
        {
            try
            {
                CaseAmb ca = c as CaseAmb;
                if ((object)ca != null)
                {
                    client.AddCase(guid, c);
                    TestAmbCase example = new TestAmbCase(guid, ca);
                    if (!example.CheckCaseInDataBase())
                    {
                        Global.errors1.AddRange(Global.errors2);
                        Global.errors1.Add("Несовпадение");
                    }
                }
                CaseStat cs = c as CaseStat;
                if ((object)cs != null)
                {
                    client.AddCase(guid, c);
                    TestStatCase example = new TestStatCase(guid, cs);
                    if (!example.CheckCaseInDataBase())
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
        ~TestEmkServiceClient()
        {
            client.Close();
        }
    }
}
