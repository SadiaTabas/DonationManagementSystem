using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donation_Management_System
{
    public partial class Campaign_Details: Form
    {
        private int userId;
        public Campaign_Details(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            VolunteerDashboardForm volunteerDashboard = new VolunteerDashboardForm(userId);
            volunteerDashboard.Show();
            this.Close();
        }
    }
}
