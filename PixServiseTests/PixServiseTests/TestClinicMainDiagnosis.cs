using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestClinicMainDiagnosis : TestMedRecord
    {
        List<TestDiagnosis> _diagnosis;
        public Array diagnosis
        {
            get
            {
                if (_diagnosis != null)
                    return _diagnosis.ToArray();
                else
                    return null;
            }
        }
        TestDoctor doctor;
        TestDiagnosisInfo diagnosInfo;
        public TestClinicMainDiagnosis(ClinicMainDiagnosis d)
        {
            if (d != null)
            {
                if (d.DiagnosisInfo != null)
                    diagnosInfo = new TestDiagnosisInfo(d.DiagnosisInfo);
                if (d.Complications != null)
                {
                    _diagnosis = new List<TestDiagnosis>();
                    foreach (Diagnosis i in d.Complications)
                        _diagnosis.Add(new TestDiagnosis(i));
                }
                if (d.Doctor != null)
                    doctor = new TestDoctor(d.Doctor);
            }
        }
        static public TestClinicMainDiagnosis BuildTestClinicMainDiagnosisFromDataBaseDate(string idStep)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findTD = "SELECT * FROM Diagnosis WHERE IdStep = '" + idStep + "'";
                SqlCommand TDcommand = new SqlCommand(findTD, connection);
                using (SqlDataReader TDReader = TDcommand.ExecuteReader())
                {
                    while (TDReader.Read())
                    {
                        if (TDReader["IdParentDiagnosis"].ToString() == "")
                        {
                            ClinicMainDiagnosis d = new ClinicMainDiagnosis();
                            TestClinicMainDiagnosis td = new TestClinicMainDiagnosis(d);
                            td._diagnosis = TestDiagnosis.BuildDiagnosisFromDataBaseDate(idStep);
                            td.diagnosInfo = TestDiagnosisInfo.BuildDiagnosisFromDataBaseDate(TDReader["IdDiagnosis"].ToString());
                            if (TDReader["IdDiagnosedDoctor"].ToString() != "")
                                td.doctor = TestDoctor.BuildTestDoctorFromDataBase(TDReader["IdDiagnosedDoctor"].ToString());
                            return td;
                        }
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestClinicMainDiagnosis tcmd)
        {
            if (Global.GetLength(this.diagnosis) != Global.GetLength(tcmd.diagnosis))
                Global.errors3.Add("Несовпадение длинны diagnosis TestClinicMainDiagnosis");
        }
        public override bool Equals(Object obj)
        {
            TestClinicMainDiagnosis p = obj as TestClinicMainDiagnosis;
            if ((object)p == null)
            {
                return false;
            }
            if (this.diagnosInfo == p.diagnosInfo)
                return true;
            if ((this.diagnosInfo == null) || (p.diagnosInfo== null))
            {
                return false;
            }
            if (Global.IsEqual(this.diagnosInfo, p.diagnosInfo)&&
                Global.IsEqual(this.doctor, p.doctor)&&
                Global.IsEqual(this.diagnosis, p.diagnosis))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestClinicMainDiagnosis a, TestClinicMainDiagnosis b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestClinicMainDiagnosis a, TestClinicMainDiagnosis b)
        {
            return !(a.Equals(b));
        }
    }
}
