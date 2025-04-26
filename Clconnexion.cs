using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace TP_4476_
{
    class Clconnexion
    {
        private string connectionString = "Data Source=DESKTOP-VRP1554;Initial Catalog=Pharmacie;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True";

        public SqlConnection GetSConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}

