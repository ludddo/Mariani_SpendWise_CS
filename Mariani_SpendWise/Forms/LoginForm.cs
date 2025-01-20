using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariani_SpendWise.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Center elements
            this.labelEmail.Left = (this.ClientSize.Width - this.labelEmail.Width) / 2 - 150;
            this.txtEmail.Left = (this.ClientSize.Width - this.txtEmail.Width) / 2;
            this.labelPassword.Left = (this.ClientSize.Width - this.labelPassword.Width) / 2 - 150;
            this.txtPassword.Left = (this.ClientSize.Width - this.txtPassword.Width) / 2;
            this.btnLogin.Left = (this.ClientSize.Width - this.btnLogin.Width) / 2;
            this.linkRegister.Left = (this.ClientSize.Width - this.linkRegister.Width) / 2;

        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
