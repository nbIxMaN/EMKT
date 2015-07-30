﻿using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestCaseBase
    {
        public string GUID;
        public CaseBase caseBase;
        public TestDoctor doctorInCharge;
        public TestGuardian guardian;
        public TestPatient patient;
        public TestParticipant autor;
        public TestParticipant authenticator;
        public TestParticipant legalAuthenticator;

        public TestCaseBase(string guid, CaseBase cb)
        {
            GUID = guid.ToLower();
            if (cb != null)
                caseBase = cb;
            if (cb.Author != null)
            {
                autor = new TestParticipant(cb.Author);
                if (autor.doctor.doctor.IdLpu == null)
                    autor.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.Authenticator != null)
            {
                authenticator = new TestParticipant(cb.Authenticator);
                if (authenticator.doctor.doctor.IdLpu == null)
                    authenticator.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.LegalAuthenticator != null)
            {
                legalAuthenticator = new TestParticipant(cb.LegalAuthenticator);
                if (legalAuthenticator.doctor.doctor.IdLpu == null)
                    legalAuthenticator.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.DoctorInCharge != null)
            {
                doctorInCharge = new TestDoctor(cb.DoctorInCharge);
                if (doctorInCharge.doctor.IdLpu == null)
                    doctorInCharge.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.Guardian != null)
                guardian = new TestGuardian(cb.Guardian);
            if (cb.IdPatientMis != null)
                patient = TestPatient.BuildPatientFromDataBaseData(guid, cb.IdLpu, cb.IdPatientMis);
        }

        static public string GetCaseId(string guid, string idlpu, string mis, string patientId)
        {
            string caseId = "";
            string findIdCaseString = "";
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
                    findIdCaseString =
                        "SELECT TOP(1) * FROM \"Case\" WHERE IdCaseMIS = '" + mis + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "' AND IdPerson = '" + patientId + "'";
                }
                SqlCommand command = new SqlCommand(findIdCaseString, connection);
                using (SqlDataReader IdCaseReader = command.ExecuteReader())
                {
                    while (IdCaseReader.Read())
                    {
                        caseId = IdCaseReader["IdCase"].ToString();
                    }
                }
            }
            if (caseId != "")
                return caseId;
            else
                return null;
        }

        static public TestCaseBase BuildBaseCaseFromDataBaseData(string guid, string idlpu, string mis, string idPerson)
        {
            string caseId = GetCaseId(guid, idlpu, mis, idPerson);
            if (caseId != null)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string IdDoctor = "";
                    string IdGuardian = "";
                    CaseBase p = new CaseBase();
                    string findCase = "SELECT TOP(1) * FROM \"Case\" WHERE IdCase = '" + caseId + "'";
                    SqlCommand caseCommand = new SqlCommand(findCase, connection);
                    using (SqlDataReader caseReader = caseCommand.ExecuteReader())
                    {
                        while (caseReader.Read())
                        {
                            if (caseReader["CloseDate"].ToString() != "")
                                p.CloseDate = Convert.ToDateTime(caseReader["CloseDate"]);
                            else
                                p.CloseDate = DateTime.MinValue;
                            if (caseReader["Comment"].ToString() != "")
                                p.Comment = Convert.ToString(caseReader["Comment"]);
                            else
                                p.Comment = null;
                            if (caseReader["HistoryNumber"].ToString() != "")
                                p.HistoryNumber = Convert.ToString(caseReader["HistoryNumber"]);
                            else
                                p.HistoryNumber = null;
                            if (caseReader["IdCaseAidType"].ToString() != "")
                                p.IdCaseAidType = Convert.ToByte(caseReader["IdCaseAidType"]);
                            else
                                p.IdCaseAidType = 0;
                            if (caseReader["IdCaseMis"].ToString() != "")
                                p.IdCaseMis = Convert.ToString(caseReader["IdCaseMis"]);
                            else
                                p.IdCaseMis = null;
                            if (caseReader["IdCaseResult"].ToString() != "")
                                p.IdCaseResult = Convert.ToByte(caseReader["IdCaseResult"]);
                            else
                                p.IdCaseResult = 0;
                            if (caseReader["IdCasePaymentType"].ToString() != "")
                                p.IdPaymentType = Convert.ToByte(caseReader["IdCasePaymentType"]);
                            else
                                p.IdPaymentType = 0;
                            if (caseReader["OpenDate"].ToString() != "")
                                p.OpenDate = Convert.ToDateTime(caseReader["OpenDate"]);
                            else
                                p.OpenDate = DateTime.MinValue;
                            if (caseReader["IDDoctor"].ToString() != "")
                                IdDoctor = Convert.ToString(caseReader["IDDoctor"]);
                            if (caseReader["IdGuardian"].ToString() != "")
                                IdGuardian = Convert.ToString(caseReader["IDGuardian"]);
                        }
                    }
                    findCase = "SELECT * FROM mm_AccessRole2Case WHERE IdCase = '" + caseId + "'";
                    caseCommand = new SqlCommand(findCase, connection);
                    using (SqlDataReader caseReader = caseCommand.ExecuteReader())
                    {
                        while (caseReader.Read())
                        {
                            switch (caseReader["IdAccessRole"].ToString())
                            {
                                case "1":
                                    p.Confidentiality = Convert.ToByte(caseReader["IdPrivacyLevel"]);
                                    break;
                                case "2":
                                    p.DoctorConfidentiality = Convert.ToByte(caseReader["IdPrivacyLevel"]);
                                    break;
                                case "3":
                                    p.CuratorConfidentiality = Convert.ToByte(caseReader["IdPrivacyLevel"]);
                                    break;
                            }
                        }
                    }
                    p.IdCaseMis = mis;
                    p.IdLpu = idlpu;
                    TestCaseBase cb = new TestCaseBase(guid, p);
                    if (IdDoctor != "")
                        cb.doctorInCharge = TestDoctor.BuildTestDoctorFromDataBase(IdDoctor);
                    else
                        cb.doctorInCharge = null;
                    if (IdGuardian != "")
                        cb.guardian = TestGuardian.BuildTestGuardianFromDataBase(IdGuardian, idPerson);
                    else
                        cb.guardian = null;
                    if (idPerson != "")
                        cb.patient = TestPatient.BuildPatientFromDataBaseData(patientId: idPerson);
                    else
                        cb.patient = null;
                    findCase = "SELECT * FROM mm_Author2Case WHERE IdCase = '" + caseId + "'";
                    caseCommand = new SqlCommand(findCase, connection);
                    using (SqlDataReader caseReader = caseCommand.ExecuteReader())
                    {
                        while (caseReader.Read())
                        {
                            switch (caseReader["IdAuthorshipType"].ToString())
                            {
                                case "1":
                                    cb.autor = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]));
                                    break;
                                case "3":
                                    cb.authenticator = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]));
                                    break;
                                case "4":
                                    cb.legalAuthenticator = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]));
                                    break;
                            }
                        }
                    }
                    return cb;
                }
            }
            return null;
        }

        //private string PatientFieldToString(Object a)
        //{
        //    if (a == null)
        //        return ("");
        //    else
        //        return a.ToString();
        //}

        public void FindMismatch(TestCaseBase cb)
        {
            if (this.caseBase.CloseDate != cb.caseBase.CloseDate)
                Global.errors3.Add("несовпадение CloseDate TestCaseBase");
            if (this.caseBase.Comment != cb.caseBase.Comment)
                Global.errors3.Add("несовпадение Comment TestCaseBase");
            if (this.caseBase.Confidentiality != cb.caseBase.Confidentiality)
                Global.errors3.Add("несовпадение Confidentiality TestCaseBase");
            if (this.caseBase.CuratorConfidentiality != cb.caseBase.CuratorConfidentiality)
                Global.errors3.Add("несовпадение CuratorConfidentiality TestCaseBase");
            if (this.caseBase.DoctorConfidentiality != cb.caseBase.DoctorConfidentiality)
                Global.errors3.Add("несовпадение DoctorConfidentiality TestCaseBase");
            if (this.caseBase.HistoryNumber != cb.caseBase.HistoryNumber)
                Global.errors3.Add("несовпадение HistoryNumber TestCaseBase");
            if (this.caseBase.IdCaseAidType != cb.caseBase.IdCaseAidType)
                Global.errors3.Add("несовпадение IdCaseAidType TestCaseBase");
            if (this.caseBase.IdCaseMis != cb.caseBase.IdCaseMis)
                Global.errors3.Add("несовпадение IdCaseMis TestCaseBase");
            if (this.caseBase.IdCaseResult != cb.caseBase.IdCaseResult)
                Global.errors3.Add("несовпадение IdCaseResult TestCaseBase");
            if (this.caseBase.IdLpu != cb.caseBase.IdLpu)
                Global.errors3.Add("несовпадение IdLpu TestCaseBase");
            if (this.caseBase.IdPaymentType != cb.caseBase.IdPaymentType)
                Global.errors3.Add("несовпадение IdPaymentType TestCaseBase");
            if (this.caseBase.OpenDate != cb.caseBase.OpenDate)
                Global.errors3.Add("несовпадение OpenDate TestCaseBase");
            if (!Global.IsEqual(this.doctorInCharge, cb.doctorInCharge))
                Global.errors3.Add("несовпадение doctorInCharge TestCaseBase");
            if (!Global.IsEqual(this.guardian, cb.guardian))
                Global.errors3.Add("несовпадение guardian TestCaseBase");
            if (!Global.IsEqual(this.patient, cb.patient))
                Global.errors3.Add("несовпадение patient TestCaseBase");
            if (!Global.IsEqual(this.authenticator, cb.authenticator))
                Global.errors3.Add("несовпадение authenticator TestCaseBase");
            if (!Global.IsEqual(this.autor, cb.autor))
                Global.errors3.Add("несовпадение autor TestCaseBase");
            if (!Global.IsEqual(this.legalAuthenticator, cb.legalAuthenticator))
                Global.errors3.Add("несовпадение legalAuthenticator TestCaseBase");      
        }

        public bool CheckCaseBaseInDataBase()
        {
            TestCaseBase p = TestCaseBase.BuildBaseCaseFromDataBaseData(GUID, caseBase.IdLpu, caseBase.IdCaseMis, TestPerson.GetPersonId(GUID, caseBase.IdLpu, caseBase.IdCaseMis));
            return (this == p);
        }

        public override bool Equals(Object obj)
        {
            TestCaseBase p = obj as TestCaseBase;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestCaseBase с другим типом");
                return false;
            }
            if ((Global.IsEqual(this.doctorInCharge, p.doctorInCharge)) &&
                (this.caseBase.CloseDate == p.caseBase.CloseDate) &&
                (Global.IsEqual(this.guardian, p.guardian)) &&
                (Global.IsEqual(this.patient, p.patient)) &&
                (Global.IsEqual(this.authenticator, p.authenticator)) &&
                (Global.IsEqual(this.autor, p.autor)) &&
                (Global.IsEqual(this.legalAuthenticator, p.legalAuthenticator)) &&
                (this.caseBase.Comment == p.caseBase.Comment) &&
                (this.caseBase.Confidentiality == p.caseBase.Confidentiality) &&
                (this.caseBase.CuratorConfidentiality == p.caseBase.CuratorConfidentiality) &&
                (this.caseBase.DoctorConfidentiality == p.caseBase.DoctorConfidentiality) &&
                (this.caseBase.HistoryNumber == p.caseBase.HistoryNumber) &&
                (this.caseBase.IdCaseAidType == p.caseBase.IdCaseAidType) &&
                //(this.caseBase.IdPatientMIS == p.caseBase.IdPatientMIS) &&
                (this.caseBase.IdCaseMis == p.caseBase.IdCaseMis) &&
                (this.caseBase.IdCaseResult == p.caseBase.IdCaseResult) &&
                (this.caseBase.IdLpu == p.caseBase.IdLpu) &&
                (this.caseBase.IdPaymentType == p.caseBase.IdPaymentType) &&
                (this.caseBase.OpenDate == p.caseBase.OpenDate))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestCaseBase a, TestCaseBase b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestCaseBase a, TestCaseBase b)
        {
            return !(a.Equals(b));
        }
    }
}