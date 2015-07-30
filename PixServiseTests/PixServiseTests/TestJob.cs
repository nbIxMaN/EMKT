using PixServiseTests.PixServise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestJob
    {
        public JobDto job;

        public TestJob(JobDto j)
        {
            job = j;
        }
        static public TestJob BuildTestJobFromDataBase (string idPerson)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT TOP(1) * FROM Job WHERE IdJob = (SELECT MAX(IdJob) FROM Job WHERE IdPerson = '" + idPerson + "')";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader jobReader = person.ExecuteReader())
                {
                    while (jobReader.Read())
                    {
                        JobDto job = new JobDto();
                        job.CompanyName = Convert.ToString(jobReader["CompanyName"]);
                        if (jobReader["DateStart"].ToString() != "")
                            job.DateStart = Convert.ToDateTime(jobReader["DateStart"]);
                        else
                            job.DateStart = null;
                        if (jobReader["DateEnd"].ToString() != "")
                            job.DateEnd = Convert.ToDateTime(jobReader["DateEnd"]);
                        else
                            job.DateEnd = null;
                        if (jobReader["Sphere"].ToString() != "")
                            job.Sphere = Convert.ToString(jobReader["Sphere"]);
                        else
                            job.Sphere = null;
                        if (jobReader["OgrnCode"].ToString() != "")
                            job.OgrnCode = Convert.ToString(jobReader["OgrnCode"]);
                        else
                            job.OgrnCode = null;
                        if (jobReader["Position"].ToString() != "")
                            job.Position = Convert.ToString(jobReader["Position"]);
                        else
                            job.Position = null;
                        TestJob j = new TestJob(job);
                        return j;
                    }
                }
            }
            return null;
        }

        public void FindMismatch(TestJob b)
        {
            if (b.job != null)
            {
                if (this.job.CompanyName != b.job.CompanyName)
                    Global.errors3.Add("Несовпадение имен компаний");
                if (this.job.DateEnd != b.job.DateEnd)
                    Global.errors3.Add("Несовпадение дат конца работы");
                if (this.job.DateStart != b.job.DateStart)
                    Global.errors3.Add("Несовпадение дат начала работы");
                if (this.job.OgrnCode != b.job.OgrnCode)
                    Global.errors3.Add("Несовпадение кодов ОГРН");
                if (this.job.Position != b.job.Position)
                    Global.errors3.Add("Несовпадение позиций");
                if (this.job.Sphere != b.job.Sphere)
                    Global.errors3.Add("Несовпадение сфер");
            }
        }

        public bool CheckJobInDataBase(string patientId)
        {
            TestJob job = TestJob.BuildTestJobFromDataBase(patientId);
            if (this != job)
            {
                this.FindMismatch(job);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestJob p = obj as TestJob;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestJob с другим типом");
                return false;
            }
            if (this.job == p.job)
                return true;
            if ((this.job == null) || (p.job == null))
            {
                Global.errors3.Add("Сравнение TestJob = null с TestJob != null");
                return false;
            }
            if ((this.job.CompanyName == p.job.CompanyName) &&
                    (this.job.DateEnd == p.job.DateEnd) &&
                    (this.job.DateStart == p.job.DateStart) &&
                    (this.job.OgrnCode == p.job.OgrnCode) &&
                    (this.job.Position == p.job.Position) &&
                    (this.job.Sphere == p.job.Sphere))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestJob a, TestJob b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestJob a, TestJob b)
        {
            return !(a.Equals(b));
        }


    }
}
