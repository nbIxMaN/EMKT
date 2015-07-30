using PixServiseTests.PixServise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestPixServiceClient
    {
        //private ArrayList er = new ArrayList();
        private PixServiceClient client = new PixServiceClient();
        private string reinclusionString = "Попытка повторного добавления пациента";
        //static private string _connectionPath =
        //    "Data Source=192.168.8.93;Initial Catalog=EMKDBv3;User ID=a.pihtin;Password=stest2";

        //private string ErrorsToString()
        //{
        //    string errors = "";
        //    for (int i = 0; i < Global.er.Count; i++)
        //    {
        //        errors += (i + 1).ToString() + ". " + Global.er[i].ToString() + "\n";
        //    }
        //    return errors;
        //}

        //public string errors
        //{
        //    get 
        //    {
        //        return ErrorsToString();
        //    }
        //}

        public void AddPatient(string guid, string idlpu, PatientDto patient)
        {
            TestPatient p = new TestPatient(guid, idlpu, patient);
            try
            {
                p.DeletePatient();
                client.AddPatient(guid, idlpu, patient);
                //PatientDto pppp = new PatientDto();
                //pppp.IdPatientMIS = patient.IdPatientMIS;
                //PatientDto[] pppps = client.GetPatient(guid, idlpu, pppp, SourceType.Reg);
                p = new TestPatient(guid, idlpu, patient);
                TestPatient a = TestPatient.BuildPatientFromDataBaseData(guid, idlpu, patient.IdPatientMIS);
                if (!p.CheckPatientInDataBase())
                    Global.errors1.Add("Не совпадение значений объектов");
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.PixServise.RequestFault> we)
            {
                if (we.Detail.Message == reinclusionString)
                {
                    TestPatient wwww = TestPatient.BuildPatientFromDataBaseData(guid ,idlpu, patient.IdPatientMIS);
                    if (TestPatient.BuildPatientFromDataBaseData(guid ,idlpu, patient.IdPatientMIS) == null)
                        Global.errors1.Add("Ошибочное сообщение о попытке повторного добавления");
                    else
                        Global.errors1.Add(we.Detail.Message);
                }
                else
                {
                    Global.errors1.Add(we.Detail.Message);
                }
            }
        }

        private bool CheckField(TestPatient returnedPatient, TestPatient examplePatient)
        {
            bool mark = true;
            if ((examplePatient.patient.FamilyName == null) || (examplePatient.patient.FamilyName == returnedPatient.patient.FamilyName)) ;
            else
            {
                Global.errors1.Add("Не верная фамилия, возвращено: " + returnedPatient.patient.FamilyName.ToString());
                mark = false;
            }
            if ((examplePatient.patient.GivenName == null) || (examplePatient.patient.GivenName == returnedPatient.patient.GivenName)) ;
            else
            {
                Global.errors1.Add("Не верное имя, возвращено: " + returnedPatient.patient.GivenName.ToString());
                mark = false;
            }
            if ((examplePatient.patient.BirthDate == DateTime.MinValue) || (examplePatient.patient.BirthDate == returnedPatient.patient.BirthDate)) ;
            else
            {
                Global.errors1.Add("Не верная дата рождения, возвращено: " + returnedPatient.patient.BirthDate.ToString());
                mark = false;
            }
            if ((examplePatient.patient.Sex == 0) || (examplePatient.patient.Sex == returnedPatient.patient.Sex)) ;
            else
            {
                Global.errors1.Add("Не верный пол, возвращено: " + returnedPatient.patient.Sex.ToString());
                mark = false;
            }
            if (examplePatient.documents != null)
            {
                foreach (TestDocument doc in returnedPatient.documents)
                {
                    TestDocument exampledoc = examplePatient.documents[0];
                    if ((exampledoc.document.DocN == doc.document.DocN) &&
                        (exampledoc.document.DocS == doc.document.DocS) &&
                        (exampledoc.document.IdDocumentType == doc.document.IdDocumentType)) ;
                    else
                    {
                        Global.errors1.Add("Несоответствие документов");
                        mark = false;
                    }
                }
            }
            return mark;
        }

        public ArrayList Find(string guid, string idlpu, TestPatient patient)
        {
            string findPatient = "";
            if (patient.patient.IdPatientMIS != null)
            {
                findPatient = "SELECT * FROM ExternalId WHERE IdPersonMIS = '" + patient.patient.IdPatientMIS + "' AND SystemGuid = '" + guid + "'";
            }
            else
            {
                if (patient.patient.Sex == 0)
                {
                    findPatient = "SELECT * FROM Person WHERE FamilyName = '" + patient.patient.FamilyName + "' AND GivenName ='" + patient.patient.GivenName + "' AND BirthDate ='" + patient.patient.BirthDate + "'";
                }
                else
                {
                    findPatient = "SELECT * FROM Person WHERE FamilyName = '" + patient.patient.FamilyName + "' AND GivenName ='" + patient.patient.GivenName + "' AND BirthDate ='" + patient.patient.BirthDate + "' AND IdSex ='" + patient.patient.Sex + "'";
                }
            }
//            ContactPersonDto p = new ContactPersonDto();
            ArrayList patients = new ArrayList();
            string patientId = "";
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                SqlCommand findPatients = new SqlCommand(findPatient, connection);
                using (SqlDataReader patientsReader = findPatients.ExecuteReader())
                {
                    //int i = 0;
                    while (patientsReader.Read())
                    {
                        patientId = Convert.ToString(patientsReader["IdPerson"]);
                        if (patient.documents != null)
                        {
                            string findDoc = "";
                            if (patient.documents[0].document.DocS != null)
                                findDoc = "SELECT * FROM Document WHERE IdPerson = '" + patientId + "' AND DocS ='" + patient.documents[0].document.DocS + "' AND DocN ='" + patient.documents[0].document.DocN + "'";
                            else
                                findDoc = "SELECT * FROM Document WHERE IdPerson = '" + patientId + "' AND DocN ='" + patient.documents[0].document.DocN + "'";
                            using (SqlConnection connection2 = Global.GetSqlConnection())
                            {
                                SqlCommand findPatients2 = new SqlCommand(findDoc, connection2);
                                using (SqlDataReader patientsReader2 = findPatients2.ExecuteReader())
                                {
                                    string addedId = "";
                                    while (patientsReader2.Read())
                                    {
                                        if (addedId != patientId)
                                        {
                                            TestPatient patientFromDb = TestPatient.BuildPatientFromDataBaseData(patientId: patientId);
                                            addedId = patientId;
                                            patientFromDb.GUID = guid;
                                            patientFromDb.IDLPU = idlpu;
                                            patients.Add(patientFromDb);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            TestPatient patientFromDb2 = TestPatient.BuildPatientFromDataBaseData(patientId: patientId);
                            patients.Add(patientFromDb2);
                        }
                    }
                }
            }
            return patients;
        }


        public PatientDto[] GetPatient(string guid, string idlpu, PatientDto patient, SourceType type)
        {
            TestPatient example = new TestPatient(guid, idlpu, patient);
            try
            {
                ArrayList patientsFromDB = new ArrayList();
                if (type == SourceType.Reg)
                    patientsFromDB = Find(guid, idlpu, example);
                PatientDto[] patients = client.GetPatient(guid, idlpu, patient, type);
                if (patients != null)
                    foreach (PatientDto a in patients)
                    {
                        TestPatient getting = new TestPatient(guid, idlpu, a);
                        CheckField(getting, example);
                        if (type == SourceType.Reg)
                        {
                            bool mark = false;
                            foreach (TestPatient i in patientsFromDB)
                            {
                                if (i.Equals(getting))
                                {
                                    mark = true;
                                }
                            }
                            if (patients.Length != patientsFromDB.Count)
                                Global.errors1.Add("Не все пациенты из базы возвращены");
                            if (!mark)
                            {
                                Global.errors1.AddRange(Global.errors2);
                                Global.errors2.Clear();
                                Global.errors1.Add("Возвращенный пациент не соответствует ни одному из базы");
                            }                        
                        }
                    }
                return patients;
            }
            catch (System.ServiceModel.FaultException<PixServiseTests.PixServise.RequestFault> we)
            {
                Global.errors1.Add(we.Detail.Message);
                return null;
            }
        }

        public void UpdatePatient(string guid, string idlpu, PatientDto patient) 
        {
            TestPatient example = TestPatient.BuildPatientFromDataBaseData(guid, idlpu, patient.IdPatientMIS);
            example.ChangePatientField(patient);
            client.UpdatePatient(guid, idlpu, patient);
            if (!example.CheckPatientInDataBase())
                Global.errors1.Add("Не совпадение значений объектов");
        }
    }
}
