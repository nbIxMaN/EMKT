using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestParticipant
    {
        public TestDoctor doctor;
        public Participant participant;

        public TestParticipant(Participant d, string idLpu = "")
        {
            if (d.Doctor != null)
                doctor = new TestDoctor(d.Doctor, idLpu);
            participant = d;
        }

        static public TestParticipant BuildTestParticipantFromDataBase(string idCase, string idDoctor)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findParticipant = "SELECT TOP(1) * FROM mm_Author2Case WHERE IdCase = '" + idCase + "' AND IdDoctor = '" + idDoctor + "'";
                SqlCommand participantCommand = new SqlCommand(findParticipant, connection);
                using (SqlDataReader participantReader = participantCommand.ExecuteReader())
                {
                    while (participantReader.Read())
                    {
                        Participant p = new Participant();
                        if (participantReader["IdPersonRole"].ToString() != "")
                            p.IdRole = Convert.ToByte(participantReader["IdPersonRole"]);
                        else
                            p.IdRole = 0;
                        TestParticipant part = new TestParticipant(p);
                        part.doctor = TestDoctor.BuildTestDoctorFromDataBase(idDoctor);
                        return part;
                    }
                }
            }
            return null;
        }

        public void FindMismatch(TestParticipant p)
        {
            if (p.participant != null)
            {
                if (this.participant.IdRole != p.participant.IdRole)
                    Global.errors3.Add("Несовпадение IdRole TestParticipant");
            }
        }

        public bool CheckParticipantInDataBase(string idCase, string idDoctor)
        {
            TestParticipant part = TestParticipant.BuildTestParticipantFromDataBase(idCase, idDoctor);
            if (this != part)
            {
                this.FindMismatch(part);
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestParticipant p = obj as TestParticipant;
            if ((object)p == null)
            {
                return false;
            }
            if (this.participant == p.participant)
                return true;
            if ((this.participant == null) || (p.participant == null))
            {
                return false;
            }
            if (Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestParticipant a, TestParticipant b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestParticipant a, TestParticipant b)
        {
            return !(a.Equals(b));
        }

    }
}
