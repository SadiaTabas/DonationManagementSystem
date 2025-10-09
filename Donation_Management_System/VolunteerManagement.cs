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
    public partial class VolunteerManagement: Form
    {
        public VolunteerManagement()
        {
            InitializeComponent();
        }

        private void VolunteerManagement_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }


            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();


            SqlCommand cmd = new SqlCommand("SELECT * FROM USSER WHERE UserId = @UserId AND Role = ''Volunteer' ", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Name"].ToString();
                textBox3.Text = dr["Email"].ToString();
                textBox4.Text = dr["PhoneNo"].ToString();
                MessageBox.Show("Volunteer found successfully.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Volunteer not found.");
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string name = textBox2.Text;
                string email = textBox3.Text;
                string phone = textBox4.Text;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Please fill all the fields before adding a new Volunteer.");
                    return;
                }

                SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
                con.Open();

                SqlCommand cmd = new SqlCommand(
        "UPDATE USSER SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo WHERE UserId = @UserId AND Role = 'Volunteer'", con);


                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PhoneNo", phone);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("✅ New Volunteer added successfully.");
                else
                    MessageBox.Show("❌ Failed to add new Volunteer.");

                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            string name = textBox2.Text;
            string email = textBox3.Text;
            string phone = textBox4.Text;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM USSER WHERE UserId = @UserId AND Role = 'Volunteer'", con);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PhoneNo", phone);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                MessageBox.Show("✏️ Volunteer updated successfully.");
            else
                MessageBox.Show("❌ Volunteer not found or update failed.");

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Volunteer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM USSER WHERE UserId = @UserId AND Role = 'Volunteer'", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("❌ Volunteer deleted successfully.");
                button5.PerformClick();
            }
            else
            {
                MessageBox.Show("❌ Volunteer not found. Nothing was deleted.");
            }

            con.Close();
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            MessageBox.Show("🧹Form cleared.");
        }
    }
}
