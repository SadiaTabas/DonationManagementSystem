using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donation_Management_System
{
    public partial class Login: Form
    {
        private int  userId;

        public Login()
        {
            InitializeComponent();
            this.linkLabel1.Text = "Forgot Password?";
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);

            

     
     this.textBox3.Enter += new EventHandler(textBox3_Enter);
    this.textBox3.Leave += new EventHandler(textBox3_Leave);
    this.textBox2.Enter += new EventHandler(textBox2_Enter);
    this.textBox2.Leave += new EventHandler(textBox2_Leave);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Please contact the system administrator to reset your password.",
                "Forgot Password",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void Login_Load(object sender, EventArgs e)
        {
        
            
            textBox3.Text = "Enter your username";
            textBox3.ForeColor = Color.Gray;

            
            textBox2.Text = "Enter your password";
            textBox2.ForeColor = Color.Gray;
            textBox2.UseSystemPasswordChar = false;
             
        }

   

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedRole = "";
            if (comboBox2.SelectedItem != null)
            {
                selectedRole = comboBox2.SelectedItem.ToString();
            }

            string username = textBox3.Text;
            string password = textBox2.Text;

            
            if (string.IsNullOrEmpty(username) || username == "Enter your username")
            {
                MessageBox.Show("Please enter your username.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

           
            if (string.IsNullOrEmpty(password) || password == "Enter your password")
            {
                MessageBox.Show("Please enter your password.", "Missing Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

             
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include uppercase, lowercase, digit, and special character.",
                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

             
            if (string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please select a role from the list.", "Role Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox2.Focus();
                return;
            }

            int userId = 0;
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT UserId FROM USSER WHERE Name = @username AND password = @password AND Role = @Role", con);
            cmd.Parameters.AddWithValue("@username", textBox3.Text.Trim());
            cmd.Parameters.AddWithValue("@password", textBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@Role", comboBox2.Text.Trim());

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                userId = reader.GetInt32(0);   
            }
            else
            {
                MessageBox.Show("Login failed. Invalid credentials or role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reader.Close();
                con.Close();
                return;
            }

            reader.Close();
            con.Close();



            bool isUserSignedUp = CheckIfUserExists();

            if (selectedRole == "Admin")
            {
                Dash adminForm = new Dash();
                adminForm.Show();
                this.Hide();
            }
            else if (selectedRole == "Volunteer")
            {
                VolunteerDashboardForm volunteerForm = new VolunteerDashboardForm(userId);
                volunteerForm.Show();
                this.Hide();
            }
            else if (selectedRole == "Donor")
            {
                if (isUserSignedUp)
                {
                    DonerDashboardForm donerForm = new DonerDashboardForm( userId);
                    donerForm.Show();
                    this.Hide();
                }
                else
                {
                    Signup signupForm = new Signup();
                    signupForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please select a valid role from the list.", "Role Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

             

              
            }






        private bool IsValidPassword(string password)
        {
           
            if (password.Length < 8)
            {
                return false;
            }

             
            bool foundUpper = false;
            foreach (char ch in password)
            {
                if (char.IsUpper(ch))
                {
                    foundUpper = true;
                    break;  
                }
            }
            if (!foundUpper)
            {
                return false;
            }

             
            bool foundLower = false;
            foreach (char ch in password)
            {
                if (char.IsLower(ch))
                {
                    foundLower = true;
                    break;
                }
            }
            if (!foundLower)
            {
                return false;
            }

             
            bool foundDigit = false;
            foreach (char ch in password)
            {
                if (char.IsDigit(ch))
                {
                    foundDigit = true;
                    break;
                }
            }
            if (!foundDigit)
            {
                return false;
            }

             
            bool foundSpecial = false;
            foreach (char ch in password)
            {
                if (!char.IsLetterOrDigit(ch))
                {
                    foundSpecial = true;
                    break;
                }
            }
            if (!foundSpecial)
            {
                return false;
            }

            
            return true;
        }


        private bool CheckIfUserExists()
        {
             
            return true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter your username")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Enter your username";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter your password")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.PasswordChar = '●';

;  
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Enter your password";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                Application.Exit();

            }


        }

        

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
             
            Signup signupForm = new Signup();
            signupForm.Show();
            this.Hide();  
        }

    }
}

