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
    public partial class Donation_Record: Form
    { private string connectionString = "Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";
        public Donation_Record()
        {
            InitializeComponent();
        }
        private void Donation_Record_Load(object sender, EventArgs e)
        {
            LoadPaymentTypes();
            LoadActiveCampaigns();
            LoadAllDonations();
        }
        private void LoadPaymentTypes()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(new string[] { "All", "Bkash", "Nogod", "Rocket", "VisaCard" });
            comboBox3.SelectedIndex = 0;
        }
        private void LoadActiveCampaigns()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CampaignId, Name FROM CAMPAIGNS WHERE Status='Active'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "CampaignId";
                    comboBox2.DataSource = dt;
                    comboBox2.SelectedIndex = -1; // No default selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading campaigns: " + ex.Message);
            }
        }
        private void LoadAllDonations()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT d.DonationId, d.Amount, d.DonationStatus, d.DonationDate, d.PayMethod, 
                                     c.Name AS CampaignName, u.Name AS DonorName
                                     FROM DONATIONS d
                                     LEFT JOIN CAMPAIGNS c ON d.CampaignId = c.CampaignId
                                     LEFT JOIN USSER u ON d.UserId = u.UserId
                                     ORDER BY d.DonationDate DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donations: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"SELECT d.DonationId, d.Amount, d.DonationStatus, d.DonationDate, d.PayMethod, 
                                     c.Name AS CampaignName, u.Name AS DonorName
                                     FROM DONATIONS d
                                     LEFT JOIN CAMPAIGNS c ON d.CampaignId = c.CampaignId
                                     LEFT JOIN USSER u ON d.UserId = u.UserId
                                     WHERE (@campaignId = 0 OR d.CampaignId = @campaignId)
                                     AND (@donorName = '' OR u.Name LIKE '%' + @donorName + '%')
                                     AND (@payMethod = 'All' OR d.PayMethod = @payMethod)
                                     ORDER BY d.DonationDate DESC";

                    SqlCommand cmd = new SqlCommand(query, con);

                    int campaignId = comboBox2.SelectedIndex == -1 ? 0 : Convert.ToInt32(comboBox2.SelectedValue);
                    string donorName = textBox1.Text.Trim();
                    string payMethod = comboBox3.SelectedItem.ToString();

                    cmd.Parameters.AddWithValue("@campaignId", campaignId);
                    cmd.Parameters.AddWithValue("@donorName", donorName);
                    cmd.Parameters.AddWithValue("@payMethod", payMethod);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying filter: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = 0;
            LoadAllDonations();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
