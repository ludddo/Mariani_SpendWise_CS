using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mariani_SpendWise.Data;

namespace Mariani_SpendWise.Forms
{
    public partial class DashboardForm : Form
    {
        private int userId;

        public DashboardForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadDashboard();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadDashboard()
        {
            // Riepilogo delle spese totali
            lblTotalExpenses.Text = $"Totale Spese: {ExpenseRepository.GetTotalExpenses(userId):C}";

            // Caricamento spese recenti
            dgvRecentExpenses.DataSource = ExpenseRepository.GetRecentExpenses(userId);

            // (Opzionale) Caricamento grafico
            LoadExpenseChart();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            AddExpenseForm addExpenseForm = new AddExpenseForm(userId);
            addExpenseForm.ShowDialog();
            LoadDashboard(); // Ricarica il dashboard dopo aver aggiunto una spesa
        }

        private void btnManageCategories_Click(object sender, EventArgs e)
        {
            ManageCategoriesForm manageCategoriesForm = new ManageCategoriesForm(userId);
            manageCategoriesForm.ShowDialog();
        }

        private void LoadExpenseChart()
        {
            // (Opzionale) Logica per caricare il grafico delle spese mensili
        }
    }
}
