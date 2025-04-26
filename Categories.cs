using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using GSF.Adapters;
using Antlr.Runtime.Tree;

namespace TP_4476_
{
    public partial class Categories: Form
    {
        public Categories()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //Clconnexion conn = new Clconnexion();
        //SqlConnection sqlconn = conn.GetSConnection();

        //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VRP1554;Initial Catalog=Pharmacie;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        //Clconnexion con = new Clconnexion()
        //public OleDbConnection con = new OleDbConnection(@"Data Source=DESKTOP-VRP1554;Initial Catalog=Pharmacie;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nom_cat.Text) && !string.IsNullOrWhiteSpace(desc_cat.Text))
            {


                ClCRUD crud = new ClCRUD();

                var data = new Dictionary<string, object>
{
                { "NomCategorie", nom_cat.Text },
                { "DescCategorie", desc_cat.Text }
};

                crud.Insert("Categorie", data);
                MessageBox.Show("Catégorie ajoutée avec succès !");

            }
            else
            {
                MessageBox.Show("le cham nom et description peuvent pas etre vide");
            }
            
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        //fonction pour remplir le datagid view

        private void RemplirDGV()
        {
            Clconnexion con = new Clconnexion();
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string query = "Select* from Categorie";
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dgv_cat.DataSource = ds.Tables[0];
                connection.Close();
            }
        }



        private void dgv_cat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_cat.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv_cat.SelectedRows[0];
                id_cat.Text = row.Cells[0].Value.ToString();
                nom_cat.Text = row.Cells[1].Value.ToString();
                desc_cat.Text = row.Cells[2].Value.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Categories_Load(object sender, EventArgs e)
        {
            RemplirDGV();
            //Clconnexion conn = new Clconnexion();
            //SqlConnection sqlconn = conn.GetSConnection();
            //Cldgv adapter = new Cldgv(sqlconn);
            //adapter.remplirDatagrid("Categorie", dgv_cat);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(id_cat.Text))
            {
                ClCRUD _crud=new ClCRUD();
                var datamodifier = new Dictionary<string, object> {
                    { "NomCategorie", nom_cat.Text },
                    { "DescCategorie", desc_cat.Text }
                    //{"IdCatEGORIE", id_cat.Text }
                };

                string condition = $"IdCatEGORIE={id_cat.Text.Trim()}";

                _crud.Update("Categorie", datamodifier, condition);
                MessageBox.Show("Modifier avec succes !");
                RemplirDGV();
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(id_cat.Text)) { 
                ClCRUD _crudd=new ClCRUD();
                string condition = $"IdCatEGORIE={id_cat.Text.Trim()}";
                _crudd.Delete("Categorie", condition);
                MessageBox.Show("categorie supprimer avec succes");
            }
            else
            {
                MessageBox.Show("indiquer l'id du produit a supprimer");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProduitForm prod = new ProduitForm();
            prod.Show();
            this.Hide();
        }
    }
}
