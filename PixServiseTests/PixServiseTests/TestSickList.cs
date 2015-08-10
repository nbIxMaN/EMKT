using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestSickList : TestMedRecord
    {
        private const string idSickList = "4";
        SickList sickList;
        TestAttachment attachment;
        TestDoctor doctor;
        TestGuardian guardian;
        public TestSickList(SickList r, string idLpu = "")
        {
            if (r != null)
            {
                sickList = r;
                attachment = new TestAttachment(r.Attachment);
                doctor = new TestDoctor(r.Author, idLpu);
                guardian = new TestGuardian(r.SickListInfo.Caregiver);
            }
        }
        static public List<TestSickList> BuildSickListFromDataBaseData(string idStep, string patientId)
        {
            List<TestSickList> tsl = new List<TestSickList>();
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idSickList);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        using (SqlConnection connection = Global.GetSqlConnection())
                        {
                            string findSL = "SELECT * FROM SickList WHERE IdMedDocument = '" + i.idMedDocument + "'";
                            SqlCommand SLcommand = new SqlCommand(findSL, connection);
                            using (SqlDataReader SLReader = SLcommand.ExecuteReader())
                            {
                                while (SLReader.Read())
                                {
                                    SickList r = new SickList();
                                    if (SLReader["Number"].ToString() != "")
                                        r.SickListInfo.Number = Convert.ToString(SLReader["Number"]);
                                    if (SLReader["DateStart"].ToString() != "")
                                        r.SickListInfo.DateStart = Convert.ToDateTime(SLReader["DateStart"]);
                                    if (SLReader["DateEnd"].ToString() != "")
                                        r.SickListInfo.DateEnd = Convert.ToDateTime(SLReader["DateEnd"]);
                                    if (SLReader["IdRDisabilityDocStatus"].ToString() != "")
                                        r.SickListInfo.DisabilityDocState = Convert.ToByte(SLReader["IdRDisabilityDocStatus"]);
                                    if (SLReader["IdRDisabilityDocReason"].ToString() != "")
                                        r.SickListInfo.DisabilityDocReason = Convert.ToByte(SLReader["IdRDisabilityDocReason"]);
                                    if (SLReader["IsPatient"].ToString() != "")
                                        r.SickListInfo.IsPatientTaker = Convert.ToBoolean(SLReader["IsPatient"]);
                                    r.CreationDate = i.CreationDate;
                                    r.Header = i.DocHead;
                                    TestSickList tr = new TestSickList(r);
                                    tr.attachment = i;
                                    tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                                    if (SLReader["IdCareGiver"].ToString() != "")
                                        tr.guardian = TestGuardian.BuildTestGuardianFromDataBase(SLReader["IdCareGiver"].ToString(), patientId);
                                    tsl.Add(tr);
                                }
                            }
                        }
                    }
            }
            if (tsl.Count != 0)
                return tsl;
            else
                return null;
        }
        private void FindMismatch(TestSickList r)
        {
            if (this.sickList.CreationDate != r.sickList.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestSickList");
            if (this.sickList.Header != r.sickList.Header)
                Global.errors3.Add("Несовпадение Header TestSickList");
            if (this.sickList.SickListInfo.Caregiver != r.sickList.SickListInfo.Caregiver)
                Global.errors3.Add("Несовпадение Caregiver TestSickList");
            if (this.sickList.SickListInfo.DateEnd != r.sickList.SickListInfo.DateEnd)
                Global.errors3.Add("Несовпадение DateEnd TestSickList");
            if (this.sickList.SickListInfo.DateStart != r.sickList.SickListInfo.DateStart)
                Global.errors3.Add("Несовпадение DateStart TestSickList");
            if (this.sickList.SickListInfo.DisabilityDocReason != r.sickList.SickListInfo.DisabilityDocReason)
                Global.errors3.Add("Несовпадение DisabilityDocReason TestSickList");
            if (this.sickList.SickListInfo.DisabilityDocState!= r.sickList.SickListInfo.DisabilityDocState)
                Global.errors3.Add("Несовпадение DisabilityDocState TestSickList");
            if (this.sickList.SickListInfo.IsPatientTaker != r.sickList.SickListInfo.IsPatientTaker)
                Global.errors3.Add("Несовпадение IsPatientTaker TestSickList");
            if (this.sickList.SickListInfo.Number != r.sickList.SickListInfo.Number)
                Global.errors3.Add("Несовпадение Number TestSickList");
            if (Global.GetLength(this.guardian) != Global.GetLength(r.guardian))
                Global.errors3.Add("Несовпадение длины guardian TestSickList");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestSickList");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длины attachment TestSickList");
        }
        public override bool Equals(Object obj)
        {
            TestSickList p = obj as TestSickList;
            if ((object)p == null)
            {
                return false;
            }
            if (this.sickList == p.sickList)
                return true;
            if ((this.sickList == null) || (p.sickList == null))
            {
                return false;
            }
            if ((this.sickList.CreationDate == p.sickList.CreationDate)&&
            (this.sickList.Header == p.sickList.Header)&&
            (this.sickList.SickListInfo.Caregiver == p.sickList.SickListInfo.Caregiver)&&
            (this.sickList.SickListInfo.DateEnd == p.sickList.SickListInfo.DateEnd)&&
            (this.sickList.SickListInfo.DateStart == p.sickList.SickListInfo.DateStart)&&
            (this.sickList.SickListInfo.DisabilityDocReason == p.sickList.SickListInfo.DisabilityDocReason)&&
            (this.sickList.SickListInfo.DisabilityDocState == p.sickList.SickListInfo.DisabilityDocState)&&
            (this.sickList.SickListInfo.IsPatientTaker == p.sickList.SickListInfo.IsPatientTaker)&&
            (this.sickList.SickListInfo.Number == p.sickList.SickListInfo.Number)&&
            Global.IsEqual(this.guardian, p.guardian)&&
            Global.IsEqual(this.doctor, p.doctor)&&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestSickList");
                return false;
            }
        }
        public static bool operator ==(TestSickList a, TestSickList b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestSickList a, TestSickList b)
        {
            return !(a.Equals(b));
        }

    }
}
