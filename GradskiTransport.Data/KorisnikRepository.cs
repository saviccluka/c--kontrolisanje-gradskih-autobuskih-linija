using Microsoft.Data.SqlClient;
using GradskiTransport.Shared.Constants;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Data
{
    public class KorisnikRepository
    {
        private readonly string connectionString;

        public KorisnikRepository()
        {
            connectionString = ApplicationConstants.CONNECTION_STRING;
        }

        public async Task<Korisnik?> GetKorisnikByUsernameAsync(string username)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_korisnika, ime, prezime, username, password, uloga FROM KORISNICI WHERE username = @username";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Korisnik
                {
                    Id = (int)reader["id_korisnika"],
                    Ime = reader["ime"].ToString() ?? "",
                    Prezime = reader["prezime"].ToString() ?? "",
                    Username = reader["username"].ToString() ?? "",
                    Password = reader["password"].ToString() ?? "",
                    Uloga = reader["uloga"].ToString() ?? ""
                };
            }

            return null;
        }

        public async Task<List<Korisnik>> GetAllKorisniciAsync()
        {
            var korisnici = new List<Korisnik>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_korisnika, ime, prezime, username, password, uloga FROM KORISNICI";
            using var command = new SqlCommand(query, connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                korisnici.Add(new Korisnik
                {
                    Id = (int)reader["id_korisnika"],
                    Ime = reader["ime"].ToString() ?? "",
                    Prezime = reader["prezime"].ToString() ?? "",
                    Username = reader["username"].ToString() ?? "",
                    Password = reader["password"].ToString() ?? "",
                    Uloga = reader["uloga"].ToString() ?? ""
                });
            }

            return korisnici;
        }

        public async Task<bool> AddKorisnikAsync(Korisnik korisnik)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "INSERT INTO KORISNICI (ime, prezime, username, password, uloga) VALUES (@ime, @prezime, @username, @password, @uloga)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ime", korisnik.Ime);
            command.Parameters.AddWithValue("@prezime", korisnik.Prezime);
            command.Parameters.AddWithValue("@username", korisnik.Username);
            command.Parameters.AddWithValue("@password", korisnik.Password);
            command.Parameters.AddWithValue("@uloga", korisnik.Uloga);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateKorisnikAsync(Korisnik korisnik)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "UPDATE KORISNICI SET ime = @ime, prezime = @prezime, username = @username, password = @password, uloga = @uloga WHERE id_korisnika = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", korisnik.Id);
            command.Parameters.AddWithValue("@ime", korisnik.Ime);
            command.Parameters.AddWithValue("@prezime", korisnik.Prezime);
            command.Parameters.AddWithValue("@username", korisnik.Username);
            command.Parameters.AddWithValue("@password", korisnik.Password);
            command.Parameters.AddWithValue("@uloga", korisnik.Uloga);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteKorisnikAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM KORISNICI WHERE id_korisnika = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
