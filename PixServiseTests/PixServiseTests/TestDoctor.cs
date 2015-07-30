using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDoctor
    {
        public TestPerson person;
        public MedicalStaff doctor;

        public TestDoctor(MedicalStaff d)
        {
            doctor = d;
            if (d.Person != null)
                person = new TestPerson(d.Person);
        }

        //static private string GetDoctorId(string mis)
        //{
        //    string patientId = "";
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string findIdPersonString =
        //                "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + mis + "'";
        //        SqlCommand command = new SqlCommand(findIdPersonString, connection);
        //        using (SqlDataReader IdPerson = command.ExecuteReader())
        //        {
        //            while (IdPerson.Read())
        //            {
        //                patientId = IdPerson["IdPerson"].ToString();
        //            }
        //        }
        //    }
        //    if (patientId != "")
        //        return patientId;
        //    else
        //        return null;
        //}

        static public TestDoctor BuildTestDoctorFromDataBase(string doctorId)
        {
            if (doctorId != null)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findDoctor = "SELECT TOP(1) * FROM Doctor WHERE IdDoctor = '" + doctorId + "'";
                    SqlCommand doc = new SqlCommand(findDoctor, connection);
                    using (SqlDataReader doctorReader = doc.ExecuteReader())
                    {
                        while (doctorReader.Read())
                        {
                            string IdPerson = Convert.ToString(doctorReader["IdPerson"]);
                            MedicalStaff ms = new MedicalStaff();
                            ms.IdPosition = Convert.ToUInt16(doctorReader["IdPosition"]);
                            ms.IdSpeciality = Convert.ToUInt16(doctorReader["IdSpeciality"]);
                            string idlpu = Convert.ToString(doctorReader["IdLpu"]);
                            string findIdInstitutionalString =
                                "SELECT TOP(1) * FROM Institution WHERE IdInstitution = '" + idlpu + "'";
                            using (SqlConnection connection2 = Global.GetSqlConnection())
                            {
                                SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection2);
                                using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                                {
                                    string InstId = "";
                                    while (IdInstitutional.Read())
                                    {
                                        ms.IdLpu = IdInstitutional["IdFedNsi"].ToString();
                                    }
                                }
                            }
                            TestDoctor p = new TestDoctor(ms);
                            p.person = TestPerson.BuildPersonFromDataBaseData(IdPerson);
                            return p;
                        }
                    }
                }
            }
            return null;
        }

        public void FindMismatch(TestDoctor d)
        {
            if (d.doctor != null)
            {
                if (this.doctor.IdLpu != d.doctor.IdLpu)
                    Global.errors3.Add("Несовпадение IdLpu TestDoctor");
                if (this.doctor.IdPosition != d.doctor.IdPosition)
                    Global.errors3.Add("Несовпадение IdPosition TestDoctor");
                if (this.doctor.IdSpeciality != d.doctor.IdSpeciality)
                    Global.errors3.Add("Несовпадение IdSpeciality TestDoctor");
                this.person.FindMismatch(d.person);
            }
        }

        public bool CheckDoctorInDataBase(string mis)
        {
            TestDoctor doc = TestDoctor.BuildTestDoctorFromDataBase(mis);
            if (this != doc)
            {
                this.FindMismatch(doc);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestDoctor p = obj as TestDoctor;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestDoctor с другим типом");
                return false;
            }
            if (this.doctor == p.doctor)
                return true;
            if ((this.doctor == null) || (p.doctor == null))
            {
                Global.errors3.Add("Сравнение TestDoctor = null с TestDoctor != null");
            }
            bool x = (this.doctor.IdLpu == p.doctor.IdLpu);
            x = (this.doctor.IdPosition == p.doctor.IdPosition);
            x = (this.doctor.IdSpeciality == p.doctor.IdSpeciality);
            x = Global.IsEqual(person, p.person);
            if ((this.doctor.IdLpu == p.doctor.IdLpu) &&
                (this.doctor.IdPosition == p.doctor.IdPosition) &&
                (this.doctor.IdSpeciality == p.doctor.IdSpeciality) &&
                Global.IsEqual(person, p.person))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestDoctor a, TestDoctor b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDoctor a, TestDoctor b)
        {
            return !(a.Equals(b));
        }
    }
}
