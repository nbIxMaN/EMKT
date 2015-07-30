﻿//using PixServiseTests.EMKServise;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PixServiseTests
//{
//    class TestAmbCase
//    {
//        public string GUID;
//        public CaseAmb caseAmb;
//        public List<TestMedRecord> records;
//        public Array medRecords
//        {
//            get
//            {
//                if (records != null)
//                    return records.ToArray();
//                else
//                    return null;
//            }
//        }
//        public TestCaseBase caseBase;
//        public List<TestAmbStep> ambSteps;
//        public Array steps
//        {
//            get
//            {
//                if (ambSteps != null)
//                    return ambSteps.ToArray();
//                else
//                    return null;
//            }
//        }
//        public TestStepBase defaultStep;

//        public TestAmbCase(string guid, CaseAmb ca)
//        {
//            GUID = guid.ToLower();
//            if (ca != null)
//            {
//                caseAmb = ca;
//                caseBase = new TestCaseBase(guid, ca);
//                if (ca.Steps != null)
//                {
//                    ambSteps = new List<TestAmbStep>();
//                    foreach (StepAmb i in ca.Steps)
//                    {
//                        ambSteps.Add(new TestAmbStep(i, ca.IdLpu));
//                    }
//                }
//                if (ca.MedRecords != null)
//                {
//                    records = new List<TestMedRecord>();
//                    foreach (object i in ca.MedRecords)
//                    {
//                        MedDocument doc = i as MedDocument;
//                        if (i != null)
//                            records.Add(new TestMedDocument(doc));
//                    }
//                }
//                if ((ca.IdLpu != null) && (ca.IdPatientMis != null))
//                {
//                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(TestCaseBase.GetCaseId(guid, ca.IdLpu, ca.IdCaseMis, TestPerson.GetPersonId(guid, ca.IdLpu, ca.IdPatientMis)), ca.IdLpu);
//                    if (st != null)
//                    {
//                        foreach (TestStepBase i in st)
//                        {
//                            if (Global.IsEqual(i.doctor, null))
//                                defaultStep = i;
//                        }
//                    }
//                }
//            }
//        }
//        static public TestAmbCase BuildAmbCaseFromDataBaseData(string guid, string idlpu, string mis, string patientId)
//        {
//            if ((guid != string.Empty) && (idlpu != string.Empty) && (mis != string.Empty) && (patientId != string.Empty))
//            {
//                string caseId = TestCaseBase.GetCaseId(guid, idlpu, mis, patientId);
//                if (caseId != null)
//                {
//                    CaseAmb ca = new CaseAmb();
//                    using (SqlConnection connection = Global.GetSqlConnection())
//                    {
//                        string findCase = "SELECT TOP(1) * FROM AmbCase WHERE IdCase = '" + caseId + "'";
//                        SqlCommand caseCommand = new SqlCommand(findCase, connection);
//                        using (SqlDataReader caseReader = caseCommand.ExecuteReader())
//                        {
//                            while (caseReader.Read())
//                            {
//                                if (caseReader["IdCasePurpose"].ToString() != "")
//                                    ca.IdCasePurpose = Convert.ToByte(caseReader["IdCasePurpose"]);
//                                if (caseReader["IdAmbResult"].ToString() != "")
//                                    ca.IdAmbResult = Convert.ToByte(caseReader["IdAmbResult"]);
//                                if (caseReader["IsActive"].ToString() != "")
//                                    ca.IsActive = Convert.ToBoolean(caseReader["IsActive"]);
//                            }
//                        }
//                    }
//                    TestAmbCase ambcase = new TestAmbCase(guid, ca);
//                    ambcase.caseBase = TestCaseBase.BuildBaseCaseFromDataBaseData(guid, idlpu, mis, patientId);
//                    ambcase.ambSteps = TestAmbStep.BuildAmbTestStepsFromDataBase(caseId, ca.IdLpu);
//                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(TestCaseBase.GetCaseId(guid, ca.IdLpu, ca.IdCaseMis, patientId), ca.IdLpu);
//                    if (st != null)
//                    {
//                        foreach (TestStepBase i in st)
//                        {
//                            if (Global.IsEqual(i.doctor, null))
//                                ambcase.defaultStep = i;
//                        }
//                        ambcase.records = TestMedDocument.BuildTestMedDocumentsFromDataBase(ambcase.defaultStep.idStep);
//                    }
//                    return ambcase;
//                }
//            }
//            return null;
//        }
//        public void FindMismatch(TestAmbCase ac)
//        {
//            if (this.caseAmb.IsActive != ac.caseAmb.IsActive)
//                Global.errors2.Add("несовпадение IsActive caseAmb");
//            if (this.caseAmb.IdAmbResult != ac.caseAmb.IdAmbResult)
//                Global.errors2.Add("несовпадение IdAmbResult caseAmb");
//            if (this.caseAmb.IdCasePurpose != ac.caseAmb.IdCasePurpose)
//                Global.errors2.Add("несовпадение IdCasePurpose caseAmb");
//            Global.IsEqual(this.caseBase, ac.caseBase);
//            Global.IsEqual(this.ambSteps, ac.ambSteps);
//            Global.IsEqual(this.records, ac.records);
//        }
//        public bool CheckCaseInDataBase()
//        {
//            string patientId = TestPerson.GetPersonId(GUID, caseAmb.IdLpu, caseAmb.IdPatientMis);
//            TestAmbCase ac = TestAmbCase.BuildAmbCaseFromDataBaseData(GUID, caseAmb.IdLpu, caseAmb.IdCaseMis, patientId);
//            this.Equals(ac);
//            return (this == ac);
//        }
//        public override bool Equals(Object obj)
//        {
//            TestAmbCase p = obj as TestAmbCase;
//            if ((object)p == null)
//            {
//                Global.errors2.Add("Сравнение TestAmbCase с другим типом");
//                return false;
//            }
//            bool x = Global.IsEqual(this.caseBase, p.caseBase);
//            x = Global.IsEqual(this.medRecords, p.medRecords);
//            x = Global.IsEqual(this.steps, p.steps);
//            x = (this.caseAmb.IsActive == p.caseAmb.IsActive);
//            x = (this.caseAmb.IdAmbResult == p.caseAmb.IdAmbResult);
//            x = (this.caseAmb.IdCasePurpose == p.caseAmb.IdCasePurpose);
//            if (Global.IsEqual(this.caseBase, p.caseBase)&&
//                Global.IsEqual(this.medRecords, p.medRecords)&&
//                Global.IsEqual(this.steps, p.steps)&&
//                (this.caseAmb.IsActive == p.caseAmb.IsActive)&&
//                (this.caseAmb.IdAmbResult == p.caseAmb.IdAmbResult)&&
//                (this.caseAmb.IdCasePurpose == p.caseAmb.IdCasePurpose))
//            {
//                return true;
//            }
//            else
//            {
//                this.FindMismatch(p);
//                return false;
//            }
//        }
//        public static bool operator ==(TestAmbCase a, TestAmbCase b)
//        {
//            return a.Equals(b);
//        }
//        public static bool operator !=(TestAmbCase a, TestAmbCase b)
//        {
//            return !(a.Equals(b));
//        }
//    }
//}