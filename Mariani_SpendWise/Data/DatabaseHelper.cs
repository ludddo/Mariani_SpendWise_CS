using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mariani_SpendWise.Data
{
    internal class DatabaseHelper
    {
        private static string connectionString = "server=localhost;database=spendwise;uid=root;pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                    // Puoi loggare l'errore se necessario
                    MessageBox.Show($"Errore di connessione: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
