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

namespace Mariani_SpendWise.Forms
{
    public partial class AddExpenseForm : Form
    {
        public AddExpenseForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void AddExpenseForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadCategories()
        {
            // Carica le categorie dal database
            List<string> categories = CategoryRepository.GetCategories();
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(categories.ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validazione input
            if (cmbCategory.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Per favore compila tutti i campi correttamente.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dati della nuova spesa
            string category = cmbCategory.SelectedItem.ToString();
            DateTime date = dtpDate.Value;
            string description = txtDescription.Text;

            // Salvataggio nel database
            bool success = ExpenseRepository.AddExpense(date, category, amount, description);

            if (success)
            {
                MessageBox.Show("Spesa aggiunta con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Chiude il form dopo il salvataggio
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
