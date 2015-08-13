using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
        public TestAttachment(MedDocument.DocumentAttachment at)
        {
            if ((at != null) && ((at.Data != null) || (at.Hash != null) || (at.MimeType != null) || (at.Url != null)))
            {
                attachment = at;
            }
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
                            {
                                byte[] ata = docsReader["Attachment"] as byte[];
                                if (ata != null)
                                    at.Data = ata;
                            }
                            else
                                at.Data = null;
                            if (docsReader["AttachmentHash"].ToString() != "")
                            {
                                byte[] ath = docsReader["AttachmentHash"] as byte[];
                                if (ath != null)
                                    at.Hash = ath;
                            }
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
        private void FindMismatch(TestAttachment at)
        {
            if (this.attachment.Url != at.attachment.Url)
                Global.errors3.Add("несовпадение Url TestAttachment");
            if (this.attachment.MimeType != at.attachment.MimeType)
                Global.errors3.Add("несовпадение MimeType TestAttachment");
        }
        public override bool Equals(Object obj)
        {
            TestAttachment p = obj as TestAttachment;
            if ((object)p == null)
            {
                return false;
            }
            if (this.attachment == p.attachment)
                return true;
            if ((this.attachment == null) || (p.attachment == null))
            {
                return false;
            }
            if ((this.attachment.Url == p.attachment.Url) &&
                (this.attachment.MimeType == p.attachment.MimeType))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestAttachment");
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
