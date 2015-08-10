using PixServiseTests.PixServise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestPrivilege
    {
        public PrivilegeDto privilege;

        public TestPrivilege(PrivilegeDto p)
        {
            privilege = p;
        }

        //static private SqlDataReader GetPrivilege(string patientId)
        //{
        //    SqlConnection connection = Global.GetSqlConnection();
        //    string findPatient = "SELECT TOP(1) * FROM Privilege WHERE IdPrivilege = (SELECT MAX(IdPrivilege) FROM Privilege  WHERE IdPerson = '" + patientId + "')";
        //    SqlCommand person = new SqlCommand(findPatient, connection);
        //    return person.ExecuteReader();
        //}

        static public TestPrivilege BuildTestPrivilegeFromDataBase(string idPerson)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT TOP(1) * FROM Privilege WHERE IdPrivilege = (SELECT MAX(IdPrivilege) FROM Privilege  WHERE IdPerson = '" + idPerson + "')";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader privilegeReader = person.ExecuteReader())
                {
                    while (privilegeReader.Read())
                    {
                        PrivilegeDto privilege = new PrivilegeDto();
                        privilege.DateStart = Convert.ToDateTime(privilegeReader["DateStart"]);
                        privilege.DateEnd = Convert.ToDateTime(privilegeReader["DateEnd"]);
                        privilege.IdPrivilegeType = Convert.ToByte(privilegeReader["IdPrivilegeType"]);
                        TestPrivilege p = new TestPrivilege(privilege);
                        return p;
                    }
                }
            }
            return null;
        }

        public void FindMismatch(TestPrivilege b)
        {
            if (b.privilege != null)
            {
                if (this.privilege.DateEnd != b.privilege.DateEnd)
                    Global.errors3.Add("Несовпадение DateEnd TestPrivilege");
                if (this.privilege.DateStart != b.privilege.DateStart)
                    Global.errors3.Add("Несовпадение DateStart TestPrivilege");
                if (this.privilege.IdPrivilegeType != b.privilege.IdPrivilegeType)
                    Global.errors3.Add("Несовпадение IdPrivilageType TestPrivilege");
            }
        }

        public bool CheckPrivilegeInDataBase(string patientId)
        {
            TestPrivilege priv = TestPrivilege.BuildTestPrivilegeFromDataBase(patientId);
            if (this != priv)
            {
                this.FindMismatch (priv);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestPrivilege p = obj as TestPrivilege;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestPrivilege с другим типом");
                return false;
            }
            if (this.privilege == p.privilege)
                return true;
            if ((this.privilege == null) || (p.privilege == null))
            {
                Global.errors3.Add("Сравнение TestPrivilege = null с TestPrivilege != null");
            }
            if ((this.privilege.DateEnd == p.privilege.DateEnd) &&
                (this.privilege.DateStart == p.privilege.DateStart) &&
                (this.privilege.IdPrivilegeType == p.privilege.IdPrivilegeType))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestPrivilege");
                return false;
            }
        }
        public static bool operator ==(TestPrivilege a, TestPrivilege b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestPrivilege a, TestPrivilege b)
        {
            return !(a.Equals(b));
        }
    }
}
