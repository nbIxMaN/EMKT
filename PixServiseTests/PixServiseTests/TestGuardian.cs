using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestGuardian
    {
        public TestPerson person;
        public Guardian guardian;

        public TestGuardian(Guardian g)
        {
            if (g != null)
            {
                guardian = g;
                if (g.Person != null)
                    person = new TestPerson(g.Person);
            }
        }

        static public TestGuardian BuildTestGuardianFromDataBase(string guardianId, string patientId)
        {
            if ((guardianId != "") && (patientId != ""))
            {
                string findIdRealationTypeString =
                    "SELECT TOP(1) * FROM mm_Person2Person WHERE IdPerson = '" + patientId + "' AND IdPersonRelated = '" + guardianId + "'";
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    SqlCommand IdInstitution = new SqlCommand(findIdRealationTypeString, connection);
                    Guardian g = new Guardian();
                    using (SqlDataReader IdRelationTypeReader = IdInstitution.ExecuteReader())
                    {
                        while (IdRelationTypeReader.Read())
                        {
                            g.IdRelationType = Convert.ToByte(IdRelationTypeReader["IdRelationType"]);
                            if (Convert.ToString(IdRelationTypeReader["UnderlyingDocument"]) != "")
                                g.UnderlyingDocument = Convert.ToString(IdRelationTypeReader["UnderlyingDocument"]);
                            else
                                g.UnderlyingDocument = null;
                        }
                    }
                    TestGuardian guard = new TestGuardian(g);
                    guard.person = TestPerson.BuildPersonFromDataBaseData(guardianId);
                    return guard;
                }
            }
            return null;
        }

        public void FindMismatch(TestGuardian g)
        {
            if (g.guardian != null)
            {
                if (this.guardian.IdRelationType != g.guardian.IdRelationType)
                    Global.errors3.Add("Несовпадение IdRelationType TestGuardian");
                if (this.guardian.UnderlyingDocument != g.guardian.UnderlyingDocument)
                    Global.errors3.Add("Несовпадение UnderlyingDocument TestGuardian");
            }
        }

        public bool CheckGuardianInDataBase(string guardianId, string patientId)
        {
            TestGuardian guad = TestGuardian.BuildTestGuardianFromDataBase(guardianId, patientId);
            if (this != guad)
            {
                this.FindMismatch(guad);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestGuardian p = obj as TestGuardian;
            if ((object)p == null)
            {
                return false;
            }
            if (this.guardian == p.guardian)
                return true;
            if ((this.guardian == null) || (p.guardian == null))
            {
                return false;
            }
            if ((this.guardian.IdRelationType == p.guardian.IdRelationType) &&
                (this.guardian.UnderlyingDocument == p.guardian.UnderlyingDocument) &&
                Global.IsEqual(guardian.Person, p.person))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestGuardian a, TestGuardian b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestGuardian a, TestGuardian b)
        {
            return !(a.Equals(b));
        }
    }
}
