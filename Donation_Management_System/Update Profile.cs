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
    public partial class Update_Profile: Form
    {
        private int userId; // Pass this when opening form
        private string connectionString = @"Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";

        public Update_Profile(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        private void Update_Profile_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name, Email, PhoneNo, Address, password FROM USSER WHERE UserId=@UserId", con);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = reader["Name"].ToString();
                        textBox2.Text = reader["Email"].ToString();
                        textBox3.Text = reader["PhoneNo"].ToString();
                        textBox4.Text = reader["Address"].ToString();
                        textBox5.Text = reader["password"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DonerDashboardForm DonerDashboardForm = new DonerDashboardForm(userId);
            DonerDashboardForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadProfile(); // Reset fields to current database values
            textBox6.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string name = textBox1.Text.Trim();
                string email = textBox2.Text.Trim();
                string phone = textBox3.Text.Trim();
                string address = textBox4.Text.Trim();
                string currentPassword = textBox5.Text.Trim();
                string newPassword = textBox6.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Name and Email cannot be empty.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Verify current password
                    SqlCommand verifyCmd = new SqlCommand("SELECT password FROM USSER WHERE UserId=@UserId", con);
                    verifyCmd.Parameters.AddWithValue("@UserId", userId);
                    string dbPassword = (string)verifyCmd.ExecuteScalar();

                    if (dbPassword != currentPassword)
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    // Update profile
                    SqlCommand updateCmd = new SqlCommand(@"UPDATE USSER 
                                                            SET Name=@Name, Email=@Email, PhoneNo=@Phone, 
                                                                Address=@Address, password=@Password 
                                                            WHERE UserId=@UserId", con);

                    updateCmd.Parameters.AddWithValue("@Name", name);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.Parameters.AddWithValue("@Phone", phone);
                    updateCmd.Parameters.AddWithValue("@Address", address);
                    updateCmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(newPassword) ? currentPassword : newPassword);
                    updateCmd.Parameters.AddWithValue("@UserId", userId);

                    int rows = updateCmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Profile updated successfully.");
                    else
                        MessageBox.Show("Failed to update profile.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
