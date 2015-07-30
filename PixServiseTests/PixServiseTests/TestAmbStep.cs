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
        public TestAmbStep(StepAmb sa, string caseLpu)
        {
            if (sa != null)
            {
                ambStep = sa;
                step = new TestStepBase(sa, caseLpu);
            }
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
        
        public bool CheckAmbStepsInDataBase(string idStep, string caseLpu)
        {
            List<TestAmbStep> steps = TestAmbStep.BuildAmbTestStepsFromDataBase(idStep, caseLpu);
            foreach (TestAmbStep step in steps)
            {
                if (this != step)
                    return false;
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            TestAmbStep p = obj as TestAmbStep;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestAmbStep с другим типом");
                return false;
            }
            if (this.ambStep == p.ambStep)
                return true;
            if ((this.ambStep == null) || (p.ambStep == null))
            {
                Global.errors3.Add("Сравнение TestAmbStep = null с TestAmbStep != null");
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
