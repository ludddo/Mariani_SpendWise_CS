using Mariani_SpendWise.Data;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validazione input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Inserisci email e password.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica credenziali
            int userId = UserRepository.Authenticate(email, password);

            if (userId != -1)
            {
                MessageBox.Show("Accesso effettuato con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Apri la dashboard e passa l'userId
                DashboardForm dashboardForm = new DashboardForm(userId);
                dashboardForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email o password errati.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
