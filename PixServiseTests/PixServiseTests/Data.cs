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
        // public static HumanName legalAuthenticatorName { get; set; }

        public static Guardian guardian { get; set; }

        public static StepAmb step { get; set; }
        public static MedicalStaff doctor { get; set; }
        public static PersonWithIdentity doctorPerson { get; set; }
        public static HumanName doctorName { get; set; }
    }

    public static class CaseStatData
    {
        public static CaseStat caseStat { get; set; }
    }

    [TestFixture]
    public abstract class Data
    {
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

            };

        }
    }
}
