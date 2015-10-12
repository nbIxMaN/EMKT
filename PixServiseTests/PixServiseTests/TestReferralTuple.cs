//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PixServiseTests.EMKServise;

//namespace PixServiseTests
//{
//    class TestReferralTuple
//    {
//        private PatientMinDto patient;
//        private List<TestReferral> refList;

//        private TestReferralTuple() { }
//        public TestReferralTuple(ReferralTupleDto d)
//        {
//            patient = d.Patient;
//            if (d.Referrals == null) return;
//            refList = new List<TestReferral>();
//            foreach (var i in d.Referrals)
//            {
//                refList.Add(new TestReferral(i));
//            }
//        }

//        public static List<TestReferralTuple> BuildTestReferralTuple(byte idReferralType, DateTime startDate, DateTime endDate)
//        {
//            List<TestReferralTuple> list = new List<TestReferralTuple>();
//            using (SqlConnection connection = Global.GetSqlConnection())
//            {
//                foreach (int i in Enum.GetValues(typeof (TestIdentityDocument.docs)))
//                {
//                    string findPatient =
//                        "SELECT * FROM Referral, MedDocument, Step, \"Case\", Person WHERE IdReferralType = '" +
//                        idReferralType +
//                        "' AND IssuedDateTime >= '" + startDate + "' AND IssuedDateTime <= '" + endDate +
//                        "' AND Referral.IdMedDocument = MedDocument.IdMedDocument AND MedDocument.IdStep = Step.IdStep AND Step.IdCase = \"Case\".IdCase AND \"Case\".IdPerson = IdPerson ORDER BY Person.IdPerson";
//                    SqlCommand person = new SqlCommand(findPatient, connection);
//                    using (SqlDataReader documentReader = person.ExecuteReader())
//                    {
//                        string patientId = "";
//                        // bool a = documentReader.Read();
//                        while (documentReader.Read())
//                        {
//                            if (patientId != documentReader["PatientId"].ToString())
//                            {
//                                TestReferralTuple trp = new TestReferralTuple
//                                {
//                                    patient = new PatientMinDto
//                                    {
//                                        FamilyName = documentReader["FamilyName"].ToString(),
//                                        MiddleName = documentReader["MiddleName"].ToString(),
//                                        GivenName = documentReader["GivenName"].ToString(),
//                                        BirthDate = Convert.ToDateTime(documentReader["BirthDate"])
//                                    },
//                                    refList = new List<TestReferral>()
//                                };
//                                trp.refList.AddRange(
//                                    TestReferral.BuildReferralFromDataBaseData(documentReader["IdStep"].ToString()));
//                                list.Add(trp);
//                            }
//                            else
//                            {
//                                list[list.Count - 1].refList.AddRange(
//                                    TestReferral.BuildReferralFromDataBaseData(documentReader["IdStep"].ToString()));
//                            }
//                        }
//                    }
//                }
//            }
//            if (list.Count != 0)
//                return list;
//            else
//                return null;
//        }
//        public override bool Equals(Object obj)
//        {
//            TestReferralTuple p = obj as TestReferralTuple;
//            if ((object)p == null)
//            {
//                return false;
//            }
//            if (this.refList == p.refList)
//                return true;
//            if ((this.refList == null) || (p.refList == null))
//            {
//                return false;
//            }
//            if ((this.patient.BirthDate == p.patient.BirthDate) &&
//            (this.patient.FamilyName == p.patient.FamilyName) &&
//            (this.patient.GivenName == p.patient.GivenName) &&
//            (this.patient.MiddleName == p.patient.MiddleName) &&
//            Global.IsEqual(this.refList, p.refList))
//            {
//                return true;
//            }
//            else
//            {
//                //this.FindMismatch(p);
//                Global.errors3.Add("несовпадение TestReferral");
//                return false;
//            }
//        }
//        public static bool operator ==(TestReferralTuple a, TestReferralTuple b)
//        {
//            return a.Equals(b);
//        }
//        public static bool operator !=(TestReferralTuple a, TestReferralTuple b)
//        {
//            return !(a.Equals(b));
//        }
//    }
//}
