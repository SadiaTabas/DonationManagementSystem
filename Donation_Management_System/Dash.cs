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
    public partial class Dash : Form
    {
        private int userId;
        public Dash()
        {
            InitializeComponent();
            TotalDonations();
            ActiveCampaigns();
            TotalDonors();
            TodaysDonations();
            DonationsBySector();
            Feedback();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DonorManagement DonorManagementForm = new DonorManagement();
            DonorManagementForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VolunteerManagement VolunteerManagementForm = new VolunteerManagement();
            VolunteerManagementForm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             Donation_Record Donation_RecordFrom = new Donation_Record();
            Donation_RecordFrom.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminDetails AdminDetailsFrom = new AdminDetails();
            AdminDetailsFrom.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sector_Details Sector_DetailsFrom = new Sector_Details();
            Sector_DetailsFrom.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FeedBack FeedBackForm = new FeedBack();
            FeedBackForm.Show();
            this.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {
             
        }

        private void ADMIN_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Fe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Dash_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            VolunteerDetails VolunteerDetailsForm = new VolunteerDetails();
            VolunteerDetailsForm.Show();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
             

            DialogResult result = MessageBox.Show(

                "Are you sure you want to Logout?",

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

        private void button16_Click(object sender, EventArgs e)
        {
            Campaigns_manage Campaigns_manageForm = new Campaigns_manage();
            Campaigns_manageForm.Show();
            this.Close();
        }

         
        private void  TotalDonations()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) AS TotalDonation FROM DONATIONS", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView3.DataSource = dt;

            con.Close();
        }


        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void  ActiveCampaigns()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM CAMPAIGNS WHERE Status = 'Active'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView4.DataSource = dt;

            con.Close();
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void  TotalDonors()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT UserId) AS TotalDonors FROM   DONATIONS", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView2.DataSource = dt;

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void  TodaysDonations()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM DONATIONS WHERE DonationDate = CONVERT(date, GETDATE())", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void  DonationsBySector()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            S.Title AS Sector,
            SUM(D.Amount) AS TotalDonated
        FROM 
            DONATIONS D
        INNER JOIN 
            SECTORS S ON D.SectorId = S.SectorId
        GROUP BY 
            S.Title
        ORDER BY 
            TotalDonated DESC
    ", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView5.DataSource = dt;

            con.Close();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void  Feedback()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            F.FeedbackId,
            U.FullName,
            F.Comment,
            F.SubmissionDate
        FROM 
            FEEDBACK F
        INNER JOIN 
            USSER U ON F.UserId = U.UserId
        ORDER BY 
            F.SubmissionDate DESC
    ", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView6.DataSource = dt;

            con.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Sector_Manage Sector_ManageForm = new Sector_Manage();
            Sector_ManageForm.Show();
            this.Close();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Campaign_Details  Campaign_DetailsForm = new Campaign_Details(userId);
            Campaign_DetailsForm.Show();
            this.Close();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Update_Profile Update_ProfileForm = new Update_Profile(userId);
            Update_ProfileForm.Show();
            this.Close();
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            Admin_View Admin_ViewForm = new Admin_View();
            Admin_ViewForm.Show();
            this.Close();
        }
    }

}
