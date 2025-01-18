using Mariani_SpendWise.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariani_SpendWise
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Center elements
            this.welcomeLabel.Left = (this.ClientSize.Width - this.welcomeLabel.Width) / 2;
            this.descriptionLabel.Left = (this.ClientSize.Width - this.descriptionLabel.Width) / 2;
            this.startButton.Left = (this.ClientSize.Width - this.startButton.Width) / 2;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Open RegisterForm
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
