using Mariani_SpendWise.Data;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mariani_SpendWise.Models;

namespace Mariani_SpendWise.Forms
{
    public partial class AddExpenseForm : Form
    {
        private int userId; // Assicurati di assegnare l'ID utente corrente

        public AddExpenseForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadCategories();
        }

        private void LoadCategories()
        {
            List<Category> categories = CategoryRepository.GetCategories(userId);
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Per favore compila tutti i campi correttamente.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoryId = (int)cmbCategory.SelectedValue;
            DateTime date = dtpDate.Value;
            string description = txtDescription.Text;

            bool success = ExpenseRepository.AddExpense(date, categoryId, amount, description, userId);

            if (success)
            {
                MessageBox.Show("Spesa aggiunta con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Errore durante l'aggiunta della spesa.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Chiude il form
        }
    }
}
