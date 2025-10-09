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
    public partial class FeedBack: Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");

        public FeedBack()
        {
            InitializeComponent();
        }

        private void FeedBack_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear(); // Feedback Type
            comboBox1.Items.Add("All");
            comboBox1.Items.Add("Positive");
            comboBox1.Items.Add("Negative");
            comboBox1.Items.Add("Suggestion");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Clear(); // Receipt/Status
            comboBox2.Items.Add("All");
            comboBox2.Items.Add("Reviewed");
            comboBox2.Items.Add("Pending");
            comboBox2.SelectedIndex = 0;

            LoadFeedbacks();
        }
        private void LoadFeedbacks(string feedbackType = "All", string receipt = "All", DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                con.Open();

                string query = "SELECT FeedbackId, SubmissionDate, UserId, Comment FROM FEEDBACK WHERE 1=1";

                // Date filter
                if (startDate.HasValue && endDate.HasValue)
                    query += " AND SubmissionDate BETWEEN @Start AND @End";

                // Feedback type filter (if you store type info later)
                // This example assumes filtering by keyword
                if (feedbackType != "All")
                    query += " AND Comment LIKE @Type";

                SqlCommand cmd = new SqlCommand(query, con);

                if (startDate.HasValue && endDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Start", startDate.Value);
                    cmd.Parameters.AddWithValue("@End", endDate.Value);
                }

                if (feedbackType != "All")
                    cmd.Parameters.AddWithValue("@Type", "%" + feedbackType + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading feedbacks: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            textBox1.Text = "";
            textBox2.Text = "";
            LoadFeedbacks();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["FeedbackId"].Value.ToString();
                textBox2.Text = row.Cells["Comment"].Value.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type = comboBox1.Text;
            string receipt = comboBox2.Text;
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;

            LoadFeedbacks(type, receipt, start, end);
            MessageBox.Show("✅ Filter applied successfully!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("⚠️ Please enter a Feedback ID to view.");
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM FEEDBACK WHERE FeedbackId=@id", con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox2.Text = dr["Comment"].ToString();
                    MessageBox.Show("✅ Feedback loaded successfully.");
                }
                else
                {
                    MessageBox.Show("❌ Feedback not found.");
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("⚠️ Enter Feedback ID first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Mark this feedback as reviewed?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FEEDBACK SET Comment = Comment + ' (Reviewed)' WHERE FeedbackId=@id", con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    MessageBox.Show("✅ Feedback marked as reviewed.");
                else
                    MessageBox.Show("❌ Feedback not found.");

                LoadFeedbacks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("⚠️ Enter Feedback ID to delete.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this feedback?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM FEEDBACK WHERE FeedbackId=@id", con);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    MessageBox.Show("🗑 Feedback deleted successfully.");
                else
                    MessageBox.Show("❌ Feedback not found.");

                LoadFeedbacks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
