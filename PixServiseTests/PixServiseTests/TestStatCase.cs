using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestStatCase
    {
        public string GUID;
        public CaseStat caseStat;
        public List<TestMedRecord> records;
        public Array medRecords
        {
            get
            {
                Service a = new Service();
                if (records != null)
                    return records.ToArray();
                else
                    return null;
            }
        }
        public List<TestStatStep> statSteps;
        public Array steps
        {
            get
            {
                if (statSteps != null)
                    return statSteps.ToArray();
                else
                    return null;
            }
        }
        public TestStepBase defaultStep;
        public TestCaseBase caseBase;
        
        public TestStatCase(string guid, CaseStat cs)
        {
            GUID = guid.ToLower();
            if (cs != null)
            {
                caseStat = cs;
                caseBase = new TestCaseBase(guid, cs);
                if (cs.Steps != null)
                {
                    statSteps = new List<TestStatStep>();
                    foreach(StepStat i in cs.Steps)
                    {
                        statSteps.Add(new TestStatStep(i, cs.IdLpu));
                    }
                }
                if (cs.MedRecords != null)
                {
                    records = new List<TestMedRecord>();
                    foreach (object i in cs.MedRecords)
                    {
                        TfomsInfo tfi = i as TfomsInfo;
                        if (tfi != null)
                            records.Add(new TestTfomsInfo(tfi));
                        DeathInfo di = i as DeathInfo;
                        if (di != null)
                            records.Add(new TestDeathInfo(di));
                        AnatomopathologicalClinicMainDiagnosis acmd = i as AnatomopathologicalClinicMainDiagnosis;
                        if (acmd != null)
                            records.Add(new TestClinicMainDiagnosis(acmd));
                        Referral r = i as Referral;
                        if (r != null)
                            records.Add(new TestReferral(r));
                    }
                }
                if ((cs.IdLpu != null) && (cs.IdPatientMis != null))
                {
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(TestCaseBase.GetCaseId(guid, cs.IdLpu, cs.IdCaseMis, TestPerson.GetPersonId(guid, cs.IdLpu, cs.IdPatientMis)), cs.IdLpu);
                    if (st != null)
                    {
                        foreach (TestStepBase i in st)
                        {
                            if (Global.IsEqual(i.doctor, null))
                                defaultStep = i;
                        }
                    }
                }
            }
        }
        static public TestStatCase BuildAmbCaseFromDataBaseData(string guid, string idlpu, string mis, string patientId)
        {
            if ((guid != string.Empty) && (idlpu != string.Empty) && (mis != string.Empty))
            {
                string caseId = TestCaseBase.GetCaseId(guid, idlpu, mis, patientId);
                if (caseId != null)
                {
                    CaseStat ca = new CaseStat();
                    using (SqlConnection connection = Global.GetSqlConnection())
                    {
                        string findCase = "SELECT TOP(1) * FROM HospCase WHERE IdCase = '" + caseId + "'";
                        SqlCommand caseCommand = new SqlCommand(findCase, connection);
                        using (SqlDataReader caseReader = caseCommand.ExecuteReader())
                        {
                            while (caseReader.Read())
                            {
                                if (caseReader["DeliveredCode"].ToString() != "")
                                    ca.DeliveryCode = Convert.ToString(caseReader["DeliveredCode"]);
                                if (caseReader["IdIntoxicationType"].ToString() != "")
                                    ca.IdIntoxicationType = Convert.ToByte(caseReader["IdIntoxicationType"]);
                                if (caseReader["IdPatientConditionOnAdmission"].ToString() != "")
                                    ca.IdPatientConditionOnAdmission = Convert.ToByte(caseReader["IdPatientConditionOnAdmission"]);
                                if (caseReader["IdTypeFromDiseaseStart"].ToString() != "")
                                    ca.IdTypeFromDiseaseStart = Convert.ToByte(caseReader["IdTypeFromDiseaseStart"]);
                                if (caseReader["IdTransportIntern"].ToString() != "")
                                    ca.IdTransportIntern = Convert.ToByte(caseReader["IdTransportIntern"]);
                                if (caseReader["IdHospChannel"].ToString() != "")
                                    ca.IdHospChannel = Convert.ToByte(caseReader["IdHospChannel"]);
                                if (caseReader["RW1Mark"].ToString() != "")
                                    ca.RW1Mark = Convert.ToBoolean(caseReader["RW1Mark"]);
                                if (caseReader["AIDSMark"].ToString() != "")
                                    ca.AIDSMark = Convert.ToBoolean(caseReader["AIDSMark"]);
                            }
                        }
                    }
                    TestStatCase statcase = new TestStatCase(guid, ca);
                    statcase.caseBase = TestCaseBase.BuildBaseCaseFromDataBaseData(guid, idlpu, mis, patientId);
                    statcase.statSteps = TestStatStep.BuildStatStepsFromDataBase(caseId, ca.IdLpu, patientId);
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(caseId, ca.IdLpu);
                    if (st != null)
                    {
                        foreach (TestStepBase i in st)
                        {
                            if (Global.IsEqual(i.doctor, null))
                                statcase.defaultStep = i;
                        }
                        statcase.records = new List<TestMedRecord>();
                        List<TestTfomsInfo> forms = TestTfomsInfo.BuildTfomsInfoFromDataBaseDate(statcase.defaultStep.idStep);
                        if (!Global.IsEqual(forms, null))
                            statcase.records.AddRange(forms);
                        TestDeathInfo tdi = TestDeathInfo.BuildDeathInfoFromDataBaseDate(statcase.defaultStep.idStep);
                        if (!Global.IsEqual(tdi, null))
                            statcase.records.Add(tdi);
                        List<TestClinicMainDiagnosis> acdm = TestClinicMainDiagnosis.BuildTestClinicMainDiagnosisFromDataBaseDate(statcase.defaultStep.idStep);
                        if (!Global.IsEqual(acdm, null))
                            statcase.records.AddRange(acdm);
                        List<TestReferral> trl = TestReferral.BuildReferralFromDataBaseData(statcase.defaultStep.idStep);
                        if (!Global.IsEqual(trl, null))
                            statcase.records.AddRange(trl);
                    }
                    return statcase;
                }
            }
            return null;
        }
        public void FindMismatch(TestStatCase cs)
        {
            if (this.caseStat.DeliveryCode != cs.caseStat.DeliveryCode)
                Global.errors2.Add("несовпадение DeliveryCode caseStat");
            if (this.caseStat.IdIntoxicationType != cs.caseStat.IdIntoxicationType)
                Global.errors2.Add("несовпадение IdIntoxicationType caseStat");
            if (this.caseStat.IdPatientConditionOnAdmission != cs.caseStat.IdPatientConditionOnAdmission)
                Global.errors2.Add("несовпадение IdPatientConditionOnAdmission caseStat");
            if (this.caseStat.IdTypeFromDiseaseStart != cs.caseStat.IdTypeFromDiseaseStart)
                Global.errors2.Add("несовпадение IdTypeFromDiseaseStart caseStat");
            if (this.caseStat.IdTransportIntern != cs.caseStat.IdTransportIntern)
                Global.errors2.Add("несовпадение IdTransportIntern caseStat");
            if (this.caseStat.IdHospChannel != cs.caseStat.IdHospChannel)
                Global.errors2.Add("несовпадение IdHospChannel caseStat");
            if (this.caseStat.RW1Mark != cs.caseStat.RW1Mark)
                Global.errors2.Add("несовпадение RW1Mark caseStat");
            if (this.caseStat.AIDSMark != cs.caseStat.AIDSMark)
                Global.errors2.Add("несовпадение AIDSMark caseStat");
            //if (!Global.IsEqual(this.caseBase, cs.caseBase))
            //    Global.errors2.Add("несовпадение caseBase caseStat");
            //if (!Global.IsEqual(this.medRecords, cs.medRecords))
            //    Global.errors2.Add("несовпадение medRecords caseStat");
        }
        public bool CheckCaseInDataBase()
        {
            string patientId = TestPerson.GetPersonId(GUID, caseStat.IdLpu, caseStat.IdPatientMis);
            TestStatCase ac = TestStatCase.BuildAmbCaseFromDataBaseData(GUID, caseStat.IdLpu, caseStat.IdCaseMis, patientId);
//            this.Equals(ac);
            return (this == ac);
        }
        public override bool Equals(Object obj)
        {
            TestStatCase p = obj as TestStatCase;
            if ((object)p == null)
            {
                return false;
            }
            if (Global.IsEqual(this.caseBase, p.caseBase)&&
                (Global.IsEqual(this.medRecords, p.medRecords)) &&
                (Global.IsEqual(this.steps, p.steps)) &&
                (this.caseStat.DeliveryCode == p.caseStat.DeliveryCode) &&
                (this.caseStat.IdIntoxicationType  == p.caseStat.IdIntoxicationType) &&
                (this.caseStat.IdPatientConditionOnAdmission == p.caseStat.IdPatientConditionOnAdmission)&&
                (this.caseStat.IdTypeFromDiseaseStart == p.caseStat.IdTypeFromDiseaseStart) &&
                (this.caseStat.IdTransportIntern == p.caseStat.IdTransportIntern) &&
                (this.caseStat.IdHospChannel == p.caseStat.IdHospChannel) &&
                (this.caseStat.RW1Mark  == p.caseStat.RW1Mark) &&
                (this.caseStat.AIDSMark == p.caseStat.AIDSMark))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestStatCase a, TestStatCase b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestStatCase a, TestStatCase b)
        {
            return !(a.Equals(b));
        }
    }
}
