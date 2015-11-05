using System;
using NUnit.Framework;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PixServiseTests.PixServise;
using PixServiseTests.EMKServise;
using System.Collections.Generic;


namespace PixServiseTests
{
    [TestFixture]
    public class AddCaseTest : Data
    {
        [Test]
        public void AddMinAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        [Test]
        public void AddMinAmbCaseWithTrueKey()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.TrueMedRecordDataWithKey
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinAmbCaseWithWrongKey()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.TrueMedRecordDataWithKey
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).MinCaseStatSet();
                caseStat.PrehospitalDefects = new List<byte>() { 1, 2 };
               
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
        private LaboratoryReport GetOnkomarkers()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 04),
                Attachment = Data.SetAttachment("Ифа.Онкомаркеры.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Онкомаркеры"
            };
        }
        private LaboratoryReport GetGemotology()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 02),
                Attachment = Data.SetAttachment("Гематология.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Гематология"
            };
        }
        private LaboratoryReport GetBlood()
        {
            return new LaboratoryReport()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 05),
                Attachment = Data.SetAttachment("Общийанализкрови.pdf", null, "application/pdf"),
                Header = "Лабораторное исследование Общий анализ крови"
            };
        }
        private DischargeSummary GetEpic()
        {
            return new DischargeSummary()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 03),
                Attachment = Data.SetAttachment("эпикриз.html", null, "text/html"),
                Header = "Эпикриз"
            };
        }

        private ConsultNote GetConsult()
        {
            return new ConsultNote()
            {
                Author = DoctorData.doctorInCharge,
                CreationDate = new DateTime(2014, 06, 08),
                Attachment = Data.SetAttachment("консультация.pdf", null, "application/pdf"),
                Header = "Консультация"
            };
        }
        [Test]
        public void _HashTest()
        {
            using (TestPixServiceClient c = new TestPixServiceClient())
            {
                PatientDto p = (new SetData()).PatientSet();
                c.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, p);
            }
            using (TestEmkServiceClient c = new TestEmkServiceClient())
            {
                CaseAmb p = (new SetData()).FullCaseAmbSet();
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                var x = GetConsult();
                x.Attachment.Hash = new byte[] { 1, 2, 3, 4, 5 };
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    //GetEpic(),
                    //GetConsult()
                    x
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers(),
                    MedRecordData.appointedMedication,
                    MedRecordData.service
                };
                c.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
                string caseMis = p.IdCaseMis;
                p = (new SetData()).FullCaseAmbSet();
                p.IdCaseMis = caseMis;
                p.Guardian = null;
                p.OpenDate = new DateTime(2014, 06, 01);
                p.CloseDate = new DateTime(2014, 06, 10);
                p.MedRecords = new List<MedRecord>
                {
                    MedRecordData.clinicMainDiagnosis,
                    //GetConsult()
                    x
                };
                p.Steps[0].DateStart = new DateTime(2014, 06, 01);
                p.Steps[0].DateEnd = new DateTime(2014, 06, 10);
                p.Steps[0].MedRecords = new List<MedRecord>
                {
                    GetGemotology(),
                    GetBlood(),
                    GetOnkomarkers()
                };
                c.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", p);
            }
            if (Global.errors == "")
                Assert.Fail("Кейс с левым хешом был добавлен");
            else
                Assert.Pass();
        }
        [Test]
        public void _AddCaseForMiac()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                patient.IdPatientMIS = "cc9d190e-7632-44a7-852f-f3fec14793ca";
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", "1.2.643.5.1.13.3.25.78.186", patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).FullCaseStatSet();
                caseStat.IdLpu = "1.2.643.5.1.13.3.25.78.186";
                caseStat.IdPatientMis = "cc9d190e-7632-44a7-852f-f3fec14793ca";
                caseStat.IdCaseMis = "3776";
                caseStat.OpenDate = new DateTime(2014, 12, 10);
                caseStat.CloseDate = new DateTime(2015, 12, 15);
                caseStat.HistoryNumber = "23030";
                caseStat.MedRecords = new List<MedRecord>
                {
                    MedRecordData.service,
                    MedRecordData.tfomsInfo,
                    MedRecordData.deathInfo,
                    MedRecordData.diagnosis,
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    MedRecordData.sickList,
                    MedRecordData.dischargeSummary,
                    MedRecordData.LaboratoryReport,
                    MedRecordData.consultNote
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
                caseStat.Steps = new List <StepStat>
                {(new SetData()).MinStepStatSet()};
                caseStat.Steps[0].MedRecords = new List<MedRecord>
                {
                    MedRecordData.appointedMedication
                };
                caseStat.DoctorInCharge = new MedicalStaff
                {
                    IdLpu = Data.idlpu,
                    IdSpeciality = 30,
                    IdPosition = 75,
                    Person = new PersonWithIdentity
                    {
                        IdPersonMis = "unknown",
                        Sex = 2,
                        Birthdate = new DateTime(1975, 02, 08),
                        Documents = new List<IdentityDocument>
                    {
                        DocumentData.SNILS,
                    },
                        HumanName = new HumanName
                        {
                            FamilyName = "unknown",
                            GivenName = "unknown",
                            MiddleName = "unknown",
                        },
                    }
                };
                caseStat.DoctorInCharge.Person.Documents[0].DocN = "11111111448";
                EmkClient.UpdateCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);

            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddMinDispCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseDispSet();
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullAmbCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).FullCaseAmbSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    set.MinClinicMainDiagnosis(),
                    MedRecordData.diagnosis,
                    set.MinRefferal(),
                    set.MinSickList(),
                    set.MinDischargeSummary(),
                    set.MinLaboratoryReport(),
                    set.MinConsultNote()
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    MedRecordData.referral,
                    set.MinLaboratoryReport(),
                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullStatCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseStat caseStat = (new SetData()).FullCaseStatSet();
                SetData set = new SetData();
                caseStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    MedRecordData.deathInfo,
                    set.MinDiagnosis(),
                    MedRecordData.anatomopathologicalClinicMainDiagnosis,
                    set.MinRefferal(),   
                    set.MinSickList(),
                    set.MinDischargeSummary(),
                    set.MinLaboratoryReport(),
                    set.MinConsultNote()
                };
                StepStat stepStat = (new SetData()).MinStepStatSet();
                stepStat.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    set.MinRefferal(),
                    set.MinLaboratoryReport(),
                };
                caseStat.Steps = new List<StepStat>
                {
                    stepStat
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseStat);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void AddFullDispCase()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).FullCaseDispSet();
                SetData set = new SetData();
                caseAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinTfomsInfo(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    set.MinDispensaryOne(),
                    set.MinLaboratoryReport(),
                };
                StepAmb stepAmb = (new SetData()).MinStepAmbSet();
                stepAmb.MedRecords = new List<MedRecord>
                {
                    set.MinService(),
                    set.MinAppointedMedication(),
                    set.MinDiagnosis(),
                    MedRecordData.clinicMainDiagnosis,
                    set.MinRefferal(),
                    set.MinLaboratoryReport(),
                };
                caseAmb.Steps = new List<StepAmb>
                {
                    stepAmb
                };
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void Test()
        {
            using (TestPixServiceClient PixClient = new TestPixServiceClient())
            {
                PatientDto patient = (new SetData()).PatientSet();
                PixClient.AddPatient("D500E893-166B-4724-9C78-D0DBE1F1C48D", Data.idlpu, patient);
            }
            using (TestEmkServiceClient EmkClient = new TestEmkServiceClient())
            {
                CaseAmb caseAmb = (new SetData()).MinCaseAmbSet();
                EmkClient.AddCase("D500E893-166B-4724-9C78-D0DBE1F1C48D", caseAmb);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
