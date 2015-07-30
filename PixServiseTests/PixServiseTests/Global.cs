using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixServiseTests
{
    sealed class Global
    {
        static public ArrayList errors3 = new ArrayList();
        static public ArrayList errors2 = new ArrayList();
        static public ArrayList errors1 = new ArrayList();
        static public string errors
        {
            get
            {
                return ErrorsToString();
            }
        }
        static private string _connectionPath =
            "Data Source=192.168.8.93;Initial Catalog=EMKDBv3;User ID=a.pihtin;Password=stest2";
        static public SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionPath);
            connection.Open();
            return connection;
        }
        static public bool EqualsArrayLists(Array a, Array b)
        {
            if (a.Length == b.Length)
            {
                if (a.Length == 0)
                    return true;
                bool mark = true;
                foreach (var i in a)
                {
                    bool mark1 = false;
                    foreach (var i1 in b)
                    {
                        if (i.Equals(i1))
                            mark1 = true;
                    }
                    if (!mark1)
                        mark = false;
                }
                return mark;
            }
            else
                return false;
        }
        static public bool IsEqual(object a, object b)
        {
            errors2.AddRange(errors3);
            errors3.Clear();
            if ((a == null) && (b == null))
            {
                errors3.Clear();
                return true;
            }
            if ((a != null) && (b != null))
            {
                Array arrayA = a as Array;
                Array arrayB = b as Array;
                if ((arrayA != null) && (arrayB != null))
                    if (EqualsArrayLists(arrayA, arrayB))
                    {
                        errors3.Clear();
                        return true;
                    }
                    else
                    {
                        errors2.AddRange(errors3);
                        errors3.Clear();
                        return false;
                    }
                if ((arrayA == null) && (arrayB == null))
                    if (a.Equals(b))
                    {
                        errors3.Clear();
                        return true;
                    }
                    else
                    {
                        errors2.AddRange(errors3);
                        errors3.Clear();
                        return false;
                    }
            }
            errors2.AddRange(errors3);
            errors3.Clear();
            return false;
        }
        static private string ErrorsToString()
        {
            string errors = "";
            for (int i = 0; i < errors1.Count; i++)
            {
                errors += (i + 1).ToString() + ". " + errors1[i].ToString() + "\n";
            }
            return errors;
        }
    }
}
