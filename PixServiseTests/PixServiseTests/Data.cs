using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using N3.EMK.Infrastructure.Helpers;

namespace PixServiseTests
{
    public enum Type { guid, oid };
    public static class PatientData
    {
        public static PatientDto Patient { get; set; }
    }

    public static class CaseAmbData
    {
        public static CaseAmb caseAmb { get; set; }
        public static StepAmb step { get; set; }
        public static StepAmb otherStep { get; set; }
        public static StepAmb stepWithSnils { get; set; }
        public static StepAmb stepWithoutSnils { get; set; }
    }

    public static class CaseDispData
    {
        public static CaseAmb caseDisp { get; set; }
        public static StepAmb step { get; set; }
        public static CaseAmb caseDispWithoutSnils { get; set; }
        public static CaseAmb caseDispWithSnils { get; set; }
        public static StepAmb stepDisp { get; set; }
    }

    public static class CaseStatData
    {
        public static CaseStat caseStat { get; set; }
        public static StepStat step { get; set; }
        public static StepStat otherStep { get; set; }
    }

    public static class DoctorData
    {
        public static MedicalStaff doctorInCharge { get; set; }
        public static MedicalStaff doctorInChargeWithSnils { get; set; }
        public static MedicalStaff doctorInChargeWithoutSnils { get; set; }
        public static MedicalStaff otherDoctor { get; set; }
        public static Participant authenticator { get; set; }
        public static Participant author { get; set; }
        public static Participant legalAuthenticator { get; set; }
        public static Participant authenticatorWithSnils { get; set; }
        public static Participant authorWithSnils { get; set; }
        public static Participant legalAuthenticatorWithSnils { get; set; }
        public static Participant authenticatorWithoutSnils { get; set; }
        public static Participant authorWithoutSnils { get; set; }
        public static Participant legalAuthenticatorWithoutSnils { get; set; }
        public static Guardian guardian { get; set; }
    }
    public static class MedRecordData
    {
        public static Service service { get; set; }
        public static TfomsInfo tfomsInfo { get; set; }
        public static AppointedMedication appointedMedication { get; set; }
        public static DeathInfo deathInfo { get; set; }
        public static Diagnosis diagnosis { get; set; }
        public static ClinicMainDiagnosis clinicMainDiagnosis { get; set; }
        public static ClinicMainDiagnosis clinicMainDiagnosisWithSnils { get; set; }
        public static ClinicMainDiagnosis clinicMainDiagnosisWithOutSnils { get; set; }
        public static DispensaryOne dispensaryOne { get; set; }
        public static DispensaryOne dispensaryOneWithSnils { get; set; }
        public static DispensaryOne dispensaryOneWithoutSnils { get; set; }
        public static Referral referral { get; set; }
        public static SickList sickList { get; set; }
        public static DischargeSummary dischargeSummary { get; set; }
        public static LaboratoryReport LaboratoryReport { get; set; }
        public static ConsultNote consultNote { get; set; }
        public static AnatomopathologicalClinicMainDiagnosis anatomopathologicalClinicMainDiagnosis { get; set; }
        public static LaboratoryReport TrueMedRecordDataWithKey { get; internal set; }
        public static LaboratoryReport WrongMedRecordDataWithKey { get; internal set; }
    }

    public static class DocumentData
    {
        public static IdentityDocument PatientPassport { get; set; }
        public static IdentityDocument DoctorPassport { get; set; }
        public static IdentityDocument GuardianPassport { get; set; }
        public static IdentityDocument SNILS { get; set; }
        public static IdentityDocument PatientSNILS { get; set; }
        public static IdentityDocument OldOMS { get; set; }
        public static IdentityDocument SingleOMS { get; set; }
        public static IdentityDocument OtherDoc { get; set; }
    }

    [TestFixture]
    public abstract class Data
    {
        public static string globalGuid = "D500E893-166B-4724-9C78-D0DBE1F1C48D"; //тест
        // "5c04e58b-07c0-421c-804a-cd774685aea2" демо
     
        //выбор oid или guid происходит здесь!!
        private static string oid = "1.2.643.5.1.13.3.25.78.118"; //тест
        //  "1.2.643.5.1.13.3.25.78.230" демо
        private static string guid = "df8ccddc-ac72-46c9-be0e-075fb9fac5c2";
        
        public static string idlpu = guid;
        public static Type type = Type.guid;

        static public void SetDocument()
        {
            DocumentData.PatientPassport = new IdentityDocument
            {
                IdDocumentType = 14,
                DocS = "2621",
                DocN = "395731",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = "128",
            };
            DocumentData.PatientSNILS = new IdentityDocument
            {
                IdDocumentType = 223,
                DocN = "11111111145",
                ProviderName = "ПФР",
                //ExpiredDate = Convert.ToDateTime("01.12.2010"),
                //IssuedDate = Convert.ToDateTime("03.09.2006"),
                //RegionCode = "128",
            };
            DocumentData.DoctorPassport = new IdentityDocument
            {
                IdDocumentType = 14,
                DocS = "2005",
                DocN = "395712",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = "128",
            };

            DocumentData.GuardianPassport = new IdentityDocument
            {
                IdDocumentType = 14,
                DocS = "2000",
                DocN = "395710",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("09.01.2021"),
                IssuedDate = Convert.ToDateTime("13.01.2000"),
                RegionCode = "128",
            };

            DocumentData.SNILS = new IdentityDocument
            {
                IdDocumentType = 223,
                DocN = "59165576238",
                ProviderName = "ПФР",
                //ExpiredDate = Convert.ToDateTime("01.12.2010"),
                //IssuedDate = Convert.ToDateTime("03.09.2006"),
                //RegionCode = "128",
            };

            DocumentData.OldOMS = new IdentityDocument
            {
                IdDocumentType = 226,
                DocN = "225916",
                DocS = "AA",
                ProviderName = "Старый полис",
                ExpiredDate = Convert.ToDateTime("31.01.2040"),
                IssuedDate = Convert.ToDateTime("11.11.2000"),
                RegionCode = "128",
                IdProvider = 23805,
            };

            DocumentData.SingleOMS = new IdentityDocument
            {
                IdDocumentType = 228,
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Единый полис",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = "128",
                IdProvider = 23806,
            };

            DocumentData.OtherDoc = new IdentityDocument
            {
                IdDocumentType = 18,
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Иной документ",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = "128",
            };
        }

        static public void SetMedRecord()
        {
            MedRecordData.service = new Service
            {
                DateStart = new DateTime(2012, 11, 1),
                DateEnd = new DateTime(2012, 11, 10),
                IdServiceType = "A01.01.001.001",
                ServiceName = "Название услуги",
                Performer = new Participant
                {
                    IdRole = 3,
                    Doctor = SetDoctor(),
                },
                PaymentInfo = new PaymentInfo
                {
                    HealthCareUnit = 1,
                    IdPaymentType = 1,
                    PaymentState = 1,
                    Quantity = 1,
                    Tariff = new Decimal(1000),
                },

            };

            MedRecordData.tfomsInfo = new TfomsInfo
            {
                IdTfomsType = 211010,
                Count = 1,
                Tariff = 100,
            };

            MedRecordData.appointedMedication = new AppointedMedication
            {
                AnatomicTherapeuticChemicalClassification = "A",
                DaysCount = 5,
                IssuedDate = new DateTime(2012, 03, 06),
                MedicineIssueType = "PRE",
                MedicineName = "Валерьянка",
                MedicineType = 136,
                MedicineUseWay = 1,
                Number = "324465",
                Seria = "3242309",
                //CourseDose = new Quantity
                //{
                //    IdUnit = 1,
                //    Value = 20,
                //},
                DayDose = new Quantity
                {
                    IdUnit = 16,
                    Value = 20
                },
                OneTimeDose = new Quantity
                {
                    IdUnit = 16,
                    Value = 2,
                },
                Doctor = SetDoctor(),
            };

            MedRecordData.deathInfo = new DeathInfo
            {
                MkbCode = "M00",
            };

            MedRecordData.diagnosis = new Diagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    IdDiseaseType = 1,
                    DiagnosedDate = new DateTime(2012, 02, 02),
                    IdDiagnosisType = 2,
                    Comment = "Комментарий",
                    DiagnosisChangeReason = 2,
                    DiagnosisStage = 1,
                    IdDispensaryState = 8,
                    IdTraumaType = 1,
                    MESImplementationFeature = 10,
                    MedicalStandard = 211010,
                    MkbCode = "F00",
                },
                Doctor = SetDoctor(),
            };

            MedRecordData.clinicMainDiagnosis = new ClinicMainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    IdDiseaseType = 1,
                    DiagnosedDate = new DateTime(2012, 02, 02),
                    IdDiagnosisType = 1,
                    Comment = "Комментарий",
                    DiagnosisChangeReason = 2,
                    DiagnosisStage = 3,
                    IdDispensaryState = 8,
                    IdTraumaType = 1,
                    MESImplementationFeature = 10,
                    MedicalStandard = 211010,
                    MkbCode = "D00",
                },
                Doctor = SetDoctor(),
                Complications = new List<Diagnosis>
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2012, 02, 02),
                            IdDiagnosisType = 2,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 3,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 211010,
                            MkbCode = "C00",
                        },
                        Doctor = SetDoctor(),
                     }
                 }
            };

            MedRecordData.clinicMainDiagnosisWithSnils = new ClinicMainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    IdDiseaseType = 1,
                    DiagnosedDate = new DateTime(2010, 02, 02),
                    IdDiagnosisType = 1,
                    Comment = "Комментарий",
                    DiagnosisChangeReason = 2,
                    DiagnosisStage = 1,
                    IdDispensaryState = 8,
                    IdTraumaType = 1,
                    MESImplementationFeature = 10,
                    MedicalStandard = 211010,
                    MkbCode = "A00",
                },
                Doctor = SetDoctorWithSNILS(),
                Complications = new List<Diagnosis>
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2010, 02, 02),
                            IdDiagnosisType = 2,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 1,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 211010,
                            MkbCode = "A00",
                        },
                        Doctor = SetDoctorWithSNILS(),
                     }
                 }
            };
            MedRecordData.clinicMainDiagnosisWithOutSnils = new ClinicMainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    IdDiseaseType = 1,
                    DiagnosedDate = new DateTime(2010, 02, 02),
                    IdDiagnosisType = 1,
                    Comment = "Комментарий",
                    DiagnosisChangeReason = 2,
                    DiagnosisStage = 1,
                    IdDispensaryState = 8,
                    IdTraumaType = 1,
                    MESImplementationFeature = 10,
                    MedicalStandard = 211010,
                    MkbCode = "A00",
                },
                Doctor = SetDoctorWithoutSNILS(),
                Complications = new List<Diagnosis>
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2010, 02, 02),
                            IdDiagnosisType = 2,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 1,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 211010,
                            MkbCode = "A00",
                        },
                        Doctor = SetDoctorWithoutSNILS(),
                     }
                 }
            };

            MedRecordData.anatomopathologicalClinicMainDiagnosis = new AnatomopathologicalClinicMainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    IdDiseaseType = 1,
                    DiagnosedDate = new DateTime(2012, 02, 02),
                    IdDiagnosisType = 1,
                    Comment = "Комментарий",
                    DiagnosisChangeReason = 2,
                    DiagnosisStage = 4,
                    IdDispensaryState = 8,
                    IdTraumaType = 1,
                    MESImplementationFeature = 10,
                    MedicalStandard = 211010,
                    MkbCode = "A00",
                },
                Doctor = SetDoctor(),
                Complications = new List<Diagnosis>
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2012, 02, 02),
                            IdDiagnosisType = 2,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 4,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 211010,
                            MkbCode = "B00",
                        },
                        Doctor = SetDoctor(),
                     }
                 }
            };

            var data = (new N3.EMK.Infrastructure.Helpers.SignatureHelper()).SignN3Gost(Convert.ToBase64String(File.ReadAllBytes("empty.pdf")), "application/pdf");
            var wrongdata = data.Remove(data.IndexOf("<Sign>") + 6, 1);
            MedRecordData.TrueMedRecordDataWithKey = new LaboratoryReport
            {
                Attachment = new MedDocument.DocumentAttachment
                {
                    Data = Encoding.UTF8.GetBytes(data),
                    Hash = N3.EMK.Infrastructure.Helpers.Md5Helper.GetGost3411Hash(data),
                    MimeType = "text/xml"
                },
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Author = SetDoctor(),
            };

            MedRecordData.WrongMedRecordDataWithKey = new LaboratoryReport
            {
                Attachment = new MedDocument.DocumentAttachment
                {
                    Data = Encoding.UTF8.GetBytes(wrongdata),
                    Hash = Md5Helper.GetGost3411Hash(wrongdata),
                    Url = new Uri("https://www.google.ru"),
                    MimeType = "text/xml"
                },
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Author = SetDoctor(),
            };

            MedRecordData.dispensaryOne = new DispensaryOne
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                IsGuested = true,
                HasExpertCareRefferal = true,
                IsUnderObservation = true,
                HasExtraResearchRefferal = false,
                HasPrescribeCure = true,
                HasHealthResortRefferal = false,
                HasSecondStageRefferal = false,
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
                HealthGroup = new HealthGroup
                {
                    Doctor = SetDoctor(),
                    HealthGroupInfo = new HealthGroupInfo
                    {
                        Date = new DateTime(2010, 02, 03),
                        IdHealthGroup = 1,
                    }
                },
                Recommendations = new List<Recommendation> 
                { 
                    new Recommendation
                    {
                         Doctor = SetDoctor(),
                         Date = new DateTime(2012,02,04),
                         Text = "Текст рекомендации",
                    }
                }
            };

            MedRecordData.dispensaryOneWithSnils = new DispensaryOne
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                IsGuested = true,
                HasExpertCareRefferal = true,
                IsUnderObservation = true,
                HasExtraResearchRefferal = false,
                HasPrescribeCure = true,
                HasHealthResortRefferal = false,
                HasSecondStageRefferal = false,
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctorWithSNILS(),
                HealthGroup = new HealthGroup
                {
                    Doctor = SetDoctorWithSNILS(),
                    HealthGroupInfo = new HealthGroupInfo
                    {
                        Date = new DateTime(2010, 02, 03),
                        IdHealthGroup = 1,
                    }
                },
                Recommendations = new List<Recommendation> 
                {
                    new Recommendation
                    {
                         Doctor = SetDoctorWithSNILS(),
                         Date = new DateTime(2010,02,04),
                         Text = "Текст рекомендации",
                    }
                }
            };
            MedRecordData.dispensaryOneWithoutSnils = new DispensaryOne
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                IsGuested = true,
                HasExpertCareRefferal = true,
                IsUnderObservation = true,
                HasExtraResearchRefferal = false,
                HasPrescribeCure = true,
                HasHealthResortRefferal = false,
                HasSecondStageRefferal = false,
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctorWithoutSNILS(),
                HealthGroup = new HealthGroup
                {
                    Doctor = SetDoctorWithoutSNILS(),
                    HealthGroupInfo = new HealthGroupInfo
                    {
                        Date = new DateTime(2010, 02, 03),
                        IdHealthGroup = 1,
                    }
                },
                Recommendations = new List<Recommendation> 
                {
                    new Recommendation
                    {
                         Doctor = SetDoctorWithoutSNILS(),
                         Date = new DateTime(2010,02,04),
                         Text = "Текст рекомендации",
                    }
                }
            };

            MedRecordData.referral = new Referral
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                IdSourceLpu = idlpu,
                IdTargetLpu = idlpu,
                ReferralID = "referralId2890",
                RelatedID = "relatedId02890",
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
                ReferralInfo = new ReferralInfo
                {
                    Reason = "Потому что",
                    IdReferralMis = "idReferralMis2983",
                    IdReferralType = 1,
                    IssuedDateTime = new DateTime(2012, 02, 05),
                    HospitalizationOrder = 2,
                    MkbCode = "A00.0",
                },
                DepartmentHead = SetDoctor(),
            };

            MedRecordData.sickList = new SickList
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
                SickListInfo = new SickListInfo
                {
                    Number = "341234567890",
                    DateStart = new DateTime(2012, 02, 02),
                    DateEnd = new DateTime(2012, 02, 22),
                    DisabilityDocState = 1,
                    DisabilityDocReason = 1,
                    IsPatientTaker = false,
                    Caregiver = SetGuardian()
                }
            };

            MedRecordData.dischargeSummary = new DischargeSummary
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
            };

            MedRecordData.LaboratoryReport = new LaboratoryReport
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
            };

            MedRecordData.consultNote = new ConsultNote
            {
                CreationDate = new DateTime(2012, 02, 02),
                Header = "Header",
                Attachment = SetAttachment("empty.pdf", "https://www.google.ru", "application/pdf"),
                Author = SetDoctor(),
            };
        }

        private static MedicalStaff SetDoctorWithoutSNILS()
        {
            MedicalStaff doctor = new MedicalStaff
            {
                IdLpu = idlpu,
                IdSpeciality = 29,
                IdPosition = 74,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "1234testTEST",
                    Sex = 1,
                    Birthdate = new DateTime(1974, 01, 07),
                    HumanName = new HumanName
                    {
                        FamilyName = "Иванов",
                        GivenName = "Дмитрий",
                        MiddleName = "Александрович",
                    },
                }
            };
            return doctor;
        }
        private static MedicalStaff SetDoctorWithSNILS()
        {
            MedicalStaff doctor = new MedicalStaff
            {
                IdLpu = idlpu,
                IdSpeciality = 29,
                IdPosition = 74,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "1234testTEST",
                    Sex = 1,
                    Birthdate = new DateTime(1974, 01, 07),
                    Documents = new List<IdentityDocument> 
                    {
                        DocumentData.SNILS,
                    },
                    HumanName = new HumanName
                    {
                        FamilyName = "Иванов",
                        GivenName = "Дмитрий",
                        MiddleName = "Александрович",
                    },
                }
            };
            return doctor;
        }

        private static MedicalStaff SetDoctor()
        {
            MedicalStaff doctor = new MedicalStaff
            {
                IdLpu = idlpu,
                IdSpeciality = 29,
                IdPosition = 74,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "Doctor" + DateTime.Now.ToString(),
                    Sex = 1,
                    Birthdate = new DateTime(1974, 01, 07),
                    Documents = new List<IdentityDocument>
                    {
                        DocumentData.SNILS
                    },
                    HumanName = new HumanName
                    {
                        FamilyName = "Иванов",
                        GivenName = "Дмитрий",
                        MiddleName = "Александрович",
                    },
                }
            };
            return doctor;
        }

        private static MedicalStaff SetOtherDoctor()
        {
            MedicalStaff doctor = new MedicalStaff
            {
                IdLpu = idlpu,
                IdSpeciality = 4,
                IdPosition = 69,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = "123123123259",
                    Sex = 1,
                    Birthdate = new DateTime(1978, 01, 07),
                    Documents = new List<IdentityDocument> 
                    {
                        DocumentData.SNILS
                    },
                    HumanName = new HumanName
                    {
                        FamilyName = "Петров",
                        GivenName = "Евгений",
                        MiddleName = "Васильевич",
                    },
                }
            };
            return doctor;
        }

        private static Guardian SetGuardian()
        {
            Guardian guardian = new Guardian
            {
                IdRelationType = 1,
                UnderlyingDocument = "реквизиты",
                Person = new PersonWithIdentity
                {
                    Sex = 1,
                    Birthdate = new DateTime(1980, 11, 17),
                    IdPersonMis = DateTime.Now.ToString(),
                    Documents = new List<IdentityDocument> 
                    {
                        DocumentData.GuardianPassport,
                        //DocumentData.SNILS
                    },
                    HumanName = new HumanName
                    {
                        FamilyName = "Жданник",
                        GivenName = "Игорь",
                        MiddleName = "Михаилович",
                    }
                }
            };

            return guardian;
        }

        public static MedDocument.DocumentAttachment SetAttachment(string path, string url, string mimeType)
        {
            MedDocument.DocumentAttachment a = new MedDocument.DocumentAttachment
            {
                Data = Encoding.UTF8.GetBytes(Convert.ToBase64String(File.ReadAllBytes(path))),
                //Url = new Uri(url),
                MimeType = mimeType
            };
            a.Hash = N3.EMK.Infrastructure.Helpers.Md5Helper.GetGost3411Hash(a.Data);
            return a;
        }

        private void SetAmbCase()
        {
            DoctorData.doctorInCharge = SetDoctor();

            DoctorData.author = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctor(),
            };

            DoctorData.authenticator = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctor(),
            };

            //DoctorData.legalAuthenticator = new Participant
            //{
            //    IdRole = 1,
            //    Doctor = SetDoctor(),
            //};

            DoctorData.doctorInChargeWithSnils = SetDoctorWithSNILS();

            DoctorData.authorWithSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithSNILS(),
            };

            DoctorData.authenticatorWithSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithSNILS(),
            };

            DoctorData.legalAuthenticatorWithSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithSNILS(),
            };

            DoctorData.doctorInChargeWithoutSnils = SetDoctorWithoutSNILS();

            DoctorData.authorWithoutSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithoutSNILS(),
            };

            DoctorData.authenticatorWithoutSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithoutSNILS(),
            };

            DoctorData.legalAuthenticatorWithoutSnils = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctorWithoutSNILS(),
            };

            DoctorData.guardian = SetGuardian();

            CaseAmbData.step = new StepAmb
            {
                DateStart = new DateTime(2012, 10, 10),
                DateEnd = new DateTime(2012, 10, 14),
                IdStepMis = "Step " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                IdVisitPlace = 1,
                IdVisitPurpose = 1,
                Comment = "Comment",

                //MedRecords

                Doctor = SetDoctor(),
            };
            CaseAmbData.stepWithSnils = new StepAmb
            {
                DateStart = new DateTime(2012, 10, 10),
                DateEnd = new DateTime(2012, 10, 14),
                IdStepMis = "Step " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                IdVisitPlace = 1,
                IdVisitPurpose = 1,
                Comment = "Comment",

                //MedRecords

                Doctor = SetDoctorWithSNILS(),
            };
            CaseAmbData.stepWithoutSnils = new StepAmb
            {
                DateStart = new DateTime(2012, 10, 10),
                DateEnd = new DateTime(2012, 10, 14),
                IdStepMis = "Step " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                IdVisitPlace = 1,
                IdVisitPurpose = 1,
                Comment = "Comment",

                //MedRecords

                Doctor = SetDoctorWithoutSNILS(),
            };

            CaseAmbData.otherStep = new StepAmb
            {
                DateStart = new DateTime(2012, 09, 07),
                DateEnd = new DateTime(2013, 10, 14),
                IdStepMis = "OtherStep " + DateTime.Now.ToString(),
                IdPaymentType = 2,
                IdVisitPlace = 2,
                IdVisitPurpose = 2,
                Comment = "OtherComment",

                //MedRecords

                Doctor = SetDoctor(),
            };

            CaseAmbData.caseAmb = new CaseAmb
            {
                OpenDate = new DateTime(2012, 10, 10),
                CloseDate = new DateTime(2012, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "CaseAmb " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = idlpu,
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseType = 2,

                IdCaseAidType = 1,
                IdAmbResult = 1,
                IdCasePurpose = 1,
                IsActive = true,

                Guardian = DoctorData.guardian,
                DoctorInCharge = DoctorData.doctorInCharge,
                Authenticator = DoctorData.authenticator,
                Author = DoctorData.author,
                LegalAuthenticator = DoctorData.legalAuthenticator,
                Steps = new List<StepAmb> { CaseAmbData.step }
                //MedRecords
            };
            CaseDispData.caseDisp = new CaseAmb
            {
                OpenDate = new DateTime(2012, 10, 10),
                CloseDate = new DateTime(2012, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "CaseDisp " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = idlpu,
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseType = 4,

                IdCaseAidType = 1,
                IdAmbResult = 1,
                IdCasePurpose = 1,
                IsActive = true,

                Guardian = DoctorData.guardian,
                DoctorInCharge = DoctorData.doctorInCharge,
                Authenticator = DoctorData.authenticator,
                Author = DoctorData.author,
                LegalAuthenticator = DoctorData.legalAuthenticator,
                Steps = new List<StepAmb> { CaseAmbData.step }
                //MedRecords
            };
            CaseDispData.caseDispWithoutSnils = new CaseAmb
            {
                OpenDate = new DateTime(2010, 10, 10),
                CloseDate = new DateTime(2010, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "CaseDisp " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = idlpu,
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseType = 4,

                IdCaseAidType = 1,
                IdAmbResult = 1,
                IdCasePurpose = 1,
                IsActive = true,

                Guardian = DoctorData.guardian,
                DoctorInCharge = DoctorData.doctorInChargeWithoutSnils,
                Authenticator = DoctorData.authenticatorWithoutSnils,
                Author = DoctorData.authorWithoutSnils,
                LegalAuthenticator = DoctorData.legalAuthenticatorWithoutSnils,
                Steps = new List<StepAmb> { CaseAmbData.step }
                //MedRecords
            };
            CaseDispData.caseDispWithSnils = new CaseAmb
            {
                OpenDate = new DateTime(2010, 10, 10),
                CloseDate = new DateTime(2010, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "CaseDisp " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = idlpu,
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseType = 4,

                IdCaseAidType = 1,
                IdAmbResult = 1,
                IdCasePurpose = 1,
                IsActive = true,

                Guardian = DoctorData.guardian,
                DoctorInCharge = DoctorData.doctorInChargeWithSnils,
                Authenticator = DoctorData.authenticatorWithSnils,
                Author = DoctorData.authorWithSnils,
                LegalAuthenticator = DoctorData.legalAuthenticatorWithSnils,
                Steps = new List<StepAmb> { CaseAmbData.step }
                //MedRecords
            };
        }

        private void SetStatCase()
        {
            DoctorData.doctorInCharge = SetDoctor();

            DoctorData.author = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctor(),
            };

            DoctorData.authenticator = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctor(),
            };

            DoctorData.legalAuthenticator = new Participant
            {
                IdRole = 1,
                Doctor = SetDoctor(),
            };

            DoctorData.guardian = SetGuardian();

            CaseStatData.step = new StepStat
            {
                DateStart = new DateTime(2012, 10, 10),
                DateEnd = new DateTime(2012, 10, 14),
                IdStepMis = "Step " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Comment = "Comment",
                BedNumber = "1A",
                BedProfile = 1,
                DaySpend = 1,
                HospitalDepartmentName = "Название Госпиталя",
                IdHospitalDepartment = "1",
                IdRegimen = 1,
                WardNumber = "1",

                //MedRecords

                Doctor = SetDoctor(),
            };

            CaseStatData.otherStep = new StepStat
            {
                DateStart = new DateTime(2012, 09, 09),
                DateEnd = new DateTime(2012, 10, 14),
                IdStepMis = "OtherStep " + DateTime.Now.ToString(),
                IdPaymentType = 2,
                Comment = "OtherComment",
                BedNumber = "3A",
                BedProfile = 2,
                DaySpend = 2,
                HospitalDepartmentName = "Другое название Госпиталя",
                IdHospitalDepartment = "2",
                IdRegimen = 2,
                WardNumber = "2",

                //MedRecords

                Doctor = SetDoctor(),
            };

            CaseStatData.caseStat = new CaseStat
            {
                OpenDate = new DateTime(2012, 10, 10),
                CloseDate = new DateTime(2012, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "CaseStat " + DateTime.Now.ToString(),
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = idlpu,
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseAidType = 1,

                // IdHospChannel = 1,

                IdHospChannel = 2,
                DeliveryCode = "Код бригады",

                IdIntoxicationType = 1,
                IdPatientConditionOnAdmission = 1,
                IdTypeFromDiseaseStart = 1,
                IdRepetition = 1,
                HospitalizationOrder = 1,
                IdTransportIntern = 1,
                HospResult = 1,
                RW1Mark = true,
                AIDSMark = true,

                PrehospitalDefects = new List<byte>() { 1, 2 },

                Guardian = DoctorData.guardian,
                DoctorInCharge = DoctorData.doctorInCharge,
                Authenticator = DoctorData.authenticator,
                Author = DoctorData.author,
                LegalAuthenticator = DoctorData.legalAuthenticator,
                Steps = new List<StepStat> { CaseStatData.step },
                //MedRecords
            };
        }

        [SetUp]
        public void SetUp()
        {
            PatientData.Patient = new PatientDto
            {
                FamilyName = "Дудкин",
                GivenName = "Алёша",
                BirthDate = new DateTime(1983, 01, 07),
                Sex = 1,
                IdPatientMIS = "PatientMIS" + DateTime.Now.ToString(),
            };

            SetDocument();

            DoctorData.otherDoctor = SetOtherDoctor();

            SetMedRecord();

            SetAmbCase();

            SetStatCase();
        }

        [TearDown]
        public void Clear()
        {
            Global.errors3.Clear();
            Global.errors2.Clear();
            Global.errors1.Clear();
        }
    }
}
