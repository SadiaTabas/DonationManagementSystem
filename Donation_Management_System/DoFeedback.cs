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
    public partial class DoFeedback: Form
    {
        private int userId;
        private string connectionString = "Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";

        public DoFeedback(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Suggestion");
            comboBox1.Items.Add("Compliment");
            comboBox1.Items.Add("Default");

            comboBox1.SelectedIndex = 2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string feedbackType = comboBox1.SelectedItem?.ToString();
            string description = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please write your feedback before submitting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO FEEDBACK (SubmissionDate, UserId, Comment, FeedbackType) VALUES (@date, @uid, @comment, @type)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@comment", description);
                    cmd.Parameters.AddWithValue("@type", feedbackType);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thank you for your feedback!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    comboBox1.SelectedIndex = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting feedback: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DonerDashboardForm DonerDashboard = new DonerDashboardForm(userId);
            DonerDashboard.Show();
            this.Close(); 
        }
    }
}
