using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDeathInfo : TestMedRecord
    {
        DeathInfo document;
        public TestDeathInfo(DeathInfo d)
        {
            if (d != null)
                document = d;
        }
        static public TestDeathInfo BuildDeathInfoFromDataBaseDate(string IdStep)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findDI = "SELECT * FROM DeathInfo WHERE IdStep = '" + IdStep + "'";
                SqlCommand DIcommand = new SqlCommand(findDI, connection);
                using (SqlDataReader DIReader = DIcommand.ExecuteReader())
                {
                    while (DIReader.Read())
                    {
                        DeathInfo d = new DeathInfo();
                        if (DIReader["DeathMKBCode"].ToString() != "")
                            d.MkbCode = Convert.ToString(DIReader["DeathMKBCode"]);
                        return new TestDeathInfo(d);
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestDeathInfo t)
        {
            if (this.document.MkbCode != t.document.MkbCode)
                Global.errors3.Add("Несовпадение MkbCode TestDeathInfo");
        }
        public bool CheckTestDeathInfoInfoInDataBase(string IdStep)
        {
            TestDeathInfo doc = BuildDeathInfoFromDataBaseDate(IdStep);
            return this == doc;
        }
        public override bool Equals(Object obj)
        {
            TestDeathInfo p = obj as TestDeathInfo;
            if ((object)p == null)
            {
                return false;
            }
            if (this.document.MkbCode == p.document.MkbCode)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                return false;
            }
            if (this.document.MkbCode == p.document.MkbCode)
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestDeathInfo a, TestDeathInfo b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDeathInfo a, TestDeathInfo b)
        {
            return !(a.Equals(b));
        }

    }
}
