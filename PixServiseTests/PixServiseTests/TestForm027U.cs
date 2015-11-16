using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    class TestForm027U : TestMedRecord
    {
        private const string idForm027U = "7";
        Form027U form027U;
        TestAttachment attachment;
        TestDoctor doctor;

        public TestForm027U(Form027U r, string idLpu = "")
        {
            if (r != null)
            {
                form027U = r;
                attachment = new TestAttachment(r.Attachment);
                doctor = new TestDoctor(r.Author, idLpu);
            }
        }

        static public List<TestForm027U> BuildForm027UFromDataBaseData(string idStep)
        {
            List<TestForm027U> form = new List<TestForm027U>();
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idForm027U);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        Form027U r = new Form027U();
                        r.CreationDate = i.CreationDate;
                        r.Header = i.DocHead;
                        TestForm027U tr = new TestForm027U(r);
                        tr.attachment = i;
                        tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                        form.Add(tr);
                    }
            }
            if (form.Count != 0)
                return form;
            else
                return null;
        }
        private void FindMismatch(TestForm027U r)
        {
            if (this.form027U.CreationDate != r.form027U.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestForm027U");
            if (this.form027U.Header != r.form027U.Header)
                Global.errors3.Add("Несовпадение Header TestForm027U");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestForm027U");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длины attachment TestForm027U");
        }
        public override bool Equals(Object obj)
        {
            TestForm027U p = obj as TestForm027U;

            if ((object)p == null) return false;

            if (this.form027U == p.form027U) return true;

            if ((this.form027U == null) || (p.form027U == null)) return false;

            if ((this.form027U.CreationDate == p.form027U.CreationDate) &&
            (this.form027U.Header == p.form027U.Header) &&
            Global.IsEqual(this.doctor, p.doctor) &&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestForm027U");
                return false;
            }
        }

        public static bool operator ==(TestForm027U a, TestForm027U b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TestForm027U a, TestForm027U b)
        {
            return !(a.Equals(b));
        }
    }
}
