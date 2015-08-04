using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestAttachment
    {
        public string idMedDocument;
        public string DocHead;
        public DateTime CreationDate;
        public string IdDoctor;
        public MedDocument.DocumentAttachment attachment;
        public string IdMedDocumentType;
        public string MIMEType;
        public TestAttachment(MedDocument.DocumentAttachment at)
        {
            if (at != null)
                attachment = at;
        }

        static public List<TestAttachment> BuildTestAttacmentFromDataBase(string idStep, string idMedDocumentType)
        {
            List<TestAttachment> taList = new List<TestAttachment>();
            if (idStep != "")
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findDocuments = "SELECT * FROM MedDocument WHERE IdStep = '" + idStep + "' AND IdMedDocumentType = '" + idMedDocumentType + "'";
                    SqlCommand participantCommand = new SqlCommand(findDocuments, connection);
                    using (SqlDataReader docsReader = participantCommand.ExecuteReader())
                    {
                        while (docsReader.Read())
                        {
                            MedDocument.DocumentAttachment at = new MedDocument.DocumentAttachment();
                            if (docsReader["Attachment"].ToString() != "")
                                at.Data = Convert.FromBase64String(docsReader["Attachment"].ToString());
                            else
                                at.Data = null;
                            if (docsReader["AttachmentHash"].ToString() != "")
                                at.Hash = Convert.FromBase64String(docsReader["AttachmentHash"].ToString());
                            else
                                at.Hash = null;
                            if (docsReader["AttachmentURL"].ToString() != "")
                                at.Url = new Uri(Convert.ToString(docsReader["AttachmentURL"]));
                            else
                                at.Url = null;
                            if (docsReader["MIMEType"].ToString() != "")
                                at.MimeType = docsReader["MIMEType"].ToString();
                            TestAttachment t = new TestAttachment(at);
                            if (docsReader["IdMedDocument"].ToString() != "")
                                t.idMedDocument = docsReader["IdMedDocument"].ToString();
                            if (docsReader["DocHead"].ToString() != "")
                                t.DocHead = docsReader["DocHead"].ToString();
                            if (docsReader["CreationDate"].ToString() != "")
                                t.CreationDate = Convert.ToDateTime(docsReader["CreationDate"]);
                            if (docsReader["IdDoctor"].ToString() != "")
                                t.IdDoctor = docsReader["IdDoctor"].ToString();
                            if (docsReader["IdMedDocumentType"].ToString() != "")
                                t.IdMedDocumentType = docsReader["IdMedDocumentType"].ToString();
                            taList.Add(t);
                        }
                    }
                }
            }
            if (taList.Count != 0)
                return taList;
            else
                return null;
        }
        public void FindMismatch(TestAttachment at)
        {
            if (this.attachment.Data != at.attachment.Data)
                Global.errors3.Add("несовпадение Data TestAttachment");
            if (this.attachment.Hash != at.attachment.Hash)
                Global.errors3.Add("несовпадение Hash TestAttachment");
            if (this.attachment.Url != at.attachment.Url)
                Global.errors3.Add("несовпадение Url TestAttachment");
        }
        public override bool Equals(Object obj)
        {
            TestAttachment p = obj as TestAttachment;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравнение TestAttachment с другим типом");
                return false;
            }
            if (this.attachment == p.attachment)
                return true;
            if ((this.attachment == null) || (p.attachment == null))
            {
                Global.errors3.Add("Сравнение TestAttachment = null с TestAttachment != null");
            }
            if ((this.attachment.Data == p.attachment.Data) &&
                (this.attachment.Hash == p.attachment.Hash) &&
                (this.attachment.Url == p.attachment.Url))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestAttachment a, TestAttachment b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAttachment a, TestAttachment b)
        {
            return !(a.Equals(b));
        }
    }
}
