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

        public CaseAmb FullCaseAmbSet()
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
    }
}
