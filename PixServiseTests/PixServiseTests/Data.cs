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

        public static MedRecord medRecord { get; set; }

        public static Service service { get; set; }
        public static TfomsInfo tfomsInfo { get; set; }
        public static AppointedMedication appointedMedication { get; set; }
        public static DeathInfo deathInfo { get; set; }
        public static Diagnosis diagnosis { get; set; }
        public static ClinicMainDiagnosis clinicMainDiagnosis { get; set; }
        public static MedDocument medDocument { get; set; }

        public static AnatomopathologicalClinicMainDiagnosis anatomopathologicalClinicMainDiagnosis { get; set; }
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
                    // Documents ещё добавить 
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
                        // Documents ещё добавить 
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
                        // Documents ещё добавить 
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
                        // Documents ещё добавить  
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
                    // Documents ещё добавить 
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
                        //Documents
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            //??
            CaseAmbData.medRecord = new MedRecord
            {

            };

            CaseAmbData.service = new Service
            {
                DateStart = new DateTime(2010, 11, 1),
                DateEnd = new DateTime(2010, 11, 10),
                IdServiceType = "1088",
                ServiceName = "Название услуги",
                Performer = new Participant
                {
                    IdRole = 3,
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
                            //Documents
                            HumanName = new HumanName
                            {
                                FamilyName = "Лукин",
                                GivenName = "Василий",
                                MiddleName = "Андреевич",
                            },
                        }
                    }
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

            CaseAmbData.tfomsInfo = new TfomsInfo
            {
                IdTfomsType = 211010,
                Count = 1,
                Tariff = 100,
            };

            CaseAmbData.appointedMedication = new AppointedMedication
            {
                AnatomicTherapeuticChemicalClassification = "1",
                DaysCount = 5,
                IssuedDate = new DateTime(2010, 03, 06),
                MedicineIssueType = 1, // по справочнику строка PRE или CURE 
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
                        //Documents
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            CaseAmbData.deathInfo = new DeathInfo
            {
                MkbCode = "11730",
            };

            CaseAmbData.diagnosis = new Diagnosis
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
                        //Documents
                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        },
                    }
                }
            };

            CaseAmbData.clinicMainDiagnosis = new ClinicMainDiagnosis
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
                        //Documents
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
                                //Documents
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

            CaseAmbData.anatomopathologicalClinicMainDiagnosis = new AnatomopathologicalClinicMainDiagnosis
            {

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
                MedRecords = new MedRecord[] { CaseAmbData.service },

            };
        }

        //в разработке
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
                    // Documents ещё добавить 
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
                        // Documents ещё добавить 
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
                        // Documents ещё добавить 
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
                        // Documents ещё добавить   
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
                    // Documents ещё добавить  
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
                        //Documents

                        HumanName = new HumanName
                        {
                            FamilyName = "Лукин",
                            GivenName = "Василий",
                            MiddleName = "Андреевич",
                        }
                    }
                }
            };

            //??
            CaseStatData.medRecord = new MedRecord
            {

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

                //PrehospitalDefects ???
                PrehospitalDefects = new byte[] { 1, 2 },

                Guardian = CaseStatData.guardian,
                DoctorInCharge = CaseStatData.doctorInCharge,
                Authenticator = CaseStatData.authenticator,
                Author = CaseStatData.author,
                LegalAuthenticator = CaseStatData.legalAuthenticator,
                Steps = new StepStat[] { CaseStatData.step },
                MedRecords = new MedRecord[] { CaseStatData.medRecord },

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

            SetAmbCase();

            SetStatCase();

        }
    }
}
