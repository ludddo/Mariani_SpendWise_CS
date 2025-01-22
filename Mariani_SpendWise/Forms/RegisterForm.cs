using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mariani_SpendWise.Forms;
using Mariani_SpendWise.Data;
using Mariani_SpendWise.Utils;

namespace Mariani_SpendWise.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Center elements
            this.labelName.Left = (this.ClientSize.Width - this.labelName.Width) / 2 - 150;
            this.txtName.Left = (this.ClientSize.Width - this.txtName.Width) / 2;
            this.labelEmail.Left = (this.ClientSize.Width - this.labelEmail.Width) / 2 - 150;
            this.txtEmail.Left = (this.ClientSize.Width - this.txtEmail.Width) / 2;
            this.labelPassword.Left = (this.ClientSize.Width - this.labelPassword.Width) / 2 - 150;
            this.txtPassword.Left = (this.ClientSize.Width - this.txtPassword.Width) / 2;
            this.btnRegister.Left = (this.ClientSize.Width - this.btnRegister.Width) / 2;
            this.linkLogin.Left = (this.ClientSize.Width - this.linkLogin.Width) / 2;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validazione input
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Tutti i campi sono obbligatori.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Validator.IsValidEmail(email))
            {
                MessageBox.Show("L'indirizzo email non è valido.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hash della password
            string hashedPassword = PasswordHelper.HashPassword(password);

            // Registrazione utente
            bool isRegistered = UserRepository.RegisterUser(name, email, hashedPassword);

            if (isRegistered)
            {
                MessageBox.Show("Registrazione completata con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Registrazione fallita. Email già in uso.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
