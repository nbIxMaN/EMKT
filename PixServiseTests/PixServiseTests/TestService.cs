using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestService : TestMedRecord
    {
        Service document;
        TestParticipant doctor;
        public TestService(Service s, string idLpu = "")
        {
            if (s != null)
            {
                document = s;
                if (s.Performer != null)
                    doctor = new TestParticipant(s.Performer, idLpu);
                if (document.PaymentInfo == null)
                    document.PaymentInfo = new PaymentInfo();
            }
        }
        static public List<TestService> BuildServiseFromDataBaseData(string IdStep)
        {
            List<TestService> serviseList = new List<TestService>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findService = "SELECT * FROM Service, Step, nsi.ServiceType WHERE Service.IdStep = '" + IdStep + "' AND Service.IdStep = Step.IdStep AND Service.idServiceType = nsi.ServiceType.IdServiceType";
                SqlCommand serviceCommand = new SqlCommand(findService, connection);
                using (SqlDataReader serviceReader = serviceCommand.ExecuteReader())
                {
                    while (serviceReader.Read())
                    {
                        Service s = new Service();
                        s.PaymentInfo = new PaymentInfo();
                        if (serviceReader["DateEnd"].ToString() != "")
                            s.DateEnd = Convert.ToDateTime(serviceReader["DateEnd"]);
                        else
                            s.DateEnd = DateTime.MinValue;
                        if (serviceReader["DateStart"].ToString() != "")
                            s.DateStart = Convert.ToDateTime(serviceReader["DateStart"]);
                        else
                            s.DateStart = DateTime.MinValue;
                        if (serviceReader["IdFedNsi"].ToString() != "")
                            s.IdServiceType = Convert.ToString(serviceReader["IdFedNsi"]);
                        if (serviceReader["ServiceName"].ToString() != "")
                            s.ServiceName = Convert.ToString(serviceReader["ServiceName"]);
                        if (serviceReader["IdRHealthCareUnit"].ToString() != "")
                            s.PaymentInfo.HealthCareUnit = Convert.ToByte(serviceReader["IdRHealthCareUnit"]);
                        if (serviceReader["IdRVisitPaymentType"].ToString() != "")
                            s.PaymentInfo.IdPaymentType = Convert.ToByte(serviceReader["IdRVisitPaymentType"]);
                        if (serviceReader["IdRPaymentStatus"].ToString() != "")
                            s.PaymentInfo.PaymentState = Convert.ToByte(serviceReader["IdRPaymentStatus"]);
                        if (serviceReader["Quantity"].ToString() != "")
                            s.PaymentInfo.Quantity = Convert.ToInt32(serviceReader["Quantity"]);
                        TestService ts = new TestService(s);
                        if ((serviceReader["IdDoctor"].ToString() != "") && (serviceReader["IdCase"].ToString() != ""))
                            ts.doctor = TestParticipant.BuildTestParticipantFromDataBase(serviceReader["IdCase"].ToString(), serviceReader["IdDoctor"].ToString());
                        serviseList.Add(ts);
                    }
                }
            }
            if (serviseList.Count != 0)
                return serviseList;
            else 
                return null;
        }
        private void FindMismatch(TestService s)
        {
            if (this.document.DateEnd != s.document.DateEnd)
                Global.errors3.Add("Несовпадение DateEnd TestService");
            if (this.document.DateStart != s.document.DateStart)
                Global.errors3.Add("Несовпадение DateStart TestService");
            if (this.document.IdServiceType != s.document.IdServiceType)
                Global.errors3.Add("Несовпадение IdServiceType TestService");
            if (this.document.PaymentInfo.HealthCareUnit != s.document.PaymentInfo.HealthCareUnit)
                Global.errors3.Add("Несовпадение HealthCareUnit TestService");
            if (this.document.PaymentInfo.IdPaymentType != s.document.PaymentInfo.IdPaymentType)
                Global.errors3.Add("Несовпадение IdPaymentType TestService");
            if (this.document.PaymentInfo.PaymentState != s.document.PaymentInfo.PaymentState)
                Global.errors3.Add("Несовпадение PaymentState TestService");
            if (this.document.PaymentInfo.Quantity != s.document.PaymentInfo.Quantity)
                Global.errors3.Add("Несовпадение Quantity TestService");
            //if (this.document.PaymentInfo.Tariff != s.document.PaymentInfo.Tariff)
            //    Global.errors3.Add("Несовпадение Tariff TestService");
            if (this.document.ServiceName != s.document.ServiceName)
                Global.errors3.Add("Несовпадение ServiceName TestService");
            if (Global.GetLength(this.doctor) != Global.GetLength(s.doctor))
                Global.errors3.Add("Несовпадение длинны doctor TestService");
        }
        public bool CheckTestServiceInDataBase(string caseId)
        {
            List<TestService> docs = BuildServiseFromDataBaseData(caseId);
            foreach (TestService document in docs)
            {
                if (this != document)
                {
                    this.FindMismatch(document);
                    return false;
                }
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            TestService p = obj as TestService;
            if ((object)p == null)
            {
                return false;
            }
            if (this.document == p.document)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                return false;
            }
            if ((this.document.DateEnd == p.document.DateEnd) &&
            (this.document.DateStart == p.document.DateStart) &&
            (this.document.IdServiceType == p.document.IdServiceType) &&
            (this.document.PaymentInfo.HealthCareUnit == p.document.PaymentInfo.HealthCareUnit) &&
            (this.document.PaymentInfo.IdPaymentType == p.document.PaymentInfo.IdPaymentType) &&
            (this.document.PaymentInfo.PaymentState == p.document.PaymentInfo.PaymentState) &&
            (this.document.PaymentInfo.Quantity == p.document.PaymentInfo.Quantity) &&
            //(this.document.PaymentInfo.Tariff == p.document.PaymentInfo.Tariff) &&
            (this.document.ServiceName == p.document.ServiceName) &&
            Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestService a, TestService b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestService a, TestService b)
        {
            return !(a.Equals(b));
        }
    }
}
