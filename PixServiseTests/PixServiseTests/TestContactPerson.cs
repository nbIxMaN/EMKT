﻿using PixServiseTests.PixServise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestContactPerson
    {
        public ContactPersonDto contactPerson;
        public List<TestContact> contacts;
        public Array conts
        {
            get
            {
                if (contacts != null)
                    return contacts.ToArray();
                else
                    return null;
            }
        }

        public TestContactPerson(ContactPersonDto c)
        {
            contactPerson = c;
            if (c.ContactList != null)
            {
                contacts = new List<TestContact>();
                foreach (var cont in c.ContactList)
                {
                    contacts.Add(new TestContact(cont));
                }
            }
        }

        static public TestContactPerson BuildTestContactPersonFromDataBase (string idPerson)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT TOP(1) * FROM mm_Person2Person WHERE IdPerson = '" + idPerson + "'";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader cpReader = person.ExecuteReader())
                {
                    while (cpReader.Read())
                    {
                        ContactPersonDto contactPerson = new ContactPersonDto();
                        if (cpReader["IdRealationType"].ToString() != "")
                            contactPerson.IdRelationType = Convert.ToByte(cpReader["IdRealationType"]);
                        if (cpReader["IdPersonRelated"].ToString() != "");
                            TestPatient personRelated = TestPatient.BuildPatientFromDataBaseData(patientId: cpReader["IdPersonRelated"].ToString());
                        contactPerson.FamilyName = personRelated.patient.FamilyName;
                        contactPerson.GivenName = personRelated.patient.GivenName;
                        contactPerson.IdPersonMis = personRelated.patient.IdPatientMIS;
                        contactPerson.MiddleName = personRelated.patient.MiddleName;
                        List<TestContact> contacts = personRelated.contacts;
                        TestContactPerson cp = new TestContactPerson(contactPerson);
                        return cp;
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestContactPerson b)
        {
            if (b.contactPerson != null)
            {
                if (this.contactPerson.FamilyName != b.contactPerson.FamilyName)
                    Global.errors3.Add("Несовпадение FamilyName TestContactPerson");
                if (this.contactPerson.GivenName != b.contactPerson.GivenName)
                    Global.errors3.Add("Несовпадение GivenName TestContactPerson");
                if (this.contactPerson.IdPersonMis != b.contactPerson.IdPersonMis)
                    Global.errors3.Add("Несовпадение IdPersonMis TestContactPerson");
                if (this.contactPerson.IdRelationType != b.contactPerson.IdRelationType)
                    Global.errors3.Add("Несовпадение IdRelationType TestContactPerson");
                if (this.contactPerson.MiddleName != b.contactPerson.MiddleName)
                    Global.errors3.Add("Несовпадение MiddleName TestContactPerson");
                if (Global.GetLength(this.conts) != Global.GetLength(b.conts))
                    Global.errors3.Add("Несовпадение длины contacts TestContactPerson");
            }
        }

        public override bool Equals(Object obj)
        {
            TestContactPerson p = obj as TestContactPerson;
            if ((object)p == null)
            {
                return false;
            }
            if ((this == null) && (p == null))
                return true;
            if (this.contactPerson == p.contactPerson)
                return true;
            if ((this.contactPerson == null) || (p.contactPerson == null))
            {
                return false;
            }
            if ((this.contactPerson.FamilyName == p.contactPerson.FamilyName) &&
                (this.contactPerson.GivenName == p.contactPerson.GivenName) &&
                (this.contactPerson.IdPersonMis == p.contactPerson.IdPersonMis) &&
                (this.contactPerson.IdRelationType == p.contactPerson.IdRelationType) &&
                (this.contactPerson.MiddleName == p.contactPerson.MiddleName) &&
                (Global.IsEqual(this.conts, p.conts)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestContactPerson");
                return false;
            }
        }
        public static bool operator ==(TestContactPerson a, TestContactPerson b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestContactPerson a, TestContactPerson b)
        {
            return !(a.Equals(b));
        }

    }
}
