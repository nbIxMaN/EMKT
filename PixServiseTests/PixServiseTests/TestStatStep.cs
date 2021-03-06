﻿using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestStatStep
    {
        TestStepBase step;
        StepStat statStep;
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
        public TestStatStep(StepStat sa, string caseLpu)
        {
            if (sa != null)
            {
                statStep = sa;
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

                        Form027U form = i as Form027U;
                        if (form != null)
                            records.Add(new TestForm027U(form, caseLpu));
                    }
                }
            }
        }

        static public TestStatStep BuildStatStepsFromDataBase(string guid, string patientMis, string idLpu, string caseMis, string stepMis)
        {
            string patientId = TestPerson.GetPersonId(guid, idLpu, patientMis);
            if (patientId != null)
            {
                string caseId = TestCaseBase.GetCaseId(guid, idLpu, caseMis, patientId);
                return (BuildStatStepsFromDataBase(caseId, idLpu).Last<TestStatStep>(s => s.step.step.IdStepMis == stepMis));
            }
            return null;
        }

        static public List<TestStatStep> BuildStatStepsFromDataBase(string idCase, string caseLpu)
        {
            List<TestStatStep> statSteps = new List<TestStatStep>();
            if (idCase != string.Empty)
            {
                List<TestStepBase> steps = TestStepBase.BuildTestStepsFromDataBase(idCase, caseLpu);
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    foreach (TestStepBase i in steps)
                    {
                        string findSteps = "SELECT * FROM HospStep WHERE IdStep = '" + i.idStep + "'";
                        SqlCommand stepsCommand = new SqlCommand(findSteps, connection);
                        using (SqlDataReader stepsReader = stepsCommand.ExecuteReader())
                        {
                            while (stepsReader.Read())
                            {
                                StepStat sa = new StepStat();
                                if (stepsReader["BedNo"].ToString() != "")
                                    sa.BedNumber = Convert.ToString(stepsReader["BedNo"]);
                                else
                                    sa.BedNumber = null;
                                if (stepsReader["IdBedProfile"].ToString() != "")
                                    sa.BedProfile = Convert.ToByte(stepsReader["IdBedProfile"]);
                                else
                                    sa.BedProfile = 0;
                                if (stepsReader["DaySpend"].ToString() != "")
                                    sa.DaySpend = Convert.ToUInt16(stepsReader["DaySpend"]);
                                else
                                    sa.DaySpend = 0;
                                if (stepsReader["HospitalDepartment"].ToString() != "")
                                    sa.HospitalDepartmentName = Convert.ToString(stepsReader["HospitalDepartment"]);
                                else
                                    sa.HospitalDepartmentName = null;
                                if (stepsReader["IdHospitalDepartment"].ToString() != "")
                                    sa.IdHospitalDepartment = Convert.ToString(stepsReader["IdHospitalDepartment"]);
                                else
                                    sa.IdHospitalDepartment = null;
                                if (stepsReader["IdRegimenType"].ToString() != "")
                                    sa.IdRegimen = Convert.ToByte(stepsReader["IdRegimenType"]);
                                else
                                    sa.IdRegimen = 0;
                                if (stepsReader["WardNumber"].ToString() != "")
                                    sa.WardNumber = Convert.ToString(stepsReader["WardNumber"]);
                                else
                                    sa.WardNumber = null;
                                TestStatStep st = new TestStatStep(sa, caseLpu);
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
                                List<TestLaboratoryReport> tlr = TestLaboratoryReport.BuildLaboratoryReportFromDataBaseData(i.idStep);
                                if (!Global.IsEqual(tlr, null))
                                    st.records.AddRange(tlr);
                                if (st.records.Count == 0)
                                    st.records = null;
                                statSteps.Add(st);
                            }
                        }
                    }
                }
            }
            if (statSteps.Count != 0)
                return statSteps;
            else
                return null;
        }
        private void FindMismatch(TestStatStep astep)
        {
            if (this.statStep.BedNumber != astep.statStep.BedNumber)
                Global.errors3.Add("несовпадение BedNumber TestStatStep");
            if (this.statStep.BedProfile != astep.statStep.BedProfile)
                Global.errors3.Add("несовпадение BedProfile TestStatStep");
            if (this.statStep.DaySpend != astep.statStep.DaySpend)
                Global.errors3.Add("несовпадение DaySpend TestStatStep");
            if (this.statStep.HospitalDepartmentName != astep.statStep.HospitalDepartmentName)
                Global.errors3.Add("несовпадение HospitalDepartmentName TestStatStep");
            if (this.statStep.IdHospitalDepartment != astep.statStep.IdHospitalDepartment)
                Global.errors3.Add("несовпадение IdHospitalDepartment TestStatStep");
            if (this.statStep.IdRegimen != astep.statStep.IdRegimen)
                Global.errors3.Add("несовпадение IdRegimen TestStatStep");
            if (this.statStep.WardNumber != astep.statStep.WardNumber)
                Global.errors3.Add("несовпадение WardNumber TestStatStep");
            if (Global.GetLength(this.medRecords) != Global.GetLength(astep.medRecords))
                Global.errors3.Add("несовпадение длины MedRecords TestStatStep");
            if (Global.GetLength(this.step) != Global.GetLength(astep.step))
                Global.errors3.Add("несовпадение длины step TestStatStep");
        }

        public bool CheckStepInDataBase(string guid, string patientMis, string idLpu, string caseMis)
        {
            TestStatStep tss = TestStatStep.BuildStatStepsFromDataBase(guid, patientMis, idLpu, caseMis, this.step.step.IdStepMis);
            return (this == tss);
        }

        public override bool Equals(Object obj)
        {
            TestStatStep p = obj as TestStatStep;
            if ((object)p == null)
            {
                return false;
            }
            if (this.statStep == p.statStep)
                return true;
            if ((this.statStep == null) || (p.statStep == null))
            {
                return false;
            }
            if ((this.statStep.BedNumber == p.statStep.BedNumber) &&
            (this.statStep.BedProfile == p.statStep.BedProfile) &&
            (this.statStep.DaySpend == p.statStep.DaySpend) &&
            (this.statStep.HospitalDepartmentName == p.statStep.HospitalDepartmentName) &&
            (this.statStep.IdHospitalDepartment == p.statStep.IdHospitalDepartment) &&
            (this.statStep.IdRegimen == p.statStep.IdRegimen) &&
            (this.statStep.WardNumber == p.statStep.WardNumber) &&
            Global.IsEqual(this.step, p.step)&&
            Global.IsEqual(this.medRecords, p.medRecords))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestStatStep");
                return false;
            }
        }
        public static bool operator ==(TestStatStep a, TestStatStep b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestStatStep a, TestStatStep b)
        {
            return !(a.Equals(b));
        }
    }
}
