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
using Mariani_SpendWise.Models;

namespace Mariani_SpendWise.Forms
{
    public partial class ManageCategoriesForm : Form
    {
        private int userId; // Assicurati di assegnare l'ID utente corrente

        public ManageCategoriesForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadCategories();
        }

        private void LoadCategories()
        {
            List<Category> categories = CategoryRepository.GetCategories(userId);
            lstCategories.DataSource = categories;
            lstCategories.DisplayMember = "Name";
            lstCategories.ValueMember = "Id";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategory.Text.Trim();

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Il nome della categoria non può essere vuoto.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryRepository.AddCategory(categoryName, userId))
            {
                MessageBox.Show("Categoria aggiunta con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategories();
                txtCategory.Clear();
            }
            else
            {
                MessageBox.Show("Errore durante l'aggiunta della categoria. La categoria potrebbe già esistere.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null)
            {
                MessageBox.Show("Seleziona una categoria da modificare.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoryId = (int)lstCategories.SelectedValue;
            string newCategoryName = txtCategory.Text.Trim();

            if (string.IsNullOrWhiteSpace(newCategoryName))
            {
                MessageBox.Show("Il nome della categoria non può essere vuoto.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryRepository.UpdateCategory(categoryId, newCategoryName, userId))
            {
                MessageBox.Show("Categoria modificata con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategories();
                txtCategory.Clear();
            }
            else
            {
                MessageBox.Show("Errore durante la modifica della categoria.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstCategories.SelectedItem == null)
            {
                MessageBox.Show("Seleziona una categoria da eliminare.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoryId = (int)lstCategories.SelectedValue;

            if (CategoryRepository.DeleteCategory(categoryId, userId))
            {
                MessageBox.Show("Categoria eliminata con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Errore durante l'eliminazione della categoria.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
