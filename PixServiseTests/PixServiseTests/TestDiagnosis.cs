﻿using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDiagnosis : TestMedRecord
    {
        public const byte IdClinicMainDiagnosis = 1;
        TestDiagnosisInfo info;
        Diagnosis document;
        TestDoctor doctor;
        public TestDiagnosis(Diagnosis d, string idLpu = "")
        {
            if (d != null)
            {
                document = d;
                info = new TestDiagnosisInfo(d.DiagnosisInfo);
                doctor = new TestDoctor(d.Doctor, idLpu);
            }
        }
        static public List<TestDiagnosis> BuildDiagnosisFromDataBaseDate(string IdStep, string IdParentDiagnosis = "")
        {
            List<TestDiagnosis> tdList = new List<TestDiagnosis>();
            if (IdStep != "")
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findTD = "";
                    if (IdParentDiagnosis == "")
                        findTD = "SELECT * FROM Diagnosis WHERE IdStep = '" + IdStep + "' AND IdDiagnosisType <> '" + IdClinicMainDiagnosis + "' AND IdParentDiagnosis IS NULL";
                    else
                        findTD = "SELECT * FROM Diagnosis WHERE IdStep = '" + IdStep + "' AND IdDiagnosisType <> '" + IdClinicMainDiagnosis + "' AND IdParentDiagnosis = '" + IdParentDiagnosis + "'";
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

        private void FindMismatch(TestDiagnosis t)
        {
            if (Global.GetLength(this.doctor) != Global.GetLength(t.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestDiagnosis");
            if (Global.GetLength(this.info) != Global.GetLength(t.info))
                Global.errors3.Add("Несовпадение длины info TestDiagnosis");
        }

        public override bool Equals(Object obj)
        {
            TestDiagnosis p = obj as TestDiagnosis;
            if ((object)p == null)
            {
                return false;
            }
            if ((this.info == p.info)&&
                (Global.IsEqual(this.doctor, p.doctor)))
                return true;
            if ((this.info == null) || (p.info == null))
            {
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
                Global.errors3.Add("несовпадение TestDiagnosis");
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
