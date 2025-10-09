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
    public partial class DoCam: Form
    {
        private int userId;
        private string connectionString = "Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";
        public DoCam(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DonerDashboardForm DonerDashboard = new  DonerDashboardForm(userId);
            DonerDashboard.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null; // Clear previous data first

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT CampaignId, CampaignName, StartDate, EndDate, Status FROM CAMPAIGN ORDER BY CampaignId DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT TOP 1 CampaignId, CampaignName, StartDate, EndDate, Status FROM CAMPAIGN ORDER BY CampaignId DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("No campaigns found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error showing latest campaign: " + ex.Message);
            }

        }
        private void DoCam_Load(object sender, EventArgs e)
        {
            LoadAllCampaigns();
        }

        private void LoadAllCampaigns()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT CampaignId, CampaignName, StartDate, EndDate, Status FROM CAMPAIGN ORDER BY CampaignId DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading campaigns: " + ex.Message);
            }
        }
    }
}
