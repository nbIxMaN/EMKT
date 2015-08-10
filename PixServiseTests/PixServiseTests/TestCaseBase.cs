using PixServiseTests.EMKServise;
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
        private const string masterCaseId = "1";
        public string GUID;
        public CaseBase caseBase;
        public TestDoctor doctorInCharge;
        public TestGuardian guardian;
        public TestPatient patient;
        public TestParticipant autor;
        public TestParticipant authenticator;
        public TestParticipant legalAuthenticator;
        public int idCaseType;

        public TestCaseBase(string guid, CaseBase cb)
        {
            GUID = guid.ToLower();
            if (cb != null)
            {
                caseBase = cb;
                if (caseBase.OpenDate == null)
                    caseBase.OpenDate = DateTime.MinValue;
                if (caseBase.CloseDate == null)
                    caseBase.CloseDate = DateTime.MinValue;
            }
            if (cb.Author != null)
            {
                autor = new TestParticipant(cb.Author, cb.IdLpu);
                if (autor.doctor.doctor.IdLpu == null)
                    autor.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.Authenticator != null)
            {
                authenticator = new TestParticipant(cb.Authenticator, cb.IdLpu);
                if (authenticator.doctor.doctor.IdLpu == null)
                    authenticator.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.LegalAuthenticator != null)
            {
                legalAuthenticator = new TestParticipant(cb.LegalAuthenticator, cb.IdLpu);
                if (legalAuthenticator.doctor.doctor.IdLpu == null)
                    legalAuthenticator.doctor.doctor.IdLpu = cb.IdLpu;
            }
            if (cb.DoctorInCharge != null)
            {
                doctorInCharge = new TestDoctor(cb.DoctorInCharge, cb.IdLpu);
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
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string InstId = Global.GetIdInstitution(idlpu);
                if (mis != "")
                    findIdCaseString =
                    "SELECT TOP(1) * FROM \"Case\" WHERE IdCase = (SELECT MAX(IdCase) FROM \"Case\" WHERE IdCaseMIS = '" + mis + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "' AND IdPerson = '" + patientId + "')";
                else
                    findIdCaseString =
                    "SELECT TOP(1) * FROM \"Case\" WHERE IdCase = (SELECT MAX(IdCase) FROM \"Case\" WHERE IdCaseType = '" + masterCaseId + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "' AND IdPerson = '" + patientId + "')";
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
                    int idCaseT = 0;
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
                            if (caseReader["IdCaseType"].ToString() != "")
                                idCaseT = Convert.ToInt32(caseReader["IdCaseType"]);
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
                    cb.idCaseType = idCaseT;
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
                                    cb.autor = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]), 1);
                                    break;
                                case "3":
                                    cb.authenticator = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]), 3);
                                    break;
                                case "4":
                                    cb.legalAuthenticator = TestParticipant.BuildTestParticipantFromDataBase(caseId, Convert.ToString(caseReader["IdDoctor"]), 4);
                                    break;
                            }
                        }
                    }
                    return cb;
                }
            }
            return null;
        }

        public void UpdateCaseBase(string guid, CaseBase cb)
        {
            if (guid != "")
                GUID = guid.ToLower();
            if (cb != null)
            {
                if (cb.CloseDate != null)
                    this.caseBase.CloseDate = cb.CloseDate;
                if (cb.Comment != null)
                    this.caseBase.Comment = cb.Comment;
                if (cb.Confidentiality != null)
                    this.caseBase.Confidentiality = cb.Confidentiality;
                if (cb.CuratorConfidentiality != null)
                    this.caseBase.CuratorConfidentiality = cb.CuratorConfidentiality;
                if (cb.DoctorConfidentiality != null)
                    this.caseBase.DoctorConfidentiality = cb.DoctorConfidentiality;
                if (cb.HistoryNumber != null)
                    this.caseBase.HistoryNumber = cb.HistoryNumber;
                if (cb.IdCaseAidType != 0)
                    this.caseBase.IdCaseAidType = cb.IdCaseAidType;
                if (cb.IdCaseMis != null)
                    this.caseBase.IdCaseMis = cb.IdCaseMis;
                if (cb.IdCaseResult != null)
                    this.caseBase.IdCaseResult = cb.IdCaseResult;
                if (cb.IdLpu != null)
                    this.caseBase.IdLpu = cb.IdLpu;
                if (cb.IdPaymentType != null)
                    this.caseBase.IdPaymentType = cb.IdPaymentType;
                if (cb.OpenDate != null)
                    this.caseBase.OpenDate = cb.OpenDate;
                if (cb.Author != null)
                {
                    autor = new TestParticipant(cb.Author, cb.IdLpu);
                    if (autor.doctor.doctor.IdLpu == null)
                        autor.doctor.doctor.IdLpu = cb.IdLpu;
                }
                if (cb.Authenticator != null)
                {
                    authenticator = new TestParticipant(cb.Authenticator, cb.IdLpu);
                    if (authenticator.doctor.doctor.IdLpu == null)
                        authenticator.doctor.doctor.IdLpu = cb.IdLpu;
                }
                if (cb.LegalAuthenticator != null)
                {
                    legalAuthenticator = new TestParticipant(cb.LegalAuthenticator, cb.IdLpu);
                    if (legalAuthenticator.doctor.doctor.IdLpu == null)
                        legalAuthenticator.doctor.doctor.IdLpu = cb.IdLpu;
                }
                if (cb.DoctorInCharge != null)
                {
                    doctorInCharge = new TestDoctor(cb.DoctorInCharge, cb.IdLpu);
                    if (doctorInCharge.doctor.IdLpu == null)
                        doctorInCharge.doctor.IdLpu = cb.IdLpu;
                }
                if (cb.Guardian != null)
                    guardian = new TestGuardian(cb.Guardian);
                if (cb.IdPatientMis != null)
                    patient = TestPatient.BuildPatientFromDataBaseData(guid, cb.IdLpu, cb.IdPatientMis);
            }
        }

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
            if (Global.GetLength(this.guardian) != Global.GetLength(cb.guardian))
                Global.errors3.Add("несовпадение длины guardian TestCaseBase");
            if (Global.GetLength(this.patient) != Global.GetLength(cb.patient))
                Global.errors3.Add("несовпадение длины patient TestCaseBase");
            if (Global.GetLength(this.autor) != Global.GetLength(cb.autor))
                Global.errors3.Add("несовпадение длины autor TestCaseBase");
            if (Global.GetLength(this.authenticator) != Global.GetLength(cb.authenticator))
                Global.errors3.Add("несовпадение длины authenticator TestCaseBase");
            if (Global.GetLength(this.legalAuthenticator) != Global.GetLength(cb.legalAuthenticator))
                Global.errors3.Add("несовпадение длины legalAuthenticator TestCaseBase");
            
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
                Global.errors3.Add("несовпадение TestCaseBase");
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
