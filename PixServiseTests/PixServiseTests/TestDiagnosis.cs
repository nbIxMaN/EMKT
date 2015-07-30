using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDiagnosis
    {
        TestDiagnosisInfo info;
        Diagnosis document;
        TestDoctor doctor;
        public TestDiagnosis(Diagnosis d)
        {
            if (d != null)
            {
                document = d;
                info = new TestDiagnosisInfo(d.DiagnosisInfo);
                doctor = new TestDoctor(d.Doctor);
            }
        }
        static public List<TestDiagnosis> BuildDiagnosisFromDataBaseDate(string IdParentDiagnosis)
        {
            List<TestDiagnosis> tdList = new List<TestDiagnosis>();
            if (IdParentDiagnosis != "")
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findTD = "SELECT * FROM Diagnosis WHERE IdParentDiagnosis = '" + IdParentDiagnosis + "'";
                    SqlCommand TDcommand = new SqlCommand(findTD, connection);
                    using (SqlDataReader TDReader = TDcommand.ExecuteReader())
                    {
                        while (TDReader.Read())
                        {
                            Diagnosis d = new Diagnosis();
                            TestDiagnosis td = new TestDiagnosis(d);
                            td.info = TestDiagnosisInfo.BuildDiagnosisFromDataBaseDate(TDReader["IdDiagnosis"].ToString());
                            if (TDReader["IdDiagnosedDoctor"].ToString() != "")
                                td.doctor = TestDoctor.BuildTestDoctorFromDataBase(TDReader["IdDiagnosedDoctor"].ToString());
                            tdList.Add(td);
                        }
                    }
                }
            }
            if (tdList.Count != 0)
                return tdList;
            else
                return null;
        }
        private void FindMismatch(TestDiagnosis td)
        {
            if (!Global.IsEqual(this.doctor, td.doctor))
                Global.errors3.Add("Несовпадение doctor TestDiagnosis");
            if (!Global.IsEqual(this.info, td.info))
                Global.errors3.Add("Несовпадение info TestDiagnosis");
        }
        public override bool Equals(Object obj)
        {
            TestDiagnosis p = obj as TestDiagnosis;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравение TestDiagnosis с другим типом");
                return false;
            }
            if (this.info == p.info)
                return true;
            if ((this.info == null) || (p.info == null))
            {
                Global.errors3.Add("Сравнение TestDiagnosis = null с TestDiagnosis != null");
                return false;
            }
            if (Global.IsEqual(this.info, p.info)&&
                Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestDiagnosis a, TestDiagnosis b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDiagnosis a, TestDiagnosis b)
        {
            return !(a.Equals(b));
        }
    }
}
