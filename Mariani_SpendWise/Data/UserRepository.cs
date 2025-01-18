using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Mariani_SpendWise.Data
{
    internal class UserRepository
    {
        public static bool RegisterUser(string name, string email, string hashedPassword)
        {
            string query = "INSERT INTO users (name, email, password) VALUES (@Name, @Email, @Password)";

            using (var conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) // Codice errore per chiave duplicata
                    {
                        return false;
                    }
                    throw;
                }
            }
        }

        public static bool Authenticate(string email, string password)
        {
            string query = "SELECT * FROM users WHERE email = @Email AND password = @Password";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Read(); // True se le credenziali sono corrette
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante l'autenticazione: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
