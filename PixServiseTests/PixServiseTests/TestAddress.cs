﻿using PixServiseTests.PixServise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    class TestAddress
    {
        enum AddressId : int
        {
            a = 1,
            b = 2,
            d = 4,
            e = 5,
            f = 6,
            g = 7,
            h = 8,
            i = 9
        };
        public AddressDto address;

        public TestAddress(AddressDto a)
        {
            address = a;
        }

        static public List<TestAddress> BuildAdressesFromDataBaseData(string idPerson)
        {
            List<TestAddress> addresses = new List<TestAddress>();
            using (SqlConnection connection = Global.GetSqlConnection())
            {
                foreach (int i in Enum.GetValues(typeof(AddressId)))
                {
                    string findIdAdresses = "SELECT TOP(1) * FROM mm_Person2Address WHERE IdAddress = (SELECT MAX(IdAddress) FROM mm_Person2Address WHERE IdPerson = '" + idPerson + "' AND IdAddressType = '" + i + "')";
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
                                    while (addressesReader.Read())
                                    {
                                        AddressDto address = new AddressDto();
                                        if (addressesIdReader["IdAddressType"].ToString() != "")
                                            address.IdAddressType = Convert.ToByte(addressesIdReader["IdAddressType"]);
                                        if (addressesReader["StringAddress"].ToString() != "")
                                            address.StringAddress = Convert.ToString(addressesReader["StringAddress"]);
                                        if (addressesReader["IdKladrCity"].ToString() != "")
                                            address.City = Convert.ToString(addressesReader["IdKladrCity"]);
                                        if (addressesReader["IdKladrStreet"].ToString() != "")
                                            address.Street = Convert.ToString(addressesReader["IdKladrStreet"]);
                                        if (addressesReader["Building"].ToString() != "")
                                            address.Building = Convert.ToString(addressesReader["Building"]);
                                        if (addressesReader["Appartment"].ToString() != "")
                                            address.Appartment = Convert.ToString(addressesReader["Appartment"]);
                                        if (addressesReader["PostalCode"].ToString() != "")
                                            address.PostalCode = Convert.ToInt32(addressesReader["PostalCode"]);
                                        if (addressesReader["GeoData"].ToString() != "")
                                            address.GeoData = Convert.ToString(addressesReader["GeoData"]);
                                        TestAddress a = new TestAddress(address);
                                        addresses.Add(a);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (addresses.Count != 0)
                return addresses;
            else
                return null;
        }

        private void FindMismatch(TestAddress b)
        {
            if (this.address.Appartment != b.address.Appartment)
                Global.errors3.Add("Несовпадение Appartment TestAddress");
            if (this.address.Building != b.address.Building)
                Global.errors3.Add("Несовпадение Building TestAddress");
            if (this.address.City != b.address.City)
                Global.errors3.Add("Несовпадение City TestAddress");
            if (this.address.GeoData != b.address.GeoData)
                Global.errors3.Add("Несовпадение GeoData TestAddress");
            if (this.address.IdAddressType != b.address.IdAddressType)
                Global.errors3.Add("Несовпадение IdAddressType TestAddress");
            if (this.address.PostalCode != b.address.PostalCode)
                Global.errors3.Add("Несовпадение PostalCode TestAddress");
            if (this.address.Street != b.address.Street)
                Global.errors3.Add("Несовпадение Street TestAddress");
            if (this.address.StringAddress != b.address.StringAddress)
                Global.errors3.Add("Несовпадение StringAddress TestAddress");
        }

        public override bool Equals(Object obj)
        {
            TestAddress p = obj as TestAddress;
            if ((object)p == null)
            {
                return false;
            }
            if (this.address == p.address)
                return true;
            if ((this.address == null) || (p.address == null))
            {
                return false;
            }
            if ((this.address.Appartment == p.address.Appartment) &&
                (this.address.Building == p.address.Building) &&
                (this.address.City == p.address.City) &&
                (this.address.GeoData == p.address.GeoData) &&
                (this.address.IdAddressType == p.address.IdAddressType) &&
                (this.address.PostalCode == p.address.PostalCode) &&
                (this.address.Street == p.address.Street) &&
                (this.address.StringAddress == p.address.StringAddress))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestAddress");
                return false;
            }
        }
        public static bool operator ==(TestAddress a, TestAddress b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAddress a, TestAddress b)
        {
            return !(a.Equals(b));
        }

    }
}
