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
    public partial class Signup: Form
    {
        public Signup()
        {
             
            InitializeComponent();
            this.Load += Signup_Load;

            // Full Name
            textBox1.Enter += new EventHandler(textBox1_Enter);
            textBox1.Leave += new EventHandler(textBox1_Leave);

            // Email
            textBox2.Enter += new EventHandler(textBox2_Enter);
            textBox2.Leave += new EventHandler(textBox2_Leave);

            // Username
            textBox3.Enter += new EventHandler(textBox3_Enter);
            textBox3.Leave += new EventHandler(textBox3_Leave);

            // Password
            textBox4.Enter += new EventHandler(textBox4_Enter);
            textBox4.Leave += new EventHandler(textBox4_Leave);

            // Confirm Password
            textBox5.Enter += new EventHandler(textBox5_Enter);
            textBox5.Leave += new EventHandler(textBox5_Leave);

            // Phone Number
            textBox6.Enter += new EventHandler(textBox6_Enter);
            textBox6.Leave += new EventHandler(textBox6_Leave);

            // Address
            textBox7.Enter += new EventHandler(textBox7_Enter);
            textBox7.Leave += new EventHandler(textBox7_Leave); 

        }

             
        
        private void Signup_Load(object sender, EventArgs e)
        {

            textBox1.Text = "Enter your Full Name";
            textBox1.ForeColor = Color.Gray;

            textBox2.Text = "Enter your Email";
            textBox2.ForeColor = Color.Gray;

            textBox3.Text = "Enter your Username";
            textBox3.ForeColor = Color.Gray;

            textBox4.Text = "Enter Password";
            textBox4.ForeColor = Color.Gray;
            textBox4.UseSystemPasswordChar = false;

            textBox5.Text = "Confirm Password";
            textBox5.ForeColor = Color.Gray;
            textBox5.UseSystemPasswordChar = false;

            textBox6.Text = "Enter Phone Number";
            textBox6.ForeColor = Color.Gray;

            textBox7.Text = "Enter Address";
            textBox7.ForeColor = Color.Gray;
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




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             

        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter your Full Name")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Enter your Full Name";
                textBox1.ForeColor = Color.Gray;
            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter your Email")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Enter your Email";
                textBox2.ForeColor = Color.Gray;
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter your Username")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Enter your Username";
                textBox3.ForeColor = Color.Gray;
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Enter Password")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "Enter Password";
                textBox4.ForeColor = Color.Gray;
                textBox4.UseSystemPasswordChar = false;
            }
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Confirm Password")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
                textBox5.UseSystemPasswordChar = true;
            }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Enter Phone Number")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "Enter Phone Number";
                textBox6.ForeColor = Color.Gray;
            }
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Enter Address")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox7.Text = "Enter Address";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "Confirm Password";
                textBox5.ForeColor = Color.Gray;
                textBox5.UseSystemPasswordChar = false;
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //email
        }
       
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //user name
        }
        
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
             //password
        }
        


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //confirm password
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //phone no
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //address
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Enter your Full Name" || textBox1.Text == "")
            {
                MessageBox.Show("Please enter your full name.");
                return;
            }


            if (textBox2.Text == "Enter your Email" || textBox2.Text == "")
            {
                MessageBox.Show("Please enter your email.");
                return;
            }

            bool symbol = false;
            foreach (char ch in textBox2.Text)
            {
                if (ch == '@')
                {
                    symbol = true;
                    break;
                }
            }

            if (!textBox2.Text.EndsWith("@gmail.com") || !symbol)
            {
                MessageBox.Show("Email must be a valid Gmail address ending with @gmail.com.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (textBox3.Text == "Enter your Username" || textBox3.Text == "")
            {
                MessageBox.Show("Please enter your username.");
                return;
            }

            if (textBox4.Text == "Enter Password" || textBox4.Text == "")
            {
                MessageBox.Show("Please enter your password.");
                return;
            }

            if (!IsValidPassword(textBox4.Text))
            {
                MessageBox.Show("Password must be at least 8 characters long and include uppercase, lowercase, digit, and special character.",
                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox5.Text == "Confirm Password" || textBox5.Text == "")
            {
                MessageBox.Show("Please confirm your password.");
                return;
            }

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Passwords do not match. Please re-enter.");
                return;
            }

            if (textBox6.Text == "Enter Phone Number" || textBox6.Text == "")
            {
                MessageBox.Show("Please enter your phone number.");
                return;
            }

            foreach (char c in textBox6.Text)
            {
                if (!char.IsDigit(c))
                {
                    MessageBox.Show("Phone number must contain only digits (0–9).", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (textBox7.Text == "Enter Address" || textBox7.Text == "")
            {
                MessageBox.Show("Please enter your address.");
                return;
            }

            if (!checkBox1.Checked)
            {
                MessageBox.Show("You must agree to the terms and conditions to sign up.", "Terms and Conditions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  
            }

            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Donation_Management_System;Integrated Security=True;");

           
            con.Open();

            try
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO USSER (Name, Gender, DateOfBirth, Email, PhoneNo, Address, password, UserStatus, SecurityAns, Role, FullName) " +
                    "VALUES (@Name, @Gender, @DateOfBirth, @Email, @PhoneNo, @Address, @password, @UserStatus, @SecurityAns, @Role, @FullName)", con);

                command.Parameters.AddWithValue("@Name", textBox3.Text);
                command.Parameters.AddWithValue("@Gender", comboBox1.Text);
                command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
                command.Parameters.AddWithValue("@Email", textBox2.Text);
                command.Parameters.AddWithValue("@PhoneNo", textBox6.Text);
                command.Parameters.AddWithValue("@Address", textBox7.Text);
                command.Parameters.AddWithValue("@password", textBox4.Text);
                command.Parameters.AddWithValue("@UserStatus", true);
                command.Parameters.AddWithValue("@SecurityAns", "DefaultAnswer");
                command.Parameters.AddWithValue("@Role", "Donor");
                command.Parameters.AddWithValue("@FullName", textBox1.Text);

                command.ExecuteNonQuery();

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Login login_Form = new Login();
                login_Form.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }





        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

         

    }
}

