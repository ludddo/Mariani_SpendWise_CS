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
    }
}
