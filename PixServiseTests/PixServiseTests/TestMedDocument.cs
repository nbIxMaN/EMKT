//using PixServiseTests.EMKServise;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PixServiseTests
//{
//    class TestMedDocument
//    {
//        public TestDoctor doctor;
//        public MedDocument document;
//        public TestAttachment attachment;

//        public TestMedDocument(MedDocument d)
//        {
//            if (d.Author != null)
//                doctor = new TestDoctor(d.Author);
//            if (d.Attachment != null)
//                attachment = new TestAttachment(d.Attachment);
//            document = d;
//        }

//        static public List<TestMedDocument> BuildTestMedDocumentsFromDataBase(string idStep)
//        {
//            List<TestMedDocument> docs = new List<TestMedDocument>();
//            using (SqlConnection connection = Global.GetSqlConnection())
//            {
//                string findDocuments = "SELECT * FROM MedDocument WHERE IdStep = '" + idStep + "'";
//                SqlCommand participantCommand = new SqlCommand(findDocuments, connection);
//                using (SqlDataReader docsReader = participantCommand.ExecuteReader())
//                {
//                    while (docsReader.Read())
//                    {
//                        MedDocument d = new MedDocument();
//                        string idDoctor = Convert.ToString(docsReader["IdDoctor"]);
//                        string idDoc = Convert.ToString(docsReader["IdMedDocument"]);
//                        if (docsReader["CreationDate"].ToString() != "")
//                            d.CreationDate = Convert.ToDateTime(docsReader["CreationDate"]);
//                        else
//                            d.CreationDate = DateTime.MinValue;
//                        if (docsReader["DocHead"].ToString() != "")
//                            d.Header = Convert.ToString(docsReader["DocHead"]);
//                        else
//                            d.Header = null;
//                        TestMedDocument rec = new TestMedDocument(d);
//                        if (idDoctor != string.Empty)
//                            rec.doctor = TestDoctor.BuildTestDoctorFromDataBase(idDoctor);
//                        rec.attachment = TestAttachment.BuildTestAttacmentFromDataBase(idDoc);
//                        docs.Add(rec);
//                    }
//                }
//            }
//            if (docs.Count != 0)
//                return docs;
//            else
//                return null;
//        }

//        public void FindMismatch(TestMedDocument p)
//        {
//            if (p.document != null)
//            {
//                if (this.document.CreationDate != p.document.CreationDate)
//                    Global.errors3.Add("несовпадение CreationDate TestMedDocument");
//                if (this.document.Header != p.document.Header)
//                    Global.errors3.Add("несовпадение Header TestMedDocument");
//                Global.IsEqual(this.attachment, p.attachment);
//                Global.IsEqual(this.doctor, p.doctor);
//            }
//        }

//        public bool CheckMedDocumentInDataBase(string idStep)
//        {
//            List<TestMedDocument> docs = TestMedDocument.BuildTestMedDocumentsFromDataBase(idStep);
//            foreach (TestMedDocument doc in docs)
//            {
//                if (this != doc)
//                    return false;
//            }
//            return true;
//        }

//        public override bool Equals(Object obj)
//        {
//            TestMedDocument p = obj as TestMedDocument;
//            if ((object)p == null)
//            {
//                Global.errors3.Add("Сравнение TestMedDocument с другим типом");
//                return false;
//            }
//            if (this.document == p.document)
//                return true;
//            if ((this.document == null) || (p.document == null))
//            {
//                Global.errors3.Add("Сравнение TestMedDocument = null с TestMedDocument != null");
//            }
//            if ((this.document.CreationDate == p.document.CreationDate)&&
//                (this.document.Header == p.document.Header)&&
//                (Global.IsEqual(this.attachment, p.attachment))&&
//                (Global.IsEqual(this.doctor, p.doctor)))
//            {
//                return true;
//            }
//            else
//            {
//                this.FindMismatch(p);
//                return false;
//            }
//        }
//        public static bool operator ==(TestMedDocument a, TestMedDocument b)
//        {
//            return a.Equals(b);
//        }
//        public static bool operator !=(TestMedDocument a, TestMedDocument b)
//        {
//            return !(a.Equals(b));
//        }
//    }
//}
