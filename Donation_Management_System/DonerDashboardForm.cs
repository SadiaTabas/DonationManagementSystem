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
    public partial class DonerDashboardForm: Form
    {
        private int  userId;
        public DonerDashboardForm(int userId)
        {
            InitializeComponent();
            TotalDonations( userId);
            DonationHistory( userId);
            RecentDonations( userId);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoCam DoCam = new DoCam(userId);
            DoCam.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(

                "Are you sure you want to Exit?",

                "CONFIRMED",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)

            {

                Login loginForm = new Login();
                loginForm.Show();
                this.Close();

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection con;

        public void sqlcon()
        {
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();
        }
         
        

        private void DonerDashboardForm_Load(object sender, EventArgs e)
        {

            if ( userId > 0)
            {
                TotalDonations (userId);
                DonationHistory( userId);
                RecentDonations( userId);
            }
            else
            {
                MessageBox.Show("User ID not set. Cannot load dashboard data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void TotalDonations(int userId)
        {
            sqlcon();

            SqlCommand sq1 = new SqlCommand(@"
        SELECT ISNULL(SUM(Amount), 0) AS TotalDonation
        FROM DONATIONS
        WHERE UserId = @UserId", con);

            sq1.Parameters.AddWithValue("@UserId", userId);

            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(sq1);
            sd.Fill(dt);

            dataGridView2.DataSource = dt;

            con.Close();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void DonationHistory(int userId)
        {
            sqlcon();

            SqlCommand sq1 = new SqlCommand(@"
        SELECT Amount, FORMAT(DonationDate, 'yyyy-MM-dd') AS DonationDate, PayMethod
        FROM DONATIONS
        WHERE UserId = @UserId
        ORDER BY DonationDate DESC", con);

            sq1.Parameters.AddWithValue("@UserId", userId);

            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(sq1);
            sd.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }



        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void RecentDonations(int userId)
        {
            
            sqlcon();

            SqlCommand sq1 = new SqlCommand(
    "SELECT Amount, DonationDate, PayMethod " +
    "FROM DONATIONS " +
    "WHERE UserId = @UserId " +
    "ORDER BY DonationDate DESC;", con);


            sq1.Parameters.AddWithValue("@UserId", userId);

            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(sq1);
            sd.Fill(dt);


            dataGridView3.DataSource = dt;

            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Donation Donation = new Donation(userId);
            Donation.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {DoFeedback DoFeedback = new DoFeedback(userId);
            DoFeedback.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update_Profile Update_Profile = new Update_Profile(userId);
            Update_Profile.Show();
            this.Close();
        }
    }
}
