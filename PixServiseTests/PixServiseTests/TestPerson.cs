using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestPerson
    {
        public PersonWithIdentity person;
        public TestHumanName name;
        public List<TestIdentityDocument> documents;
        public Array docs
        {
            get
            {
                if (documents != null)
                    return documents.ToArray();
                else
                    return new int[0];
            }
        }
        public TestPerson(PersonWithIdentity p)
        {
            person = p;
            if (person.Sex == 0)
                person.Sex = 3;
            name = new TestHumanName(p.HumanName);
            if ((p.Documents != null) && (p.Documents.Length != 0))
            {
                List<TestIdentityDocument> doc = new List<TestIdentityDocument>();
                foreach (IdentityDocument d in p.Documents)
                {
                    doc.Add(new TestIdentityDocument(d));
                }
                documents = doc;
            }
        }

        static public string GetPersonId(string guid, string idlpu, string mis)
        {
            string patientId = "";
            string findIdPersonString = "";
            string findIdInstitutionalString =
                "SELECT TOP(1) IdInstitution FROM Institution WHERE IdFedNsi = '" + idlpu + "'";
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection);
                using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                {
                    string InstId = "";
                    while (IdInstitutional.Read())
                    {
                        InstId = IdInstitutional["IdInstitution"].ToString();
                    }
                    findIdPersonString =
                        "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + mis + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "'";
                }
                SqlCommand command = new SqlCommand(findIdPersonString, connection);
                using (SqlDataReader IdPerson = command.ExecuteReader())
                {
                    while (IdPerson.Read())
                    {
                        patientId = IdPerson["IdPerson"].ToString();
                    }
                }
            }
            if (patientId != "")
                return patientId;
            else
                return null;
        }

        static private string GetIDMIS(string idPerson)
        {
            string mis = "";
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT TOP(1) * FROM ExternalId WHERE IdPerson = '" + idPerson + "'";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader patientFromDataBase = person.ExecuteReader())
                {
                    while (patientFromDataBase.Read())
                        mis = Convert.ToString(patientFromDataBase["IdPersonMIS"]);
                }
            }
            return mis;
        }

        static public TestPerson BuildPersonFromDataBaseData(string personId)
        {
            string mis = GetIDMIS(personId);
            if (personId != null)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findPatient = "SELECT TOP(1) * FROM Person WHERE IdPerson = '" + personId + "'";
                    SqlCommand person = new SqlCommand(findPatient, connection);
                    using (SqlDataReader personFromDataBase = person.ExecuteReader())
                    {
                        PersonWithIdentity p = new PersonWithIdentity();
                        while (personFromDataBase.Read())
                        {
                            DateTime a = Convert.ToDateTime(personFromDataBase["BirthDate"]);
                            DateTime b =  Convert.ToDateTime(personFromDataBase["RecordCreated"]);
                            if (a.Date == b.Date)
                                p.Birthdate = DateTime.MinValue;
                            else
                                p.Birthdate = Convert.ToDateTime(personFromDataBase["BirthDate"]);
                            p.Sex = Convert.ToByte(personFromDataBase["IdSex"]);
                        }
                        p.IdPersonMis = mis;
                        TestPerson pers = new TestPerson(p);
                        pers.name = TestHumanName.BuildHumanNameFromDataBaseData(personId);
                        pers.documents = TestIdentityDocument.BuildDocumentsFromDataBaseData(personId);
                        return pers;
                    }
                }
            }
            return null;
        }

        private string PersonFieldToString(Object a)
        {
            if (a == null)
                return ("");
            else
                return a.ToString();
        }

        public void FindMismatch(TestPerson b)
        {
            if (this.person.Birthdate != b.person.Birthdate)
                Global.errors3.Add("Несовпадение Birthdate TestPerson");
            if (this.person.Sex != b.person.Sex)
                Global.errors3.Add("Несовпадение Sex TestPerson");
            if (this.person.IdPersonMis != b.person.IdPersonMis)
                Global.errors3.Add("Несовпадение IdPersonMis TestPerson");
            if (!Global.IsEqual(this.docs, b.docs))
                Global.errors3.Add("Несовпадение Documents TestPerson");
            if (!Global.IsEqual(this.name, b.name))
                Global.errors3.Add("Несовпадение Name TestPerson");
        }

        public bool CheckPersonInDataBase()
        {
            TestPerson p = TestPerson.BuildPersonFromDataBaseData(person.IdPersonMis);
            this.Equals(p);
            return (this == p);
        }

        public override bool Equals(Object obj)
        {
            TestPerson p = obj as TestPerson;
            if ((object)p == null)
            {
                return false;
            }
            if ((this.person.Birthdate == p.person.Birthdate) &&
                (this.person.IdPersonMis == p.person.IdPersonMis) &&
                (Global.IsEqual(this.docs, p.docs)) &&
                (this.person.Sex == p.person.Sex) &&
                (Global.IsEqual(this.name, p.name)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestPerson a, TestPerson b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestPerson a, TestPerson b)
        {
            return !(a.Equals(b));
        }
    }
}
