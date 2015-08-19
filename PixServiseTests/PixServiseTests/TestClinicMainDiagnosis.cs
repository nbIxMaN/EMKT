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
        public TestClinicMainDiagnosis(ClinicMainDiagnosis d, string idLpu = "")
        {
            if (d != null)
            {
                if (d.DiagnosisInfo != null)
                    diagnosInfo = new TestDiagnosisInfo(d.DiagnosisInfo);
                if (d.Complications != null)
                {
                    _diagnosis = new List<TestDiagnosis>();
                    foreach (Diagnosis i in d.Complications)
                        _diagnosis.Add(new TestDiagnosis(i, idLpu));
                }
                if (d.Doctor != null)
                    doctor = new TestDoctor(d.Doctor, idLpu);
            }
        }
        public TestClinicMainDiagnosis(Diagnosis d, string idLpu = "")
        {
            if (d != null)
            {
                if (d.DiagnosisInfo != null)
                    diagnosInfo = new TestDiagnosisInfo(d.DiagnosisInfo);
                if (d.Doctor != null)
                    doctor = new TestDoctor(d.Doctor, idLpu);
            }
        }
        static public TestClinicMainDiagnosis BuildTestClinicMainDiagnosisFromDataBaseDate(string idStep)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findTD = "SELECT TOP(1) * FROM Diagnosis WHERE IdDiagnosis = (SELECT MAX(IdDiagnosis) FROM Diagnosis WHERE IdStep = '" + idStep + "' AND IdDiagnosisType = '" + TestDiagnosis.IdClinicMainDiagnosis + "')";
                SqlCommand TDcommand = new SqlCommand(findTD, connection);
                using (SqlDataReader TDReader = TDcommand.ExecuteReader())
                {
                    while (TDReader.Read())
                    {
                        if (TDReader["IdParentDiagnosis"].ToString() == "")
                        {
                            ClinicMainDiagnosis d = new ClinicMainDiagnosis();
                            TestClinicMainDiagnosis td = new TestClinicMainDiagnosis(d);
                            td._diagnosis = TestDiagnosis.BuildDiagnosisFromDataBaseDate(idStep, TDReader["IdDiagnosis"].ToString());
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
                Global.errors3.Add("Несовпадение длины diagnosis TestClinicMainDiagnosis");
            if (Global.GetLength(this.diagnosInfo) != Global.GetLength(tcmd.diagnosInfo))
                Global.errors3.Add("Несовпадение длины diagnosInfo TestClinicMainDiagnosis");
            if (Global.GetLength(this.doctor) != Global.GetLength(tcmd.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestClinicMainDiagnosis");
        }
        public override bool Equals(Object obj)
        {
            TestClinicMainDiagnosis p = obj as TestClinicMainDiagnosis;
            if ((object)p == null)
            {
                return false;
            }
            if ((Global.IsEqual(this.doctor, p.doctor))&&
                (Global.IsEqual(this.diagnosInfo, p.diagnosInfo))&&
                (Global.IsEqual(this.diagnosis, p.diagnosis)))
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
                Global.errors3.Add("несовпадение TestClinicMainDiagnosis");
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
