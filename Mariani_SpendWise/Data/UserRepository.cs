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

        public static int Authenticate(string email, string password)
        {
            string query = "SELECT id, password FROM users WHERE email = @Email";

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
                            int userId = reader.GetInt32("id");
                            string hashedPassword = reader.GetString("password");

                            if (PasswordHelper.VerifyPassword(password, hashedPassword))
                            {
                                return userId; // Autenticazione riuscita, ritorna l'userId
                            }
                        }
                        return -1; // Autenticazione fallita
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Errore durante l'autenticazione: {ex.Message}");
                    return -1;
                }
            }
        }
    }
}
