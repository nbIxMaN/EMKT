using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestHumanName
    {
        public HumanName humanName;

        public TestHumanName(HumanName b)
        {
            humanName = b;
        }

        static public TestHumanName BuildHumanNameFromDataBaseData(string idPerson)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPerson = "SELECT TOP(1) * FROM Person WHERE IdPerson = '" + idPerson + "'";
                SqlCommand commandPerson = new SqlCommand(findPerson, connection);
                using (SqlDataReader humanNameReader = commandPerson.ExecuteReader())
                {
                    HumanName hn = new HumanName();
                    while (humanNameReader.Read())
                    {
                        hn.FamilyName = Convert.ToString(humanNameReader["FamilyName"]);
                        hn.GivenName = Convert.ToString(humanNameReader["GivenName"]);
                        if (Convert.ToString(humanNameReader["MiddleName"]) != "")
                            hn.MiddleName = Convert.ToString(humanNameReader["MiddleName"]);
                        else
                            hn.MiddleName = null;
                        return (new TestHumanName(hn));
                    }
                }
            }
            return null;
        }

        public void FindMismatch(TestHumanName n)
        {
            if (this.humanName.FamilyName != n.humanName.FamilyName)
                Global.errors3.Add("Несовпадение FamilyName HumanName");
            if (this.humanName.GivenName != n.humanName.GivenName)
                Global.errors3.Add("Несовпадение GivenName HumanName");
            if (this.humanName.MiddleName != n.humanName.MiddleName)
                Global.errors3.Add("Несовпадение MiddleName HumanName");
        }

        public bool CheckHumanNameInDataBase(string patientId)
        {
            TestHumanName hn = BuildHumanNameFromDataBaseData(patientId);
            if (this != hn)
            {
                this.FindMismatch(hn);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestHumanName p = obj as TestHumanName;
            if ((object)p == null)
            {
                return false;
            }
            if (this.humanName == p.humanName)
                return true;
            if ((this.humanName == null) || (p.humanName == null))
            {
                return false;
            }
            if ((this.humanName.FamilyName == p.humanName.FamilyName) &&
                (this.humanName.GivenName == p.humanName.GivenName) &&
                (this.humanName.MiddleName == p.humanName.MiddleName))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestHumanName");
                return false;
            }
        }
        public static bool operator ==(TestHumanName a, TestHumanName b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestHumanName a, TestHumanName b)
        {
            return !(a.Equals(b));
        }

    }
}
