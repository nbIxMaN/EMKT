using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixServiseTests.PixServise;
using System.Data.SqlClient;
using System.Collections;

namespace PixServiseTests
{
    class TestDocument
    {
        public enum docs : int
        {
            a = 1,
            b = 2,
            c = 3,
            d = 4,
            e = 5,
            f = 6,
            g = 7,
            h = 8,
            i = 9,
            j = 10,
            k = 11,
            l = 12,
            m = 13,
            n = 14,
            o = 15,
            p = 16,
            q = 17,
            r = 18,
            s = 23,
            t = 223,
            u = 226,
            v = 228
        };

        public DocumentDto document;

        public TestDocument(DocumentDto d)
        {
            document = d;
            if (document.IssuedDate == DateTime.MinValue)
                document.IssuedDate = null;
        }

        static public List<TestDocument> BuildDocumentsFromDataBaseData(string idPerson)
        {
            List<TestDocument> documents = new List<TestDocument>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                foreach (int i in Enum.GetValues(typeof(docs)))
                {
                    string findPatient = "SELECT TOP(1) * FROM Document WHERE IdDocument = (SELECT MAX(IdDocument) FROM Document WHERE IdPerson = '" + idPerson + "' AND IdDocumentType = '" + i + "')";
                    SqlCommand person = new SqlCommand(findPatient, connection);
                    using (SqlDataReader documentReader = person.ExecuteReader())
                    {
                        while (documentReader.Read())
                        {
                            DocumentDto doc = new DocumentDto();
                            if (documentReader["DocN"].ToString() != "")
                                doc.DocN = Convert.ToString(documentReader["DocN"]);
                            if (documentReader["ProviderName"].ToString() != "")
                                doc.ProviderName = Convert.ToString(documentReader["ProviderName"]);
                            if (documentReader["IdDocumentType"].ToString() != "")
                                doc.IdDocumentType = Convert.ToByte(documentReader["IdDocumentType"]);
                            if (documentReader["DocS"].ToString() != "")
                                doc.DocS = Convert.ToString(documentReader["DocS"]);
                            if (documentReader["IdProvider"].ToString() != "")
                                doc.IdProvider = Convert.ToString(documentReader["IdProvider"]);
                            if (documentReader["IssuedDate"].ToString() != "")
                                doc.IssuedDate = Convert.ToDateTime(documentReader["IssuedDate"]);
                            if (documentReader["ExpiredDate"].ToString() != "")
                                doc.ExpiredDate = Convert.ToDateTime(documentReader["ExpiredDate"]);
                            if (documentReader["RegionCode"].ToString() != "")
                                doc.RegionCode = Convert.ToString(documentReader["RegionCode"]);
                            TestDocument document = new TestDocument(doc);
                            documents.Add(document);
                        }
                    }
                }
            }
            if (documents.Count != 0)
                return documents;
            else
                return null;
        }

        private void FindMismatch(TestDocument b)
        {
            if (this.document.DocN != b.document.DocN)
                Global.errors3.Add("Несовпадение DocN TestDocument");
            if (this.document.DocS != b.document.DocS)
                Global.errors3.Add("Несовпадение DocS TestDocument");
            if (this.document.ExpiredDate != b.document.ExpiredDate)
                Global.errors3.Add("Несовпадение ExpiredDate TestDocument");
            if (this.document.IdDocumentType != b.document.IdDocumentType)
                Global.errors3.Add("Несовпадение IdDocumentType TestDocument");
            if (this.document.IdProvider != b.document.IdProvider)
                Global.errors3.Add("Несовпадение IdProvider TestDocument");
            if (this.document.IssuedDate != b.document.IssuedDate)
                Global.errors3.Add("Несовпадение IssuedDate TestDocument");
            if (this.document.ProviderName != b.document.ProviderName)
                Global.errors3.Add("Несовпадение ProviderName TestDocument");
            if (this.document.RegionCode != b.document.RegionCode)
                Global.errors3.Add("Несовпадение RegionCode TestDocument");
        }

        public override bool Equals(Object obj)
        {
            TestDocument p = obj as TestDocument;
            if ((object)p == null)
            {
                return false;
            }
            if (this.document == p.document)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                return false;
            }
            if ((this.document.DocN == p.document.DocN) &&
                (this.document.DocS == p.document.DocS) &&
                (this.document.DocumentName == p.document.DocumentName) &&
                (this.document.ExpiredDate == p.document.ExpiredDate) &&
                (this.document.IdDocumentType == p.document.IdDocumentType) &&
                (this.document.IdProvider == p.document.IdProvider) &&
                (this.document.IssuedDate == p.document.IssuedDate) &&
                (this.document.ProviderName == p.document.ProviderName) &&
                (this.document.RegionCode == p.document.RegionCode))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestDocument a, TestDocument b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDocument a, TestDocument b)
        {
            return !(a.Equals(b));
        }

    }
}
