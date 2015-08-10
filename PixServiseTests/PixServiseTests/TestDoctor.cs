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

        public TestDoctor(MedicalStaff d, string idLpu = "")
        {
            if (d != null)
            {
                doctor = d;
                if (d.IdLpu == null)
                    d.IdLpu = idLpu;
                if (d.Person != null)
                    person = new TestPerson(d.Person);
            }
        }

        static public TestDoctor BuildTestDoctorFromDataBase(string doctorId)
        {
            if (doctorId != null)
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findDoctor = "SELECT TOP(1) * FROM Doctor, nsi.DoctorSpec WHERE Doctor.IdDoctor = '" + doctorId + "' AND Doctor.IdSpeciality = nsi.DoctorSpec.IdSpeciality";
                    SqlCommand doc = new SqlCommand(findDoctor, connection);
                    using (SqlDataReader doctorReader = doc.ExecuteReader())
                    {
                        while (doctorReader.Read())
                        {
                            string IdPerson = Convert.ToString(doctorReader["IdPerson"]);
                            MedicalStaff ms = new MedicalStaff();
                            ms.IdPosition = Convert.ToUInt16(doctorReader["IdPosition"]);
                            ms.IdSpeciality = Convert.ToUInt16(doctorReader["Code"]);
                            string idlpu = Convert.ToString(doctorReader["IdLpu"]);
                            string findIdInstitutionalString =
                                "SELECT TOP(1) * FROM Institution WHERE IdInstitution = '" + idlpu + "'";
                            using (SqlConnection connection2 = Global.GetSqlConnection())
                            {
                                SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection2);
                                using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                                {
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
                if (Global.GetLength(this.person) != Global.GetLength(d.person))
                    Global.errors3.Add("Несовпадение длины person TestDoctor");
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
                return false;
            }
            if (this.doctor == p.doctor)
                return true;
            if ((this.doctor == null) || (p.doctor == null))
            {
                return false;
            }
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
                Global.errors3.Add("несовпадение TestDoctor");
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
