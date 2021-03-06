﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixServiseTests.PixServise;
using System.Data.SqlClient;
using System.Collections;

namespace PixServiseTests
{
    class TestPatient
    {
        public string GUID;
        public string IDLPU;
        public PatientDto patient;
        public List<TestDocument> documents;
        public Array docs
        {
            get
            {
                if (documents != null)
                    return documents.ToArray();
                else
                    return null;
            }
        }
        public List<TestAddress> addreses;
        public Array adds
        {
            get
            {
                if (addreses != null)
                    return addreses.ToArray();
                else
                    return null;
            }
        }
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
        public TestJob job;
        public TestPrivilege privilege;
        public TestBirthplace birthplace;

        public TestPatient(string guid, string idlpu, PatientDto p)
        {
            patient = p;
            if ((guid != null) && (idlpu != null) && (p.IdPatientMIS != null))
                patient.IdGlobal = GetPatientId(guid, idlpu, p.IdPatientMIS);
            GUID = guid.ToLower();
            IDLPU = idlpu;
            if ((p.Documents != null) && (p.Documents.Length != 0))
            {
                List<TestDocument> doc = new List<TestDocument>();
                foreach (DocumentDto d in p.Documents)
                {
                    doc.Add(new TestDocument(d));
                }
                documents = doc;
            }
            if ((p.Addresses != null) && (p.Addresses.Length != 0))
            {
                List<TestAddress> add = new List<TestAddress>();
                foreach (AddressDto a in p.Addresses)
                {
                    add.Add(new TestAddress(a));
                }
                addreses = add;
            }
            if ((p.Contacts != null) && (p.Contacts.Length != 0))
            {
                List<TestContact> cont = new List<TestContact>();
                foreach(ContactDto c in p.Contacts)
                {
                    cont.Add(new TestContact(c));
                }
                contacts = cont;
            }
            if (p.Job != null)
                job = new TestJob(p.Job);
            if (p.Privilege != null)
                privilege = new TestPrivilege(p.Privilege);
            if (p.BirthPlace != null)
                birthplace = new TestBirthplace(p.BirthPlace);
        }

        static private string GetPatientId(string guid, string idlpu, string mis)
        {
            string patientId = "";
            string findIdPersonString = "";
            string findIdInstitutionalString =
                "SELECT TOP(1) IdInstitution FROM Institution WHERE IdFedNsi = '" + idlpu + "'";
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                if (Data.type == Type.guid)
                    findIdPersonString =
                        "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + mis + "' AND IdLpu = '" + idlpu + "' AND SystemGuid = '" + guid.ToLower() + "'";
                else
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

        static private string[] GetGUIDandIDLPUandIDMIS(string idPerson)
        {
            string guid = "";
            string idlpu = "";
            string mis = "";
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT TOP(1) * FROM ExternalId WHERE IdPerson = '" + idPerson + "'";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader patientFromDataBase = person.ExecuteReader())
                {
                    while (patientFromDataBase.Read())
                    {
                        guid = Convert.ToString(patientFromDataBase["SystemGuid"]);
                        mis = Convert.ToString(patientFromDataBase["IdPersonMIS"]);
                        if (Data.type == Type.guid)
                            idlpu = Convert.ToString(patientFromDataBase["IdLpu"]);
                        else
                        {
                            idlpu = Global.GetIdIdLpu(Convert.ToString(patientFromDataBase["IdLpu"]));
                            //string findIdInstitutionalString =
                            //    "SELECT TOP(1) IdFedNsi FROM Institution WHERE IdInstitution = '" + lpu + "'";
                            //using (SqlConnection connection2 = Global.GetSqlConnection())
                            //{
                            //    SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection2);
                            //    using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                            //    {
                            //        while (IdInstitutional.Read())
                            //        {
                            //            idlpu = Convert.ToString(IdInstitutional["IdFedNsi"]);
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            string[] s = new string[] {guid, idlpu, mis};
            return s;
        }

        static public TestPatient BuildPatientFromDataBaseData(string guid = null, string idlpu = null, string mis = null, string patientId = null)
        {
            if (patientId == null)
                patientId = GetPatientId(guid, idlpu, mis);
            else
            {
                string[] s = GetGUIDandIDLPUandIDMIS(patientId);
                guid = s[0];
                idlpu = s[1];
                mis = s[2];
            }
            if (patientId != null)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findPatient = "SELECT TOP(1) * FROM Person, PatientAdditionalInfo WHERE Person.IdPerson = '" + patientId + "' AND Person.IdPerson = PatientAdditionalInfo.IdPerson";
                    SqlCommand person = new SqlCommand(findPatient, connection);
                    using (SqlDataReader patientFromDataBase = person.ExecuteReader())
                    {
                        while (patientFromDataBase.Read())
                        {
                            PatientDto p = new PatientDto();
                            if (patientFromDataBase["FamilyName"].ToString() != "")
                                p.FamilyName = Convert.ToString(patientFromDataBase["FamilyName"]);
                            else
                                p.FamilyName = null;
                            if (patientFromDataBase["MiddleName"].ToString() != "")
                                p.MiddleName = Convert.ToString(patientFromDataBase["MiddleName"]);
                            else
                                p.MiddleName = null;
                            if (patientFromDataBase["GivenName"].ToString() != "")
                                p.GivenName = Convert.ToString(patientFromDataBase["GivenName"]);
                            else
                                p.GivenName = null;
                            p.BirthDate = Convert.ToDateTime(patientFromDataBase["BirthDate"]);
                            p.Sex = Convert.ToByte(patientFromDataBase["IdSex"]);
                            p.IsVip = Convert.ToBoolean(patientFromDataBase["IsVip"]);
                            if (patientFromDataBase["IdSocialStatus"].ToString() != "")
                                p.SocialStatus = Convert.ToString(patientFromDataBase["IdSocialStatus"]);
                            else
                                p.SocialStatus = null;
                            if (patientFromDataBase["IdSocialGroup"].ToString() != "")
                                p.SocialGroup = Convert.ToByte(patientFromDataBase["IdSocialGroup"]);
                            else
                                p.SocialGroup = null;
                            if (patientFromDataBase["IdLivingAreaType"].ToString() != "")
                                p.IdLivingAreaType = Convert.ToByte(patientFromDataBase["IdLivingAreaType"]);
                            else
                                p.IdLivingAreaType = null;
                            if (patientFromDataBase["IdBloodType"].ToString() != "")
                                p.IdBloodType = Convert.ToByte(patientFromDataBase["IdBloodType"]);
                            else
                                p.IdBloodType = null;
                            if (patientFromDataBase["DeathTime"].ToString() != "")
                                p.DeathTime = Convert.ToDateTime(patientFromDataBase["DeathTime"]);
                            else
                                p.DeathTime = null;
                            p.IdPatientMIS = mis;
                            p.IdGlobal = patientId;
                            TestPatient patient = new TestPatient(guid, idlpu, p);
                            patient.documents = TestDocument.BuildDocumentsFromDataBaseData(patientId);
                            patient.addreses = TestAddress.BuildAdressesFromDataBaseData(patientId);
                            patient.contacts = TestContact.BuildContactsFromDataBaseData(patientId);
                            patient.job = TestJob.BuildTestJobFromDataBase(patientId);
                            patient.privilege = TestPrivilege.BuildTestPrivilegeFromDataBase(patientId);
                            patient.birthplace = TestBirthplace.BuildBirthplaceFromDataBaseData(patientId);
                            return patient;
                        }
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestPatient b)
        {
            if (this.patient.FamilyName != b.patient.FamilyName)
                Global.errors2.Add("несовпадение FamilyName TestPatient");
            if (this.patient.MiddleName != b.patient.MiddleName)
                Global.errors2.Add("несовпадение MiddleName TestPatient");
            if (this.patient.GivenName != b.patient.GivenName)
                Global.errors2.Add("несовпадение GivenName TestPatient");
            if (this.patient.BirthDate != b.patient.BirthDate)
                Global.errors2.Add("несовпадение BirthDate TestPatient");
            if (this.patient.Sex != b.patient.Sex)
                Global.errors2.Add("несовпадение Sex TestPatient");
            if (this.patient.IsVip != b.patient.IsVip)
                Global.errors2.Add("несовпадение IsVip TestPatient");
            if (this.patient.SocialStatus != b.patient.SocialStatus)
                Global.errors2.Add("несовпадение SocialStatus TestPatient");
            if (this.patient.SocialGroup != b.patient.SocialGroup)
                Global.errors2.Add("несовпадение SocialGroup TestPatient");
            if (this.patient.IdLivingAreaType != b.patient.IdLivingAreaType)
                Global.errors2.Add("несовпадение IdLivingAreaType TestPatient");
            if (this.patient.IdBloodType != b.patient.IdBloodType)
                Global.errors2.Add("несовпадение IdBloodType TestPatient");
            if (this.patient.DeathTime != b.patient.DeathTime)
                Global.errors2.Add("несовпадение DeathTime TestPatient");
            if (Global.GetLength(this.docs) != Global.GetLength(b.docs))
                Global.errors2.Add("несовпадение длины documents TestPatient");
            if (Global.GetLength(this.conts) != Global.GetLength(b.conts))
                Global.errors2.Add("несовпадение длины contacts TestPatient");
            if (Global.GetLength(this.adds) != Global.GetLength(b.adds))
                Global.errors2.Add("несовпадение длины addreses TestPatient");
            if (Global.GetLength(this.job) != Global.GetLength(b.job))
                Global.errors2.Add("несовпадение длины job TestPatient");
            if (Global.GetLength(this.privilege) != Global.GetLength(b.privilege))
                Global.errors2.Add("несовпадение длины privilege TestPatient");
            if (Global.GetLength(this.birthplace) != Global.GetLength(b.birthplace))
                Global.errors2.Add("несовпадение длины birthplace TestPatient");
            
        }

        public void ChangePatientField(PatientDto b)
        {
            if ((b.FamilyName != null) && (this.patient.FamilyName != b.FamilyName))
                this.patient.FamilyName = b.FamilyName;
            if  ((b.MiddleName != null) && (this.patient.MiddleName != b.MiddleName))
                this.patient.MiddleName = b.MiddleName;
            if ((b.GivenName != null) && (this.patient.GivenName != b.GivenName))
                this.patient.GivenName = b.GivenName;
            if ((b.BirthDate != DateTime.MinValue) && (this.patient.BirthDate != b.BirthDate))
                this.patient.BirthDate = b.BirthDate;
            if ((b.Sex != 0) && (this.patient.Sex != b.Sex))
                this.patient.Sex = b.Sex;
            this.patient.IsVip = b.IsVip;
            if ((b.SocialStatus != null) && (this.patient.SocialStatus != b.SocialStatus))
                this.patient.SocialStatus = b.SocialStatus;
            if ((b.IdLivingAreaType != null) && (this.patient.IdLivingAreaType != b.IdLivingAreaType))
                this.patient.IdLivingAreaType = b.IdLivingAreaType;
            if ((b.IdBloodType != null) && (this.patient.IdBloodType != b.IdBloodType))
                this.patient.IdBloodType = b.IdBloodType;
            if ((b.DeathTime != null) && (this.patient.DeathTime != b.DeathTime))
                this.patient.DeathTime = b.DeathTime;
            if (b.Documents != null)
            {
                foreach (DocumentDto d in b.Documents)
                {
                    if (this.documents != null)
                    {
                        bool mark = false;
                        foreach (TestDocument td in this.documents)
                        {
                            if (d.IdDocumentType == td.document.IdDocumentType)
                            {
                                td.document = d;
                                mark = true;
                            }
                        }
                        if (!mark)
                            this.documents.Add(new TestDocument(d));
                    }
                    else
                    {
                        this.documents = new List<TestDocument>();
                        documents.Add(new TestDocument(d));
                    }
                }
            }
            if (b.Addresses != null)
            {
                foreach (AddressDto a in b.Addresses)
                {
                    if (this.addreses != null)
                    {
                        bool mark = false;
                        foreach (TestAddress ta in this.addreses)
                        {
                            if (a.IdAddressType == ta.address.IdAddressType)
                            {
                                ta.address = a;
                                mark = true;
                            }
                        }
                        if (!mark)
                            this.addreses.Add(new TestAddress(a));
                    }
                    else
                    {
                        this.addreses = new List<TestAddress>();
                        addreses.Add(new TestAddress(a));
                    }
                }
            }
            if (b.Contacts != null)
                foreach (ContactDto c in b.Contacts)
                {
                    TestContact tc = new TestContact(c);
                    if (this.contacts != null)
                    {
                        bool mark = false;
                        foreach (TestContact c1 in this.contacts)
                            if (tc == c1)
                                mark = true;
                        if (!mark)
                            this.contacts.Add(tc);
                    }
                    else
                        this.contacts.Add(tc);
                }
            if (b.Job != null)
                this.job = new TestJob(b.Job);
            if (b.Privilege != null)
                this.privilege = new TestPrivilege(b.Privilege);
            if (b.BirthPlace != null)
                this.birthplace = new TestBirthplace(b.BirthPlace);
        }

        public bool CheckPatientInDataBase()
        {
            TestPatient p = TestPatient.BuildPatientFromDataBaseData(GUID, IDLPU, patient.IdPatientMIS);
            this.Equals(p);
            return (this == p);
        }

        public void DeletePatient()
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findIdPersonString =
                    "SELECT * FROM ExternalId WHERE IdPersonMIS = '" + patient.IdPatientMIS + "'";
                SqlCommand command = new SqlCommand(findIdPersonString, connection);
                using (SqlDataReader MISreader = command.ExecuteReader())
                {
                    string patientId = GetPatientId(GUID, IDLPU, patient.IdPatientMIS);
                    while ((MISreader.Read()) && (patientId != null))
                    {
                        string command2 = "EXEC dbo.Delete_Patient @IdPatientMIS = '" + patient.IdPatientMIS + "'";
                        using (SqlConnection connection2 = Global.GetSqlConnection())
                        {
                            var SqlComm = new SqlCommand(command2, connection2);
                            SqlComm.ExecuteNonQuery();
                        }
                        patientId = GetPatientId(GUID, IDLPU, patient.IdPatientMIS);
                    }
                }
            }
        }
        public override bool Equals(Object obj)
        {
            TestPatient p = obj as TestPatient;
            if ((object)p == null)
            {
                return false;
            }
            if ((Global.IsEqual(this.adds, p.adds)) &&
                (this.patient.BirthDate == p.patient.BirthDate) &&
                (Global.IsEqual(this.conts, p.conts)) &&
                (this.patient.DeathTime == p.patient.DeathTime) &&
                (Global.IsEqual(this.docs, p.docs)) &&
                (this.patient.FamilyName == p.patient.FamilyName) &&
                (this.patient.GivenName == p.patient.GivenName) &&
                (this.patient.IdBloodType == p.patient.IdBloodType) &&
                (this.patient.IdGlobal == p.patient.IdGlobal) &&
                (this.patient.IdLivingAreaType == p.patient.IdLivingAreaType) &&
                (this.patient.IsVip == p.patient.IsVip) &&
                (Global.IsEqual(this.job, p.job)) &&
                (this.patient.MiddleName == p.patient.MiddleName) &&
                (Global.IsEqual(this.privilege, p.privilege)) &&
                (this.patient.Sex == p.patient.Sex) &&
                (this.patient.SocialGroup == p.patient.SocialGroup) &&
                (this.patient.SocialStatus == p.patient.SocialStatus) &&
                Global.IsEqual(this.birthplace, p.birthplace))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestPatient");
                return false;
            }
        }
        public static bool operator ==(TestPatient a, TestPatient b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestPatient a, TestPatient b)
        {
            return !(a.Equals(b));
        }
    }
}
