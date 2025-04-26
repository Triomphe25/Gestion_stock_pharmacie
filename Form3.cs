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
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RemplirDGV()
        {
            Clconnexion con = new Clconnexion();
            using (SqlConnection connection = con.GetSConnection())
            {
                connection.Open();
                string query = "Select* from VendeurTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                seller_dgv.DataSource = ds.Tables[0];
                connection.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            RemplirDGV();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (seller_dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = seller_dgv.SelectedRows[0];
                id_seller.Text = row.Cells[0].Value.ToString();
                nom_seller.Text = row.Cells[1].Value.ToString();
                age_seller.Text = row.Cells[2].Value.ToString();
                phone_seller.Text = row.Cells[3].Value.ToString();
                pass_seller.Text = row.Cells[4].Value.ToString();
            }
        }

        //bouton pour inserer le vendeur

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
           

            
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(nom_seller.Text) && !string.IsNullOrWhiteSpace(age_seller.Text) && !string.IsNullOrWhiteSpace(phone_seller.Text) && !string.IsNullOrWhiteSpace(pass_seller.Text))
                {


                    ClCRUD crud = new ClCRUD();

                    var data = new Dictionary<string, object>
                    {
                        { "NomVendeur", nom_seller.Text },
                        { "AgeVendeur", int.Parse(age_seller.Text) },
                        { "PhoneVendeur", phone_seller.Text },
                        { "PassVendeur", pass_seller.Text }
                    };

                    crud.Insert("VendeurTbl", data);
                    MessageBox.Show("Vendeur ajoutée avec succès !");
                    RemplirDGV();

                }
                else
                {
                    MessageBox.Show("vous devez completer tout les champs!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(id_seller.Text))
                {
                    ClCRUD crud = new ClCRUD();
                    var data = new Dictionary<string, object>
                    {
                        {"NomVendeur",nom_seller.Text},
                        {"AgeVendeur", int.Parse(age_seller.Text)},
                        {"PhoneVendeur", phone_seller.Text},
                        {"PassVendeur", pass_seller.Text}
                    };
                    string condition = $"IdVendeur={id_seller.Text.Trim()}";
                    crud.Update("VendeurTbl", data, condition);
                    RemplirDGV();
                }
                else
                {
                    MessageBox.Show("vous devez iindiquer le numero du vendeur a supprimer");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            try
            {
                ClCRUD crud = new ClCRUD();
                if (!string.IsNullOrWhiteSpace(id_seller.Text))
                {


                    string condition = $"@IdVendeur={id_seller.Text.Trim()}";
                    crud.Delete("VendeurTbl", condition);
                    MessageBox.Show("Vendeur supprimer avec succes!");
                }
                else
                {
                    MessageBox.Show("veillez indiquer le numero du vendeur a supprimer");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProduitForm prod = new ProduitForm();
            prod.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Approvisionner aprov = new Approvisionner();
            aprov.Show();
            this.Hide();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
