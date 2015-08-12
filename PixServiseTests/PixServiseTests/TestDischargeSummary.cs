using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestDischargeSummary : TestMedRecord
    {
        private const string idEpicrisis = "1";
        DischargeSummary dischargeSummary;
        TestAttachment attachment;
        TestDoctor doctor;
        public TestDischargeSummary(DischargeSummary r, string idLpu = "")
        {
            if (r != null)
            {
                dischargeSummary = r;
                attachment = new TestAttachment(r.Attachment, "input");
                doctor = new TestDoctor(r.Author, idLpu);
            }
        }
        static public TestDischargeSummary BuildSickListFromDataBaseData(string idStep)
        {
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idEpicrisis);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        DischargeSummary r = new DischargeSummary();
                        r.CreationDate = i.CreationDate;
                        r.Header = i.DocHead;
                        TestDischargeSummary tr = new TestDischargeSummary(r);
                        tr.attachment = i;
                        tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                        return tr;
                    }
            }
            return null;
        }
        private void FindMismatch(TestDischargeSummary r)
        {
            if (this.dischargeSummary.CreationDate != r.dischargeSummary.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestDischargeSummary");
            if (this.dischargeSummary.Header != r.dischargeSummary.Header)
                Global.errors3.Add("Несовпадение Header TestDischargeSummary");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несовпадение длины doctor TestDiagnosis");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несовпадение длины attachment TestDiagnosis");
        }
        public override bool Equals(Object obj)
        {
            TestDischargeSummary p = obj as TestDischargeSummary;
            if ((object)p == null)
            {
                return false;
            }
            if (this.dischargeSummary == p.dischargeSummary)
                return true;
            if ((this.dischargeSummary == null) || (p.dischargeSummary == null))
            {
                return false;
            }
            if ((this.dischargeSummary.CreationDate == p.dischargeSummary.CreationDate)&&
            (this.dischargeSummary.Header == p.dischargeSummary.Header)&&
            Global.IsEqual(this.doctor, p.doctor)&&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDischargeSummary");
                return false;
            }
        }
        public static bool operator ==(TestDischargeSummary a, TestDischargeSummary b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDischargeSummary a, TestDischargeSummary b)
        {
            return !(a.Equals(b));
        }
    }
}
