using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TP_4476_
{
    public partial class ProduitForm: Form
    {
        public ProduitForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Form3 vendeur = new Form3();
            vendeur.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Bouton pour enregistrer les produits dans la base de donnee

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }


        

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        //Datagridview pour afficher les 

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (prod_dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = prod_dgv.SelectedRows[0];
                prod_id.Text = row.Cells[0].Value.ToString();
                prod_nom.Text = row.Cells[1].Value.ToString();
                prod_qt.Text = row.Cells[2].Value.ToString();
                prod_prix.Text = row.Cells[3].Value.ToString();
                cb_cat.Text = row.Cells[4].Value.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        //Methode pour remplir le combobox
        private void remplircb()
        {
            Clconnexion con = new Clconnexion();
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string query = "select NomCategorie from Categorie";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("NomCategorie", typeof(string));
                dt.Load(rdr);
                recherchecb.ValueMember = "NomCategorie";
                recherchecb.DataSource = dt;
                connection.Close();
            }
        }

        //methode pour remplir le datagridview

        private void remplirdgv()
        {
            Clconnexion con = new Clconnexion();
            using(SqlConnection _connection = con.GetSConnection())
            {
                _connection.Open();
                string query = "select * from Produit";
                SqlDataAdapter sda = new SqlDataAdapter(query, _connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                prod_dgv.DataSource = ds.Tables[0];
                _connection.Close();
            }
        }




        //methode pour remplir le combo box
        private void RemplirCb()
        {
            Clconnexion con = new Clconnexion();
            using(SqlConnection seconnecter = con.GetSConnection())
            {
                seconnecter.Open();
                SqlCommand cmd = new SqlCommand("select NomCategorie from Categorie", seconnecter);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("NomCategorie", typeof(string));
                dt.Load(rdr);
                cb_cat.ValueMember = "NomCategorie";
                cb_cat.DataSource = dt;
                seconnecter.Close();
            }
        }
        private void ProduitForm_Load(object sender, EventArgs e)
        {
            RemplirCb();
            remplirdgv();
            remplircb();
            //PrivateFontCollection

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Clconnexion con = new Clconnexion();
                using (SqlConnection connection = con.GetSConnection())
                {
                    connection.Open();
                    string query = "Select * from Produit  where CategorieProd= '" + recherchecb.SelectedValue.ToString() + "' ";
                    SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    prod_dgv.DataSource = ds.Tables[0];
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Form1 aceuil = new Form1();
            aceuil.Show();
            this.Hide();
        }
        // Bouton por enregistrer les produit
        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            ClCRUD crud = new ClCRUD();
            Clconnexion connexion = new Clconnexion();
            using (SqlConnection connection = connexion.GetSConnection())
            {
                connection.Open();

                if (!string.IsNullOrWhiteSpace(prod_nom.Text) && !string.IsNullOrWhiteSpace(prod_prix.Text) && !string.IsNullOrWhiteSpace(prod_qt.Text))
                {
                    if (cb_cat.SelectedIndex > -1)
                    {




                        string query = "SELECT IdCatEGORIE FROM Categorie WHERE NomCategorie = @NomCategorie";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@NomCategorie", cb_cat.Text.Trim());
                        object resultat = cmd.ExecuteScalar();

                        var data = new Dictionary<string, object>
                            {
                                { "NomProd", prod_nom.Text },
                                { "QtProd", prod_qt.Text },
                                {"PrixProd", prod_prix.Text  },
                                { "IdCategorie", resultat }
                            };

                        crud.Insert("Produit", data);
                        MessageBox.Show("Produit ajoutée avec succès !");
                        remplirdgv();
                    }
                    else
                    {
                        MessageBox.Show("vous devez entrez une categorie pour ce produit");
                    }

                }
                else
                {
                    MessageBox.Show("vous devez completer tous les chsmps");
                }
                connection.Close();
            }
        }
        // Bouton pour modifier les produits dans la base de donnee
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(prod_id.Text))
            {
                ClCRUD crud = new ClCRUD();
                var data = new Dictionary<string, object>
                {
                    {"NomProd",  prod_nom.Text},
                    {"QtProd",int.Parse(prod_qt.Text)},
                    {"PrixProd", int.Parse(prod_prix.Text)},
                    {"CategorieProd", cb_cat.Text}
                 };

                string condition = $"IdProd={prod_id.Text.Trim()}";

                crud.Update("Produit", data, condition);
                MessageBox.Show("Modifier avec succes !");
                remplirdgv();
            }
        }

        //Bouton pour supprimer le produit
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(prod_id.Text))
            {
                ClCRUD crud = new ClCRUD();
                string condition = $"IdProd={prod_id.Text.Trim()}";
                crud.Delete("Produit", condition);
                MessageBox.Show("produit supprimer avec succes!");
                remplirdgv();
            }
        }
    }
}
