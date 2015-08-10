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
        public TestAppointedMedication(AppointedMedication a, string idLpu = "")
        {
            if (a != null)
            {
                document = a;
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
                    string findAM = "SELECT * FROM PrescribedMedication, RAnatomicTherapeuticChemicalClassification WHERE PrescribedMedication.IdStep = '" + idStep + "' AND PrescribedMedication.IdAnatomicTherapeuticChemicalClassification = RAnatomicTherapeuticChemicalClassification.IdAnatomicTherapeuticChemicalClassification";
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
                            if (AMReader["CourseDose"].ToString() != "")
                            {
                                int id = Convert.ToInt32(AMReader["CourseDose"]);
                                using (SqlConnection connection2 = Global.GetSqlConnection())
                                {
                                    string findDose = "SELECT * FROM MedicationQuantity WHERE IdMedicationQuantity = '" + id + "'";
                                    SqlCommand DoseCommand = new SqlCommand(findDose, connection2);
                                    using (SqlDataReader DoseReader = DoseCommand.ExecuteReader())
                                    {
                                        while (DoseReader.Read())
                                        {
                                            a.CourseDose = new Quantity();
                                            if (DoseReader["Quantity"].ToString() != "")
                                                a.CourseDose.Value = Convert.ToDecimal(DoseReader["Quantity"]);
                                            if (DoseReader["IdUnitClassifier"].ToString() != "")
                                                a.CourseDose.IdUnit = Convert.ToInt32(DoseReader["IdUnitClassifier"]);
                                        }
                                    }
                                }
                            }
                            if (AMReader["DayDose"].ToString() != "")
                            {
                                int id = Convert.ToInt32(AMReader["DayDose"]);
                                using (SqlConnection connection2 = Global.GetSqlConnection())
                                {
                                    string findDose = "SELECT * FROM MedicationQuantity WHERE IdMedicationQuantity = '" + id + "'";
                                    SqlCommand DoseCommand = new SqlCommand(findDose, connection2);
                                    using (SqlDataReader DoseReader = DoseCommand.ExecuteReader())
                                    {
                                        while (DoseReader.Read())
                                        {
                                            a.DayDose = new Quantity();
                                            if (DoseReader["Quantity"].ToString() != "")
                                                a.DayDose.Value = Convert.ToDecimal(DoseReader["Quantity"]);
                                            if (DoseReader["IdUnitClassifier"].ToString() != "")
                                                a.DayDose.IdUnit = Convert.ToInt32(DoseReader["IdUnitClassifier"]);
                                        }
                                    }
                                }
                            }
                            if (AMReader["SingleDose"].ToString() != "")
                            {
                                int id = Convert.ToInt32(AMReader["SingleDose"]);
                                using (SqlConnection connection2 = Global.GetSqlConnection())
                                {
                                    string findDose = "SELECT * FROM MedicationQuantity WHERE IdMedicationQuantity = '" + id + "'";
                                    SqlCommand DoseCommand = new SqlCommand(findDose, connection2);
                                    using (SqlDataReader DoseReader = DoseCommand.ExecuteReader())
                                    {
                                        while (DoseReader.Read())
                                        {
                                            a.OneTimeDose = new Quantity();
                                            if (DoseReader["Quantity"].ToString() != "")
                                                a.OneTimeDose.Value = Convert.ToDecimal(DoseReader["Quantity"]);
                                            if (DoseReader["IdUnitClassifier"].ToString() != "")
                                                a.OneTimeDose.IdUnit = Convert.ToInt32(DoseReader["IdUnitClassifier"]);
                                        }
                                    }
                                }
                            }
                            TestAppointedMedication ta = new TestAppointedMedication(a);
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
        public void FindMismatch(TestAppointedMedication ta)
        {
            if (this.document.AnatomicTherapeuticChemicalClassification != ta.document.AnatomicTherapeuticChemicalClassification)
                Global.errors3.Add("несовпадение AnatomicTherapeuticChemicalClassification TestAppointedMedication");
            if (this.document.CourseDose.IdUnit != ta.document.CourseDose.IdUnit)
                Global.errors3.Add("несовпадение CourseDose.IdUnit TestAppointedMedication");
            if (this.document.CourseDose.Value != ta.document.CourseDose.Value)
                Global.errors3.Add("несовпадение CourseDose.Value TestAppointedMedication");
            if (this.document.DayDose.IdUnit != ta.document.DayDose.IdUnit)
                Global.errors3.Add("несовпадение DayDose.IdUnit TestAppointedMedication");
            if (this.document.DayDose.Value != ta.document.DayDose.Value)
                Global.errors3.Add("несовпадение DayDose.Value TestAppointedMedication");
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
            if (this.document.OneTimeDose.IdUnit != ta.document.OneTimeDose.IdUnit)
                Global.errors3.Add("несовпадение OneTimeDose.IdUnit TestAppointedMedication");
            if (this.document.OneTimeDose.Value != ta.document.OneTimeDose.Value)
                Global.errors3.Add("несовпадение OneTimeDose.Value TestAppointedMedication");
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
            (this.document.CourseDose.IdUnit == p.document.CourseDose.IdUnit) &&
            (this.document.CourseDose.Value == p.document.CourseDose.Value) &&
            (this.document.DayDose.IdUnit == p.document.DayDose.IdUnit) &&
            (this.document.DayDose.Value == p.document.DayDose.Value) &&
            (this.document.DaysCount == p.document.DaysCount)&&
            (this.document.IssuedDate == p.document.IssuedDate)&&
            (this.document.MedicineIssueType == p.document.MedicineIssueType)&&
            (this.document.MedicineName == p.document.MedicineName)&&
            (this.document.MedicineType == p.document.MedicineType)&&
            (this.document.MedicineUseWay == p.document.MedicineUseWay)&&
            (this.document.Number == p.document.Number)&&
            (this.document.OneTimeDose.IdUnit == p.document.OneTimeDose.IdUnit) &&
            (this.document.OneTimeDose.Value == p.document.OneTimeDose.Value) &&
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
