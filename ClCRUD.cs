using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TP_4476_
{
    class ClCRUD
    {
        private Clconnexion con = new Clconnexion();

        // CREATE
        public void Insert(string table, Dictionary<string, object> data)
        {
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string columns = string.Join(", ", data.Keys);
                string parameters = string.Join(", ", data.Keys.Select(k => "@" + k));
                string query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (var pair in data)
                    {
                        cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // READ
        public DataTable Select(string query)
        {
            using (SqlConnection connection = con.GetSConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // UPDATE
        public void Update(string table, Dictionary<string, object> data, string condition)
        {
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));
                string query = $"UPDATE {table} SET {setClause} WHERE {condition}";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (var pair in data)
                    {
                        cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE
        public void Delete(string table, string condition)
        {
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string query = $"DELETE FROM {table} WHERE {condition}";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public int InsertAndGetId(string table, Dictionary<string, object> data, SqlTransaction transaction)
        {
            string columns = string.Join(", ", data.Keys);
            string parameters = string.Join(", ", data.Keys.Select(k => "@" + k));
            string query = $"INSERT INTO {table} ({columns}) VALUES ({parameters}); SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, transaction.Connection, transaction))
            {
                foreach (var pair in data)
                {
                    cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                }
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public void InsertWithTransaction(string table, Dictionary<string, object> data, SqlTransaction transaction)
        {
            string columns = string.Join(", ", data.Keys);
            string parameters = string.Join(", ", data.Keys.Select(k => "@" + k));
            string query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";

            using (SqlCommand cmd = new SqlCommand(query, transaction.Connection, transaction))
            {
                foreach (var pair in data)
                {
                    cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                }
                cmd.ExecuteNonQuery();
            }
        }




    }
}


