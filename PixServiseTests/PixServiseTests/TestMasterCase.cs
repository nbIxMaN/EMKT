using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestMasterCase
    {
        private const int dispensaryId = 4;
        public string GUID;
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
        public TestStepBase defaultStep;

        private TestMasterCase() { }

        public static TestMasterCase BuildTestMasterCase (string guid, string idlpu, string patientIdmis, string mis = "")
        {
            if ((guid != string.Empty) && (idlpu != string.Empty) && (patientIdmis != string.Empty))
            {
                string patientId = TestPerson.GetPersonId(guid, idlpu, patientIdmis);
                string caseId = TestCaseBase.GetCaseId(guid, idlpu, mis, patientId);
                if (caseId != null)
                {
                    TestMasterCase mcase = new TestMasterCase();
                    mcase.caseBase = TestCaseBase.BuildBaseCaseFromDataBaseData(guid, idlpu, mis, patientId);
                    List<TestStepBase> st = TestStepBase.BuildTestStepsFromDataBase(caseId, idlpu);
                    if (st != null)
                    {
                        foreach (TestStepBase i in st)
                        {
                            if (Global.IsEqual(i.doctor, null))
                                mcase.defaultStep = i;
                        }
                        if (!Global.IsEqual(mcase.defaultStep, null))
                        {
                            mcase.records = new List<TestMedRecord>();
                            List<TestService> serv = TestService.BuildServiseFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(serv, null))
                                mcase.records.AddRange(serv);
                            TestTfomsInfo forms = TestTfomsInfo.BuildTfomsInfoFromDataBaseDate(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(forms, null))
                                mcase.records.Add(forms);
                            TestDeathInfo tdi = TestDeathInfo.BuildDeathInfoFromDataBaseDate(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(tdi, null))
                                mcase.records.Add(tdi);
                            List<TestDiagnosis> td = TestDiagnosis.BuildDiagnosisFromDataBaseDate(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(td, null))
                                mcase.records.AddRange(td);
                            TestClinicMainDiagnosis acdm = TestClinicMainDiagnosis.BuildTestClinicMainDiagnosisFromDataBaseDate(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(acdm, null))
                                mcase.records.Add(acdm);
                            List<TestReferral> trl = TestReferral.BuildReferralFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(trl, null))
                                mcase.records.AddRange(trl);
                            List<TestSickList> tsl = TestSickList.BuildSickListFromDataBaseData(mcase.defaultStep.idStep, patientId);
                            if (!Global.IsEqual(tsl, null))
                                mcase.records.AddRange(tsl);
                            TestDischargeSummary tds = TestDischargeSummary.BuildSickListFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(tds, null))
                                mcase.records.Add(tds);
                            List<TestLaboratoryReport> tlr = TestLaboratoryReport.BuildSickListFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(tlr, null))
                                mcase.records.AddRange(tlr);
                            TestConsultNote tcn = TestConsultNote.BuildSickListFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(tcn, null))
                                mcase.records.Add(tcn);
                            TestDispensaryOne d1 = TestDispensaryOne.BuildSickListFromDataBaseData(mcase.defaultStep.idStep);
                            if (!Global.IsEqual(d1, null))
                                mcase.records.Add(d1);
                        }
                    }
                    return mcase;
                }
            }
            return null;
        }

        public bool CheckDocumentInCase(MedRecord i, string idLpu)
        {
            TestMedRecord doc = null;
            Service serv = i as Service;
            if (serv != null)
                doc = new TestService(serv);
            TfomsInfo tfi = i as TfomsInfo;
            if (tfi != null)
                doc = new TestTfomsInfo(tfi);
            DeathInfo di = i as DeathInfo;
            if (di != null)
                doc = new TestDeathInfo(di);
            Diagnosis diag = i as Diagnosis;
            if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                doc = new TestDiagnosis(diag, idLpu);
            ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
            if ((cmd != null) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                doc = new TestClinicMainDiagnosis(cmd, idLpu);
            AnatomopathologicalClinicMainDiagnosis acmd = i as AnatomopathologicalClinicMainDiagnosis;
            if ((acmd != null) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                doc = new TestClinicMainDiagnosis(acmd, idLpu);
            Referral r = i as Referral;
            if (r != null)
                doc = new TestReferral(r, idLpu);
            SickList sl = i as SickList;
            if (sl != null)
                doc = new TestSickList(sl, idLpu);
            DischargeSummary ds = i as DischargeSummary;
            if (ds != null)
                doc = new TestDischargeSummary(ds, idLpu);
            LaboratoryReport lr = i as LaboratoryReport;
            if (lr != null)
                doc = new TestLaboratoryReport(lr, idLpu);
            ConsultNote cn = i as ConsultNote;
            if (cn != null)
                doc = new TestConsultNote(cn, idLpu);
            DispensaryOne d1 = i as DispensaryOne;
            if ((d1 != null) && (caseBase.idCaseType == dispensaryId))
                doc = new TestDispensaryOne(d1, idLpu);
            if (Global.IsEqual(doc, null))
            {
                Global.errors1.Add("Документ не найден");
                return true;
            }
            foreach (TestMedRecord document in this.medRecords)
            {
                if (Global.IsEqual(doc, document))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
