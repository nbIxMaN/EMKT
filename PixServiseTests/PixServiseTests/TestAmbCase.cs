using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestAmbCase
    {
        private const byte dispanseryId = 4;
        public string GUID;
        public CaseAmb caseAmb;
        public List<TestMedRecord> records;
        public Array medRecords
        {
            get
            {
                if (records != null)
                    return records.ToArray();
                else
                    return null;
            }
        }
        public TestCaseBase caseBase;
        public List<TestAmbStep> ambSteps;
        public Array steps
        {
            get
            {
                if (ambSteps != null)
                    return ambSteps.ToArray();
                else
                    return null;
            }
        }
        public TestStepBase defaultStep;

        public TestAmbCase(string guid, CaseAmb ca)
        {
            GUID = guid.ToLower();
            if (ca != null)
            {
                caseAmb = ca;
                caseBase = new TestCaseBase(guid, ca);
                if (ca.Steps != null)
                {
                    ambSteps = new List<TestAmbStep>();
                    foreach (StepAmb i in ca.Steps)
                    {
                        ambSteps.Add(new TestAmbStep(i, ca.IdLpu));
                    }
                }
                if (ca.MedRecords != null)
                {
                    records = new List<TestMedRecord>();
                    foreach (object i in ca.MedRecords)
                    {
                        Service ser = i as Service;
                        if (ser != null)
                            records.Add(new TestService(ser, ca.IdLpu));
                        TfomsInfo tfi = i as TfomsInfo;
                        if (tfi != null)
                            records.Add(new TestTfomsInfo(tfi));
                        ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
                        if ((cmd != null) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            records.Add(new TestClinicMainDiagnosis(cmd, ca.IdLpu));
                        if (cmd == null)
                        {
                            Diagnosis diag = i as Diagnosis;
                            if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                                records.Add(new TestClinicMainDiagnosis(diag, ca.IdLpu));
                            if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                                records.Add(new TestDiagnosis(diag, ca.IdLpu));
                        }
                        Referral r = i as Referral;
                        if (r != null)
                            records.Add(new TestReferral(r, ca.IdLpu));
                        SickList sl = i as SickList;
                        if (sl != null)
                            records.Add(new TestSickList(sl, ca.IdLpu));
                        DischargeSummary ds = i as DischargeSummary;
                        if (ds != null)
                            records.Add(new TestDischargeSummary(ds, ca.IdLpu));
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            records.Add(new TestLaboratoryReport(lr, ca.IdLpu));
                        ConsultNote cn = i as ConsultNote;
                        if (cn != null)
                            records.Add(new TestConsultNote(cn, ca.IdLpu));
                        DispensaryOne d1 = i as DispensaryOne;
                        if ((d1 != null) && (ca.IdCaseType == TestAmbCase.dispanseryId))
                            records.Add(new TestDispensaryOne(d1, ca.IdLpu));
                    }
                }
                if ((ca.IdLpu != null) && (ca.IdPatientMis != null))
                {
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(TestCaseBase.GetCaseId(guid, ca.IdLpu, ca.IdCaseMis, TestPerson.GetPersonId(guid, ca.IdLpu, ca.IdPatientMis)), ca.IdLpu);
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
        static public TestAmbCase BuildAmbCaseFromDataBaseData(string guid, string idlpu, string mis, string patientId)
        {
            if ((guid != string.Empty) && (idlpu != string.Empty) && (mis != string.Empty) && (patientId != string.Empty))
            {
                string caseId = TestCaseBase.GetCaseId(guid, idlpu, mis, patientId);
                if (caseId != null)
                {
                    CaseAmb ca = new CaseAmb();
                    using (SqlConnection connection = Global.GetSqlConnection())
                    {
                        string findCase = "SELECT TOP(1) * FROM AmbCase WHERE IdCase = '" + caseId + "'";
                        SqlCommand caseCommand = new SqlCommand(findCase, connection);
                        using (SqlDataReader caseReader = caseCommand.ExecuteReader())
                        {
                            while (caseReader.Read())
                            {
                                if (caseReader["IdCasePurpose"].ToString() != "")
                                    ca.IdCasePurpose = Convert.ToByte(caseReader["IdCasePurpose"]);
                                if (caseReader["IdAmbResult"].ToString() != "")
                                    ca.IdAmbResult = Convert.ToByte(caseReader["IdAmbResult"]);
                                if (caseReader["IsActive"].ToString() != "")
                                    ca.IsActive = Convert.ToBoolean(caseReader["IsActive"]);
                            }
                        }
                    }
                    TestAmbCase ambcase = new TestAmbCase(guid, ca);
                    ambcase.caseBase = TestCaseBase.BuildBaseCaseFromDataBaseData(guid, idlpu, mis, patientId);
                    ambcase.ambSteps = TestAmbStep.BuildAmbTestStepsFromDataBase(caseId, ca.IdLpu);
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(caseId, ca.IdLpu);
                    if (st != null)
                    {
                        foreach (TestStepBase i in st)
                        {
                            if (Global.IsEqual(i.step.IdStepMis, null))
                                ambcase.defaultStep = i;
                        }
                        if (!Global.IsEqual(ambcase.defaultStep, null))
                        {
                            ambcase.records = new List<TestMedRecord>();
                            TestTfomsInfo forms = TestTfomsInfo.BuildTfomsInfoFromDataBaseDate(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(forms, null))
                                ambcase.records.Add(forms);
                            TestDeathInfo tdi = TestDeathInfo.BuildDeathInfoFromDataBaseDate(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(tdi, null))
                                ambcase.records.Add(tdi);
                            List<TestDiagnosis> td = TestDiagnosis.BuildDiagnosisFromDataBaseDate(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(td, null))
                                ambcase.records.AddRange(td);
                            TestClinicMainDiagnosis acdm = TestClinicMainDiagnosis.BuildTestClinicMainDiagnosisFromDataBaseDate(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(acdm, null))
                                ambcase.records.Add(acdm);
                            List<TestReferral> trl = TestReferral.BuildReferralFromDataBaseData(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(trl, null))
                                ambcase.records.AddRange(trl);
                            List<TestSickList> tsl = TestSickList.BuildSickListFromDataBaseData(ambcase.defaultStep.idStep, patientId);
                            if (!Global.IsEqual(tsl, null))
                                ambcase.records.AddRange(trl);
                            TestDischargeSummary tds = TestDischargeSummary.BuildSickListFromDataBaseData(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(tds, null))
                                ambcase.records.Add(tds);
                            List<TestLaboratoryReport> tlr = TestLaboratoryReport.BuildSickListFromDataBaseData(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(tlr, null))
                                ambcase.records.AddRange(tlr);
                            TestConsultNote tcn = TestConsultNote.BuildSickListFromDataBaseData(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(tcn, null))
                                ambcase.records.Add(tcn);
                            TestDispensaryOne d1 = TestDispensaryOne.BuildSickListFromDataBaseData(ambcase.defaultStep.idStep);
                            if (!Global.IsEqual(d1, null))
                                ambcase.records.Add(d1);
                            if (ambcase.records.Count == 0)
                                ambcase.records = null;
                        }
                    }
                    return ambcase;
                }
            }
            return null;
        }
        public void ChangeUpdateAmbCase(string guid, CaseAmb ca)
        {
            GUID = guid.ToLower();
            if (ca != null)
            {
                if (ca.IsActive != null)
                    this.caseAmb.IsActive = ca.IsActive;
                if (ca.IdAmbResult != null)
                    this.caseAmb.IdAmbResult = ca.IdAmbResult;
                if (ca.IdCasePurpose != null)
                    this.caseAmb.IdCasePurpose = ca.IdCasePurpose;
                caseBase.UpdateCaseBase(guid, ca);
                if (ca.MedRecords != null)
                {
                    List<TestMedRecord> newMedRecords = new List<TestMedRecord>();
                    TestTfomsInfo tfi = null;
                    TestClinicMainDiagnosis cmd = null;
                    TestDischargeSummary ds = null;
                    TestConsultNote cn = null;
                    TestDispensaryOne d1 = null;
                    foreach (object i in ca.MedRecords)
                    {
                        Service ser = i as Service;
                        if (ser != null)
                            newMedRecords.Add(new TestService(ser, ca.IdLpu));
                        TfomsInfo tf = i as TfomsInfo;
                        if (tf != null)
                            tfi = new TestTfomsInfo(tf);
                        Diagnosis diag = i as Diagnosis;
                        if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                            newMedRecords.Add(new TestDiagnosis(diag, ca.IdLpu));
                        ClinicMainDiagnosis cm = i as ClinicMainDiagnosis;
                        if ((cm != null) && (cm.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            cmd = new TestClinicMainDiagnosis(cm, ca.IdLpu);
                        Referral r = i as Referral;
                        if (r != null)
                            newMedRecords.Add(new TestReferral(r, ca.IdLpu));
                        SickList sl = i as SickList;
                        if (sl != null)
                            newMedRecords.Add(new TestSickList(sl, ca.IdLpu));
                        DischargeSummary pds = i as DischargeSummary;
                        if (pds != null)
                            ds = new TestDischargeSummary(pds, ca.IdLpu);
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            newMedRecords.Add(new TestLaboratoryReport(lr, ca.IdLpu));
                        ConsultNote pcn = i as ConsultNote;
                        if (pcn != null)
                            cn = new TestConsultNote(pcn, ca.IdLpu);
                        DispensaryOne d = i as DispensaryOne;
                        if ((d != null) && (ca.IdCaseType == TestAmbCase.dispanseryId))
                            d1 = new TestDispensaryOne(d, ca.IdLpu);
                    }
                    foreach (object i in this.medRecords)
                    {
                        TestService ser = i as TestService;
                        if (ser != null)
                            newMedRecords.Add(ser);
                        TestTfomsInfo tf = i as TestTfomsInfo;
                        if ((tfi != null) && (tf != null))
                            newMedRecords.Add(tf);
                        else
                            if (tfi != null)
                                newMedRecords.Add(tfi);
                        TestDiagnosis diag = i as TestDiagnosis;
                        if (diag != null)
                            newMedRecords.Add(diag);
                        TestClinicMainDiagnosis cm = i as TestClinicMainDiagnosis;
                        if (cmd != null)
                            newMedRecords.Add(cmd);
                        else
                            if (cm != null)
                                newMedRecords.Add(cm);
                        TestReferral r = i as TestReferral;
                        if (r != null)
                            newMedRecords.Add(r);
                        TestSickList sl = i as TestSickList;
                        if (sl != null)
                            newMedRecords.Add(sl);
                        TestDischargeSummary pds = i as TestDischargeSummary;
                        if (ds != null)
                            newMedRecords.Add(ds);
                        else
                            if (pds != null)
                                newMedRecords.Add(pds);
                        TestLaboratoryReport lr = i as TestLaboratoryReport;
                        if (lr != null)
                            newMedRecords.Add(lr);
                        TestConsultNote pcn = i as TestConsultNote;
                        if (cn != null)
                            newMedRecords.Add(cn);
                        else
                            if (pcn != null)
                                newMedRecords.Add(pcn);
                        TestDispensaryOne d = i as TestDispensaryOne;
                        if (d1 != null)
                            newMedRecords.Add(d1);
                        else
                            if (d != null)
                                newMedRecords.Add(d);
                    }
                }
            }
        }
        public void FindMismatch(TestAmbCase ac)
        {
            if (this.caseAmb.IsActive != ac.caseAmb.IsActive)
                Global.errors2.Add("несовпадение IsActive caseAmb");
            if (this.caseAmb.IdAmbResult != ac.caseAmb.IdAmbResult)
                Global.errors2.Add("несовпадение IdAmbResult caseAmb");
            if (this.caseAmb.IdCasePurpose != ac.caseAmb.IdCasePurpose)
                Global.errors2.Add("несовпадение IdCasePurpose caseAmb");
            if (Global.GetLength(this.steps) != Global.GetLength(ac.steps))
                Global.errors2.Add("несовпадение длины Steps caseAmb");
            if (Global.GetLength(this.medRecords) != Global.GetLength(ac.medRecords))
                Global.errors2.Add("Несовпадение длины MedRecords CaseAmb");
            if (Global.GetLength(this.caseBase) != Global.GetLength(ac.caseBase))
                Global.errors2.Add("Несовпадение длины caseBase CaseAmb");
        }
        public bool CheckCaseInDataBase()
        {
            string patientId = TestPerson.GetPersonId(GUID, caseAmb.IdLpu, caseAmb.IdPatientMis);
            TestAmbCase ac = TestAmbCase.BuildAmbCaseFromDataBaseData(GUID, caseAmb.IdLpu, caseAmb.IdCaseMis, patientId);
            this.Equals(ac);
            return (this == ac);
        }
        public override bool Equals(Object obj)
        {
            TestAmbCase p = obj as TestAmbCase;
            if ((object)p == null)
            {
                Global.errors2.Add("Сравнение TestAmbCase с другим типом");
                return false;
            }
            if (Global.IsEqual(this.caseBase, p.caseBase) &&
                Global.IsEqual(this.medRecords, p.medRecords) &&
                Global.IsEqual(this.steps, p.steps) &&
                (this.caseAmb.IsActive == p.caseAmb.IsActive) &&
                (this.caseAmb.IdAmbResult == p.caseAmb.IdAmbResult) &&
                (this.caseAmb.IdCasePurpose == p.caseAmb.IdCasePurpose))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                return false;
            }
        }
        public static bool operator ==(TestAmbCase a, TestAmbCase b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAmbCase a, TestAmbCase b)
        {
            return !(a.Equals(b));
        }
    }
}
