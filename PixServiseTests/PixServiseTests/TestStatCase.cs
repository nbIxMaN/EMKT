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
        private const byte IdDeath = 6;
        public string GUID;
        public CaseStat caseStat;
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
                        Service ser = i as Service;
                        if (ser != null)
                            records.Add(new TestService(ser, cs.IdLpu));
                        TfomsInfo tfi = i as TfomsInfo;
                        if (tfi != null)
                            records.Add(new TestTfomsInfo(tfi));
                        DeathInfo di = i as DeathInfo;
                        if (di != null)
                            records.Add(new TestDeathInfo(di));
                        ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
                        if ((cmd != null) && (cs.HospResult != IdDeath) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            records.Add(new TestClinicMainDiagnosis(cmd, cs.IdLpu));
                        AnatomopathologicalClinicMainDiagnosis acmd = i as AnatomopathologicalClinicMainDiagnosis;
                        if ((acmd != null) && (cs.HospResult == IdDeath) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            records.Add(new TestClinicMainDiagnosis(acmd, cs.IdLpu));
                        if ((cmd == null) && (acmd == null))
                        {
                            Diagnosis diag = i as Diagnosis;
                            if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                                records.Add(new TestDiagnosis(diag, cs.IdLpu));
                            if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                                records.Add(new TestClinicMainDiagnosis(diag, cs.IdLpu));
                        }
                        Referral r = i as Referral;
                        if (r != null)
                            records.Add(new TestReferral(r, cs.IdLpu));
                        SickList sl = i as SickList;
                        if (sl != null)
                            records.Add(new TestSickList(sl, cs.IdLpu));
                        DischargeSummary ds = i as DischargeSummary;
                        if (ds != null)
                            records.Add(new TestDischargeSummary(ds, cs.IdLpu));
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            records.Add(new TestLaboratoryReport(lr, cs.IdLpu));
                        ConsultNote cn = i as ConsultNote;
                        if (cn != null)
                            records.Add(new TestConsultNote(cn, cs.IdLpu));
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
                    statcase.statSteps = TestStatStep.BuildStatStepsFromDataBase(caseId, ca.IdLpu);
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(caseId, ca.IdLpu);
                    if (st != null)
                    {
                        foreach (TestStepBase i in st)
                        {
                            if (Global.IsEqual(i.doctor, null))
                                statcase.defaultStep = i;
                        }
                        if (!Global.IsEqual(statcase.defaultStep, null))
                        {
                            statcase.records = new List<TestMedRecord>();
                            List<TestService> serv = TestService.BuildServiseFromDataBaseData(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(serv, null))
                                statcase.records.AddRange(serv);
                            TestTfomsInfo forms = TestTfomsInfo.BuildTfomsInfoFromDataBaseDate(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(forms, null))
                                statcase.records.Add(forms);
                            TestDeathInfo tdi = TestDeathInfo.BuildDeathInfoFromDataBaseDate(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(tdi, null))
                                statcase.records.Add(tdi);
                            List<TestDiagnosis> td = TestDiagnosis.BuildDiagnosisFromDataBaseDate(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(td, null))
                                statcase.records.AddRange(td);
                            TestClinicMainDiagnosis acdm = TestClinicMainDiagnosis.BuildTestClinicMainDiagnosisFromDataBaseDate(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(acdm, null))
                                statcase.records.Add(acdm);
                            List<TestReferral> trl = TestReferral.BuildReferralFromDataBaseData(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(trl, null))
                                statcase.records.AddRange(trl);
                            List<TestSickList> tsl = TestSickList.BuildSickListFromDataBaseData(statcase.defaultStep.idStep, patientId);
                            if (!Global.IsEqual(tsl, null))
                                statcase.records.AddRange(tsl);
                            TestDischargeSummary tds = TestDischargeSummary.BuildSickListFromDataBaseData(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(tds, null))
                                statcase.records.Add(tds);
                            List<TestLaboratoryReport> tlr = TestLaboratoryReport.BuildSickListFromDataBaseData(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(tlr, null))
                                statcase.records.AddRange(tlr);
                            TestConsultNote tcn = TestConsultNote.BuildSickListFromDataBaseData(statcase.defaultStep.idStep);
                            if (!Global.IsEqual(tcn, null))
                                statcase.records.Add(tcn);
                            if (statcase.records.Count == 0)
                                statcase.records = null;
                        }
                    }
                    return statcase;
                }
            }
            return null;
        }
        public void ChangeUpdateStatCase(string guid, CaseStat cs)
        {
            GUID = guid.ToLower();
            if (cs != null)
            {
                if (cs.DeliveryCode != null)
                    this.caseStat.DeliveryCode = cs.DeliveryCode;
                if (cs.IdIntoxicationType != 0)
                    this.caseStat.IdIntoxicationType = cs.IdIntoxicationType;
                if (cs.IdPatientConditionOnAdmission != 0)
                    this.caseStat.IdPatientConditionOnAdmission = cs.IdPatientConditionOnAdmission;
                if (cs.IdTypeFromDiseaseStart != 0)
                    this.caseStat.IdTypeFromDiseaseStart = cs.IdTypeFromDiseaseStart;
                if (cs.IdTransportIntern != 0)
                    this.caseStat.IdTransportIntern = cs.IdTransportIntern;
                if (cs.IdHospChannel != 0)
                    this.caseStat.IdHospChannel = cs.IdHospChannel;
                this.caseStat.RW1Mark = cs.RW1Mark;
                this.caseStat.AIDSMark = cs.AIDSMark;
                caseBase.UpdateCaseBase(guid, cs);
                if (cs.MedRecords != null)
                {
                    List<TestMedRecord> newMedRecords = new List<TestMedRecord>();
                    TestTfomsInfo tfi = null;
                    TestClinicMainDiagnosis cmd = null;
                    TestDischargeSummary ds = null;
                    TestConsultNote cn = null;
                    foreach (object i in cs.MedRecords)
                    {
                        Service ser = i as Service;
                        if (ser != null)
                            newMedRecords.Add(new TestService(ser, cs.IdLpu));
                        TfomsInfo tf = i as TfomsInfo;
                        if (tf != null)
                            tfi = new TestTfomsInfo(tf);
                        Diagnosis diag = i as Diagnosis;
                        if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                            newMedRecords.Add(new TestDiagnosis(diag, cs.IdLpu));
                        ClinicMainDiagnosis cd = i as ClinicMainDiagnosis;
                        if ((cd != null) && (cs.HospResult != IdDeath) && (cd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            cmd = new TestClinicMainDiagnosis(cd, cs.IdLpu);
                        ClinicMainDiagnosis ca = i as ClinicMainDiagnosis;
                        if ((ca != null) && (cs.HospResult == IdDeath) && (ca.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                            cmd = new TestClinicMainDiagnosis(ca, cs.IdLpu);
                        Referral r = i as Referral;
                        if (r != null)
                            newMedRecords.Add(new TestReferral(r, cs.IdLpu));
                        SickList sl = i as SickList;
                        if (sl != null)
                            newMedRecords.Add(new TestSickList(sl, cs.IdLpu));
                        DischargeSummary pds = i as DischargeSummary;
                        if (pds != null)
                            ds = new TestDischargeSummary(pds, cs.IdLpu);
                        LaboratoryReport lr = i as LaboratoryReport;
                        if (lr != null)
                            newMedRecords.Add(new TestLaboratoryReport(lr, cs.IdLpu));
                        ConsultNote pcn = i as ConsultNote;
                        if (pcn != null)
                            cn = new TestConsultNote(pcn, cs.IdLpu);
                    }
                    if (Global.GetLength(this.medRecords) != 0)
                    {
                        foreach (object i in this.medRecords)
                        {
                            TestService ser = i as TestService;
                            if (!Global.IsEqual(ser, null))
                                newMedRecords.Add(ser);
                            TestTfomsInfo tf = i as TestTfomsInfo;
                            if (!Global.IsEqual(tfi, null) && (!Global.IsEqual(tf, null)))
                                newMedRecords.Add(tf);
                            else
                                if (!Global.IsEqual(tfi, null))
                                    newMedRecords.Add(tfi);
                            TestDiagnosis diag = i as TestDiagnosis;
                            if (!Global.IsEqual(diag, null))
                                newMedRecords.Add(diag);
                            TestClinicMainDiagnosis cm = i as TestClinicMainDiagnosis;
                            if (!Global.IsEqual(cmd, null))
                                newMedRecords.Add(cmd);
                            else
                                if (!Global.IsEqual(cm, null))
                                    newMedRecords.Add(cm);
                            TestReferral r = i as TestReferral;
                            if (!Global.IsEqual(r, null))
                                newMedRecords.Add(r);
                            TestSickList sl = i as TestSickList;
                            if (!Global.IsEqual(sl, null))
                                newMedRecords.Add(sl);
                            TestDischargeSummary pds = i as TestDischargeSummary;
                            if (!Global.IsEqual(ds, null))
                                newMedRecords.Add(ds);
                            else
                                if (!Global.IsEqual(pds, null))
                                    newMedRecords.Add(pds);
                            TestLaboratoryReport lr = i as TestLaboratoryReport;
                            if (!Global.IsEqual(lr, null))
                                newMedRecords.Add(lr);
                            TestConsultNote pcn = i as TestConsultNote;
                            if (!Global.IsEqual(cn, null))
                                newMedRecords.Add(cn);
                            else
                                if (!Global.IsEqual(pcn, null))
                                    newMedRecords.Add(pcn);
                        }
                    }
                    else
                    {
                        if (!Global.IsEqual(tfi, null))
                            newMedRecords.Add(tfi);
                        if (!Global.IsEqual(cmd, null))
                            newMedRecords.Add(cmd);
                        if (!Global.IsEqual(ds, null))
                            newMedRecords.Add(ds);
                        if (!Global.IsEqual(cn, null))
                            newMedRecords.Add(cn);
                    }
                    this.records = newMedRecords;
                }
            }
        }
        private void FindMismatch(TestStatCase cs)
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
            if (Global.GetLength(this.medRecords) != Global.GetLength(cs.medRecords))
                Global.errors2.Add("несовпадение длины medRecords caseStat");
            if (Global.GetLength(this.steps) != Global.GetLength(cs.steps))
                Global.errors2.Add("несовпадение длины statSteps caseStat");
            if (Global.GetLength(this.caseBase) != Global.GetLength(cs.caseBase))
                Global.errors2.Add("несовпадение длины caseBase caseStat");
        }
        public bool CheckCaseInDataBase()
        {
            string patientId = TestPerson.GetPersonId(GUID, caseBase.caseBase.IdLpu, caseBase.patient.patient.IdPatientMIS);
            TestStatCase ac = TestStatCase.BuildAmbCaseFromDataBaseData(GUID, caseBase.caseBase.IdLpu, caseBase.caseBase.IdCaseMis, patientId);
            //this.Equals(ac);
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
                Global.errors2.Add("несовпадение TestStatCase");
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
