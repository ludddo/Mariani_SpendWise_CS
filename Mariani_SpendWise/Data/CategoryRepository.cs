using System;
using System.Collections.Generic;
using Mariani_SpendWise.Data;
using MySql.Data.MySqlClient;
using Mariani_SpendWise.Models;

namespace Mariani_SpendWise.Data
{
    public static class CategoryRepository
    {
        public static List<Category> GetCategories(int userId)
        {
            string query = "SELECT id, name FROM categories WHERE user_id = @UserId";
            var categories = new List<Category>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new Category
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        };
                        categories.Add(category);
                    }
                }
            }
            return categories;
        }

        public static bool AddCategory(string category, int userId)
        {
            string query = "INSERT INTO categories (name, user_id) VALUES (@Category, @UserId)";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@UserId", userId);

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

        public static bool UpdateCategory(int categoryId, string newCategoryName, int userId)
        {
            string query = "UPDATE categories SET name = @NewCategory WHERE id = @CategoryId AND user_id = @UserId";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NewCategory", newCategoryName);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@UserId", userId);

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

        public static bool DeleteCategory(int categoryId, int userId)
        {
            string query = "DELETE FROM categories WHERE id = @CategoryId AND user_id = @UserId";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@UserId", userId);

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
