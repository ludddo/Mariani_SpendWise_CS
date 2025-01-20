using System;
using System.Data;
using Mariani_SpendWise.Data;
using MySql.Data.MySqlClient;

namespace Mariani_SpendWise.Data
{
    public static class ExpenseRepository
    {
        public static decimal GetTotalExpenses()
        {
            string query = "SELECT SUM(amount) FROM expenses";
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        public static DataTable GetRecentExpenses()
        {
            string query = "SELECT date, category, amount, description FROM expenses ORDER BY date DESC LIMIT 10";
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static bool AddExpense(DateTime date, string category, decimal amount, string description)
        {
            string query = "INSERT INTO expenses (date, category, amount, description) VALUES (@Date, @Category, @Amount, @Description)";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Description", description);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante l'aggiunta della spesa: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
