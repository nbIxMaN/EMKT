using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    class SetData
    {
        public PatientDto PatientSet()
        {
            PatientDto patient = new PatientDto();
            patient.IdPatientMIS = PatientData.Patient.IdPatientMIS;
            patient.FamilyName = PatientData.Patient.FamilyName;
            patient.GivenName = PatientData.Patient.GivenName;
            patient.Sex = PatientData.Patient.Sex;
            patient.BirthDate = new DateTime(2010, 10, 10);
            patient.IdBloodType = 1;
            //PixServise.DocumentDto d = new PixServise.DocumentDto();
            //d.DocN = DocumentData.PatientPassport.DocN;
            //d.IdDocumentType = DocumentData.PatientPassport.IdDocumentType;
            //d.ProviderName = DocumentData.PatientPassport.ProviderName;
            //d.DocS = DocumentData.PatientPassport.DocS;
            PixServise.DocumentDto s = new PixServise.DocumentDto();
            s.DocN = DocumentData.PatientSNILS.DocN;
            s.IdDocumentType = DocumentData.PatientSNILS.IdDocumentType;
            s.ProviderName = DocumentData.PatientSNILS.ProviderName;
            patient.Documents = new PixServise.DocumentDto []
            {
                s
            };

            return patient;
        }

        public LaboratoryReport MinLaboratoryReport()
        {
            LaboratoryReport labrep = new LaboratoryReport
            {
                CreationDate = MedRecordData.LaboratoryReport.CreationDate,
                Header = MedRecordData.LaboratoryReport.Header,
                Author = MinDoctorSet(),
            };
            return labrep;
        }

        public DispensaryOne MinDispensaryOne()
        {
            DispensaryOne disp = new DispensaryOne
            {
                CreationDate = MedRecordData.dispensaryOne.CreationDate,
                Header = MedRecordData.dispensaryOne.Header,
                IsGuested = MedRecordData.dispensaryOne.IsGuested,
                HasExpertCareRefferal = MedRecordData.dispensaryOne.HasExpertCareRefferal,
                IsUnderObservation = MedRecordData.dispensaryOne.IsUnderObservation,
                HasExtraResearchRefferal = MedRecordData.dispensaryOne.HasExtraResearchRefferal,
                HasPrescribeCure = MedRecordData.dispensaryOne.HasPrescribeCure,
                HasHealthResortRefferal = MedRecordData.dispensaryOne.HasHealthResortRefferal,
                HasSecondStageRefferal = MedRecordData.dispensaryOne.HasSecondStageRefferal,
                Author = MinDoctorSet(),
                HealthGroup = new HealthGroup
                {
                    Doctor = MinDoctorSet(),
                    HealthGroupInfo = new HealthGroupInfo
                    {
                        Date = MedRecordData.dispensaryOne.HealthGroup.HealthGroupInfo.Date,
                        IdHealthGroup = MedRecordData.dispensaryOne.HealthGroup.HealthGroupInfo.IdHealthGroup,
                    }
                }
            };

            return disp;
        }

        public Referral MinRefferal()
        {
            Referral r = new Referral
            {
                CreationDate = MedRecordData.referral.CreationDate,
                Header = MedRecordData.referral.Header,
                IdSourceLpu = MedRecordData.referral.IdSourceLpu,
                IdTargetLpu = MedRecordData.referral.IdTargetLpu,
                Author = MinDoctorSet(),
                ReferralInfo = new ReferralInfo
                {
                    Reason = MedRecordData.referral.ReferralInfo.Reason,
                    IdReferralMis = MedRecordData.referral.ReferralInfo.IdReferralMis,
                    IdReferralType = MedRecordData.referral.ReferralInfo.IdReferralType,
                    HospitalizationOrder = MedRecordData.referral.ReferralInfo.HospitalizationOrder,
                    IssuedDateTime = MedRecordData.referral.ReferralInfo.IssuedDateTime,
                    MkbCode = MedRecordData.referral.ReferralInfo.MkbCode
                },
                DepartmentHead = MinDoctorSet()
            };
            return r;
        }

        public ConsultNote MinConsultNote()
        {
            ConsultNote consultNote = new ConsultNote
            {
                CreationDate = MedRecordData.consultNote.CreationDate,
                Header = MedRecordData.consultNote.Header,
                Author = MinDoctorSet(),
            };
            return consultNote;
        }

        public DischargeSummary MinDischargeSummary()
        {
            DischargeSummary dischargeSummary = new DischargeSummary
            {
                CreationDate = MedRecordData.dischargeSummary.CreationDate,
                Header = MedRecordData.dischargeSummary.Header,
                Author = MinDoctorSet(),
            };

            return dischargeSummary;
        }

        public Service MinService()
        {
            Service service = new Service
            {
                DateStart = MedRecordData.service.DateStart,
                DateEnd = MedRecordData.service.DateEnd,
                IdServiceType = MedRecordData.service.IdServiceType,
                ServiceName = MedRecordData.service.ServiceName,

                Performer = new Participant
                {
                    IdRole = 1
                }
            };
            return service;
        }

        public TfomsInfo MinTfomsInfo()
        {
            TfomsInfo tfomsInfo = new TfomsInfo
            {
                IdTfomsType = MedRecordData.tfomsInfo.IdTfomsType,
                Count = MedRecordData.tfomsInfo.Count,
            };
            return tfomsInfo;
        }

        public AppointedMedication MinAppointedMedication()
        {
            AppointedMedication apMed = new AppointedMedication
            {
                //IssuedDate = MedRecordData.appointedMedication.IssuedDate,
                MedicineName = MedRecordData.appointedMedication.MedicineName,
                //AnatomicTherapeuticChemicalClassification = MedRecordData.appointedMedication.AnatomicTherapeuticChemicalClassification,
               
                Doctor = MinDoctorSet(),
            };
            return apMed;
        }

        public Diagnosis MinDiagnosis()
        {
            Diagnosis diag = new Diagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    DiagnosedDate = MedRecordData.diagnosis.DiagnosisInfo.DiagnosedDate,
                    IdDiagnosisType = MedRecordData.diagnosis.DiagnosisInfo.IdDiagnosisType,
                    Comment = MedRecordData.diagnosis.DiagnosisInfo.Comment,
                    MkbCode = MedRecordData.diagnosis.DiagnosisInfo.MkbCode
                },
                Doctor = MinDoctorSet()
            };
            return diag;
        }

        public ClinicMainDiagnosis MinClinicMainDiagnosis()
        {
            ClinicMainDiagnosis diag = new ClinicMainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    DiagnosedDate = MedRecordData.clinicMainDiagnosis.DiagnosisInfo.DiagnosedDate,
                    IdDiagnosisType = MedRecordData.clinicMainDiagnosis.DiagnosisInfo.IdDiagnosisType,
                    Comment = MedRecordData.clinicMainDiagnosis.DiagnosisInfo.Comment,
                    MkbCode = MedRecordData.clinicMainDiagnosis.DiagnosisInfo.MkbCode
                },
                Doctor = MinDoctorSet()
            };
            return diag;
        }

        public SickList MinSickList()
        {
            SickList sl= new SickList
            {
                CreationDate = MedRecordData.sickList.CreationDate,
                Header = MedRecordData.sickList.Header,
                Author = MinDoctorSet(),
                SickListInfo = new SickListInfo
                {
                    Number = MedRecordData.sickList.SickListInfo.Number,
                    DateStart = MedRecordData.sickList.SickListInfo.DateStart,
                    DateEnd = MedRecordData.sickList.SickListInfo.DateEnd
                }
            };
            return sl;
        }

        public MedicalStaff MinDoctorSet()
        {
            MedicalStaff doctor = new MedicalStaff
            {
                //IdSpeciality = DoctorData.otherDoctor.IdSpeciality,
                //IdPosition = DoctorData.otherDoctor.IdPosition,
                //Person = new PersonWithIdentity
                //{
                //    IdPersonMis = DoctorData.otherDoctor.Person.IdPersonMis,
                //    HumanName = new HumanName
                //    {
                //        FamilyName = DoctorData.otherDoctor.Person.HumanName.FamilyName,
                //        GivenName = DoctorData.otherDoctor.Person.HumanName.GivenName,
                //    }
                //}

                //"Organization/ab0dba1c-f83f-4605-b436-5da807187466"
                //Data.idlpu
                IdSpeciality = 27,
                IdPosition = 72,
                IdLpu = Data.idlpu,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "NOSNILSDOCTOR",
                    HumanName = new HumanName
                    {
                        FamilyName = "Игрович",
                        GivenName = "Борис",
                        MiddleName = "Петрович",
                    },
                }
            };

            return doctor;
        }

        public StepAmb MinStepAmbSet()
        {
            StepAmb stepAmb = new StepAmb
            {
                DateStart = CaseAmbData.step.DateStart,
                DateEnd = CaseAmbData.step.DateEnd,
                IdStepMis = CaseAmbData.step.IdStepMis,
                IdPaymentType = CaseAmbData.step.IdPaymentType,
                IdVisitPlace = CaseAmbData.step.IdVisitPlace,
                IdVisitPurpose = CaseAmbData.step.IdVisitPurpose,
                Doctor = MinDoctorSet(),
            };

            return stepAmb;
        }

        public StepAmb MinOtherStepAmbSet()
        {
            StepAmb stepAmb = new StepAmb
            {
                DateStart = CaseAmbData.otherStep.DateStart,
                DateEnd = CaseAmbData.otherStep.DateEnd,
                IdStepMis = CaseAmbData.otherStep.IdStepMis,
                IdPaymentType = CaseAmbData.otherStep.IdPaymentType,
                IdVisitPlace = CaseAmbData.otherStep.IdVisitPlace,
                IdVisitPurpose = CaseAmbData.otherStep.IdVisitPurpose,
                Doctor = MinDoctorSet(),
            };

            return stepAmb;
        }

        public StepStat MinStepStatSet()
        {
            StepStat stepStat = new StepStat
            {
                DateStart = CaseStatData.step.DateStart,
                DateEnd = CaseStatData.step.DateEnd,
                IdStepMis = CaseStatData.step.IdStepMis,
                IdPaymentType = CaseStatData.step.IdPaymentType,
                BedProfile = CaseStatData.step.BedProfile,
                DaySpend = CaseStatData.step.DaySpend,
                HospitalDepartmentName = CaseStatData.step.HospitalDepartmentName,
                IdHospitalDepartment = CaseStatData.step.IdHospitalDepartment,

                Doctor = MinDoctorSet(),
            };

            return stepStat;
        }

        public StepStat MinOtherStepStatSet()
        {
            StepStat stepStat = new StepStat
            {
                DateStart = CaseStatData.otherStep.DateStart,
                DateEnd = CaseStatData.otherStep.DateEnd,
                IdStepMis = CaseStatData.otherStep.IdStepMis,
                IdPaymentType = CaseStatData.otherStep.IdPaymentType,
                BedProfile = CaseStatData.otherStep.BedProfile,
                DaySpend = CaseStatData.otherStep.DaySpend,
                HospitalDepartmentName = CaseStatData.otherStep.HospitalDepartmentName,
                IdHospitalDepartment = CaseStatData.otherStep.IdHospitalDepartment,

                Doctor = MinDoctorSet(),
            };

            return stepStat;
        }

        //диспансеризация
        public CaseAmb MinCaseDispSet()
        {
            CaseAmb caseDisp = new CaseAmb();
            caseDisp.OpenDate = CaseDispData.caseDisp.OpenDate;
            caseDisp.CloseDate = CaseDispData.caseDisp.CloseDate;
            caseDisp.HistoryNumber = CaseDispData.caseDisp.HistoryNumber;
            caseDisp.IdCaseMis = CaseDispData.caseDisp.IdCaseMis;
            caseDisp.IdPaymentType = CaseDispData.caseDisp.IdPaymentType;
            caseDisp.Confidentiality = CaseDispData.caseDisp.Confidentiality;
            caseDisp.DoctorConfidentiality = CaseDispData.caseDisp.DoctorConfidentiality;
            caseDisp.CuratorConfidentiality = CaseDispData.caseDisp.CuratorConfidentiality;
            caseDisp.IdLpu = CaseDispData.caseDisp.IdLpu;
            caseDisp.IdCaseResult = CaseDispData.caseDisp.IdCaseResult;
            caseDisp.Comment = CaseDispData.caseDisp.Comment;
            caseDisp.IdPatientMis = CaseDispData.caseDisp.IdPatientMis;
            caseDisp.IdCaseType = CaseDispData.caseDisp.IdCaseType;

            caseDisp.DoctorInCharge = MinDoctorSet();

            caseDisp.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.Steps = new List<StepAmb>
            {
               MinStepAmbSet()
            };

            caseDisp.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseDisp;
        }

        public CaseAmb MinCaseDispSetForCreate()
        {
            CaseAmb caseDisp = new CaseAmb();
            caseDisp.OpenDate = CaseDispData.caseDisp.OpenDate;
            caseDisp.IdCaseMis = CaseDispData.caseDisp.IdCaseMis;
            caseDisp.IdPaymentType = CaseDispData.caseDisp.IdPaymentType;
            caseDisp.Confidentiality = CaseDispData.caseDisp.Confidentiality;
            caseDisp.DoctorConfidentiality = CaseDispData.caseDisp.DoctorConfidentiality;
            caseDisp.CuratorConfidentiality = CaseDispData.caseDisp.CuratorConfidentiality;
            caseDisp.IdLpu = CaseDispData.caseDisp.IdLpu;
            caseDisp.IdPatientMis = CaseDispData.caseDisp.IdPatientMis;
            caseDisp.IdCaseType = CaseDispData.caseDisp.IdCaseType;

            caseDisp.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.Steps = new List<StepAmb>
            {
               MinStepAmbSet()
            };

            caseDisp.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseDisp;
        }

        public CaseAmb MinCaseDispSetForClose()
        {
            CaseAmb caseDisp = new CaseAmb();
            caseDisp.CloseDate = CaseDispData.caseDisp.CloseDate;
            caseDisp.IdCaseMis = CaseDispData.caseDisp.IdCaseMis;
            caseDisp.Confidentiality = CaseDispData.caseDisp.Confidentiality;
            caseDisp.DoctorConfidentiality = CaseDispData.caseDisp.DoctorConfidentiality;
            caseDisp.CuratorConfidentiality = CaseDispData.caseDisp.CuratorConfidentiality;
            caseDisp.IdLpu = CaseDispData.caseDisp.IdLpu;
            caseDisp.IdCaseResult = CaseDispData.caseDisp.IdCaseResult;
            caseDisp.Comment = CaseDispData.caseDisp.Comment;
            caseDisp.IdPatientMis = CaseDispData.caseDisp.IdPatientMis;

            caseDisp.DoctorInCharge = MinDoctorSet();

            caseDisp.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseDisp.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseDisp;
        }

        public CaseAmb MinCaseAmbSet()
        {
            CaseAmb caseAmb = new CaseAmb();
            caseAmb.OpenDate = CaseAmbData.caseAmb.OpenDate;
            caseAmb.CloseDate = CaseAmbData.caseAmb.CloseDate;
            caseAmb.HistoryNumber = CaseAmbData.caseAmb.HistoryNumber;
            caseAmb.IdCaseMis = CaseAmbData.caseAmb.IdCaseMis;
            caseAmb.IdPaymentType = CaseAmbData.caseAmb.IdPaymentType;
            caseAmb.Confidentiality = CaseAmbData.caseAmb.Confidentiality;
            caseAmb.DoctorConfidentiality = CaseAmbData.caseAmb.DoctorConfidentiality;
            caseAmb.CuratorConfidentiality = CaseAmbData.caseAmb.CuratorConfidentiality;
            caseAmb.IdLpu = CaseAmbData.caseAmb.IdLpu;
            caseAmb.IdCaseResult = CaseAmbData.caseAmb.IdCaseResult;
            caseAmb.Comment = CaseAmbData.caseAmb.Comment;
            caseAmb.IdPatientMis = CaseAmbData.caseAmb.IdPatientMis;
            caseAmb.IdCaseType = CaseAmbData.caseAmb.IdCaseType;

            caseAmb.DoctorInCharge = MinDoctorSet();

            caseAmb.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseAmb.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            // это врменно
           // caseAmb.Guardian = DoctorData.guardian;
            // это врменно

            caseAmb.Steps = new List<StepAmb>
            {
               MinStepAmbSet()
            };

            caseAmb.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseAmb;
        }

        public CaseAmb MinCaseAmbSetForCreate()
        {
            CaseAmb caseAmb = new CaseAmb();
            caseAmb.OpenDate = CaseAmbData.caseAmb.OpenDate;
            caseAmb.HistoryNumber = CaseAmbData.caseAmb.HistoryNumber;
            caseAmb.IdCaseMis = CaseAmbData.caseAmb.IdCaseMis;
            caseAmb.IdPaymentType = CaseAmbData.caseAmb.IdPaymentType;
            caseAmb.Confidentiality = CaseAmbData.caseAmb.Confidentiality;
            caseAmb.DoctorConfidentiality = CaseAmbData.caseAmb.DoctorConfidentiality;
            caseAmb.CuratorConfidentiality = CaseAmbData.caseAmb.CuratorConfidentiality;
            caseAmb.IdLpu = CaseAmbData.caseAmb.IdLpu;
            caseAmb.IdPatientMis = CaseAmbData.caseAmb.IdPatientMis;
            caseAmb.IdCaseType = CaseAmbData.caseAmb.IdCaseType;
            caseAmb.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseAmb.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseAmb.Steps = new List<StepAmb>
            {
               MinStepAmbSet()
            };

            return caseAmb;
        }

        public CaseAmb MinCaseAmbSetForClose()
        {
            CaseAmb caseAmb = new CaseAmb();
            caseAmb.CloseDate = CaseAmbData.caseAmb.CloseDate;
            caseAmb.IdCaseMis = CaseAmbData.caseAmb.IdCaseMis;
            caseAmb.Confidentiality = CaseAmbData.caseAmb.Confidentiality;
            caseAmb.DoctorConfidentiality = CaseAmbData.caseAmb.DoctorConfidentiality;
            caseAmb.CuratorConfidentiality = CaseAmbData.caseAmb.CuratorConfidentiality;
            caseAmb.IdLpu = CaseAmbData.caseAmb.IdLpu;
            caseAmb.IdCaseResult = CaseAmbData.caseAmb.IdCaseResult;
            caseAmb.Comment = CaseAmbData.caseAmb.Comment;
            caseAmb.IdPatientMis = CaseAmbData.caseAmb.IdPatientMis;

            caseAmb.DoctorInCharge = MinDoctorSet();

            caseAmb.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseAmb.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseAmb.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseAmb;
        }

        public CaseAmb FullCaseAmbSetForCreate()
        {
            CaseAmb caseAmb = CaseAmbData.caseAmb;

            caseAmb.CloseDate = new DateTime();
            caseAmb.IdCaseResult = new byte();
            caseAmb.Comment = null;
            caseAmb.IdAmbResult = new byte();

            return caseAmb;
        }

        public CaseAmb FullCaseAmbSetForClose()
        {
            CaseAmb caseAmb = CaseAmbData.caseAmb;
            caseAmb.IdCaseType = new byte();
            caseAmb.Steps = null;
            return caseAmb;
        }

        public CaseStat MinCaseStatSet()
        {
            CaseStat caseStat = new CaseStat();

            caseStat.OpenDate = CaseStatData.caseStat.OpenDate;
            caseStat.CloseDate = CaseStatData.caseStat.CloseDate;
            caseStat.HistoryNumber = CaseStatData.caseStat.HistoryNumber;
            caseStat.IdCaseMis = CaseStatData.caseStat.IdCaseMis;
            caseStat.IdPaymentType = CaseStatData.caseStat.IdPaymentType;
            caseStat.Confidentiality = CaseStatData.caseStat.Confidentiality;
            caseStat.DoctorConfidentiality = CaseStatData.caseStat.DoctorConfidentiality;
            caseStat.CuratorConfidentiality = CaseStatData.caseStat.CuratorConfidentiality;
            caseStat.IdLpu = CaseStatData.caseStat.IdLpu;
            caseStat.IdCaseResult = CaseStatData.caseStat.IdCaseResult;
            caseStat.Comment = CaseStatData.caseStat.Comment;
            caseStat.IdPatientMis = CaseStatData.caseStat.IdPatientMis;
            caseStat.IdTypeFromDiseaseStart = CaseStatData.caseStat.IdTypeFromDiseaseStart;
            caseStat.IdRepetition = CaseStatData.caseStat.IdRepetition;
            caseStat.HospResult = CaseStatData.caseStat.HospResult;
            caseStat.IdHospChannel = CaseStatData.caseStat.IdHospChannel;

            caseStat.DoctorInCharge = MinDoctorSet();

            caseStat.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.Steps = new List<StepStat>
            {
               MinStepStatSet()
            };

            caseStat.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseStat;
        }

        public CaseStat MinCaseStatSetForClose()
        {
            CaseStat caseStat = new CaseStat();

            caseStat.CloseDate = CaseStatData.caseStat.CloseDate;
            caseStat.IdCaseMis = CaseStatData.caseStat.IdCaseMis;
            caseStat.Confidentiality = CaseStatData.caseStat.Confidentiality;
            caseStat.DoctorConfidentiality = CaseStatData.caseStat.DoctorConfidentiality;
            caseStat.CuratorConfidentiality = CaseStatData.caseStat.CuratorConfidentiality;
            caseStat.IdLpu = CaseStatData.caseStat.IdLpu;
            caseStat.IdCaseResult = CaseStatData.caseStat.IdCaseResult;
            caseStat.Comment = CaseStatData.caseStat.Comment;
            caseStat.IdPatientMis = CaseStatData.caseStat.IdPatientMis;
            caseStat.HospResult = CaseStatData.caseStat.HospResult;

            caseStat.DoctorInCharge = MinDoctorSet();

            caseStat.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.MedRecords = new List<MedRecord>
            {
                MinClinicMainDiagnosis()
            };

            return caseStat;
        }

        public CaseStat MinCaseStatSetForCreate()
        {
            CaseStat caseStat = new CaseStat();

            caseStat.OpenDate = CaseStatData.caseStat.OpenDate;
            caseStat.IdCaseMis = CaseStatData.caseStat.IdCaseMis;
            caseStat.IdPaymentType = CaseStatData.caseStat.IdPaymentType;
            caseStat.Confidentiality = CaseStatData.caseStat.Confidentiality;
            caseStat.DoctorConfidentiality = CaseStatData.caseStat.DoctorConfidentiality;
            caseStat.CuratorConfidentiality = CaseStatData.caseStat.CuratorConfidentiality;
            caseStat.IdLpu = CaseStatData.caseStat.IdLpu;
            caseStat.IdPatientMis = CaseStatData.caseStat.IdPatientMis;
            caseStat.HistoryNumber = CaseStatData.caseStat.HistoryNumber;
            caseStat.IdTypeFromDiseaseStart = CaseStatData.caseStat.IdTypeFromDiseaseStart;
            caseStat.IdRepetition = CaseStatData.caseStat.IdRepetition;
            caseStat.IdHospChannel = CaseStatData.caseStat.IdHospChannel;

            caseStat.Author = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.Authenticator = new Participant
            {
                Doctor = MinDoctorSet()
            };

            caseStat.Steps = new List<StepStat>
            {
               MinStepStatSet()
            };

            return caseStat;
        }

        public CaseStat FullCaseStatSetForCreate()
        {
            CaseStat caseStat = CaseStatData.caseStat;

            caseStat.CloseDate = new DateTime();
            caseStat.IdCaseResult = new byte();
            caseStat.Comment = null;
            caseStat.HospResult = new byte();

            return caseStat;
        }

        public CaseStat FullCaseStatSetForClose()
        {
            CaseStat caseStat = CaseStatData.caseStat;
            caseStat.Steps = null;
            caseStat.HospResult = 1;
            caseStat.IdCaseResult = 1;
            return caseStat;
        }

        public CaseAmb FullCaseAmbSet()
        {
            return CaseAmbData.caseAmb;
        }

        public CaseStat FullCaseStatSet()
        {
            return CaseStatData.caseStat;
        }

        public CaseAmb FullCaseDispSet()
        {
            return CaseDispData.caseDisp;
        }
        public CaseAmb FullCaseDispSetWithDoctorWithoutSNILS()
        {
            return CaseDispData.caseDispWithoutSnils;
        }
        public CaseAmb FullCaseDispSetWithDoctorWithSNILS()
        {
            return CaseDispData.caseDispWithSnils;
        }
        public CaseAmb FullCaseDispSetForCreate()
        {
            CaseAmb caseAmb = CaseDispData.caseDisp;

            caseAmb.CloseDate = new DateTime();
            caseAmb.IdCaseResult = new byte();
            caseAmb.Comment = null;
            caseAmb.IdAmbResult = new byte();

            return caseAmb;
        }

        public CaseAmb FullCaseDispSetForClose()
        {
            CaseAmb caseAmb = CaseDispData.caseDisp;
            caseAmb.IdCaseType = new byte();
            caseAmb.Steps = null;
            return caseAmb;
        }
    }
}
