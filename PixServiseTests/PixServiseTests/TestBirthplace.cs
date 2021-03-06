﻿using PixServiseTests.PixServise;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestBirthplace
    {
        public BirthPlaceDto birthplace;

        public TestBirthplace(BirthPlaceDto b)
        {
            birthplace = b;
        }

        static public TestBirthplace BuildBirthplaceFromDataBaseData(string idPerson)
        {
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                string findIdAdresses = "SELECT TOP(1) * FROM mm_Person2Address WHERE IdAddress = (SELECT MAX(IdAddress) FROM mm_Person2Address WHERE IdPerson = '" + idPerson + "' AND IdAddressType = '" + 3 + "')";
                SqlCommand IdAddresses = new SqlCommand(findIdAdresses, connection);
                using (SqlDataReader addressesIdReader = IdAddresses.ExecuteReader())
                {
                    while (addressesIdReader.Read())
                    {
                        string id = (Convert.ToString(addressesIdReader["IdAddress"]));
                        using (SqlConnection connection2 = Global.GetSqlConnection())
                        {
                            string findIdAdresses2 = "SELECT TOP(1) * FROM Address WHERE IdAddress = '" + id + "'";
                            SqlCommand IdAddresses2 = new SqlCommand(findIdAdresses2, connection2);
                            using (SqlDataReader addressesReader = IdAddresses2.ExecuteReader())
                            {
                                BirthPlaceDto birthp = new BirthPlaceDto();
                                while (addressesReader.Read())
                                {
                                    if (addressesReader["Country"].ToString() != "")
                                        birthp.Country = Convert.ToString(addressesReader["Country"]);
                                    if (addressesReader["Region"].ToString() != "")
                                        birthp.Region = Convert.ToString(addressesReader["Region"]);
                                    if (addressesReader["CityName"].ToString() != "")
                                        birthp.City = Convert.ToString(addressesReader["CityName"]);
                                }
                                return (new TestBirthplace(birthp));
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestBirthplace b)
        {
            if (this.birthplace.City != b.birthplace.City)
                Global.errors3.Add("Несовпадение City TestBirthplace");
            if (this.birthplace.Country != b.birthplace.Country)
                Global.errors3.Add("Несовпадение Country TestBirthplace");
            if (this.birthplace.Region != b.birthplace.Region)
                Global.errors3.Add("Несовпадение Region TestBirthplace");
        }

        public override bool Equals(Object obj)
        {
            TestBirthplace p = obj as TestBirthplace;
            if ((object)p == null)
            {
                return false;
            }
            if (this.birthplace == p.birthplace)
                return true;
            if ((this.birthplace == null) || (p.birthplace == null))
            {
                return false;
            }
            if ((this.birthplace.City == p.birthplace.City) &&
                (this.birthplace.Country == p.birthplace.Country) &&
                (this.birthplace.Region == p.birthplace.Region))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestBirthPlace");
                return false;
            }
        }
        public static bool operator ==(TestBirthplace a, TestBirthplace b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestBirthplace a, TestBirthplace b)
        {
            return !(a.Equals(b));
        }

    }
}
