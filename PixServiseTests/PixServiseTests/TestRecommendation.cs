using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestRecommendation
    {
        Recommendation recommendation;
        TestDoctor doctor;
        public TestRecommendation(Recommendation r, string idLpu = "")
        {
            if (r != null)
            {
                recommendation = r;
                doctor = new TestDoctor(r.Doctor, idLpu);
            }
        }
        static public List<TestRecommendation> BuildSickListFromDataBaseData(string idDispensaryStage1)
        {
            List<TestRecommendation> llr = new List<TestRecommendation>();
            if (idDispensaryStage1 != "")
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findR = "SELECT * FROM Recommendation WHERE idDispensaryStage1 = '" + idDispensaryStage1 + "'";
                    SqlCommand Rcommand = new SqlCommand(findR, connection);
                    using (SqlDataReader RReader = Rcommand.ExecuteReader())
                    {
                        while (RReader.Read())
                        {
                            Recommendation r = new Recommendation();
                            if (RReader["Text"].ToString() != "")
                                r.Text = RReader["Text"].ToString();
                            if (RReader["Date"].ToString() != "")
                                r.Date = Convert.ToDateTime(RReader["Date"]);
                            TestRecommendation tr = new TestRecommendation(r);
                            if (RReader["IdDoctor"].ToString() != "")
                                tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(RReader["IdDoctor"].ToString());
                            llr.Add(tr);
                        }
                    }
                }
            }
            if (llr.Count != 0)
                return llr;
            else
                return null;
        }
        private void FindMismatch(TestRecommendation r)
        {
            if (this.recommendation.Date != r.recommendation.Date)
                Global.errors3.Add("Несовпадение Date TestRecommendation");
            if (this.recommendation.Text != r.recommendation.Text)
                Global.errors3.Add("Несовпадение Text TestRecommendation");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestRecommendation");
        }
        public override bool Equals(Object obj)
        {
            TestRecommendation p = obj as TestRecommendation;
            if ((object)p == null)
            {
                return false;
            }
            if (this.recommendation == p.recommendation)
                return true;
            if ((this.recommendation == null) || (p.recommendation == null))
            {
                return false;
            }
            if ((this.recommendation.Date == p.recommendation.Date)&&
            (this.recommendation.Text == p.recommendation.Text)&&
            Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestRecommendation");
                return false;
            }
        }
        public static bool operator ==(TestRecommendation a, TestRecommendation b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestRecommendation a, TestRecommendation b)
        {
            return !(a.Equals(b));
        }

    }
}
