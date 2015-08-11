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

        public TestContact(ContactDto c)
        {
            contact = c;
        }

        static public List<TestContact> BuildContactsFromDataBaseData(string idPerson)
        {
            List<TestContact> contacts = new List<TestContact>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT * FROM Contact WHERE IdPerson = '" + idPerson + "'";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader contactReader = person.ExecuteReader())
                {
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

        private void FindMismatch(TestContact b)
        {
            if (this.contact.ContactValue != b.contact.ContactValue)
                Global.errors3.Add("Несовпадение ContactValue TestContact");
            if (this.contact.IdContactType != b.contact.IdContactType)
                Global.errors3.Add("Несовпадение IdContactType TestContact");
        }

        public override bool Equals(Object obj)
        {
            TestContact p = obj as TestContact;
            if ((object)p == null)
            {
                return false;
            }
            if (this.contact == p.contact)
                return true;
            if ((this.contact == null) || (p.contact == null))
            {
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
