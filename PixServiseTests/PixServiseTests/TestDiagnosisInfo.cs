using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDiagnosisInfo
    {
        DiagnosisInfo info;
        public TestDiagnosisInfo(DiagnosisInfo d)
        {
            if (d != null)
                info = d;
        }
        static public TestDiagnosisInfo BuildDiagnosisFromDataBaseDate(string idDiagnosis)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findTD = "SELECT * FROM Diagnosis WHERE IdDiagnosis = '" + idDiagnosis + "'";
                SqlCommand TDcommand = new SqlCommand(findTD, connection);
                using (SqlDataReader TDReader = TDcommand.ExecuteReader())
                {
                    while (TDReader.Read())
                    {
                        DiagnosisInfo d = new DiagnosisInfo();
                        if (TDReader["Comment"].ToString() != "")
                            d.Comment = Convert.ToString(TDReader["Comment"]);
                        if (TDReader["DiagnosedDate"].ToString() != "")
                            d.DiagnosedDate = Convert.ToDateTime(TDReader["DiagnosedDate"]);
                        if (TDReader["IdRDiagnosisChangeReason"].ToString() != "")
                            d.DiagnosisChangeReason = Convert.ToByte(TDReader["IdRDiagnosisChangeReason"]);
                        if (TDReader["IdDiagnoseStage"].ToString() != "")
                            d.DiagnosisStage = Convert.ToByte(TDReader["IdDiagnoseStage"]);
                        if (TDReader["IdDiagnosisType"].ToString() != "")
                            d.IdDiagnosisType = Convert.ToByte(TDReader["IdDiagnosisType"]);
                        if (TDReader["IdDiseaseType"].ToString() != "")
                            d.IdDiseaseType = Convert.ToByte(TDReader["IdDiseaseType"]);
                        if (TDReader["IdRDispensaryState"].ToString() != "")
                            d.IdDispensaryState = Convert.ToByte(TDReader["IdRDispensaryState"]);
                        if (TDReader["IdTraumaType"].ToString() != "")
                            d.IdTraumaType = Convert.ToByte(TDReader["IdTraumaType"]);
                        if (TDReader["IdRMedicalStandard"].ToString() != "")
                            d.MedicalStandard = Convert.ToInt32(TDReader["IdRMedicalStandard"]);
                        if (TDReader["IdRMESImplementationFeature"].ToString() != "")
                            d.MESImplementationFeature = Convert.ToByte(TDReader["IdRMESImplementationFeature"]);
                        if (TDReader["MkbCode"].ToString() != "")
                            d.MkbCode = Convert.ToString(TDReader["MkbCode"]);
                        return (new TestDiagnosisInfo(d));
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestDiagnosisInfo td)
        {
            if (this.info.Comment != td.info.Comment)
                Global.errors3.Add("Несовпадение Comment TestDiagnosisInfo");
            if (this.info.DiagnosedDate != td.info.DiagnosedDate)
                Global.errors3.Add("Несовпадение DiagnosedDate TestDiagnosisInfo");
            if (this.info.DiagnosisChangeReason != td.info.DiagnosisChangeReason)
                Global.errors3.Add("Несовпадение DiagnosisChangeReason DiagnosisChangeReason");
            if (this.info.DiagnosisStage != td.info.DiagnosisStage)
                Global.errors3.Add("Несовпадение DiagnosisStage TestDiagnosisInfo");
            if (this.info.IdDiagnosisType != td.info.IdDiagnosisType)
                Global.errors3.Add("Несовпадение IdDiagnosisType TestDiagnosisInfo");
            if (this.info.IdDiseaseType != td.info.IdDiseaseType)
                Global.errors3.Add("Несовпадение IdDiseaseType TestDiagnosisInfo");
            if (this.info.IdDispensaryState != td.info.IdDispensaryState)
                Global.errors3.Add("Несовпадение IdDispensaryState TestDiagnosisInfo");
            if (this.info.IdTraumaType != td.info.IdTraumaType)
                Global.errors3.Add("Несовпадение IdTraumaType TestDiagnosisInfo");
            if (this.info.MedicalStandard != td.info.MedicalStandard)
                Global.errors3.Add("Несовпадение MedicalStandard TestDiagnosisInfo");
            if (this.info.MESImplementationFeature != td.info.MESImplementationFeature)
                Global.errors3.Add("Несовпадение MESImplementationFeature TestDiagnosisInfo");
            if (this.info.MkbCode != td.info.MkbCode)
                Global.errors3.Add("Несовпадение MkbCode TestDiagnosisInfo");
        }
        public override bool Equals(Object obj)
        {
            TestDiagnosisInfo p = obj as TestDiagnosisInfo;
            if ((object)p == null)
            {
                return false;
            }
            if (this.info == p.info)
                return true;
            if ((this.info == null) || (p.info == null))
            {
                return false;
            }
            if ((this.info.Comment == p.info.Comment)&&
            (this.info.DiagnosedDate == p.info.DiagnosedDate)&&
            (this.info.DiagnosisChangeReason == p.info.DiagnosisChangeReason)&&
            (this.info.DiagnosisStage == p.info.DiagnosisStage)&&
            (this.info.IdDiagnosisType == p.info.IdDiagnosisType)&&
            (this.info.IdDiseaseType == p.info.IdDiseaseType)&&
            (this.info.IdDispensaryState == p.info.IdDispensaryState)&&
            (this.info.IdTraumaType == p.info.IdTraumaType)&&
            (this.info.MedicalStandard == p.info.MedicalStandard)&&
            (this.info.MESImplementationFeature == p.info.MESImplementationFeature)&&
            (this.info.MkbCode == p.info.MkbCode))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDiagnosisInfo");
                return false;
            }
        }
        public static bool operator ==(TestDiagnosisInfo a, TestDiagnosisInfo b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDiagnosisInfo a, TestDiagnosisInfo b)
        {
            return !(a.Equals(b));
        }
    }
}
