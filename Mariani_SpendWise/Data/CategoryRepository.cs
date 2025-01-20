using System.Collections.Generic;
using Mariani_SpendWise.Data;
using MySql.Data.MySqlClient;

namespace Mariani_SpendWise.Data
{
    public static class CategoryRepository
    {
        public static List<string> GetCategories()
        {
            string query = "SELECT name FROM categories";
            var categories = new List<string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(0));
                    }
                }
            }
            return categories;
        }
    }
}
