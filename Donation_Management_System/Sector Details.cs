using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donation_Management_System
{
    public partial class Sector_Details: Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";
        private int sectorId = 0;
        public Sector_Details(int sectorId = 0)
        {
            InitializeComponent();
            this.sectorId = sectorId;
        }
        private void Sector_Details_Load(object sender, EventArgs e)
        {
            if (sectorId > 0)
            {
                LoadSectorDetails();
            }
        }

        // Load sector details for editing
        private void LoadSectorDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Title, Description FROM SECTORS WHERE SectorId=@SectorId", con);
                    cmd.Parameters.AddWithValue("@SectorId", sectorId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = reader["Title"].ToString();
                        textBox2.Text = reader["Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sector: " + ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();
            string description = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Sector name cannot be empty.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd;

                    if (sectorId > 0)
                    {
                        // Update existing sector
                        cmd = new SqlCommand(@"UPDATE SECTORS 
                                               SET Title=@Title, Description=@Description 
                                               WHERE SectorId=@SectorId", con);
                        cmd.Parameters.AddWithValue("@SectorId", sectorId);
                    }
                    else
                    {
                        // Insert new sector
                        cmd = new SqlCommand(@"INSERT INTO SECTORS (Title, Description, SectorStatus) 
                                               VALUES (@Title, @Description, 'Active')", con);
                    }

                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Sector saved successfully.");
                    else
                        MessageBox.Show("Failed to save sector.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving sector: " + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
