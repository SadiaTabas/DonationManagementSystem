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
    public partial class VolDonation: Form
    {
        private int userId;
        private string connectionString = @"Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";
        public VolDonation(int userId)
        {
            this.userId = userId;
            InitializeComponent();
        }
   
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCampaignNames();
            comboBox2.Items.AddRange(new string[] { "Active", "Deactive" });
            comboBox2.SelectedIndex = 0;
            LoadDonations();
        }
        private void LoadCampaignNames()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM CAMPAIGNS WHERE Status='Active'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    comboBox1.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["Name"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading campaigns: " + ex.Message);
            }
        }
        private void LoadDonations()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT DonationId, UserId, CampaignName, Status, Amount, PaymentType, DonationDate FROM DONATIONS ORDER BY DonationDate DESC";
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT DonationId, UserId, CampaignName, Status, Amount, PaymentType, DonationDate
                                     FROM DONATIONS
                                     WHERE CampaignName LIKE @campaign AND Status LIKE @status
                                     AND DonationDate BETWEEN @start AND @end";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@campaign", "%" + comboBox1.Text + "%");
                    cmd.Parameters.AddWithValue("@status", "%" + comboBox2.Text + "%");
                    cmd.Parameters.AddWithValue("@start", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@end", dateTimePicker2.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering donations: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;
            LoadDonations();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); // DonationId
            textBox2.Clear(); // Status
            textBox3.Clear(); // PaymentType
            textBox4.Clear(); // Amount

            // Reset comboboxes
            comboBox1.SelectedIndex = -1;      // Campaign Name
            comboBox2.SelectedIndex = 0;       // Status

            // Reset date pickers
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;

            // Reload all donations
            LoadDonations();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["DonationId"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Status"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["PaymentType"].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO DONATIONS (DonationId, Status, Amount, PaymentType, CampaignName, DonationDate, UserId)
                                     VALUES (@id, @status, @amount, @payment, @campaign, @date, @uid)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@status", textBox2.Text);
                    cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(textBox4.Text));
                    cmd.Parameters.AddWithValue("@payment", textBox3.Text);
                    cmd.Parameters.AddWithValue("@campaign", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@uid", userId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donation added successfully.");
                    LoadDonations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding donation: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VolunteerDashboardForm volunteerDashboard = new VolunteerDashboardForm(userId);
            volunteerDashboard.Show();
            this.Close();
        }
    }
}
