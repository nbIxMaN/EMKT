using System;
using NUnit.Framework;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;

namespace PixServiseTests
{
    public static class PatientData
    {
        public static PatientDto Patient { get; set; }
    }

    public static class CaseAmbData
    {
        public static CaseAmb caseAmb { get; set; }

        public static MedicalStaff doctorInCharge { get; set; }
        // public static PersonWithIdentity doctorInChargePerson { get; set; }
        // public static HumanName doctorInChargeName { get; set; }

        public static Participant authenticator { get; set; }
        //public static PersonWithIdentity authenticatorPerson { get; set; }
        //public static HumanName authenticatorName { get; set; }

        public static Participant author { get; set; }
        //public static PersonWithIdentity authorPerson { get; set; }
        //public static HumanName authorName { get; set; }

        public static Participant legalAuthenticator { get; set; }
        //public static PersonWithIdentity legalAuthenticatorPerson { get; set; }
        //public static HumanName legalAuthenticatorName { get; set; }

        public static Guardian guardian { get; set; }

        public static StepAmb step { get; set; }
        //public static MedicalStaff doctor { get; set; }
        //public static PersonWithIdentity doctorPerson { get; set; }
        //public static HumanName doctorName { get; set; }
    }

    public static class MedRecordData
    {
        public static Service service { get; set; }
        public static TfomsInfo tfomsInfo { get; set; }
        public static AppointedMedication appointedMedication { get; set; }
        public static DeathInfo deathInfo { get; set; }
        public static Diagnosis diagnosis { get; set; }
        public static ClinicMainDiagnosis clinicMainDiagnosis { get; set; }
        public static MedDocument medDocument { get; set; }
        public static DispensaryOne dispensaryOne { get; set; }
        public static Referral referral { get; set; }
        public static SickList sickList { get; set; }
        public static DischargeSummary dischargeSummary { get; set; }
        public static LaboratoryReport LaboratoryReport { get; set; }
        public static ConsultNote consultNote { get; set; }
        public static AnatomopathologicalClinicMainDiagnosis anatomopathologicalClinicMainDiagnosis { get; set; }
    }

    public static class DocumentData
    {
        public static IdentityDocument Passport { get; set; }
        public static IdentityDocument SNILS { get; set; }
        public static IdentityDocument OldOMS { get; set; }
        public static IdentityDocument SingleOMS { get; set; }
        public static IdentityDocument OtherDoc { get; set; }
    }

    public static class CaseStatData
    {
        public static CaseStat caseStat { get; set; }

        public static MedicalStaff doctorInCharge { get; set; }
        // public static PersonWithIdentity doctorInChargePerson { get; set; }
        // public static HumanName doctorInChargeName { get; set; }

        public static Participant authenticator { get; set; }
        //public static PersonWithIdentity authenticatorPerson { get; set; }
        //public static HumanName authenticatorName { get; set; }

        public static Participant author { get; set; }
        //public static PersonWithIdentity authorPerson { get; set; }
        //public static HumanName authorName { get; set; }

        public static Participant legalAuthenticator { get; set; }
        //public static PersonWithIdentity legalAuthenticatorPerson { get; set; }
        //public static HumanName legalAuthenticatorName { get; set; }

        public static Guardian guardian { get; set; }

        public static StepStat step { get; set; }
        //public static MedicalStaff doctor { get; set; }
        //public static PersonWithIdentity doctorPerson { get; set; }
        //public static HumanName doctorName { get; set; }



        public static MedRecord medRecord { get; set; }
    }

    [TestFixture]
    public abstract class Data
    {
        private void SetDocument()
        {
            DocumentData.Passport = new IdentityDocument
            {
                IdDocumentType = 14,
                DocS = "2007",
                DocN = "395732",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = "128",
            };

            DocumentData.SNILS = new IdentityDocument
            {
                IdDocumentType = 223,
                DocN = "59165576238",
                ProviderName = "ПФР",
                ExpiredDate = Convert.ToDateTime("01.12.2010"),
                IssuedDate = Convert.ToDateTime("03.09.2006"),
                RegionCode = "128",
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

        private void SetMedRecord()
        {
            MedRecordData.service = new Service
            {
                DateStart = new DateTime(2010, 11, 1),
                DateEnd = new DateTime(2010, 11, 10),
                IdServiceType = "1088",
                ServiceName = "Название услуги",
                Performer = new Participant
                {
                    /*IdRole = 3,
                     Doctor = new MedicalStaff
                     {
                         IdLpu = "1.2.643.5.1.13.3.25.78.118",
                         IdSpeciality = 29,
                         IdPosition = 72,
                         Person = new PersonWithIdentity
                         {
                             IdPersonMis = "123123123",
                             Sex = 1,
                             Birthdate = new DateTime(1973, 01, 07),
                             Documents = new IdentityDocument[]
                             {
                                 DocumentData.Passport,
                                 DocumentData.SNILS
                             },
                             HumanName = new HumanName
                             {
                                 FamilyName = "Лукин",
                                 GivenName = "Василий",
                                 MiddleName = "Андреевич",
                             },
                         }
                     }*/
                 },
               /*  PaymentInfo = new PaymentInfo
                 {
                     HealthCareUnit = 1,
                     IdPaymentType = 1,
                     PaymentState = 1,
                     Quantity = 1,
                     Tariff = new Decimal(1000),
                 },*/
                
            };

            MedRecordData.tfomsInfo = new TfomsInfo
            {
                IdTfomsType = 211010,
                Count = 1,
                Tariff = 100,
            };

            MedRecordData.appointedMedication = new AppointedMedication
            {
                AnatomicTherapeuticChemicalClassification = "1",
                DaysCount = 5,
                IssuedDate = new DateTime(2010, 03, 06),
                MedicineIssueType = "PRE", // по справочнику строка PRE или CURE 
                MedicineName = "Валерьянка",
                MedicineType = 136,
                MedicineUseWay = 1,
                Number = "324465",
                Seria = "3242309",
                CourseDose = new Quantity
                {
                    IdUnit = 1,
                    Value = 20,
                },
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
                Doctor = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            MedRecordData.deathInfo = new DeathInfo
            {
                MkbCode = "11730",
            };

            MedRecordData.diagnosis = new Diagnosis
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
                    MedicalStandard = 21,// по справочнику 211010
                    MkbCode = "11730",
                },
                Doctor = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            MedRecordData.clinicMainDiagnosis = new ClinicMainDiagnosis
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
                     MedicalStandard = 21,// по справочнику 211010
                     MkbCode = "11730",
                 },
                 Doctor = new MedicalStaff
                 {
                     IdLpu = "1.2.643.5.1.13.3.25.78.118",
                     IdSpeciality = 29,
                     IdPosition = 72,
                     Person = new PersonWithIdentity
                     {
                         IdPersonMis = "123123123",
                         Sex = 1,
                         Birthdate = new DateTime(1973, 01, 07),
                         Documents = new IdentityDocument[]
                         {
                            DocumentData.Passport,
                            DocumentData.SNILS
                         },
                         HumanName = new HumanName
                         {
                             FamilyName = "Лукин",
                             GivenName = "Василий",
                             MiddleName = "Андреевич",
                         },
                     }
                 },
                 Complications = new Diagnosis[]
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2010, 02, 02),
                            IdDiagnosisType = 1,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 1,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 21,// по справочнику 211010
                            MkbCode = "11730",
                        },
                        Doctor = new MedicalStaff
                        {
                            IdLpu = "1.2.643.5.1.13.3.25.78.118",
                            IdSpeciality = 29,
                            IdPosition = 72,
                            Person = new PersonWithIdentity
                            {
                                IdPersonMis = "123123123",
                                Sex = 1,
                                Birthdate = new DateTime(1973, 01, 07),
                                Documents = new IdentityDocument[]
                                {
                                    DocumentData.Passport,
                                    DocumentData.SNILS
                                },
                                HumanName = new HumanName
                                {
                                    FamilyName = "Лукин",
                                    GivenName = "Василий",
                                    MiddleName = "Андреевич",
                                },
                            }
                         }
                     }
                 }
             };

            MedRecordData.anatomopathologicalClinicMainDiagnosis = new AnatomopathologicalClinicMainDiagnosis
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
                    MedicalStandard = 21,// по справочнику 211010
                    MkbCode = "11730",
                },
                Doctor = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
                Complications = new Diagnosis[]
                 {
                     new  Diagnosis
                     {
                         DiagnosisInfo = new DiagnosisInfo
                         {
                            IdDiseaseType = 2,
                            DiagnosedDate = new DateTime(2010, 02, 02),
                            IdDiagnosisType = 1,
                            Comment = "Комментарий",
                            DiagnosisChangeReason = 2,
                            DiagnosisStage = 1,
                            IdDispensaryState = 8,
                            IdTraumaType = 1,
                            MESImplementationFeature = 10,
                            MedicalStandard = 21,// по справочнику 211010
                            MkbCode = "11730",
                        },
                        Doctor = new MedicalStaff
                        {
                            IdLpu = "1.2.643.5.1.13.3.25.78.118",
                            IdSpeciality = 29,
                            IdPosition = 72,
                            Person = new PersonWithIdentity
                            {
                                IdPersonMis = "123123123",
                                Sex = 1,
                                Birthdate = new DateTime(1973, 01, 07),
                                Documents = new IdentityDocument[]
                                {
                                    DocumentData.Passport,
                                    DocumentData.SNILS
                                },
                                HumanName = new HumanName
                                {
                                    FamilyName = "Лукин",
                                    GivenName = "Василий",
                                    MiddleName = "Андреевич",
                                },
                            }
                         }
                     }
                 }
            };

            MedRecordData.dispensaryOne = new DispensaryOne
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
                Attachment = new MedDocument.DocumentAttachment
                {

                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
                HealthGroup = new HealthGroup
                {
                    Doctor = new MedicalStaff
                    {
                        IdLpu = "1.2.643.5.1.13.3.25.78.118",
                        IdSpeciality = 29,
                        IdPosition = 72,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = "123123123",
                            Sex = 1,
                            Birthdate = new DateTime(1973, 01, 07),
                            Documents = new IdentityDocument[]
                            {
                                DocumentData.Passport,
                                DocumentData.SNILS
                            },
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий",
                                MiddleName = "Андреевич",
                            },
                        }
                    },
                    HealthGroupInfo = new HealthGroupInfo
                    {
                        Date = new DateTime(2010, 02, 03),
                        IdHealthGroup = 1,
                    }
                },
                Recommendations = new Recommendation[] 
                { 
                    new Recommendation
                    {
                         Doctor = new MedicalStaff
                         {
                            IdLpu = "1.2.643.5.1.13.3.25.78.118",
                            IdSpeciality = 29,
                            IdPosition = 72,
                            Person = new PersonWithIdentity
                            {
                                IdPersonMis = "123123123",
                                Sex = 1,
                                Birthdate = new DateTime(1973, 01, 07),
                                Documents = new IdentityDocument[]
                                {
                                    DocumentData.Passport,
                                    DocumentData.SNILS
                                },
                                HumanName = new HumanName
                                {
                                    FamilyName = "Лукин",
                                    GivenName = "Василий",
                                    MiddleName = "Андреевич",
                                },
                            }
                         },
                         Date = new DateTime(2010,02,04),
                         Text = "Текст рекомендации",
                    }
                }
            };

            MedRecordData.referral = new Referral
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                IdSourceLpu = "1.2.643.5.1.13.3.25.78.118",
                IdTargetLpu = "1.2.643.5.1.13.3.25.78.118",
                ReferralID = "referralId2890",
                RelatedID = "relatedId02890",
                Attachment = new MedDocument.DocumentAttachment
                {

                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
                ReferralInfo = new ReferralInfo
                {
                    Reason = "Потому что",
                    IdReferralMis = "idReferralMis2983",
                    IdReferralType = 1,
                    IssuedDateTime = new DateTime(2010, 02, 05),
                    HospitalizationOrder = 2,
                    MkbCode = "11730",
                },
                DepartmentHead = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            MedRecordData.sickList = new SickList
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                Attachment = new MedDocument.DocumentAttachment
                {

                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
                SickListInfo = new SickListInfo
                {
                    Number = "34",
                    DateStart = new DateTime(2010, 02, 02),
                    DateEnd = new DateTime(2010, 02, 22),
                    DisabilityDocState = 1,
                    DisabilityDocReason = 1,
                    IsPatientTaker = false,
                    Caregiver = new Guardian
                    {
                        IdRelationType = 1,
                        UnderlyingDocument = "Реквизиты",
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = "123123123",
                            Sex = 1,
                            Birthdate = new DateTime(1973, 01, 07),
                            Documents = new IdentityDocument[]
                            {
                                DocumentData.Passport,
                                DocumentData.SNILS
                            }, 
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий",
                                MiddleName = "Андреевич",
                            },
                        }
                    }
                }
            };

            MedRecordData.dischargeSummary = new DischargeSummary
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                Attachment = new MedDocument.DocumentAttachment
                {

                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            MedRecordData.LaboratoryReport = new LaboratoryReport
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                Attachment = new MedDocument.DocumentAttachment
                {
                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
            };
            MedRecordData.consultNote = new ConsultNote
            {
                CreationDate = new DateTime(2010, 02, 02),
                Header = "Header",
                Attachment = new MedDocument.DocumentAttachment
                {
                    Data = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Hash = Convert.FromBase64String("SGVsbG8sIFdvcmxk"),
                    Url = new Uri("https://www.google.ru"),
                },
                Author = new MedicalStaff
                {
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    IdSpeciality = 29,
                    IdPosition = 72,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "123123123",
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                },
            };

        }

        private void SetAmbCase()
        {
            CaseAmbData.doctorInCharge = new MedicalStaff
            {
                IdSpeciality = 29,
                IdPosition = 72,
                IdLpu = "1.2.643.5.1.13.3.25.78.118",
                Person = new PersonWithIdentity
                {
                    Sex = 1,
                    Birthdate = new DateTime(1973, 01, 07),
                    IdPersonMis = "123123123",
                    Documents = new IdentityDocument[]
                    {
                        DocumentData.Passport,
                        DocumentData.SNILS
                    },

                    HumanName = new HumanName
                    {
                        FamilyName = "Лукин",
                        GivenName = "Василий",
                        MiddleName = "Андреевич",
                    }
                }
            };

            CaseAmbData.author = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseAmbData.authenticator = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseAmbData.legalAuthenticator = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        }, 
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseAmbData.guardian = new Guardian
            {
                IdRelationType = 1,
                UnderlyingDocument = "реквизиты",
                Person = new PersonWithIdentity
                {
                    Sex = 1,
                    Birthdate = new DateTime(1973, 01, 07),
                    IdPersonMis = "123123123",
                    Documents = new IdentityDocument[]
                    {
                        DocumentData.Passport,
                        DocumentData.SNILS
                    },
                    HumanName = new HumanName
                    {
                        FamilyName = "Лукин",
                        GivenName = "Василий",
                        MiddleName = "Андреевич",
                    }
                }
            };

            CaseAmbData.step = new StepAmb
            {
                DateStart = new DateTime(2010, 10, 10),
                DateEnd = new DateTime(2010, 10, 14),
                IdStepMis = "12341234",
                IdPaymentType = 1,
                IdVisitPlace = 1,
                IdVisitPurpose = 1,
                Comment = "Comment",

                //MedRecords

                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseAmbData.caseAmb = new CaseAmb
            {
                OpenDate = new DateTime(2010, 10, 10),
                CloseDate = new DateTime(2010, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "123412341234",
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = "1.2.643.5.1.13.3.25.78.118",
                IdCaseResult = 1,
                Comment = "КОММЕНТ",
                IdPatientMis = PatientData.Patient.IdPatientMIS,
                IdCaseType = 2,

                IdCaseAidType = 1,
                IdAmbResult = 1,
                IdCasePurpose = 1,
                IsActive = true,

                Guardian = CaseAmbData.guardian,
                DoctorInCharge = CaseAmbData.doctorInCharge,
                Authenticator = CaseAmbData.authenticator,
                Author = CaseAmbData.author,
                LegalAuthenticator = CaseAmbData.legalAuthenticator,
                Steps = new StepAmb[] { CaseAmbData.step },
                MedRecords = new MedRecord[] { MedRecordData.tfomsInfo/*, MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    MedRecordData.appointedMedication, MedRecordData.clinicMainDiagnosis, MedRecordData.consultNote, 
                    MedRecordData.deathInfo, MedRecordData.diagnosis, MedRecordData.dischargeSummary, MedRecordData.dispensaryOne, 
                    MedRecordData.LaboratoryReport, MedRecordData.service*/ },

            };
        }

        private void SetStatCase()
        {
            CaseStatData.doctorInCharge = new MedicalStaff
            {
                IdSpeciality = 29,
                IdPosition = 72,
                IdLpu = "1.2.643.5.1.13.3.25.78.118",
                Person = new PersonWithIdentity
                {
                    Sex = 1,
                    Birthdate = new DateTime(1973, 01, 07),
                    IdPersonMis = "123123123",
                    Documents = new IdentityDocument[]
                    {
                        DocumentData.Passport,
                        DocumentData.SNILS
                    }, 
                    HumanName = new HumanName
                    {
                        FamilyName = "Лукин",
                        GivenName = "Василий",
                        MiddleName = "Андреевич",
                    }
                }
            };

            CaseStatData.author = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        }, 
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseStatData.authenticator = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        }, 
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseStatData.legalAuthenticator = new Participant
            {
                IdRole = 1,
                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },   
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseStatData.guardian = new Guardian
            {
                IdRelationType = 1,
                UnderlyingDocument = "реквизиты",
                Person = new PersonWithIdentity
                {
                    Sex = 1,
                    Birthdate = new DateTime(1973, 01, 07),
                    IdPersonMis = "123123123",
                    Documents = new IdentityDocument[]
                    {
                        DocumentData.Passport,
                        DocumentData.SNILS
                    },  
                    HumanName = new HumanName
                    {
                        FamilyName = "Лукин",
                        GivenName = "Василий",
                        MiddleName = "Андреевич",
                    }
                }
            };

            CaseStatData.step = new StepStat
            {
                DateStart = new DateTime(2010, 10, 10),
                DateEnd = new DateTime(2010, 10, 14),
                IdStepMis = "12341234",
                IdPaymentType = 1,
                Comment = "Comment",
                BedNumber = "1A",
                BedProfile = 1,
                DaySpend = 1,
                HospitalDepartmentName = "Название Госпиталя",
                IdHospitalDepartment = "1",
                IdRegimen = 1,
                WardNumber = "1",

                //MedReacords

                Doctor = new MedicalStaff
                {
                    IdSpeciality = 29,
                    IdPosition = 72,
                    IdLpu = "1.2.643.5.1.13.3.25.78.118",
                    Person = new PersonWithIdentity
                    {
                        Sex = 1,
                        Birthdate = new DateTime(1973, 01, 07),
                        IdPersonMis = "123123123",
                        Documents = new IdentityDocument[]
                        {
                            DocumentData.Passport,
                            DocumentData.SNILS
                        },
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            CaseStatData.caseStat = new CaseStat
            {
                OpenDate = new DateTime(2010, 10, 10),
                CloseDate = new DateTime(2010, 10, 14),
                HistoryNumber = "1000121",
                IdCaseMis = "123412341234",
                IdPaymentType = 1,
                Confidentiality = 1,
                DoctorConfidentiality = 1,
                CuratorConfidentiality = 1,
                IdLpu = "1.2.643.5.1.13.3.25.78.118",
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

                PrehospitalDefects = new byte[] { 1, 2 },

                Guardian = CaseStatData.guardian,
                DoctorInCharge = CaseStatData.doctorInCharge,
                Authenticator = CaseStatData.authenticator,
                Author = CaseStatData.author,
                LegalAuthenticator = CaseStatData.legalAuthenticator,
                Steps = new StepStat[] { CaseStatData.step },
                MedRecords = new MedRecord[] { MedRecordData.service, MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    MedRecordData.appointedMedication, MedRecordData.clinicMainDiagnosis, MedRecordData.consultNote, 
                    MedRecordData.deathInfo, MedRecordData.diagnosis, MedRecordData.dischargeSummary, MedRecordData.dispensaryOne, 
                    MedRecordData.LaboratoryReport, MedRecordData.service },

            };
        }

        [SetUp]
        public void SetUp()
        {
            PatientData.Patient = new PatientDto
            {
                FamilyName = "Жукин",
                GivenName = "Дмитрий",
                BirthDate = new DateTime(1983, 01, 07),
                Sex = 1,
                IdPatientMIS = "123456789010",
            };

            SetMedRecord();
            SetDocument();

            SetAmbCase();

            SetStatCase();

        }
    }
}
