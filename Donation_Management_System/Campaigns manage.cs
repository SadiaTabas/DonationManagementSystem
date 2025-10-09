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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation_Management_System
{
    public partial class Campaigns_manage: Form
    {
        public Campaigns_manage()
        {
            InitializeComponent();
        }
       

        private void button6_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int campaignId))
            {
                MessageBox.Show("Please enter a valid Campaign ID.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM CAMPAIGNS WHERE CampaignId = @CampaignId", con);
            cmd.Parameters.AddWithValue("@CampaignId", campaignId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Name"].ToString();
                textBox3.Text = dr["Description"].ToString();
                textBox4.Text = dr["Venue"].ToString();
                comboBox1.Text = dr["Status"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["StartdDate"]);
                dateTimePicker2.Value = Convert.ToDateTime(dr["EndDate"]);

                MessageBox.Show("✅ Campaign found successfully.");
            }
            else
            {
                MessageBox.Show("❌ Campaign not found.");
            }

            dr.Close();
            con.Close();
            LoadCampaignsToGrid();

        }


        private void button5_Click(object sender, EventArgs e)
        {
           
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO CAMPAIGNS (Name, Description, StartdDate, EndDate, Venue, Status) VALUES (@Name, @Description, @StartdDate, @EndDate, @Venue, @Status)", con);
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Description", textBox3.Text);
            cmd.Parameters.AddWithValue("@StartdDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@Venue", textBox4.Text);
            cmd.Parameters.AddWithValue("@Status", comboBox1.Text);


            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✅ Campaign added successfully.");
            else
                MessageBox.Show("❌ Failed to add campaign.");

            con.Close();
            LoadCampaignsToGrid();

        }




        private void button4_Click(object sender, EventArgs e)
        {
         
            if (!int.TryParse(textBox1.Text, out int campaignId))
            {
                MessageBox.Show("Please enter a valid Campaign ID.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE CAMPAIGNS SET Name = @Name, Description = @Description, StartdDate = @StartdDate, EndDate = @EndDate, Venue = @Venue, Status = @Status WHERE CampaignId = @CampaignId", con);
            cmd.Parameters.AddWithValue("@CampaignId", campaignId);
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Description", textBox3.Text);
            cmd.Parameters.AddWithValue("@StartdDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@Venue", textBox4.Text);
            cmd.Parameters.AddWithValue("@Status", comboBox1.Text);


            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✏️ Campaign updated successfully.");
            else
                MessageBox.Show("❌ Campaign not found or update failed.");

            con.Close();
            LoadCampaignsToGrid();

        }



        private void button3_Click(object sender, EventArgs e)
        {
         
            if (!int.TryParse(textBox1.Text, out int campaignId))
            {
                MessageBox.Show("Please enter a valid Campaign ID.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this campaign?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM CAMPAIGNS WHERE CampaignId = @CampaignId", con);
            cmd.Parameters.AddWithValue("@CampaignId", campaignId);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("🗑 Campaign deleted successfully.");
            else
                MessageBox.Show("❌ Campaign not found.");

            con.Close();
            LoadCampaignsToGrid();

        }



        private void button2_Click(object sender, EventArgs e)
        {
         
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;  
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;

            MessageBox.Show("🧹 Form cleared.");
        }

        private void Campaigns_manage_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Active");
            comboBox1.Items.Add("Inactive");
            comboBox1.Items.Add("Completed");
            comboBox1.SelectedIndex = 0;

            LoadCampaignsToGrid();
        }
        private void LoadCampaignsToGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;"))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CAMPAIGNS", con);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            if (e.RowIndex >= 0)  
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["CampaignId"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Description"].Value.ToString();
                textBox4.Text = row.Cells["Venue"].Value.ToString();
                comboBox1.Text = row.Cells["Status"].Value.ToString();

                if (DateTime.TryParse(row.Cells["StartdDate"].Value.ToString(), out DateTime startDate))
                    dateTimePicker1.Value = startDate;

                if (DateTime.TryParse(row.Cells["EndDate"].Value.ToString(), out DateTime endDate))
                    dateTimePicker2.Value = endDate;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
        
