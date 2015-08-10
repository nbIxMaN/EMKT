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
            if (d != null)
            {
                if (d.Doctor != null)
                    doctor = new TestDoctor(d.Doctor, idLpu);
                participant = d;
            }
        }

        static public TestParticipant BuildTestParticipantFromDataBase(string idCase, string idDoctor, byte idPersonRole)
        {
            Participant p = new Participant();
            p.IdRole = idPersonRole;
            TestParticipant part = new TestParticipant(p);
            part.doctor = TestDoctor.BuildTestDoctorFromDataBase(idDoctor);
            return part;
        }

        public void FindMismatch(TestParticipant p)
        {
            if (p.participant != null)
            {
                if (this.participant.IdRole != p.participant.IdRole)
                    Global.errors3.Add("Несовпадение IdRole TestParticipant");
                if (Global.GetLength(this.doctor) != Global.GetLength(p.doctor))
                    Global.errors3.Add("Несjвпадение длины doctor TestParticipant");
            }
        }

        public override bool Equals(Object obj)
        {
            TestParticipant p = obj as TestParticipant;
            if ((object)p == null)
            {
                return false;
            }
            if ((this.participant.IdRole == p.participant.IdRole)&&
                (Global.IsEqual(this.doctor, p.doctor)))
                return true;
            if ((this.participant == null) || (p.participant == null))
            {
                return false;
            }
            if (Global.IsEqual(this.doctor, p.doctor)&&
                (this.participant.IdRole != p.participant.IdRole))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestParticipant");
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
