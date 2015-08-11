using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestTfomsInfo : TestMedRecord
    {
        TfomsInfo document;
        public TestTfomsInfo(TfomsInfo t)
        {
            if (t != null)
                document = t;
        }
        static public TestTfomsInfo BuildTfomsInfoFromDataBaseDate(string idStep)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findTFI = "SELECT * FROM TfomsInfo WHERE IdTfomsInfo = (SELECT MAX(IdTfomsInfo) FROM TfomsInfo WHERE IdStep = '" + idStep + "')";
                SqlCommand TFIcommand = new SqlCommand(findTFI, connection);
                using (SqlDataReader TFIReader = TFIcommand.ExecuteReader())
                {
                    while (TFIReader.Read())
                    {
                        TfomsInfo t = new TfomsInfo();
                        if (TFIReader["Count"].ToString() != "")
                            t.Count = Convert.ToInt32(TFIReader["Count"]);
                        if (TFIReader["IdRTFORMSMedicalStandard"].ToString() != "")
                            t.IdTfomsType = Convert.ToInt32(TFIReader["IdRTFORMSMedicalStandard"]);
                        if (TFIReader["Tariff"].ToString() != "")
                            t.Tariff = Convert.ToDecimal(TFIReader["Tariff"]);
                        TestTfomsInfo i = new TestTfomsInfo(t);
                        return i;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestTfomsInfo t)
        {
            if (this.document.Count != t.document.Count)
                Global.errors3.Add("Несовпадение Count TestTfomsInfo");
            if (this.document.IdTfomsType != t.document.IdTfomsType)
                Global.errors3.Add("Несовпадение IdTfomsType TestTfomsInfo");
            if (this.document.Tariff  != t.document.Tariff)
                Global.errors3.Add("Несовпадение Tariff TestTfomsInfo");
        }

        public override bool Equals(Object obj)
        {
            TestTfomsInfo p = obj as TestTfomsInfo;
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
            if ((this.document.Count == p.document.Count) &&
                (this.document.IdTfomsType == p.document.IdTfomsType) &&
                (this.document.Tariff == p.document.Tariff))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestTfomsInfo");
                return false;
            }
        }
        public static bool operator ==(TestTfomsInfo a, TestTfomsInfo b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestTfomsInfo a, TestTfomsInfo b)
        {
            return !(a.Equals(b));
        }

    }
}
