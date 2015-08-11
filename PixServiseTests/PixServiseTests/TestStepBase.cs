using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestStepBase
    {
        public string idStep;
        public StepBase step;
        public TestDoctor doctor;
        public TestStepBase(StepBase s, string caseLpu)
        {
            if (s != null)
                step = s;
            if (s.Doctor != null)
            {
                doctor = new TestDoctor(s.Doctor, caseLpu);
            }
        }
        static public List<TestStepBase> BuildTestStepsFromDataBase(string idCase, string stepLpu)
        {
            List<TestStepBase> steps = new List<TestStepBase>();
            if (idCase != string.Empty)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findSteps = "SELECT * FROM Step WHERE IdCase = '" + idCase + "'";
                    SqlCommand stepsCommand = new SqlCommand(findSteps, connection);
                    using (SqlDataReader stepsReader = stepsCommand.ExecuteReader())
                    {
                        while (stepsReader.Read())
                        {
                            StepBase sb = new StepBase();
                            if (stepsReader["Comment"].ToString() != "")
                                sb.Comment = Convert.ToString(stepsReader["Comment"]);
                            else
                                sb.Comment = null;
                            if (stepsReader["DateEnd"].ToString() != "")
                                sb.DateEnd = Convert.ToDateTime(stepsReader["DateEnd"]);
                            else
                                sb.DateEnd = DateTime.MinValue;
                            if (stepsReader["DateStart"].ToString() != "")
                                sb.DateStart = Convert.ToDateTime(stepsReader["DateStart"]);
                            else
                                sb.DateStart = DateTime.MinValue;
                            if (stepsReader["IdPaymentType"].ToString() != "")
                                sb.IdPaymentType = Convert.ToByte(stepsReader["IdPaymentType"]);
                            else
                                sb.IdPaymentType = 0;
                            if (stepsReader["IdStepMis"].ToString() != "")
                                sb.IdStepMis = Convert.ToString(stepsReader["IdStepMis"]);
                            else
                                sb.IdStepMis = null;
                            TestStepBase step = new TestStepBase(sb, stepLpu);
                            if (stepsReader["IdDoctor"].ToString() != "")
                                step.doctor = TestDoctor.BuildTestDoctorFromDataBase(stepsReader["IdDoctor"].ToString());
                            else
                                step.doctor = null;
                            step.idStep = Convert.ToString(stepsReader["IdStep"]);
                            steps.Add(step);
                        }
                    }
                }
            }
            if (steps.Count != 0)
                return steps;
            else
                return null;
        }
        private void FindMismatch(TestStepBase sb)
        {
            if (this.step.Comment != sb.step.Comment)
                Global.errors3.Add("несовпадение Comment TestStepBase");
            if (this.step.DateEnd != sb.step.DateEnd)
                Global.errors3.Add("несовпадение DateEnd TestStepBase");
            if (this.step.DateStart != sb.step.DateStart)
                Global.errors3.Add("несовпадение DateStart TestStepBase");
            if (this.step.IdPaymentType != sb.step.IdPaymentType)
                Global.errors3.Add("несовпадение IdPaymentType TestStepBase");
            if (this.step.IdStepMis != sb.step.IdStepMis)
                Global.errors3.Add("несовпадение IdStepMis TestStepBase");
            if (Global.GetLength(step) != Global.GetLength(sb.step))
                Global.errors3.Add("несовпадение длины step TestStepBase");
            if (Global.GetLength(doctor) != Global.GetLength(sb.doctor))
                Global.errors3.Add("несовпадение длины doctor TestStepBase");
        }

        public override bool Equals(Object obj)
        {
            TestStepBase p = obj as TestStepBase;
            if ((object)p == null)
            {
                return false;
            }
            if (this.step == p.step)
                return true;
            if ((this.step == null) || (p.step == null))
            {
                return false;
            }
            if ((this.step.Comment == p.step.Comment) &&
                (this.step.DateEnd == p.step.DateEnd) &&
                (this.step.DateStart == p.step.DateStart) &&
                (this.step.IdPaymentType == p.step.IdPaymentType) &&
                (this.step.IdStepMis == p.step.IdStepMis) &&
                Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestStepBase");
                return false;
            }
        }
        public static bool operator ==(TestStepBase a, TestStepBase b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestStepBase a, TestStepBase b)
        {
            return !(a.Equals(b));
        }
    }
}
