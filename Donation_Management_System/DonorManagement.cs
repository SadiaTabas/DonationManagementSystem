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
    public partial class DonorManagement: Form
    {
        public DonorManagement()
        {
            InitializeComponent();
            LoadDonorData();
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

            SqlCommand cmd = new SqlCommand("SELECT * FROM USSER WHERE UserId = @UserId AND Role = 'Donor'", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Name"].ToString();
                textBox3.Text = dr["Email"].ToString();
                textBox4.Text = dr["PhoneNo"].ToString();
                MessageBox.Show("✅ Donor found.");
            }
            else
            {
                MessageBox.Show("❌ Donor not found.");
            }

            dr.Close();
            con.Close();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
         
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(@"INSERT INTO USSER (Name, Gender, DateOfBirth, Email, PhoneNo, Address, password, UserStatus, SecurityAns, Role, FullName) 
                                      VALUES (@Name, 'N/A', GETDATE(), @Email, @PhoneNo, 'N/A', 'default123', 1, 'N/A', 'Donor', @FullName)", con);

            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Email", textBox3.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox4.Text);
            cmd.Parameters.AddWithValue("@FullName", textBox2.Text); // Or some other field

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✅ Donor added.");
            else
                MessageBox.Show("❌ Failed to add donor.");

            con.Close();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
         
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(@"UPDATE USSER SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo, FullName = @FullName 
                                      WHERE UserId = @UserId AND Role = 'Donor'", con);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Email", textBox3.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", textBox4.Text);
            cmd.Parameters.AddWithValue("@FullName", textBox2.Text);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✏️ Donor updated.");
            else
                MessageBox.Show("❌ Update failed.");

            con.Close();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
         
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this donor?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM USSER WHERE UserId = @UserId AND Role = 'Donor'", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("🗑 Donor deleted.");
            else
                MessageBox.Show("❌ Donor not found.");

            con.Close();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {


         
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            LoadDonorData(); // reload donor list
            MessageBox.Show("🔄 Refreshed successfully.");
        

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["UserId"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Email"].Value.ToString();
                textBox4.Text = row.Cells["PhoneNo"].Value.ToString();
            }
        }

        
        private void LoadDonorData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;"))
                {
                    con.Open();

                    string query = "SELECT UserId, Name, Email, PhoneNo FROM USSER WHERE Role = 'Donor'";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Optional: Make columns auto-fit nicely
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Error loading donors: " + ex.Message);
            }
        }

    }
}
