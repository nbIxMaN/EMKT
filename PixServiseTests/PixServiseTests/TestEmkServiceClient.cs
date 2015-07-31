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
            catch (Exception e)
            {
                Global.errors1.Add(e.Message);
            }
        }
        ~TestEmkServiceClient()
        {
            client.Close();
        }
    }
}
