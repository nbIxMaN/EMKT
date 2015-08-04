﻿using System;
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
            patient.BirthDate = PatientData.Patient.BirthDate;

            return patient;
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

            caseAmb.DoctorInCharge = new MedicalStaff
            {
                IdSpeciality = CaseAmbData.doctorInCharge.IdSpeciality,
                IdPosition = CaseAmbData.doctorInCharge.IdPosition,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = CaseAmbData.doctorInCharge.Person.IdPersonMis,
                    HumanName = new HumanName
                    {
                        FamilyName = CaseAmbData.doctorInCharge.Person.HumanName.FamilyName,
                        GivenName = CaseAmbData.doctorInCharge.Person.HumanName.GivenName,
                    }
                }
            };

            caseAmb.Author = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = CaseAmbData.author.Doctor.IdSpeciality,
                    IdPosition = CaseAmbData.author.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseAmbData.author.Doctor.Person.IdPersonMis,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseAmbData.author.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseAmbData.author.Doctor.Person.HumanName.GivenName,
                        }
                    }
                }
            };

            caseAmb.Authenticator = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = CaseAmbData.authenticator.Doctor.IdSpeciality,
                    IdPosition = CaseAmbData.authenticator.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseAmbData.authenticator.Doctor.Person.IdPersonMis,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseAmbData.authenticator.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseAmbData.authenticator.Doctor.Person.HumanName.GivenName,
                        }
                    }
                }
            };

            caseAmb.Steps = new StepAmb[]
            {
               new StepAmb
               {
                    DateStart = CaseAmbData.step.DateStart,
                    DateEnd = CaseAmbData.step.DateEnd,
                    IdStepMis = CaseAmbData.step.IdStepMis,
                    IdPaymentType = CaseAmbData.step.IdPaymentType,
                    IdVisitPlace = CaseAmbData.step.IdVisitPlace,
                    IdVisitPurpose = CaseAmbData.step.IdVisitPurpose,
                    Doctor = new MedicalStaff
                    {
                        IdSpeciality = CaseAmbData.step.Doctor.IdSpeciality,
                        IdPosition = CaseAmbData.step.Doctor.IdPosition,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = CaseAmbData.step.Doctor.Person.IdPersonMis,
                            HumanName = new HumanName
                            {
                                FamilyName = CaseAmbData.step.Doctor.Person.HumanName.FamilyName,
                                GivenName = CaseAmbData.step.Doctor.Person.HumanName.GivenName,
                            }
                        }
                    }
                },
            };

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


            caseStat.DoctorInCharge = new MedicalStaff
            {
                IdSpeciality = CaseStatData.doctorInCharge.IdSpeciality,
                IdPosition = CaseStatData.doctorInCharge.IdPosition,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = CaseStatData.doctorInCharge.Person.IdPersonMis,
                    HumanName = new HumanName
                    {
                        FamilyName = CaseStatData.doctorInCharge.Person.HumanName.FamilyName,
                        GivenName = CaseStatData.doctorInCharge.Person.HumanName.GivenName,
                    }
                }
            };

            caseStat.Author = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = CaseStatData.author.Doctor.IdSpeciality,
                    IdPosition = CaseStatData.author.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseStatData.author.Doctor.Person.IdPersonMis,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseStatData.author.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseStatData.author.Doctor.Person.HumanName.GivenName,
                        }
                    }
                }
            };

            caseStat.Authenticator = new Participant
            {
                Doctor = new MedicalStaff
                {
                    IdSpeciality = CaseStatData.authenticator.Doctor.IdSpeciality,
                    IdPosition = CaseStatData.authenticator.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseStatData.authenticator.Doctor.Person.IdPersonMis,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseStatData.authenticator.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseStatData.authenticator.Doctor.Person.HumanName.GivenName,
                        }
                    }
                }
            };

            caseStat.Steps = new StepStat[]
            {
               new StepStat
               {
                    DateStart = CaseStatData.step.DateStart,
                    DateEnd = CaseStatData.step.DateEnd,
                    IdStepMis = CaseStatData.step.IdStepMis,
                    IdPaymentType = CaseStatData.step.IdPaymentType,
                    HospitalDepartmentName = CaseStatData.step.HospitalDepartmentName,
                    IdHospitalDepartment = CaseStatData.step.IdHospitalDepartment,
                     BedProfile = CaseStatData.step.BedProfile,
                      DaySpend = CaseStatData.step.DaySpend,

                    Doctor = new MedicalStaff
                    {
                        IdSpeciality = CaseStatData.step.Doctor.IdSpeciality,
                        IdPosition = CaseStatData.step.Doctor.IdPosition,
                        Person = new PersonWithIdentity
                        {
                            IdPersonMis = CaseStatData.step.Doctor.Person.IdPersonMis,
                            HumanName = new HumanName
                            {
                                FamilyName = CaseStatData.step.Doctor.Person.HumanName.FamilyName,
                                GivenName = CaseStatData.step.Doctor.Person.HumanName.GivenName,
                            }
                        }
                    }
                },
            };


            return caseStat;
        }

        /// в разработке
        public CaseAmb FullCaseAmbSet()
        {
            CaseAmb caseAmb = new CaseAmb();
           /* caseAmb.OpenDate = CaseAmbData.caseAmb.OpenDate;
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

            caseAmb.IdCaseAidType = CaseAmbData.caseAmb.IdCaseAidType;
            caseAmb.IdCasePurpose = CaseAmbData.caseAmb.IdCasePurpose;
            caseAmb.IsActive = CaseAmbData.caseAmb.IsActive;
            caseAmb.IdAmbResult = CaseAmbData.caseAmb.IdAmbResult;
            caseAmb.MedRecords = CaseAmbData.caseAmb.MedRecords;

            caseAmb.DoctorInCharge = new MedicalStaff
            {
                IdLpu = CaseAmbData.doctorInCharge.IdLpu,
                IdSpeciality = CaseAmbData.doctorInCharge.IdSpeciality,
                IdPosition = CaseAmbData.doctorInCharge.IdPosition,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = CaseAmbData.doctorInCharge.Person.IdPersonMis,
                    Sex = CaseAmbData.doctorInCharge.Person.Sex,
                    Birthdate = CaseAmbData.doctorInCharge.Person.Birthdate,
                    Documents = CaseAmbData.doctorInCharge.Person.Documents,
                    HumanName = new HumanName
                    {
                        FamilyName = CaseAmbData.doctorInCharge.Person.HumanName.FamilyName,
                        GivenName = CaseAmbData.doctorInCharge.Person.HumanName.GivenName,
                        MiddleName = CaseAmbData.doctorInCharge.Person.HumanName.MiddleName,
                    }
                }
            };

            caseAmb.Author = new Participant
            {
                IdRole = CaseAmbData.author.IdRole,
                Doctor = new MedicalStaff
                {
                    IdLpu = CaseAmbData.author.Doctor.IdLpu,
                    IdSpeciality = CaseAmbData.author.Doctor.IdSpeciality,
                    IdPosition = CaseAmbData.author.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseAmbData.author.Doctor.Person.IdPersonMis,
                        Sex = CaseAmbData.author.Doctor.Person.Sex,
                        Birthdate = CaseAmbData.author.Doctor.Person.Birthdate,
                        Documents = CaseAmbData.author.Doctor.Person.Documents,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseAmbData.author.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseAmbData.author.Doctor.Person.HumanName.GivenName,
                            MiddleName = CaseAmbData.author.Doctor.Person.HumanName.MiddleName,
                        }
                    }
                }
            };

            caseAmb.Authenticator = new Participant
            {
                IdRole = CaseAmbData.authenticator.IdRole,
                Doctor = new MedicalStaff
                {
                    IdLpu = CaseAmbData.authenticator.Doctor.IdLpu,
                    IdSpeciality = CaseAmbData.authenticator.Doctor.IdSpeciality,
                    IdPosition = CaseAmbData.authenticator.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseAmbData.authenticator.Doctor.Person.IdPersonMis,
                        Sex = CaseAmbData.authenticator.Doctor.Person.Sex,
                        Birthdate = CaseAmbData.authenticator.Doctor.Person.Birthdate,
                        Documents = CaseAmbData.authenticator.Doctor.Person.Documents,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseAmbData.authenticator.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseAmbData.authenticator.Doctor.Person.HumanName.GivenName,
                            MiddleName = CaseAmbData.authenticator.Doctor.Person.HumanName.MiddleName,
                        }
                    }
                }
            };

            caseAmb.LegalAuthenticator = new Participant
            {
                IdRole = CaseAmbData.legalAuthenticator.IdRole,
                Doctor = new MedicalStaff
                {
                    IdLpu = CaseAmbData.legalAuthenticator.Doctor.IdLpu,
                    IdSpeciality = CaseAmbData.legalAuthenticator.Doctor.IdSpeciality,
                    IdPosition = CaseAmbData.legalAuthenticator.Doctor.IdPosition,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = CaseAmbData.legalAuthenticator.Doctor.Person.IdPersonMis,
                        Sex = CaseAmbData.legalAuthenticator.Doctor.Person.Sex,
                        Birthdate = CaseAmbData.legalAuthenticator.Doctor.Person.Birthdate,
                        Documents = CaseAmbData.legalAuthenticator.Doctor.Person.Documents,
                        HumanName = new HumanName
                        {
                            FamilyName = CaseAmbData.legalAuthenticator.Doctor.Person.HumanName.FamilyName,
                            GivenName = CaseAmbData.legalAuthenticator.Doctor.Person.HumanName.GivenName,
                            MiddleName = CaseAmbData.legalAuthenticator.Doctor.Person.HumanName.MiddleName,
                        }
                    }
                }
            };

            caseAmb.Guardian = new Guardian
            {
                IdRelationType = CaseAmbData.guardian.IdRelationType,
                Person = new PersonWithIdentity
                {
                    IdPersonMis = CaseAmbData.guardian.Person.IdPersonMis,
                    Sex = CaseAmbData.guardian.Person.Sex,
                    Birthdate = CaseAmbData.guardian.Person.Birthdate,
                    Documents = CaseAmbData.guardian.Person.Documents,
                    HumanName = new HumanName
                    {
                        FamilyName = CaseAmbData.guardian.Person.HumanName.FamilyName,
                        GivenName = CaseAmbData.guardian.Person.HumanName.GivenName,
                        MiddleName = CaseAmbData.guardian.Person.HumanName.MiddleName,
                    }
                },
                UnderlyingDocument = CaseAmbData.guardian.UnderlyingDocument,
            };

            caseAmb.Steps = new StepAmb[]
            {
               new StepAmb
               {
                    DateStart = CaseAmbData.step.DateStart,
                    DateEnd = CaseAmbData.step.DateEnd,
                    IdStepMis = CaseAmbData.step.IdStepMis,
                    IdPaymentType = CaseAmbData.step.IdPaymentType,
                    IdVisitPlace = CaseAmbData.step.IdVisitPlace,
                    IdVisitPurpose = CaseAmbData.step.IdVisitPurpose,

                    Comment = CaseAmbData.step.Comment,
                    MedRecords = CaseAmbData.step.MedRecords,

                    Doctor = new MedicalStaff
                    {
                         IdLpu = CaseAmbData.step.Doctor.IdLpu,
                        IdSpeciality = CaseAmbData.step.Doctor.IdSpeciality,
                        IdPosition = CaseAmbData.step.Doctor.IdPosition,
                        Person = new PersonWithIdentity
                        {
                            Sex = CaseAmbData.step.Doctor.Person.Sex,
                            Birthdate = CaseAmbData.step.Doctor.Person.Birthdate,
                            Documents = CaseAmbData.step.Doctor.Person.Documents,
                            IdPersonMis = CaseAmbData.step.Doctor.Person.IdPersonMis,
                            HumanName = new HumanName
                            {
                                FamilyName = CaseAmbData.step.Doctor.Person.HumanName.FamilyName,
                                GivenName = CaseAmbData.step.Doctor.Person.HumanName.GivenName,
                                 MiddleName = CaseAmbData.step.Doctor.Person.HumanName.MiddleName,
                            }
                        }
                    }
                },
            };*/
            caseAmb = CaseAmbData.caseAmb;

            return caseAmb;
        }
    }
}
