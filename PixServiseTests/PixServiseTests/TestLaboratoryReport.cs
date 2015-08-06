using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestLaboratoryReport : TestMedRecord
    {
        private const string idLaboratoryReport = "2";
        LaboratoryReport laboratoryReport;
        TestAttachment attachment;
        TestDoctor doctor;
        public TestLaboratoryReport(LaboratoryReport r, string idLpu = "")
        {
            if (r != null)
            {
                laboratoryReport = r;
                attachment = new TestAttachment(r.Attachment);
                doctor = new TestDoctor(r.Author, idLpu);
            }
        }
        static public List<TestLaboratoryReport> BuildSickListFromDataBaseData(string idStep)
        {
            List<TestLaboratoryReport> llr = new List<TestLaboratoryReport>();
            if (idStep != "")
            {
                List<TestAttachment> lta = TestAttachment.BuildTestAttacmentFromDataBase(idStep, idLaboratoryReport);
                if (lta != null)
                    foreach (TestAttachment i in lta)
                    {
                        LaboratoryReport r = new LaboratoryReport();
                        r.CreationDate = i.CreationDate;
                        r.Header = i.DocHead;
                        TestLaboratoryReport tr = new TestLaboratoryReport(r);
                        tr.attachment = i;
                        tr.doctor = TestDoctor.BuildTestDoctorFromDataBase(i.IdDoctor);
                        llr.Add(tr);
                    }
            }
            if (llr.Count != 0)
                return llr;
            else
                return null;
        }
        private void FindMismatch(TestLaboratoryReport r)
        {
            if (this.laboratoryReport.CreationDate != r.laboratoryReport.CreationDate)
                Global.errors3.Add("Несовпадение CreationDate TestLaboratoryReport");
            if (this.laboratoryReport.Header != r.laboratoryReport.Header)
                Global.errors3.Add("Несовпадение Header TestLaboratoryReport");
            if (Global.GetLength(this.doctor) != Global.GetLength(r.doctor))
                Global.errors3.Add("Несjвпадение длины doctor TestLaboratoryReport");
            if (Global.GetLength(this.attachment) != Global.GetLength(r.attachment))
                Global.errors3.Add("Несjвпадение длины attachment TestLaboratoryReport");
        }
        public override bool Equals(Object obj)
        {
            TestLaboratoryReport p = obj as TestLaboratoryReport;
            if ((object)p == null)
            {
                return false;
            }
            if (this.laboratoryReport == p.laboratoryReport)
                return true;
            if ((this.laboratoryReport == null) || (p.laboratoryReport == null))
            {
                return false;
            }
            if ((this.laboratoryReport.CreationDate == p.laboratoryReport.CreationDate)&&
            (this.laboratoryReport.Header == p.laboratoryReport.Header)&&
            Global.IsEqual(this.doctor, p.doctor)&&
            Global.IsEqual(this.attachment, p.attachment))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestLaboratoryReport a, TestLaboratoryReport b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestLaboratoryReport a, TestLaboratoryReport b)
        {
            return !(a.Equals(b));
        }
    }
}
