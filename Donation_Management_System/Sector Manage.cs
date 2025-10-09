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
    public partial class Sector_Manage: Form
    {
        public Sector_Manage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Dash DashForm = new Dash();
            DashForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            if (!int.TryParse(textBox1.Text, out int sectorId))
            {
                MessageBox.Show("Please enter a valid Sector ID.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM SECTORS WHERE SectorId = @SectorId", con);
            cmd.Parameters.AddWithValue("@SectorId", sectorId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["Title"].ToString();
                textBox3.Text = dr["Description"].ToString();
                textBox4.Text = dr["SectorStatus"].ToString();


                MessageBox.Show("✅ Sector found.");
            }
            else
            {
                MessageBox.Show("❌ Sector not found.");
            }

            dr.Close();
            con.Close();
            LoadSectorsToGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO SECTORS (Title, Description, SectorStatus) VALUES (@Title, @Description, @Status)", con);
            cmd.Parameters.AddWithValue("@Title", textBox2.Text);
            cmd.Parameters.AddWithValue("@Description", textBox3.Text);
            cmd.Parameters.AddWithValue("@Status", textBox4.Text);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✅ Sector added.");


            else
                MessageBox.Show("❌ Failed to add sector.");

            con.Close();
            LoadSectorsToGrid();


        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            if (!int.TryParse(textBox1.Text, out int sectorId))
            {
                MessageBox.Show("Please enter a valid Sector ID.");
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(@"UPDATE SECTORS SET Title=@Title, Description=@Description, SectorStatus=@Status WHERE SectorId=@SectorId", con);
            cmd.Parameters.AddWithValue("@SectorId", sectorId);
            cmd.Parameters.AddWithValue("@Title", textBox2.Text);
            cmd.Parameters.AddWithValue("@Description", textBox3.Text);
            cmd.Parameters.AddWithValue("@Status", textBox4.Text);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("✏️ Sector updated.");
            else
                MessageBox.Show("❌ Update failed or Sector not found.");

            con.Close();
            LoadSectorsToGrid();


        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            if (!int.TryParse(textBox1.Text, out int sectorId))
            {
                MessageBox.Show("Please enter a valid Sector ID.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this sector?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;InitialCatalog=Donation_Management_System;IntegratedSecurity=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM SECTORS WHERE SectorId = @SectorId", con);
            cmd.Parameters.AddWithValue("@SectorId", sectorId);

            int result = cmd.ExecuteNonQuery();
            if (result > 0)
                MessageBox.Show("🗑 Sector deleted.");
            else
                MessageBox.Show("❌ Sector not found.");

            con.Close();
            LoadSectorsToGrid();


        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            MessageBox.Show("🧹 Form cleared.");
            LoadSectorsToGrid();
        }
        private void LoadSectorsToGrid()
        {
         
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SECTORS", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Refresh(); // ✅ force update
            }
        

        }
         
             private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string status = textBox4.Text.Trim().ToLower();

            if (status == "active" || status == "inactive")
            {
                // Optional: change color to indicate valid input
                textBox4.BackColor = Color.LightGreen;
            }
            else if (textBox4.Text == "")
            {
                // Empty input — reset color
                textBox4.BackColor = Color.White;
            }
            else
            {
                // Invalid input — show visual feedback
                textBox4.BackColor = Color.LightCoral;
            }
        }


        
        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["SectorId"].Value.ToString();
                textBox2.Text = row.Cells["Title"].Value.ToString();
                textBox3.Text = row.Cells["Description"].Value.ToString();
                textBox4.Text = row.Cells["SectorStatus"].Value.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

