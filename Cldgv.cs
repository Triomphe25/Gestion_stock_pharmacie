using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_4476_
{
    class Cldgv
    {
        private SqlConnection con;
        public Cldgv(SqlConnection connexion)
        {
            con = connexion;
        }

        public void remplirDatagrid (string tablenom, DataGridView Dgv)
        {
            con.Open ();
            string query = $"SELECT * FROM {tablenom}";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder= new SqlCommandBuilder(sda);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Dgv.DataSource = ds.Tables[0];
            con.Close();
        }
        
        
    }
}
