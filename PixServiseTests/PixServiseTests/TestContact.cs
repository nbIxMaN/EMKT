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
    class TestContact
    {
        public ContactDto contact;
        //static private string _connectionPath =
        //    "Data Source=192.168.8.93;Initial Catalog=EMKDBv3;User ID=a.pihtin;Password=stest2";
        //private ArrayList _errors;

        public TestContact(ContactDto c)
        {
            contact = c;
            //_errors = errors;
        }

        //static private SqlDataReader GetContact(string patientId)
        //{
        //    SqlConnection connection = Global.GetSqlConnection();
        //    string findPatient = "SELECT * FROM Contact WHERE IdPerson = '" + patientId + "'";
        //    SqlCommand person = new SqlCommand(findPatient, connection);
        //    return person.ExecuteReader();
        //}

        static public List<TestContact> BuildContactsFromDataBaseData(string idPerson)
        {
            List<TestContact> contacts = new List<TestContact>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT * FROM Contact WHERE IdPerson = '" + idPerson + "'";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader contactReader = person.ExecuteReader())
                {
                    // bool a = documentReader.Read();
                    while (contactReader.Read())
                    {
                        ContactDto cont = new ContactDto();
                        cont.IdContactType = Convert.ToByte(contactReader["IdContactType"]);
                        cont.ContactValue = Convert.ToString(contactReader["ContactValue"]);
                        TestContact contact = new TestContact(cont);
                        contacts.Add(contact);
                    }
                }
            }
            if (contacts.Count != 0)
                return contacts;
            else
                return null;
        }

        public void FindMismatch(TestContact b)
        {
            if (this.contact.ContactValue != b.contact.ContactValue)
                Global.errors3.Add("Несовпадение ContactValue TestContact");
            if (this.contact.IdContactType != b.contact.IdContactType)
                Global.errors3.Add("Несовпадение IdContactType TestContact");
        }

        public bool CheckContactInDataBase(string patientId)
        {
            List<TestContact> conts = BuildContactsFromDataBaseData(patientId);
            foreach (TestContact contact in conts)
            {
                if (this != contact)
                {
                    this.FindMismatch(contact);
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestContact p = obj as TestContact;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestContact с другим типом");
                return false;
            }
            if (this.contact == p.contact)
                return true;
            if ((this.contact == null) || (p.contact == null))
            {
                Global.errors3.Add("Сравнение TestContact = null с TestContact != null");
                return false;
            }
            if ((this.contact.ContactValue == p.contact.ContactValue) &&
                (this.contact.IdContactType == p.contact.IdContactType))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestContact");
                return false;
            }
        }
        public static bool operator ==(TestContact a, TestContact b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestContact a, TestContact b)
        {
            return !(a.Equals(b));
        }
    }
}
