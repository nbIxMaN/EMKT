using NUnit.Framework;
using PixServiseTests.MiacStatistics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestMiacClient : IDisposable
    {
        private MiacStatisticServiceClient client = new MiacStatisticServiceClient();
        private bool disposed = false;
        //public void getErrors(object obj)
        //{
        //    Array errors = obj as Array;
        //    if (errors != null)
        //    {
        //        foreach(RequestFault i in errors)
        //        {
        //            Global.errors1.Add(i.PropertyName + " - " + i.Message);
        //            getErrors(i.Errors);
        //        }
        //    }
        //}
        private EMKServise.HumanName ConvertHumanName(HumanName c)
        {
            if ((object)c != null)
            {
                EMKServise.HumanName ed = new EMKServise.HumanName();
                if (c.FamilyName != "")
                    ed.FamilyName = c.FamilyName;
                if (c.MiddleName != "")
                    ed.MiddleName = c.MiddleName;
                if (c.GivenName != "")
                    ed.GivenName = c.GivenName;
                return ed;
            }
            else
                return null;
        }
        private List<EMKServise.IdentityDocument> ConvertDocuments(List<IdentityDocument> d)
        {
            if (((object)d != null) && (d.Count != 0))
            {
                List<EMKServise.IdentityDocument> ld = new List<EMKServise.IdentityDocument>();
                foreach (IdentityDocument c in d)
                {
                    EMKServise.IdentityDocument ed = new EMKServise.IdentityDocument();
                    if (c.DocN != "")
                        ed.DocN = c.DocN;
                    if (c.DocS != "")
                        ed.DocS = c.DocS;
                    if (c.ExpiredDate != DateTime.MinValue)
                        ed.ExpiredDate = c.ExpiredDate;
                    ed.IdDocumentType = c.IdDocumentType;
                    ed.IdProvider = c.IdProvider;
                    if (c.IssuedDate != DateTime.MinValue)
                        ed.IssuedDate = c.IssuedDate;
                    if (c.ProviderName != "")
                        ed.ProviderName = c.ProviderName;
                    if (c.RegionCode != "")
                        ed.RegionCode = c.RegionCode;
                    ld.Add(ed);
                }
                return ld;
            }
            else
                return null;
        }
        private EMKServise.PersonWithIdentity ConvertPersonWithIdentity(PersonWithIdentity c)
        {
            if ((object)c != null)
            {
                EMKServise.PersonWithIdentity ep = new EMKServise.PersonWithIdentity();
                if (c.Birthdate != DateTime.MinValue)
                    ep.Birthdate = c.Birthdate;
                ep.Documents = ConvertDocuments(c.Documents.ToList<IdentityDocument>());
                ep.HumanName = ConvertHumanName(c.HumanName);
                if (c.IdPersonMis != "")
                    ep.IdPersonMis = c.IdPersonMis;
                ep.Sex = c.Sex;
                return ep;
            }
            else
                return null;
        }

        private EMKServise.MedicalStaff ConvertMedicalStaff(MedicalStaff c)
        {
            if ((object)c != null)
            {
                EMKServise.MedicalStaff em = new EMKServise.MedicalStaff();
                if (c.IdLpu != "")
                    em.IdLpu = c.IdLpu;
                em.IdPosition = c.IdPosition;
                em.IdSpeciality = c.IdSpeciality;
                em.Person = ConvertPersonWithIdentity(c.Person);
                return em;
            }
            else
                return null;
        }

        private EMKServise.Participant ConvertParticipant(Participant c)
        {
            if ((object)c != null)
            {
                EMKServise.Participant ep = new EMKServise.Participant();
                ep.IdRole = c.IdRole;
                ep.Doctor = ConvertMedicalStaff(c.Doctor);
                return ep;
            }
            else
                return null;
        }

        private EMKServise.Guardian ConvertGuardian(Guardian c)
        {
            if ((object)c != null)
            {
                EMKServise.Guardian ed = new EMKServise.Guardian();
                ed.IdRelationType = c.IdRelationType;
                ed.Person = ConvertPersonWithIdentity(c.Person);
                if (c.UnderlyingDocument != "")
                    ed.UnderlyingDocument = c.UnderlyingDocument;
                return ed;
            }
            else
                return null;
        }

        private EMKServise.PaymentInfo ConvertPaymentInfo(PaymentInfo c)
        {
            if ((object)c != null)
            {
                EMKServise.PaymentInfo ep = new EMKServise.PaymentInfo();
                ep.HealthCareUnit = c.HealthCareUnit;
                ep.IdPaymentType = c.IdPaymentType;
                ep.PaymentState = c.PaymentState;
                ep.Quantity = c.Quantity;
                ep.Tariff = c.Tariff;
                return ep;
            }
            else
                return null;
        }

        private EMKServise.Quantity ConvertQuantity(Quantity c)
        {
            if ((object)c != null)
            {
                EMKServise.Quantity eq = new EMKServise.Quantity();
                eq.IdUnit = c.IdUnit;
                eq.Value = c.Value;
                return eq;
            }
            else
                return null;
        }

        private EMKServise.DiagnosisInfo ConvertDiagnosisInfo(DiagnosisInfo c)
        {
            if ((object)c != null)
            {
                EMKServise.DiagnosisInfo edi = new EMKServise.DiagnosisInfo();
                if (c.Comment != "")
                    edi.Comment = c.Comment;
                if (c.DiagnosedDate != DateTime.MinValue)
                    edi.DiagnosedDate = c.DiagnosedDate;
                edi.DiagnosisChangeReason = c.DiagnosisChangeReason;
                edi.DiagnosisStage = c.DiagnosisStage;
                edi.IdDiagnosisType = c.IdDiagnosisType;
                edi.IdDiseaseType = c.IdDiseaseType;
                edi.IdDispensaryState = c.IdDispensaryState;
                edi.IdTraumaType = c.IdTraumaType;
                edi.MedicalStandard = c.MedicalStandard;
                edi.MESImplementationFeature = c.MESImplementationFeature;
                edi.MkbCode = c.MkbCode;
                return edi;
            }
            else
                return null;
        }

        private EMKServise.ReferralInfo ConvertReferralInfo(ReferralInfo c)
        {
            if ((object)c != null)
            {
                EMKServise.ReferralInfo eri = new EMKServise.ReferralInfo();
                eri.HospitalizationOrder = c.HospitalizationOrder;
                if (c.IdReferralMis != "")
                    eri.IdReferralMis = c.IdReferralMis;
                eri.IdReferralType = c.IdReferralType;
                if (c.IssuedDateTime != DateTime.MinValue)
                    eri.IssuedDateTime = c.IssuedDateTime;
                if (c.MkbCode != "")
                    eri.MkbCode = c.MkbCode;
                if (c.Reason != "")
                    eri.Reason = c.Reason;
                return eri;
            }
            else
                return null;
        }

        private EMKServise.SickListInfo ConvertSickListInfo(SickListInfo c)
        {
            if ((object)c != null)
            {
                EMKServise.SickListInfo esl = new EMKServise.SickListInfo();
                esl.Caregiver = ConvertGuardian(c.Caregiver);
                if (c.DateEnd != DateTime.MinValue)
                    esl.DateEnd = c.DateEnd;
                if (c.DateStart != DateTime.MinValue)
                    esl.DateStart = c.DateStart;
                esl.DisabilityDocReason = c.DisabilityDocReason;
                esl.DisabilityDocState = c.DisabilityDocState;
                esl.IsPatientTaker = c.IsPatientTaker;
                if (c.Number != "")
                    esl.Number = c.Number;
                return esl;
            }
            else
                return null;
        }

        private List<EMKServise.Recommendation> ConvertRecommendation(Recommendation[] c)
        {
            if (((object)c != null) && (c.Length != 0))
            {
                List<EMKServise.Recommendation> l = new List<EMKServise.Recommendation>();
                foreach (Recommendation i in c)
                {
                    EMKServise.Recommendation er = new EMKServise.Recommendation();
                    if (i.Date != DateTime.MinValue)
                        er.Date = i.Date;
                    er.Doctor = ConvertMedicalStaff(i.Doctor);
                    if (i.Text != "")
                        er.Text = i.Text;
                    l.Add(er);
                }
                return l;
            }
            else
                return null;
        }

        private EMKServise.HealthGroupInfo ConvertHealthGroupInfo(HealthGroupInfo c)
        {
            if ((object)c != null)
            {
                EMKServise.HealthGroupInfo ehi = new EMKServise.HealthGroupInfo();
                if (c.Date != DateTime.MinValue)
                    ehi.Date = c.Date;
                ehi.IdHealthGroup = c.IdHealthGroup;
                return ehi;
            }
            else
                return null;
        }

        private EMKServise.HealthGroup ConvertHealthGroup(HealthGroup c)
        {
            if ((object)c != null)
            {
                EMKServise.HealthGroup eh = new EMKServise.HealthGroup();
                eh.Doctor = ConvertMedicalStaff(c.Doctor);
                eh.HealthGroupInfo = ConvertHealthGroupInfo(c.HealthGroupInfo);
                return eh;
            }
            else
                return null;
        }

        private EMKServise.MedDocument.DocumentAttachment ConvertAttachment(MedDocument.DocumentAttachment c)
        {
            if ((object)c != null)
            {
                EMKServise.MedDocument.DocumentAttachment eda = new EMKServise.MedDocument.DocumentAttachment();
                if (c.Data != null)
                    eda.Data = c.Data;
                if (c.Hash != null)
                    eda.Hash = c.Hash;
                if (c.MimeType != "")
                    eda.MimeType = c.MimeType;
                if (c.Url != null)
                    eda.Url = c.Url;
                return eda;
            }
            else
                return null;
        }

        private List<EMKServise.Diagnosis> ConvertDiagnosis(Diagnosis[] c)
        {
            List<EMKServise.Diagnosis> lmr = new List<EMKServise.Diagnosis>();
            foreach (Diagnosis i in c)
            {
                EMKServise.Diagnosis ecmd = new EMKServise.Diagnosis();
                ecmd.DiagnosisInfo = ConvertDiagnosisInfo(i.DiagnosisInfo);
                ecmd.Doctor = ConvertMedicalStaff(i.Doctor);
                lmr.Add(ecmd);
            }
            if (lmr.Count == 0)
                return null;
            else
                return lmr;
        }

        private List<EMKServise.MedRecord> ConvertMedRecords(MedRecord[] c)
        {
            if (((object)c != null) && (c.Length != 0))
            {
                List<EMKServise.MedRecord> lmr = new List<EMKServise.MedRecord>();
                foreach (object i in c)
                {
                    Service ser = i as Service;
                    if (ser != null)
                    {
                        EMKServise.Service eser = new EMKServise.Service();
                        if (ser.DateEnd != DateTime.MinValue)
                            eser.DateEnd = ser.DateEnd;
                        if (ser.DateStart != DateTime.MinValue)
                            eser.DateStart = ser.DateStart;
                        if (ser.IdServiceType != "")
                            eser.IdServiceType = ser.IdServiceType;
                        eser.PaymentInfo = ConvertPaymentInfo(ser.PaymentInfo);
                        eser.Performer = ConvertParticipant(ser.Performer);
                        if (ser.ServiceName != "")
                            eser.ServiceName = ser.ServiceName;
                        lmr.Add(eser);
                    }
                    TfomsInfo tfi = i as TfomsInfo;
                    if (tfi != null)
                    {
                        EMKServise.TfomsInfo etfi = new EMKServise.TfomsInfo();
                        etfi.Count = tfi.Count;
                        etfi.IdTfomsType = tfi.IdTfomsType;
                        etfi.Tariff = tfi.Tariff;
                        lmr.Add(etfi);
                    }
                    AppointedMedication ap = i as AppointedMedication;
                    if (ap != null)
                    {
                        EMKServise.AppointedMedication eap = new EMKServise.AppointedMedication();
                        if (ap.AnatomicTherapeuticChemicalClassification != "")
                            eap.AnatomicTherapeuticChemicalClassification = ap.AnatomicTherapeuticChemicalClassification;
                        eap.CourseDose = ConvertQuantity(ap.CourseDose);
                        eap.DayDose = ConvertQuantity(ap.DayDose);
                        eap.DaysCount = ap.DaysCount;
                        eap.Doctor = ConvertMedicalStaff(ap.Doctor);
                        if (ap.IssuedDate != DateTime.MinValue)
                            eap.IssuedDate = ap.IssuedDate;
                        if (ap.MedicineIssueType != "")
                            eap.MedicineIssueType = ap.MedicineIssueType;
                        if (ap.MedicineName != "")
                            eap.MedicineName = ap.MedicineName;
                        eap.MedicineType = ap.MedicineType;
                        eap.MedicineUseWay = ap.MedicineUseWay;
                        if (ap.Number != "")
                            eap.Number = ap.Number;
                        eap.OneTimeDose = ConvertQuantity(ap.OneTimeDose);
                        if (ap.Seria != "")
                            eap.Seria = ap.Seria;
                        lmr.Add(eap);
                    }
                    DeathInfo di = i as DeathInfo;
                    if (di != null)
                    {
                        EMKServise.DeathInfo edi = new EMKServise.DeathInfo();
                        if (di.MkbCode != "")
                            edi.MkbCode = di.MkbCode;
                        lmr.Add(edi);
                    }
                    ClinicMainDiagnosis cmd = i as ClinicMainDiagnosis;
                    if ((cmd != null) && (cmd.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                    {
                        EMKServise.ClinicMainDiagnosis ecmd = new EMKServise.ClinicMainDiagnosis();
                        ecmd.Complications = ConvertDiagnosis(cmd.Complications);
                        ecmd.DiagnosisInfo = ConvertDiagnosisInfo(cmd.DiagnosisInfo);
                        ecmd.Doctor = ConvertMedicalStaff(cmd.Doctor);
                        lmr.Add(ecmd);
                    }
                    if (cmd == null)
                    {
                        Diagnosis diag = i as Diagnosis;
                        if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType == TestDiagnosis.IdClinicMainDiagnosis))
                        {
                            EMKServise.ClinicMainDiagnosis ecmd = new EMKServise.ClinicMainDiagnosis();
                            ecmd.DiagnosisInfo = ConvertDiagnosisInfo(diag.DiagnosisInfo);
                            ecmd.Doctor = ConvertMedicalStaff(diag.Doctor);
                            lmr.Add(ecmd);
                        }
                        if ((diag != null) && (diag.DiagnosisInfo.IdDiagnosisType != TestDiagnosis.IdClinicMainDiagnosis))
                        {
                            EMKServise.Diagnosis ecmd = new EMKServise.Diagnosis();
                            ecmd.DiagnosisInfo = ConvertDiagnosisInfo(diag.DiagnosisInfo);
                            ecmd.Doctor = ConvertMedicalStaff(diag.Doctor);
                            lmr.Add(ecmd);
                        }
                    }
                    Referral r = i as Referral;
                    if (r != null)
                    {
                        EMKServise.Referral er = new EMKServise.Referral();
                        er.Attachment = ConvertAttachment(r.Attachment);
                        er.Author = ConvertMedicalStaff(r.Author);
                        if (r.CreationDate != DateTime.MinValue)
                            er.CreationDate = r.CreationDate;
                        er.DepartmentHead = ConvertMedicalStaff(r.DepartmentHead);
                        if (r.Header != "")
                            er.Header = r.Header;
                        if (r.IdSourceLpu != "")
                            er.IdSourceLpu = r.IdSourceLpu;
                        if (r.IdTargetLpu != "")
                            er.IdTargetLpu = r.IdTargetLpu;
                        if (r.ReferralID != "")
                            er.ReferralID = r.ReferralID;
                        er.ReferralInfo = ConvertReferralInfo(r.ReferralInfo);
                        if (r.RelatedID != "")
                            er.RelatedID = r.RelatedID;
                        lmr.Add(er);
                    }
                    SickList sl = i as SickList;
                    if (sl != null)
                    {
                        EMKServise.SickList esl = new EMKServise.SickList();
                        esl.Attachment = ConvertAttachment(sl.Attachment);
                        esl.Author = ConvertMedicalStaff(sl.Author);
                        if (sl.CreationDate != DateTime.MinValue)
                            esl.CreationDate = sl.CreationDate;
                        if (sl.Header != "")
                            esl.Header = sl.Header;
                        esl.SickListInfo = ConvertSickListInfo(sl.SickListInfo);
                        lmr.Add(esl);
                    }
                    DischargeSummary ds = i as DischargeSummary;
                    if (ds != null)
                    {
                        EMKServise.DischargeSummary eds = new EMKServise.DischargeSummary();
                        eds.Attachment = ConvertAttachment(ds.Attachment);
                        eds.Author = ConvertMedicalStaff(ds.Author);
                        if (ds.CreationDate != DateTime.MinValue)
                            eds.CreationDate = ds.CreationDate;
                        if (ds.Header != "")
                            eds.Header = ds.Header;
                        lmr.Add(eds);
                    }
                    LaboratoryReport lr = i as LaboratoryReport;
                    if (lr != null)
                    {
                        EMKServise.LaboratoryReport elr = new EMKServise.LaboratoryReport();
                        elr.Attachment = ConvertAttachment(lr.Attachment);
                        elr.Author = ConvertMedicalStaff(lr.Author);
                        if (lr.CreationDate != DateTime.MinValue)
                            elr.CreationDate = lr.CreationDate;
                        if (lr.Header != "")
                            elr.Header = lr.Header;
                        lmr.Add(elr);
                    }
                    ConsultNote cn = i as ConsultNote;
                    if (cn != null)
                    {
                        EMKServise.ConsultNote ecn = new EMKServise.ConsultNote();
                        ecn.Attachment = ConvertAttachment(cn.Attachment);
                        ecn.Author = ConvertMedicalStaff(cn.Author);
                        if (cn.CreationDate != DateTime.MinValue)
                            ecn.CreationDate = cn.CreationDate;
                        if (cn.Header != "")
                            ecn.Header = cn.Header;
                        lmr.Add(ecn);
                    }
                    DispensaryOne d1 = i as DispensaryOne;
                    if (d1 != null)
                    {
                        EMKServise.DispensaryOne ed1 = new EMKServise.DispensaryOne();
                        ed1.Attachment = ConvertAttachment(d1.Attachment);
                        ed1.Author = ConvertMedicalStaff(d1.Author);
                        if (d1.CreationDate != DateTime.MinValue)
                            ed1.CreationDate = d1.CreationDate;
                        if (d1.Header != "")
                            ed1.Header = d1.Header;
                        ed1.HasExpertCareRefferal = d1.HasExpertCareRefferal;
                        ed1.HasExtraResearchRefferal = d1.HasExtraResearchRefferal;
                        ed1.HasHealthResortRefferal = d1.HasHealthResortRefferal;
                        ed1.HasPrescribeCure = d1.HasPrescribeCure;
                        ed1.HasSecondStageRefferal = d1.HasSecondStageRefferal;
                        ed1.HealthGroup = ConvertHealthGroup(d1.HealthGroup);
                        ed1.IsGuested = d1.IsGuested;
                        ed1.IsUnderObservation = d1.IsUnderObservation;
                        ed1.Recommendations = ConvertRecommendation(d1.Recommendations);
                        lmr.Add(ed1);
                    }
                }
                return lmr;
            }
            else
                return null;
        }

        private List<EMKServise.StepAmb> ConvertAmbSteps(StepAmb[] ca)
        {
            if ((object)ca != null)
            {
                List<EMKServise.StepAmb> l = new List<EMKServise.StepAmb>();
                foreach (StepAmb i in ca)
                {
                    EMKServise.StepAmb esa = new EMKServise.StepAmb();
                    if (i.Comment != "")
                        esa.Comment = i.Comment;
                    if (i.DateEnd != DateTime.MinValue)
                        esa.DateEnd = i.DateEnd;
                    if (i.DateStart != DateTime.MinValue)
                        esa.DateStart = i.DateStart;
                    esa.Doctor = ConvertMedicalStaff(i.Doctor);
                    esa.IdPaymentType = i.IdPaymentType;
                    if (i.IdStepMis != "")
                        esa.IdStepMis = i.IdStepMis;
                    esa.IdVisitPlace = i.IdVisitPlace;
                    esa.IdVisitPurpose = i.IdVisitPurpose;
                    esa.MedRecords = ConvertMedRecords(i.MedRecords);
                    l.Add(esa);
                }
                return l;
            }
            return null;
        }
        private List<EMKServise.StepStat> ConvertStatSteps(StepStat[] cs)
        {
            if ((object)cs != null)
            {
                List<EMKServise.StepStat> l = new List<EMKServise.StepStat>();
                foreach (StepStat i in cs)
                {
                    EMKServise.StepStat esa = new EMKServise.StepStat();
                    if (i.BedNumber != "")
                        esa.BedNumber = i.BedNumber;
                    esa.BedProfile = i.BedProfile;
                    if (i.Comment != "")
                        esa.Comment = i.Comment;
                    if (i.DateEnd != DateTime.MinValue)
                        esa.DateEnd = i.DateEnd;
                    if (i.DateStart != DateTime.MinValue)
                        esa.DateStart = i.DateStart;
                    esa.DaySpend = i.DaySpend;
                    esa.Doctor = ConvertMedicalStaff(i.Doctor);
                    if (i.HospitalDepartmentName != "")
                        esa.HospitalDepartmentName = i.HospitalDepartmentName;
                    if (i.IdHospitalDepartment != "")
                        esa.IdHospitalDepartment = i.IdHospitalDepartment;
                    esa.IdPaymentType = i.IdPaymentType;
                    esa.IdRegimen = i.IdRegimen;
                    if (i.IdStepMis != "")
                        esa.IdStepMis = i.IdStepMis;
                    esa.MedRecords = ConvertMedRecords(i.MedRecords);
                    l.Add(esa);
                }
                return l;
            }
            return null;
        }
        //private EMKServise.StepBase[] ConvertSteps(StepBase[] c)
        //{
        //    StepAmb[] ca = c as StepAmb[];
        //    if ((object)ca != null)
        //    {
        //        List<EMKServise.StepAmb> l = new List<EMKServise.StepAmb>();
        //        foreach (StepAmb i in ca)
        //        {
        //            EMKServise.StepAmb esa = new EMKServise.StepAmb();
        //            if (i.Comment != "")
        //                esa.Comment = i.Comment;
        //            if (i.DateEnd != DateTime.MinValue)
        //                esa.DateEnd = i.DateEnd;
        //            if (i.DateStart != DateTime.MinValue)
        //                esa.DateStart = i.DateStart;
        //            esa.Doctor = ConvertMedicalStaff(i.Doctor);
        //            esa.IdPaymentType = i.IdPaymentType;
        //            if (i.IdStepMis != "")
        //                esa.IdStepMis = i.IdStepMis;
        //            esa.IdVisitPlace = i.IdVisitPlace;
        //            esa.IdVisitPurpose = i.IdVisitPurpose;
        //            esa.MedRecords = ConvertMedRecords(i.MedRecords);
        //            l.Add(esa);
        //        }
        //        return l.ToArray();
        //    }
        //    StepStat[] cs = c as StepStat[];
        //    if ((object)cs != null)
        //    {
        //        List<EMKServise.StepStat> l = new List<EMKServise.StepStat>();
        //        foreach (StepStat i in cs)
        //        {
        //            EMKServise.StepStat esa = new EMKServise.StepStat();
        //            if (i.BedNumber != "")
        //                esa.BedNumber = i.BedNumber;
        //            esa.BedProfile = i.BedProfile;
        //            if (i.Comment != "")
        //                esa.Comment = i.Comment;
        //            if (i.DateEnd != DateTime.MinValue)
        //                esa.DateEnd = i.DateEnd;
        //            if (i.DateStart != DateTime.MinValue)
        //                esa.DateStart = i.DateStart;
        //            esa.DaySpend = i.DaySpend;
        //            esa.Doctor = ConvertMedicalStaff(i.Doctor);
        //            if (i.HospitalDepartmentName != "")
        //                esa.HospitalDepartmentName = i.HospitalDepartmentName;
        //            if (i.IdHospitalDepartment != "")
        //                esa.IdHospitalDepartment = i.IdHospitalDepartment;
        //            esa.IdPaymentType = i.IdPaymentType;
        //            esa.IdRegimen = i.IdRegimen;
        //            if (i.IdStepMis != "")
        //                esa.IdStepMis = i.IdStepMis;
        //            esa.MedRecords = ConvertMedRecords(i.MedRecords);
        //            l.Add(esa);
        //        }
        //        return l.ToArray();
        //    }
        //    return null;
        //}

        private EMKServise.CaseBase ConvertCase(CaseBase c)
        {
            CaseAmb ca = c as CaseAmb;
            if ((object)ca != null)
            {
                EMKServise.CaseAmb eca = new EMKServise.CaseAmb();
                eca.Authenticator = ConvertParticipant(ca.Authenticator);
                eca.Author = ConvertParticipant(ca.Author);
                if (ca.CloseDate != DateTime.MinValue)
                    eca.CloseDate = ca.CloseDate;
                if (ca.Comment != "")
                    eca.Comment = ca.Comment;
                eca.Confidentiality = ca.Confidentiality;
                eca.CuratorConfidentiality = ca.CuratorConfidentiality;
                eca.DoctorConfidentiality = ca.DoctorConfidentiality;
                eca.DoctorInCharge = ConvertMedicalStaff(ca.DoctorInCharge);
                eca.Guardian = ConvertGuardian(ca.Guardian);
                if (ca.HistoryNumber != "")
                    eca.HistoryNumber = ca.HistoryNumber;
                eca.IdAmbResult = ca.IdAmbResult;
                eca.IdAmbResult = ca.IdAmbResult;
                eca.IdCaseAidType = ca.IdCaseAidType;
                if (ca.IdCaseMis != "")
                    eca.IdCaseMis = ca.IdCaseMis;
                eca.IdCasePurpose = ca.IdCasePurpose;
                eca.IdCaseResult = ca.IdCaseResult;
                eca.IdCaseType = ca.IdCaseType;
                if (ca.IdLpu != "")
                    eca.IdLpu = ca.IdLpu;
                if (ca.IdPatientMis != "")
                    eca.IdPatientMis = ca.IdPatientMis;
                eca.IdPaymentType = ca.IdPaymentType;
                eca.IsActive = ca.IsActive;
                eca.LegalAuthenticator = ConvertParticipant(ca.LegalAuthenticator);
                eca.MedRecords = ConvertMedRecords(ca.MedRecords);
                if (ca.OpenDate != DateTime.MinValue)
                    eca.OpenDate = ca.OpenDate;
                eca.Steps = ConvertAmbSteps(ca.Steps);
                return eca;
            }
            CaseStat cs = c as CaseStat;
            if ((object)cs != null)
            {
                EMKServise.CaseStat eca = new EMKServise.CaseStat();
                eca.AIDSMark = cs.AIDSMark;
                eca.Authenticator = ConvertParticipant(cs.Authenticator);
                eca.Author = ConvertParticipant(cs.Author);
                if (cs.CloseDate != DateTime.MinValue)
                    eca.CloseDate = cs.CloseDate;
                if (cs.Comment != "")
                    eca.Comment = cs.Comment;
                eca.Confidentiality = cs.Confidentiality;
                eca.CuratorConfidentiality = cs.CuratorConfidentiality;
                if (cs.DeliveryCode != "")
                    eca.DeliveryCode = cs.DeliveryCode;
                eca.DoctorConfidentiality = cs.DoctorConfidentiality;
                eca.DoctorInCharge = ConvertMedicalStaff(cs.DoctorInCharge);
                eca.Guardian = ConvertGuardian(cs.Guardian);
                if (cs.HistoryNumber != "")
                    eca.HistoryNumber = cs.HistoryNumber;
                eca.HospitalizationOrder = cs.HospitalizationOrder;
                eca.HospResult = cs.HospResult;
                eca.IdCaseAidType = cs.IdCaseAidType;
                if (cs.IdCaseMis != "")
                    eca.IdCaseMis = cs.IdCaseMis;
                //eca.IdCaseResult = cs.IdCasePurpose;
                eca.IdCaseResult = cs.IdCaseResult;
                eca.IdHospChannel = cs.IdHospChannel;
                eca.IdIntoxicationType = cs.IdIntoxicationType;
                if (cs.IdLpu != "")
                    eca.IdLpu = cs.IdLpu;
                eca.IdPatientConditionOnAdmission = cs.IdPatientConditionOnAdmission;
                if (cs.IdPatientMis != "")
                    eca.IdPatientMis = cs.IdPatientMis;
                eca.IdPaymentType = cs.IdPaymentType;
                eca.IdRepetition = cs.IdRepetition;
                eca.IdTransportIntern = cs.IdTransportIntern;
                eca.IdTypeFromDiseaseStart = cs.IdTypeFromDiseaseStart;
                eca.LegalAuthenticator = ConvertParticipant(cs.LegalAuthenticator);
                eca.MedRecords = ConvertMedRecords(cs.MedRecords);
                if (cs.OpenDate != DateTime.MinValue)
                    eca.OpenDate = cs.OpenDate;
                eca.PrehospitalDefects = cs.PrehospitalDefects.ToList<byte>();
                eca.RW1Mark = cs.RW1Mark;
                eca.Steps = ConvertStatSteps(cs.Steps);
                return eca;
            }
            return null;
        }

        public void GetCasesByPeriod(string GUID, DateTime datestart, DateTime dateend)
        {
            List<TestCaseBase> MyFindedCase = new List<TestCaseBase>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findCaseString =
                    "SELECT * FROM \"Case\" WHERE OpenDate >= '" + datestart + "' AND OpenDate <= '" + dateend + "' AND CloseDate IS NOT NULL AND IsCancelled = 'False'";
                SqlCommand command = new SqlCommand(findCaseString, connection);
                using (SqlDataReader IdCaseReader = command.ExecuteReader())
                {
                    while (IdCaseReader.Read())
                    {
                        string guid = IdCaseReader["SystemGuid"].ToString();
                        string idCaseMis = IdCaseReader["IdCaseMIS"].ToString();
                        string idLpu = Global.GetIdIdLpu(IdCaseReader["IdLpu"].ToString());
                        string patientId = IdCaseReader["IdPerson"].ToString();
                        TestAmbCase ambCase = TestAmbCase.BuildAmbCaseFromDataBaseData(guid, idLpu, idCaseMis, patientId);
                        if ((object)ambCase != null)
                            MyFindedCase.Add(ambCase);
                        TestStatCase statCase = TestStatCase.BuildAmbCaseFromDataBaseData(guid, idLpu, idCaseMis, patientId);
                        if ((object)statCase != null)
                            MyFindedCase.Add(statCase);
                        string idCase = IdCaseReader["IdCase"].ToString();
                    }
                }
            }
            CaseBase[] cb = client.GetCasesByPeriod(GUID, datestart, dateend);
            List<TestCaseBase> FunctionFindedCase = new List<TestCaseBase>();
            foreach (CaseBase i in cb)
            {
                CaseAmb ca = i as CaseAmb;
                if ((object)ca != null)
                {
                    using (SqlConnection connection = Global.GetSqlConnection())
                    {
                        string findCaseString =
                            "SELECT TOP(1) [SystemGuid] FROM \"ExternalId\" WHERE IdLpu = '" + Global.GetIdInstitution(ca.IdLpu) + "' AND IdPersonMIS = '" + ca.IdPatientMis + "'";
                        SqlCommand command = new SqlCommand(findCaseString, connection);
                        using (SqlDataReader IdCaseReader = command.ExecuteReader())
                        {
                            while (IdCaseReader.Read())
                            {
                                GUID = IdCaseReader["SystemGuid"].ToString();
                            }
                        }
                    }
                    TestAmbCase tac = new TestAmbCase(GUID, (EMKServise.CaseAmb)ConvertCase(ca));
                    //if (!tac.CheckCaseInDataBase())
                    //    Global.errors1.Add("Что-то пошло не так");
                    FunctionFindedCase.Add(tac);
                }
                CaseStat cs = i as CaseStat;
                if ((object)cs != null)
                {
                    using (SqlConnection connection = Global.GetSqlConnection())
                    {
                        string findCaseString =
                            "SELECT TOP(1) [SystemGuid] FROM \"ExternalId\" WHERE IdLpu = '" + Global.GetIdInstitution(cs.IdLpu) + "' AND IdPersonMIS = '" + cs.IdPatientMis + "'";
                        SqlCommand command = new SqlCommand(findCaseString, connection);
                        using (SqlDataReader IdCaseReader = command.ExecuteReader())
                        {
                            while (IdCaseReader.Read())
                            {
                                GUID = IdCaseReader["SystemGuid"].ToString();
                            }
                        }
                    }
                    TestStatCase tsc = new TestStatCase(GUID, (EMKServise.CaseStat)ConvertCase(cs));
                    //if (!tsc.CheckCaseInDataBase())
                    //    Global.errors1.Add("Что-то пошло не так");
                    FunctionFindedCase.Add(tsc);
                }
            }
            if (Global.GetLength(MyFindedCase) > Global.GetLength(FunctionFindedCase))
                Global.errors1.Add("Возвращено меньше, чем в базе");
            if (Global.GetLength(MyFindedCase) < Global.GetLength(FunctionFindedCase))
                Global.errors1.Add("Возвращено больше, чем в базе");
            if (Global.GetLength(MyFindedCase) == Global.GetLength(FunctionFindedCase))
            {
                if (!Global.IsEqual(MyFindedCase.ToArray(), FunctionFindedCase.ToArray()))
                    Global.errors1.AddRange(Global.errors2);
            }
        }

        ~TestMiacClient()
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
