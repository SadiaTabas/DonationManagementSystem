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
using System.Xml.Linq;

namespace Donation_Management_System
{
    public partial class Admin_View: Form
    {
        public Admin_View()
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


          
            
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            
            SqlCommand cmd = new SqlCommand("SELECT * FROM USSER WHERE UserId = @UserId AND Role = 'Admin' ", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Name"].ToString();
                textBox3.Text = dr["Email"].ToString();
                textBox4.Text = dr["PhoneNo"].ToString();
                MessageBox.Show("Admin found successfully.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Admin not found.");
                LoadAdminsToGrid(); // Refresh grid if not found
            }

            con.Close();
        }

        

        

        private void button2_Click(object sender, EventArgs e)
         
        {
            string name = textBox2.Text;
            string email = textBox3.Text;
            string phone = textBox4.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please fill all the fields before adding a new admin.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(
    "UPDATE USSER SET Name = @Name, Email = @Email, PhoneNo = @PhoneNo WHERE UserId = @UserId AND Role = 'Admin'", con);


            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PhoneNo", phone);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("✅ New admin added successfully.");
                LoadAdminsToGrid();
            }
            else
                MessageBox.Show("❌ Failed to add new admin.");

            con.Close();
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

            SqlCommand cmd = new SqlCommand("DELETE FROM USSER WHERE UserId = @UserId AND Role = 'Admin'", con);

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PhoneNo", phone);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("✏️ Admin updated successfully.");
                LoadAdminsToGrid();
            }
            else
                MessageBox.Show("❌ Admin not found or update failed.");

            con.Close();
        }

        

     
            private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Please enter a valid numeric User ID.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this admin?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM USSER WHERE UserId = @UserId AND Role = 'Admin'", con);

            cmd.Parameters.AddWithValue("@UserId", userId);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                MessageBox.Show("❌ Admin deleted successfully.");
                button5.PerformClick();
                LoadAdminsToGrid();
            }
            else
            {
                MessageBox.Show("❌ Admin not found. Nothing was deleted.");
            }

            con.Close();
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
         
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            MessageBox.Show("🧹Form cleared.");
            LoadAdminsToGrid();
        }

        private void LoadAdminsToGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;"))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT UserId, Name, Email, PhoneNo FROM USSER WHERE Role = 'Admin'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading admins: " + ex.Message);
            }
        }
        private void Admin_View_Load(object sender, EventArgs e)
        {
            LoadAdminsToGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure not header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["UserId"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Email"].Value.ToString();
                textBox4.Text = row.Cells["PhoneNo"].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
