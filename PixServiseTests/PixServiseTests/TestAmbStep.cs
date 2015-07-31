﻿using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestAmbStep
    {
        TestStepBase step;
        StepAmb ambStep;
        public List<TestMedRecord> records;
        public Array medRecords
        {
            get
            {
                if (records != null)
                    return records.ToArray();
                else
                    return null;
            }
        }
        public TestAmbStep(StepAmb sa, string caseLpu)
        {
            if (sa != null)
            {
                ambStep = sa;
                step = new TestStepBase(sa, caseLpu);
                if (sa.MedRecords != null)
                {
                    records = new List<TestMedRecord>();
                    foreach (object i in sa.MedRecords)
                    {
                        Service s = i as Service;
                        if (s != null)
                            records.Add(new TestService(s));
                        AppointedMedication am = i as AppointedMedication;
                        if (am != null)
                            records.Add(new TestAppointedMedication(am));
                        Diagnosis diag = i as Diagnosis;
                        if (diag != null)
                            records.Add(new TestDiagnosis(diag));
                        ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
                        if (cmd != null)
                            records.Add(new TestClinicMainDiagnosis(cmd));
                        Referral r = i as Referral;
                        if (r != null)
                            records.Add(new TestReferral(r));
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            records.Add(new TestLaboratoryReport(lr));
                    }
                }
            }
        }
        static public List<TestAmbStep> BuildAmbTestStepsFromDataBase(string idCase, string caseLpu, string patientId)
        {
            List<TestAmbStep> ambSteps = new List<TestAmbStep>();
            if (idCase != string.Empty)
            {
                List<TestStepBase> steps = TestStepBase.BuildTestStepsFromDataBase(idCase, caseLpu);
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    foreach (TestStepBase i in steps)
                    {
                        string findSteps = "SELECT * FROM AmbStep WHERE IdStep = '" + i.idStep + "'";
                        SqlCommand stepsCommand = new SqlCommand(findSteps, connection);
                        using (SqlDataReader stepsReader = stepsCommand.ExecuteReader())
                        {
                            while (stepsReader.Read())
                            {
                                StepAmb sa = new StepAmb();
                                if (stepsReader["IdVisitPlace"].ToString() != "")
                                    sa.IdVisitPlace = Convert.ToByte(stepsReader["IdVisitPlace"]);
                                else
                                    sa.IdVisitPlace = 0;
                                if (stepsReader["IdVisitPurpose"].ToString() != "")
                                    sa.IdVisitPurpose = Convert.ToByte(stepsReader["IdVisitPurpose"]);
                                else
                                    sa.IdVisitPurpose = 0;
                                TestAmbStep st = new TestAmbStep(sa, caseLpu);
                                st.step = i;
                                st.records = new List<TestMedRecord>();
                                List<TestService> s = TestService.BuildServiseFromDataBaseData(i.idStep);
                                if (!Global.IsEqual(s, null))
                                    st.records.AddRange(s);
                                List<TestAppointedMedication> am = TestAppointedMedication.BuildAppointedMedicationFromDataBaseDate(i.idStep);
                                if (!Global.IsEqual(am, null))
                                    st.records.AddRange(am);
                                List<TestDiagnosis> diag = TestDiagnosis.BuildDiagnosisFromDataBaseDate(i.idStep);
                                if (!Global.IsEqual(diag, null))
                                    st.records.AddRange(diag);
                                TestClinicMainDiagnosis acdm = TestClinicMainDiagnosis.BuildTestClinicMainDiagnosisFromDataBaseDate(i.idStep);
                                if (!Global.IsEqual(acdm, null))
                                    st.records.Add(acdm);
                                List<TestReferral> trl = TestReferral.BuildReferralFromDataBaseData(i.idStep);
                                if (!Global.IsEqual(trl, null))
                                    st.records.AddRange(trl);
                                List<TestLaboratoryReport> tlr = TestLaboratoryReport.BuildSickListFromDataBaseData(i.idStep);
                                if (!Global.IsEqual(tlr, null))
                                    st.records.AddRange(trl);
                                ambSteps.Add(st);
                            }
                        }
                    }
                }
            }
            if (ambSteps.Count != 0)
                return ambSteps;
            else
                return null;
        }
        public void FindMismatch(TestAmbStep astep)
        {
            if (this.ambStep.IdVisitPlace != astep.ambStep.IdVisitPlace)
                Global.errors3.Add("несовпадение IdVisitPlace TestAmbStep");
            if (this.ambStep.IdVisitPurpose != astep.ambStep.IdVisitPurpose)
                Global.errors3.Add("несовпадение IdVisitPurpose TestAmbStep");
            Global.IsEqual(this.step, astep.step);
        }
        
        public override bool Equals(Object obj)
        {
            TestAmbStep p = obj as TestAmbStep;
            if ((object)p == null)
            {
                return false;
            }
            if (this.ambStep == p.ambStep)
                return true;
            if ((this.ambStep == null) || (p.ambStep == null))
            {
                return false;
            }
            if ((this.ambStep.IdVisitPlace == p.ambStep.IdVisitPlace) &&
                (this.ambStep.IdVisitPurpose == p.ambStep.IdVisitPurpose) &&
                Global.IsEqual(this.step, p.step))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestAmbStep a, TestAmbStep b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAmbStep a, TestAmbStep b)
        {
            return !(a.Equals(b));
        }
    }
}
