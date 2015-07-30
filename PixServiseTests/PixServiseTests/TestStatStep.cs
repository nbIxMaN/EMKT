using PixServiseTests.EMKServise;
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
        public TestStatStep(StepStat sa, string caseLpu)
        {
            if (sa != null)
            {
                statStep = sa;
                step = new TestStepBase(sa, caseLpu);
            }
        }
        static public List<TestStatStep> BuildStatStepsFromDataBase(string idCase, string caseLpu, string patientId)
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
                                    sa.IdHospitalDepartment = Convert.ToString(stepsReader["HospitalDepartment"]);
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
        public void FindMismatch(TestStatStep astep)
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
            Global.IsEqual(this.step, astep.step);
        }
        
        //public bool CheckAmbStepsInDataBase(string idStep, string caseLpu)
        //{
        //    List<TestStatStep> steps = TestStatStep.BuildStatStepsFromDataBase(idStep, caseLpu);
        //    foreach (TestStatStep step in steps)
        //    {
        //        if (this != step)
        //            return false;
        //    }
        //    return true;
        //}
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
