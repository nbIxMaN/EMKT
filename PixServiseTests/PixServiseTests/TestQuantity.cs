using PixServiseTests.EMKServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestQuantity
    {
        Quantity quantity;
        public TestQuantity(Quantity a)
        {
            if (a != null)
                quantity = a;
        }
        static public TestQuantity BuildQuantityFromDataBaseData(string idQuantity)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findDose = "SELECT * FROM MedicationQuantity WHERE IdMedicationQuantity = '" + idQuantity + "'";
                SqlCommand DoseCommand = new SqlCommand(findDose, connection);
                using (SqlDataReader DoseReader = DoseCommand.ExecuteReader())
                {
                    while (DoseReader.Read())
                    {
                        Quantity q = new Quantity();
                        if (DoseReader["Quantity"].ToString() != "")
                            q.Value = Convert.ToDecimal(DoseReader["Quantity"]);
                        if (DoseReader["IdUnitClassifier"].ToString() != "")
                            q.IdUnit = Convert.ToInt32(DoseReader["IdUnitClassifier"]);
                        return (new TestQuantity(q));
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestQuantity q)
        {
            if (this.quantity.IdUnit != q.quantity.IdUnit)
                Global.errors3.Add("Несовпадение IdUnit TestQuantity");
            if (this.quantity.Value != q.quantity.Value)
                Global.errors3.Add("Несовпадение Value TestQuantity");
        }
        public override bool Equals(Object obj)
        {
            TestQuantity p = obj as TestQuantity;
            if ((object)p == null)
            {
                return false;
            }
            if (this.quantity == p.quantity)
                return true;
            if ((this.quantity == null) || (p.quantity == null))
            {
                return false;
            }
            if ((this.quantity.IdUnit == p.quantity.IdUnit)&&
                (this.quantity.Value == p.quantity.Value))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestQuantity");
                return false;
            }
        }
        public static bool operator ==(TestQuantity a, TestQuantity b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestQuantity a, TestQuantity b)
        {
            return !(a.Equals(b));
        }
    }
}
