using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestIdentityDocument
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

        public IdentityDocument document;

        public TestIdentityDocument(IdentityDocument d)
        {
            document = d;
            if (document.IssuedDate == DateTime.MinValue)
                document.IssuedDate = null;
        }

        static public List<TestIdentityDocument> BuildDocumentsFromDataBaseData(string idPerson)
        {
            List<TestIdentityDocument> documents = new List<TestIdentityDocument>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                foreach (int i in Enum.GetValues(typeof(docs)))
                {
                    string findPatient = "SELECT TOP(1) * FROM Document WHERE IdDocument = (SELECT MAX(IdDocument) FROM Document WHERE IdPerson = '" + idPerson + "' AND IdDocumentType = '" + i + "')";
                    SqlCommand person = new SqlCommand(findPatient, connection);
                    using (SqlDataReader documentReader = person.ExecuteReader())
                    {
                        // bool a = documentReader.Read();
                        while (documentReader.Read())
                        {
                            IdentityDocument doc = new IdentityDocument();
                            doc.DocN = Convert.ToString(documentReader["DocN"]);
                            doc.ProviderName = Convert.ToString(documentReader["ProviderName"]);
                            doc.IdDocumentType = Convert.ToByte(documentReader["IdDocumentType"]);
                            if (documentReader["DocS"].ToString() != "")
                                doc.DocS = Convert.ToString(documentReader["DocS"]);
                            else
                                doc.DocS = null;
                            if (documentReader["IdProvider"].ToString() != "")
                                doc.IdProvider = Convert.ToInt32(documentReader["IdProvider"]);
                            else
                                doc.IdProvider = null;
                            if (documentReader["IssuedDate"].ToString() != "")
                                doc.IssuedDate = Convert.ToDateTime(documentReader["IssuedDate"]);
                            else
                                doc.IssuedDate = null;
                            if (documentReader["ExpiredDate"].ToString() != "")
                                doc.ExpiredDate = Convert.ToDateTime(documentReader["ExpiredDate"]);
                            else
                                doc.ExpiredDate = null;
                            if (documentReader["RegionCode"].ToString() != "")
                                doc.RegionCode = Convert.ToString(documentReader["RegionCode"]);
                            else
                                doc.RegionCode = null;
                            TestIdentityDocument document = new TestIdentityDocument(doc);
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

        private void FindMismatch(TestIdentityDocument b)
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

        public bool CheckDocumentsInDataBase(string patientId)
        {
            List<TestIdentityDocument> docs = BuildDocumentsFromDataBaseData (patientId);
            foreach (TestIdentityDocument document in docs)
            {
                if (this != document)
                {
                    this.FindMismatch(document);
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            TestIdentityDocument p = obj as TestIdentityDocument;
            if ((object)p == null)
            {
                Global.errors3.Add("Сравение TestDocument с другим типом");
                return false;
            }
            if (this.document == p.document)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                Global.errors3.Add("Сравнение TestDocument = null с TestDocument != null");
                return false;
            }
            if ((this.document.DocN == p.document.DocN) &&
                (this.document.DocS == p.document.DocS) &&
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
        public static bool operator ==(TestIdentityDocument a, TestIdentityDocument b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestIdentityDocument a, TestIdentityDocument b)
        {
            return !(a.Equals(b));
        }

    }
}
