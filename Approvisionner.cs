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
    public partial class Approvisionner: Form
    {
        public Approvisionner()
        {
            InitializeComponent();
        }
        //Methode pour remplir le datagridview
        private void remplirdgvproduit_()
        {

            Clconnexion con = new Clconnexion();
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string query = "Select NomProd from Produit";
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dgv_produit.DataSource = ds.Tables[0];
                connection.Close();
            }
        }

        //methode pour remplir le combo box
        private void RemplirCb()
        {
            Clconnexion con = new Clconnexion();
            using (SqlConnection seconnecter = con.GetSConnection())
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
        private void Approvisionner_Load(object sender, EventArgs e)
        {
            remplirdgvproduit_();
            RemplirCb();
        }

        //bouton pour enregistrer la vente et imprimer la facture

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            ClCRUD crud = new ClCRUD();
            Clconnexion con = new Clconnexion(); // Ta connexion
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 1. Insertion Approvisionnement
                    Dictionary<string, object> approvData = new Dictionary<string, object>()
                    {
                        { "dateApprov", DateTime.Now } // Ou ta date choisie
                    };

                    int idApprovisionnement = crud.InsertAndGetId("Approvisionnement", approvData, transaction);

                    // 2. Insertion des détails
                    foreach (DataGridViewRow row in proddgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string query = "SELECT IdProd FROM Produit WHERE NomProd = @NomProd";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Transaction = transaction; // Ligne ajoutée ici
                            cmd.Parameters.AddWithValue("@NomProd", row.Cells[1].Value);

                            object resultat = cmd.ExecuteScalar();


                            Dictionary<string, object> detailData = new Dictionary<string, object>()
                            {
                                { "IdApprov", idApprovisionnement },
                                //row.Cells[1].Value.ToString()
                                { "IdProd", resultat }, // Change le nom si nécessaire
                                {"Quantite", row.Cells[2].Value }
                            };

                            crud.InsertWithTransaction("DetaillAprov", detailData, transaction);
                        }
                    }

                    // 3. Commit si tout est ok
                    transaction.Commit();
                    MessageBox.Show("Approvisionnement et détails enregistrés avec succès !");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //initialisation du compteur des produit dans le panier
        int n =0;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            DataGridViewRow nrow = new DataGridViewRow();
            nrow.CreateCells(proddgv);
            nrow.Cells[0].Value = n + 1;
            nrow.Cells[1].Value = prod_nom.Text;
            nrow.Cells[2].Value = prod_qt.Text;
            proddgv.Rows.Add(nrow);
            n++;
        }

        private void dgv_produit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //en cliquant la ligne les donner s'affiche dans le combobox
            prod_nom.Text = dgv_produit.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cb_cat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Clconnexion con = new Clconnexion();
                using (SqlConnection connection = con.GetSConnection())
                {
                    connection.Open();
                    string query = "Select NomProd from Produit inner join Categorie on Produit.IdCategorie=Categorie.IdCatEGORIE where NomCategorie= '" + cb_cat.SelectedValue.ToString() + "' ";
                    SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dgv_produit.DataSource = ds.Tables[0];
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_cat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void proddgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void proddgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                string nomproduit = proddgv.Rows[e.RowIndex].Cells[1].Value.ToString();

                DialogResult result = MessageBox.Show($"voulez vous retirez le produit: \"{nomproduit}\" du panier?",
                    "confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    proddgv.Rows.RemoveAt(e.RowIndex);
                    foreach (DataGridViewRow newrow in proddgv.Rows)
                    {
                        if(newrow.Index > e.RowIndex)
                        {
                            //int valeur = Convert.ToInt32(newrow.Cells[0]);
                            object cellvalue = newrow.Cells[0].Value;
                            if (cellvalue != null && int.TryParse(cellvalue.ToString(), out int valeur)) {


                                newrow.Cells[0].Value = valeur =- 1;
                            }
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProduitForm formp = new ProduitForm();
            formp.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();
        }
    }
    
}
