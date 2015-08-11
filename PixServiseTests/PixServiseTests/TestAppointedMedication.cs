using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestAppointedMedication : TestMedRecord
    {
        AppointedMedication document;
        TestDoctor doctor;
        TestQuantity courseDose;
        TestQuantity dayDose;
        TestQuantity oneTimeDose;
        public TestAppointedMedication(AppointedMedication a, string idLpu = "")
        {
            if (a != null)
            {
                document = a;
                if (a.CourseDose != null)
                    courseDose = new TestQuantity(a.CourseDose);
                if (a.DayDose != null)
                    dayDose = new TestQuantity(a.DayDose);
                if (a.OneTimeDose != null)
                    oneTimeDose = new TestQuantity(a.OneTimeDose);
                doctor = new TestDoctor(a.Doctor, idLpu);
            }
        }
        static public List<TestAppointedMedication> BuildAppointedMedicationFromDataBaseDate(string idStep)
        {
            List<TestAppointedMedication> amList = new List<TestAppointedMedication>();
            if (idStep != "")
            {
                using (SqlConnection connection = Global.GetSqlConnection())
                {
                    string findAM = "SELECT * FROM PrescribedMedication, nsi.RAnatomicTherapeuticChemicalClassification WHERE PrescribedMedication.IdStep = '" + idStep + "' AND PrescribedMedication.IdRAnatomicTherapeuticChemicalClassification = nsi.RAnatomicTherapeuticChemicalClassification.IdRAnatomicTherapeuticChemicalClassification";
                    SqlCommand AMcommand = new SqlCommand(findAM, connection);
                    using (SqlDataReader AMReader = AMcommand.ExecuteReader())
                    {
                        while (AMReader.Read())
                        {
                            AppointedMedication a = new AppointedMedication();
                            if (AMReader["PrescriptionNo"].ToString() != "")
                                a.Number = AMReader["PrescriptionNo"].ToString();
                            if (AMReader["PrescriptionSeries"].ToString() != "")
                                a.Seria = AMReader["PrescriptionSeries"].ToString();
                            if (AMReader["IdRMedicineIssueType"].ToString() != "")
                                a.MedicineIssueType = Convert.ToString(AMReader["IdRMedicineIssueType"]);
                            if (AMReader["MedicineIssueTypeDate"].ToString() != "")
                                a.IssuedDate = Convert.ToDateTime(AMReader["MedicineIssueTypeDate"]);
                            if (AMReader["IdRMedicineType"].ToString() != "")
                                a.MedicineType = Convert.ToUInt16(AMReader["IdRMedicineType"]);
                            if (AMReader["code"].ToString() != "")
                                a.AnatomicTherapeuticChemicalClassification = Convert.ToString(AMReader["code"]);
                            if (AMReader["IdRMedicineUseWay"].ToString() != "")
                                a.MedicineUseWay = Convert.ToByte(AMReader["IdRMedicineUseWay"]);
                            if (AMReader["TreatmentDays"].ToString() != "")
                                a.DaysCount = Convert.ToUInt16(AMReader["TreatmentDays"]);
                            if (AMReader["MedecineName"].ToString() != "")
                                a.MedicineName = AMReader["MedecineName"].ToString();
                            TestAppointedMedication ta = new TestAppointedMedication(a);
                            if (AMReader["CourseDose"].ToString() != "")
                                ta.courseDose = TestQuantity.BuildQuantityFromDataBaseData(AMReader["CourseDose"].ToString());
                            else
                                ta.courseDose = null;
                            if (AMReader["DayDose"].ToString() != "")
                                ta.dayDose = TestQuantity.BuildQuantityFromDataBaseData(AMReader["DayDose"].ToString());
                            if (AMReader["SingleDose"].ToString() != "")
                                ta.oneTimeDose = TestQuantity.BuildQuantityFromDataBaseData(AMReader["SingleDose"].ToString());
                            if (AMReader["IdDoctor"].ToString() != "")
                                ta.doctor = TestDoctor.BuildTestDoctorFromDataBase(AMReader["IdDoctor"].ToString());
                            amList.Add(ta);
                        }
                    }
                }
            }
            if (amList.Count == 0)
                return null;
            else
                return amList;
        }
        private void FindMismatch(TestAppointedMedication ta)
        {
            if (this.document.AnatomicTherapeuticChemicalClassification != ta.document.AnatomicTherapeuticChemicalClassification)
                Global.errors3.Add("несовпадение AnatomicTherapeuticChemicalClassification TestAppointedMedication");
            if (Global.GetLength(this.courseDose) != Global.GetLength(ta.courseDose))
                Global.errors3.Add("несовпадение длины CourseDose TestAppointedMedication");
            if (Global.GetLength(this.dayDose) != Global.GetLength(ta.dayDose))
                Global.errors3.Add("несовпадение длины DayDose TestAppointedMedication");
            if (this.document.DaysCount != ta.document.DaysCount)
                Global.errors3.Add("несовпадение DaysCount TestAppointedMedication");
            if (this.document.IssuedDate != ta.document.IssuedDate)
                Global.errors3.Add("несовпадение IssuedDate TestAppointedMedication");
            if (this.document.MedicineIssueType != ta.document.MedicineIssueType)
                Global.errors3.Add("несовпадение MedicineIssueType TestAppointedMedication");
            if (this.document.MedicineName != ta.document.MedicineName)
                Global.errors3.Add("несовпадение MedicineName TestAppointedMedication");
            if (this.document.MedicineType != ta.document.MedicineType)
                Global.errors3.Add("несовпадение MedicineType TestAppointedMedication");
            if (this.document.MedicineUseWay != ta.document.MedicineUseWay)
                Global.errors3.Add("несовпадение MedicineUseWay TestAppointedMedication");
            if (this.document.Number != ta.document.Number)
                Global.errors3.Add("несовпадение Number TestAppointedMedication");
            if (Global.GetLength(this.oneTimeDose) != Global.GetLength(ta.oneTimeDose))
                Global.errors3.Add("несовпадение длины OneTimeDose TestAppointedMedication");
            if (this.document.Seria != ta.document.Seria)
                Global.errors3.Add("несовпадение Seria TestAppointedMedication");
            if (Global.GetLength(this.doctor) != Global.GetLength(ta.doctor))
                Global.errors3.Add("несовпадение длины doctor TestAppointedMedication");
        }
        public override bool Equals(Object obj)
        {
            TestAppointedMedication p = obj as TestAppointedMedication;
            if ((object)p == null)
            {
                return false;
            }
            if (this.document == p.document)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                return false;
            }
            if ((this.document.AnatomicTherapeuticChemicalClassification == p.document.AnatomicTherapeuticChemicalClassification)&&
            (Global.IsEqual(this.courseDose, p.courseDose)) &&
            (Global.IsEqual(this.dayDose, p.dayDose)) &&
            (this.document.DaysCount == p.document.DaysCount)&&
            (this.document.IssuedDate == p.document.IssuedDate)&&
            (this.document.MedicineIssueType == p.document.MedicineIssueType)&&
            (this.document.MedicineName == p.document.MedicineName)&&
            (this.document.MedicineType == p.document.MedicineType)&&
            (this.document.MedicineUseWay == p.document.MedicineUseWay)&&
            (this.document.Number == p.document.Number)&&
            (Global.IsEqual(this.oneTimeDose, p.oneTimeDose)) &&
            (this.document.Seria == p.document.Seria)&&
            Global.IsEqual(this.doctor, p.doctor))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestAppointedMedication");
                return false;
            }
        }
        public static bool operator ==(TestAppointedMedication a, TestAppointedMedication b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAppointedMedication a, TestAppointedMedication b)
        {
            return !(a.Equals(b));
        }
    }
}
