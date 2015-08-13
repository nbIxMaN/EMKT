using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestConsultNote : TestMedRecord
    {
        private const string idConsultNote = "3";
        ConsultNote consultNote;
        TestAttachment attachment;
        TestDoctor doctor;
        public TestConsultNote(ConsultNote r, string idLpu = "")
        {
            if (r != null)
            {
                consultNote = r;
                attachment = new TestAttachment(r.Attachment);
                doctor = new TestDoctor(r.Author, idLpu);
            }
        }
        static public TestConsultNote BuildSickListFromDataBaseData(string idStep)
        {
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idConsultNote);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        ConsultNote r = new ConsultNote();
                        r.CreationDate = i.CreationDate;
                        r.Header = i.DocHead;
                        TestConsultNote tr = new TestConsultNote(r);
                        tr.attachment = i;
                        tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                        return tr;
                    }
            }
            return null;
        }
        private void FindMismatch(TestConsultNote r)
        {
            if (this.consultNote.CreationDate != r.consultNote.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestConsultNote");
            if (this.consultNote.Header != r.consultNote.Header)
                Global.errors3.Add("Несовпадение Header TestConsultNote");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestConsultNote");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длины attachment TestConsultNote");
        }
        public override bool Equals(Object obj)
        {
            TestConsultNote p = obj as TestConsultNote;
            if ((object)p == null)
            {
                return false;
            }
            if (this.consultNote == p.consultNote)
                return true;
            if ((this.consultNote == null) || (p.consultNote == null))
            {
                return false;
            }
            if ((this.consultNote.CreationDate == p.consultNote.CreationDate)&&
            (this.consultNote.Header == p.consultNote.Header)&&
            Global.IsEqual(this.doctor, p.doctor)&&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestConsultNote");
                return false;
            }
        }
        public static bool operator ==(TestConsultNote a, TestConsultNote b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestConsultNote a, TestConsultNote b)
        {
            return !(a.Equals(b));
        }
    }
}
