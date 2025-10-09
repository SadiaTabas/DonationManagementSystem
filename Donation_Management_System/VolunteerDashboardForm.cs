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
    public partial class VolunteerDashboardForm : Form
    {
        private int userId;
        private string userRole;


        public VolunteerDashboardForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            Feedback();
            Campaigns();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VolunteerDashboardForm_Load(object sender, EventArgs e)
        {

            if (userId > 0)
            {
                Feedback();
                Campaigns();
                
            }
            else
            {
                MessageBox.Show("User ID not set. Cannot load dashboard data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Feedback()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand sq1 = new SqlCommand("SELECT FeedbackId, UserId, Comment, SubmissionDate FROM FEEDBACK", con);
            DataTable dt = new DataTable();

            SqlDataAdapter sd = new SqlDataAdapter(sq1);
            sd.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
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

        private void button6_Click(object sender, EventArgs e)
        {
            Signup signupForm = new Signup();
            signupForm.Show();
            this.Hide();
        }

       
        private void Campaigns()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();


            SqlCommand sq1 = new SqlCommand("SELECT CampaignId, Name, Description, StartdDate, EndDate, Venue, Status FROM CAMPAIGNS", con);


            sq1.Parameters.AddWithValue("@UserId", this.userId);


            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(sq1);
            sd.Fill(dt);

            dataGridView2.DataSource = dt;

            con.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sector_Manage Sector_Manage = new Sector_Manage();
            Sector_Manage.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DonorManagement DonorManagement = new DonorManagement();
            DonorManagement.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VolDonation VolDonation = new VolDonation(userId);
            VolDonation.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Campaign_Details Campaign_Details = new Campaign_Details(userId);
            Campaign_Details.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void VolunteerDashboardForm_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DonorDetails DonorDetails = new DonorDetails(userId);
            DonorDetails.Show();
            this.Hide();
        }
    }
}
