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
    public partial class DonorDetails: Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=Donation_Management_System;Integrated Security=True";
        private int userId = 0;
        public DonorDetails(int userId = 0)
        {
            InitializeComponent();
            this.userId = userId;
        }
        private void DonorDetails_Load(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                LoadDonorDetails();
            }
        }

        // Load donor details if editing
        private void LoadDonorDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name, Email, FullName AS Username, DateOfBirth, PhoneNo, Address FROM USSER WHERE UserId=@UserId", con);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = reader["Name"].ToString();
                        textBox2.Text = reader["Email"].ToString();
                        textBox3.Text = reader["Username"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                        textBox4.Text = reader["PhoneNo"].ToString();
                        textBox5.Text = reader["Address"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donor: " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd;
                    if (userId > 0)
                    {
                        // Update existing donor
                        cmd = new SqlCommand(@"UPDATE USSER SET Name=@Name, Email=@Email, FullName=@Username, 
                                              DateOfBirth=@DOB, PhoneNo=@Phone, Address=@Address 
                                              WHERE UserId=@UserId", con);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                    }
                    else
                    {
                        // Insert new donor
                        cmd = new SqlCommand(@"INSERT INTO USSER (Name, Email, FullName, DateOfBirth, PhoneNo, Address, Role, UserStatus) 
                                              VALUES (@Name, @Email, @Username, @DOB, @Phone, @Address, 'User', 'Active')", con);
                    }

                    cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@Phone", textBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", textBox5.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Donor details saved successfully.");
                    else
                        MessageBox.Show("Failed to save donor details.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving donor: " + ex.Message);
            }
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
