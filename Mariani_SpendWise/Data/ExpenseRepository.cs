using System;
using System.Data;
using Mariani_SpendWise.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Mariani_SpendWise.Data
{
    public static class ExpenseRepository
    {
        public static decimal GetTotalExpenses(int userId)
        {
            string query = "SELECT SUM(amount) FROM expenses WHERE user_id = @UserId";
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        public static DataTable GetRecentExpenses(int userId)
        {
            string query = @"SELECT e.date, c.name AS category, e.amount, e.description 
                         FROM expenses e 
                         JOIN categories c ON e.category_id = c.id 
                         WHERE e.user_id = @UserId 
                         ORDER BY e.date DESC LIMIT 10";
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static bool AddExpense(DateTime date, int categoryId, decimal amount, string description, int userId)
        {
            string query = "INSERT INTO expenses (date, category_id, amount, description, user_id) VALUES (@Date, @CategoryId, @Amount, @Description, @UserId)";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@UserId", userId);

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

        public static List<ExpenseByCategory> GetExpensesByCategory(int userId)
        {
            string query = @"SELECT c.name AS Category, SUM(e.amount) AS Amount
                             FROM expenses e
                             JOIN categories c ON e.category_id = c.id
                             WHERE e.user_id = @UserId
                             GROUP BY c.name";

            var expensesByCategory = new List<ExpenseByCategory>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expensesByCategory.Add(new ExpenseByCategory
                        {
                            Category = reader.GetString("Category"),
                            Amount = reader.GetDecimal("Amount")
                        });
                    }
                }
            }

            return expensesByCategory;
        }
    }
    public class ExpenseByCategory
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
