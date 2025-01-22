using System;
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

        public static bool AddCategory(string category)
        {
            string query = "INSERT INTO categories (name) VALUES (@Category)";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Category", category);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante l'aggiunta della categoria: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool UpdateCategory(string oldCategory, string newCategory)
        {
            string query = "UPDATE categories SET name = @NewCategory WHERE name = @OldCategory";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OldCategory", oldCategory);
                cmd.Parameters.AddWithValue("@NewCategory", newCategory);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante la modifica della categoria: {ex.Message}");
                    return false;
                }
            }
        }

        public static bool DeleteCategory(string category)
        {
            string query = "DELETE FROM categories WHERE name = @Category";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Category", category);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante l'eliminazione della categoria: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
