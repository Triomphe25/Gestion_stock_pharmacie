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

namespace TP_4476_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string vendeur_Nom = "";


        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            u_role.Text = "";
            u_nom.Text = "";
            u_pass.Text = "";

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(u_nom.Text) || !string.IsNullOrWhiteSpace(u_pass.Text))
            {
                if (u_role.SelectedIndex > -1)
                {
                    if (u_role.SelectedItem.ToString() == "Admin")
                    {
                        if (u_nom.Text == "Admin" || u_pass.Text == "Admin")
                        {
                            ProduitForm prod = new ProduitForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("si vous etes reelement l'admin entrer le vrai nom et mot de pass!");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("your in the seller section!");
                        Clconnexion con = new Clconnexion();
                        using (SqlConnection connection = con.GetSConnection())
                        {
                            connection.Open();
                            string query = "select count(8) from VendeurTbl where NomVendeur='" + u_nom.Text + "' and PassVendeur='" + u_pass.Text + "' ";
                            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1")
                            {

                                vendeur_Nom = u_nom.Text;

                                VenteForm vente = new VenteForm();
                                vente.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("votre nom ou mot de pass est iincorect!");
                            }
                            connection.Close();


                        }
                    }

                }
                else
                {
                    MessageBox.Show("selectionner un role s'il vous plait");

                }

            }
            else
            {
                MessageBox.Show("vous devez renseigner les champs");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}