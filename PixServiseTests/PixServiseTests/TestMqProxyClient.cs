using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixServiseTests.EMKServise;
using PixServiseTests.mqProxy;

namespace PixServiseTests
{
    class TestMqProxyClient : IDisposable
    {
        private MqServiceClient client = new MqServiceClient();
        private bool disposed = false;
        public void getErrors(object obj)
        {
            Array errors = obj as Array;
            if (errors != null)
            {
                foreach (RequestFault i in errors)
                {
                    Global.errors1.Add(i.PropertyName + " - " + i.Message);
                    getErrors(i.Errors);
                }
            }
        }

        public byte[] GetResultDocument(string GUID, string idLpu, string idCaseMis, int documentType)
        {
            ResultDocument a = client.GetResultDocument(GUID, idLpu, idCaseMis, documentType);
            byte[] b = {};
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient =
                    "SELECT TOP(1) Attachment FROM MedDocument, Step, \"Case\" WHERE IdMedDocumentType = '" +
                    documentType +
                    "' AND IdCaseMis = '" + idCaseMis + "' AND IdLpu <= '" + Global.GetIdInstitution(idLpu) +
                    "' AND MedDocument.IdStep = Step.IdStep AND Step.IdCase = \"Case\".IdCase ORDER BY IdMedDocument DESC";
                SqlCommand person = new SqlCommand(findPatient, connection);
                using (SqlDataReader documentReader = person.ExecuteReader())
                {
                    // bool a = documentReader.Read();
                    while (documentReader.Read())
                    {
                        b = (byte[])(documentReader["Attachment"]);
                    }
                }
            }
            if (!a.Data.SequenceEqual(b))
                Global.errors1.Add("Несовпадение базового и возвращенного документов");
            return a.Data;
        }

        ~TestMqProxyClient()
        {
            client.Close();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                client.Close();
                disposed = true;
            }
        }
    }
}
