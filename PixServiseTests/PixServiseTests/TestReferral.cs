using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestReferral : TestMedRecord
    {
        private const string idReferral = "6";
        Referral referral;
        TestAttachment attachment;
        TestDoctor doctor;
        TestDoctor departmentHead;
        public TestReferral(Referral r, string idLpu = "")
        {
            referral = r;
            attachment = new TestAttachment(r.Attachment);
            doctor = new TestDoctor(r.Author, idLpu);
            departmentHead = new TestDoctor(r.DepartmentHead, idLpu);
        }
        static public List<TestReferral> BuildReferralFromDataBaseData(string idStep)
        {
            List<TestReferral> trl = new List<TestReferral>();
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idReferral);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        using (SqlConnection connection = Global.GetSqlConnection())
                        {
                            string findR = "SELECT * FROM Referral WHERE IdMedDocument = '" + i.idMedDocument + "'";
                            SqlCommand Rcommand = new SqlCommand(findR, connection);
                            using (SqlDataReader RReader = Rcommand.ExecuteReader())
                            {
                                while (RReader.Read())
                                {
                                    Referral r = new Referral();
                                    r.ReferralInfo = new ReferralInfo();
                                    if (RReader["MkbCode"].ToString() != "")
                                        r.ReferralInfo.MkbCode = Convert.ToString(RReader["MkbCode"]);
                                    if (RReader["IdSourceLpu"].ToString() != "")
                                        r.IdSourceLpu = Global.GetIdIdLpu(Convert.ToString(RReader["IdSourceLpu"]));
                                    if (RReader["IdTargetLpu"].ToString() != "")
                                        r.IdTargetLpu = Global.GetIdIdLpu(Convert.ToString(RReader["IdTargetLpu"]));
                                    if (RReader["Reason"].ToString() != "")
                                        r.ReferralInfo.Reason = Convert.ToString(RReader["Reason"]);
                                    if (RReader["IdReferralMIS"].ToString() != "")
                                        r.ReferralInfo.IdReferralMis = Convert.ToString(RReader["IdReferralMIS"]);
                                    if (RReader["IdReferralType"].ToString() != "")
                                        r.ReferralInfo.IdReferralType = Convert.ToByte(RReader["IdReferralType"]);
                                    if (RReader["IssuedDateTime"].ToString() != "")
                                        r.ReferralInfo.IssuedDateTime = Convert.ToDateTime(RReader["IssuedDateTime"]);
                                    if (RReader["IdHospitalizationOrder"].ToString() != "")
                                        r.ReferralInfo.HospitalizationOrder = Convert.ToByte(RReader["IdHospitalizationOrder"]);
                                    if (RReader["ReferralId"].ToString() != "")
                                        r.ReferralID = Convert.ToString(RReader["ReferralId"]);
                                    if (RReader["RelatedId"].ToString() != "")
                                        r.RelatedID = Convert.ToString(RReader["RelatedId"]);
                                    r.CreationDate = i.CreationDate;
                                    r.Header = i.DocHead;
                                    //Тут нет ReferalId и referedId
                                    TestReferral tr = new TestReferral(r);
                                    tr.attachment = i;
                                    if (RReader["IdRefDepartmentHead"].ToString() != "")
                                        tr.departmentHead = TestDoctor.BuildTestDoctorFromDataBase((RReader["IdRefDepartmentHead"]).ToString());
                                    tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                                    trl.Add(tr);
                                }
                            }
                        }
                    }
            }
            if (trl.Count != 0)
                return trl;
            else
                return null;
        }
        private void FindMismatch(TestReferral r)
        {
            if (this.referral.CreationDate != r.referral.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestReferral");
            if (this.referral.Header != r.referral.Header)
                Global.errors3.Add("Несовпадение Header TestReferral");
            if (this.referral.IdSourceLpu != r.referral.IdSourceLpu)
                Global.errors3.Add("Несовпадение IdSourceLpu TestReferral");
            if (this.referral.IdTargetLpu != r.referral.IdTargetLpu)
                Global.errors3.Add("Несовпадение IdTargetLpu TestReferral");
            if (this.referral.ReferralID != r.referral.ReferralID)
                Global.errors3.Add("Несовпадение ReferralID TestReferral");
            if (this.referral.ReferralInfo.HospitalizationOrder != r.referral.ReferralInfo.HospitalizationOrder)
                Global.errors3.Add("Несовпадение HospitalizationOrderTestReferral");
            if (this.referral.ReferralInfo.IdReferralMis != r.referral.ReferralInfo.IdReferralMis)
                Global.errors3.Add("Несовпадение IdReferralMis TestReferral");
            if (this.referral.ReferralInfo.IdReferralType != r.referral.ReferralInfo.IdReferralType)
                Global.errors3.Add("Несовпадение IdReferralType TestReferral");
            if (this.referral.ReferralInfo.IssuedDateTime != r.referral.ReferralInfo.IssuedDateTime)
                Global.errors3.Add("Несовпадение IssuedDateTime TestReferral");
            if (this.referral.ReferralInfo.MkbCode != r.referral.ReferralInfo.MkbCode)
                Global.errors3.Add("Несовпадение MkbCode TestReferral");
            if (this.referral.ReferralInfo.Reason != r.referral.ReferralInfo.Reason)
                Global.errors3.Add("Несовпадение Reason TestReferral");
            if (Global.GetLength(this.departmentHead) != Global.GetLength(r.departmentHead))
                Global.errors3.Add("Несовпадение длинны departmentHead TestReferral");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длинны doctor TestReferral");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длинны attachment TestReferral");
        }
        public override bool Equals(Object obj)
        {
            TestReferral p = obj as TestReferral;
            if ((object)p == null)
            {
                return false;
            }
            if (this.referral == p.referral)
                return true;
            if ((this.referral == null) || (p.referral== null))
            {
                return false;
            }
            if ((this.referral.CreationDate == p.referral.CreationDate)&&
            (this.referral.Header == p.referral.Header)&&
            (this.referral.IdSourceLpu == p.referral.IdSourceLpu)&&
            (this.referral.IdTargetLpu == p.referral.IdTargetLpu)&&
            (this.referral.ReferralID == p.referral.ReferralID)&&
            (this.referral.ReferralInfo.HospitalizationOrder == p.referral.ReferralInfo.HospitalizationOrder)&&
            (this.referral.ReferralInfo.IdReferralMis == p.referral.ReferralInfo.IdReferralMis)&&
            (this.referral.ReferralInfo.IdReferralType == p.referral.ReferralInfo.IdReferralType)&&
            (this.referral.ReferralInfo.IssuedDateTime == p.referral.ReferralInfo.IssuedDateTime)&&
            (this.referral.ReferralInfo.MkbCode == p.referral.ReferralInfo.MkbCode)&&
            (this.referral.ReferralInfo.Reason == p.referral.ReferralInfo.Reason)&&
            Global.IsEqual(this.departmentHead, p.departmentHead)&&
            Global.IsEqual(this.doctor, p.doctor)&&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestReferral");
                return false;
            }
        }
        public static bool operator ==(TestReferral a, TestReferral b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestReferral a, TestReferral b)
        {
            return !(a.Equals(b));
        }
    }
}
