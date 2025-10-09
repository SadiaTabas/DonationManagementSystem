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
    public partial class Donation: Form
    {
        private int userId; // Pass this when opening form
        private string connectionString = @"Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";

        public Donation(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        private void Donation_Load(object sender, EventArgs e)
        {
            // Load payment types
            comboBox2.Items.AddRange(new string[] { "Bkash", "Nogod", "Rocket", "VisaCard" });
            comboBox2.SelectedIndex = 0;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string campaignName = textBox2.Text.Trim();
            string paymentType = comboBox2.SelectedItem.ToString();
            decimal amount;

            // Validation
            if (string.IsNullOrEmpty(campaignName))
            {
                MessageBox.Show("Please enter the Campaign Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(textBox1.Text.Trim(), out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid Amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO DONATIONS (UserId, CampaignName, Amount, PaymentType, DonationDate, Status)
                                     VALUES (@uid, @campaign, @amount, @payment, @date, 'Active')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@campaign", campaignName);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@payment", paymentType);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donation submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear after submit
                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox2.SelectedIndex = 0;
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting donation: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VolunteerDashboardForm volunteerDashboard = new VolunteerDashboardForm(userId);
            volunteerDashboard.Show();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
