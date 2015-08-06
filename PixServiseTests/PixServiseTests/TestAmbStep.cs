using PixServiseTests.EMKServise;
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
                            records.Add(new TestService(s, caseLpu));
                        AppointedMedication am = i as AppointedMedication;
                        if (am != null)
                            records.Add(new TestAppointedMedication(am, caseLpu));
                        Diagnosis diag = i as Diagnosis;
                        if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                            records.Add(new TestDiagnosis(diag, caseLpu));
                        ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
                        if ((cmd != null) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            records.Add(new TestClinicMainDiagnosis(cmd, caseLpu));
                        Referral r = i as Referral;
                        if (r != null)
                            records.Add(new TestReferral(r, caseLpu));
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            records.Add(new TestLaboratoryReport(lr, caseLpu));
                    }
                }
            }
        }
        static public TestAmbStep BuildAmbTestStepsFromDataBase(string guid, string patientMis, string idLpu, string caseMis, string stepMis)
        {
            string patientId = TestPerson.GetPersonId(guid, idLpu, patientMis);
            if (patientId != null)
            {
                string caseId = TestCaseBase.GetCaseId(guid, idLpu, caseMis, patientId);
                return (BuildAmbTestStepsFromDataBase(caseId, idLpu).Last(st => st.step.step.IdStepMis == stepMis));
            }
            return null;
        }
        static public List<TestAmbStep> BuildAmbTestStepsFromDataBase(string idCase, string caseLpu)
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
                                    st.records.AddRange(tlr);
                                if (st.records.Count == 0)
                                    st.records = null;
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
            if (Global.GetLength(this.medRecords) != Global.GetLength(astep.medRecords))
                Global.errors3.Add("несовпадение длинны medRecords TestAmbStep");
        }

        public bool CheckStepInDataBase(string guid, string patientMis, string idLpu, string caseMis)
        {
            TestAmbStep tas = TestAmbStep.BuildAmbTestStepsFromDataBase(guid, patientMis, idLpu, caseMis, this.step.step.IdStepMis);
            return (this == tas);
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
                Global.IsEqual(this.step, p.step)&&
                Global.IsEqual(this.medRecords, p.medRecords))
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
