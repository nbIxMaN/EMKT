using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDispensaryOne : TestMedRecord
    {
        private const string idDispensaryOne = "5";
        DispensaryOne dispansaryOne;
        TestAttachment attachment;
        TestDoctor doctor;
        TestDoctor hdDoctor;
        List<TestRecommendation> recom;
        public Array recommendation
        {
            get
            {
                if (recom != null)
                    return recom.ToArray();
                else
                    return null;
            }
        }
        public TestDispensaryOne(DispensaryOne r, string idLpu = "")
        {
            if (r != null)
            {
                dispansaryOne = r;
                attachment = new TestAttachment(r.Attachment);
                doctor = new TestDoctor(r.Author, idLpu);
                hdDoctor = new TestDoctor(r.HealthGroup.Doctor, idLpu);
                if (r.Recommendations != null)
                {
                    recom = new List<TestRecommendation>();
                    foreach(Recommendation i in r.Recommendations)
                    {
                        recom.Add(new TestRecommendation(i, idLpu));
                    }
                }
            }
        }
        static public TestDispensaryOne BuildSickListFromDataBaseData(string idStep)
        {
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idDispensaryOne);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        using (SqlConnection connection = Global.GetSqlConnection())
                        {
                            string findDO = "SELECT TOP(1) * FROM DispensaryStage1, DispensaryStage1HealthGroup WHERE DispensaryStage1.idDispensaryStage1 = (SELECT MAX(idDispensaryStage1) FROM DispensaryStage1 WHERE IdMedDocument = '" + i.idMedDocument + "') AND DispensaryStage1.idDispensaryStage1 = DispensaryStage1HealthGroup.idDispensaryStage1";
                            SqlCommand DOcommand = new SqlCommand(findDO, connection);
                            using (SqlDataReader DOReader = DOcommand.ExecuteReader())
                            {
                                while (DOReader.Read())
                                {
                                    DispensaryOne r = new DispensaryOne();
                                    r.HealthGroup = new HealthGroup();
                                    r.HealthGroup.HealthGroupInfo = new HealthGroupInfo();
                                    if (DOReader["IsGuested"].ToString() != "")
                                        r.IsGuested = Convert.ToBoolean(DOReader["IsGuested"]);
                                    if (DOReader["HasExtraResearchRefferal"].ToString() != "")
                                        r.HasExtraResearchRefferal = Convert.ToBoolean(DOReader["HasExtraResearchRefferal"]);
                                    if (DOReader["IsUnderObservation"].ToString() != "")
                                        r.IsUnderObservation = Convert.ToBoolean(DOReader["IsUnderObservation"]);
                                    if (DOReader["HasExpertCareRefferal"].ToString() != "")
                                        r.HasExpertCareRefferal = Convert.ToBoolean(DOReader["HasExpertCareRefferal"]);
                                    if (DOReader["HasPrescribeCure"].ToString() != "")
                                        r.HasPrescribeCure = Convert.ToBoolean(DOReader["HasPrescribeCure"]);
                                    if (DOReader["HasHealthResortRefferal"].ToString() != "")
                                        r.HasHealthResortRefferal = Convert.ToBoolean(DOReader["HasHealthResortRefferal"]);
                                    if (DOReader["HasSecondStageRefferal"].ToString() != "")
                                        r.HasSecondStageRefferal = Convert.ToBoolean(DOReader["HasSecondStageRefferal"]);
                                    if (DOReader["IdHealthGroup"].ToString() != "")
                                        r.HealthGroup.HealthGroupInfo.IdHealthGroup = Convert.ToByte(DOReader["IdHealthGroup"]);
                                    if (DOReader["Date"].ToString() != "")
                                        r.HealthGroup.HealthGroupInfo.Date = Convert.ToDateTime(DOReader["Date"]);
                                    r.Header = i.DocHead;
                                    r.CreationDate = i.CreationDate;
                                    TestDispensaryOne tdo = new TestDispensaryOne(r);
                                    if (DOReader["IdDoctor"].ToString() != "")
                                        tdo.hdDoctor = TestDoctor.BuildTestDoctorFromDataBase((DOReader["IdDoctor"]).ToString());
                                    if (DOReader["idDispensaryStage1"].ToString() != "")
                                        tdo.recom = TestRecommendation.BuildSickListFromDataBaseData(DOReader["idDispensaryStage1"].ToString());
                                    tdo.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                                    tdo.attachment = i;
                                    return tdo;
                                }
                            }
                        }
                    }
            }
            return null;
        }
        private void FindMismatch(TestDispensaryOne r)
        {
            if (this.dispansaryOne.CreationDate != r.dispansaryOne.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestDispensaryOne");
            if (this.dispansaryOne.HasExpertCareRefferal != r.dispansaryOne.HasExpertCareRefferal)
                Global.errors3.Add("Несовпадение HasExpertCareRefferal TestDispensaryOne");
            if (this.dispansaryOne.HasExtraResearchRefferal != r.dispansaryOne.HasExtraResearchRefferal)
                Global.errors3.Add("Несовпадение HasExtraResearchRefferal TestDispensaryOne");
            if (this.dispansaryOne.HasHealthResortRefferal != r.dispansaryOne.HasHealthResortRefferal)
                Global.errors3.Add("Несовпадение HasHealthResortRefferal TestDispensaryOne");
            if (this.dispansaryOne.HasPrescribeCure != r.dispansaryOne.HasPrescribeCure)
                Global.errors3.Add("Несовпадение HasPrescribeCure TestDispensaryOne");
            if (this.dispansaryOne.HasSecondStageRefferal != r.dispansaryOne.HasSecondStageRefferal)
                Global.errors3.Add("Несовпадение HasSecondStageRefferal TestDispensaryOne");
            if (this.dispansaryOne.Header != r.dispansaryOne.Header)
                Global.errors3.Add("Несовпадение CreationDate TestDispensaryOne");
            if (this.dispansaryOne.IsGuested != r.dispansaryOne.IsGuested)
                Global.errors3.Add("Несовпадение IsGuested TestDispensaryOne");
            if (this.dispansaryOne.IsUnderObservation != r.dispansaryOne.IsUnderObservation)
                Global.errors3.Add("Несовпадение IsUnderObservation TestDispensaryOne");
            if (this.dispansaryOne.HealthGroup.HealthGroupInfo.Date != r.dispansaryOne.HealthGroup.HealthGroupInfo.Date)
                Global.errors3.Add("Несовпадение HealthGroupInfo.Date TestDispensaryOne");
            if (this.dispansaryOne.HealthGroup.HealthGroupInfo.IdHealthGroup != r.dispansaryOne.HealthGroup.HealthGroupInfo.IdHealthGroup)
                Global.errors3.Add("Несовпадение HealthGroupInfo.IdHealthGroup TestDispensaryOne");
            if (Global.GetLength(this.recommendation) != Global.GetLength (r.recommendation))
                Global.errors3.Add("Несовпадение длины recommendation TestDispensaryOne");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestDispensaryOne");
            if (Global.GetLength(this.hdDoctor) != Global.GetLength(r.hdDoctor))
                Global.errors3.Add("Несовпадение длины hdDoctor TestDispensaryOne");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длины attachment TestDispensaryOne");
        }
        public override bool Equals(Object obj)
        {
            TestDispensaryOne p = obj as TestDispensaryOne;
            if ((object)p == null)
            {
                return false;
            }
            if (this.dispansaryOne == p.dispansaryOne)
                return true;
            if ((this.dispansaryOne == null) || (p.dispansaryOne == null))
            {
                return false;
            }
            if ((this.dispansaryOne.CreationDate == p.dispansaryOne.CreationDate)&&
            (this.dispansaryOne.HasExpertCareRefferal == p.dispansaryOne.HasExpertCareRefferal)&&
            (this.dispansaryOne.HasExtraResearchRefferal == p.dispansaryOne.HasExtraResearchRefferal)&&
            (this.dispansaryOne.HasHealthResortRefferal == p.dispansaryOne.HasHealthResortRefferal)&&
            (this.dispansaryOne.HasPrescribeCure == p.dispansaryOne.HasPrescribeCure)&&
            (this.dispansaryOne.HasSecondStageRefferal == p.dispansaryOne.HasSecondStageRefferal)&&
            (this.dispansaryOne.Header == p.dispansaryOne.Header)&&
            (this.dispansaryOne.IsGuested == p.dispansaryOne.IsGuested)&&
            (this.dispansaryOne.IsUnderObservation == p.dispansaryOne.IsUnderObservation)&&
            (this.dispansaryOne.HealthGroup.HealthGroupInfo.Date == p.dispansaryOne.HealthGroup.HealthGroupInfo.Date)&&
            (this.dispansaryOne.HealthGroup.HealthGroupInfo.IdHealthGroup == p.dispansaryOne.HealthGroup.HealthGroupInfo.IdHealthGroup)&&
            (Global.IsEqual(this.attachment, p.attachment))&&
            (Global.IsEqual(this.doctor, p.doctor))&&
            (Global.IsEqual(this.hdDoctor, p.hdDoctor))&&
            (Global.IsEqual(this.recommendation, p.recommendation)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDispensaryOne");
                return false;
            }
        }
        public static bool operator ==(TestDispensaryOne a, TestDispensaryOne b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDispensaryOne a, TestDispensaryOne b)
        {
            return !(a.Equals(b));
        }
    }
}
