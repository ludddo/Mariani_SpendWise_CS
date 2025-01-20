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
    public partial class ManageCategoriesForm : Form
    {
        public ManageCategoriesForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            // Carica le categorie dal database
            List<string> categories = CategoryRepository.GetCategories();
            lstCategories.Items.Clear();
            lstCategories.Items.AddRange(categories.ToArray());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string category = txtCategory.Text.Trim();

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Il nome della categoria non può essere vuoto.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryRepository.AddCategory(category))
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

            string oldCategory = lstCategories.SelectedItem.ToString();
            string newCategory = txtCategory.Text.Trim();

            if (string.IsNullOrWhiteSpace(newCategory))
            {
                MessageBox.Show("Il nome della categoria non può essere vuoto.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CategoryRepository.UpdateCategory(oldCategory, newCategory))
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

            string category = lstCategories.SelectedItem.ToString();

            if (CategoryRepository.DeleteCategory(category))
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
