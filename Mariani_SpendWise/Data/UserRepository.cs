using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mariani_SpendWise.Utils;

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
            string query = "SELECT password FROM users WHERE email = @Email";

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPassword = reader.GetString("password");
                            return PasswordHelper.VerifyPassword(password, hashedPassword);
                        }
                        return false;
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
