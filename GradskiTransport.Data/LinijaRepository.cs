using Microsoft.Data.SqlClient;
using GradskiTransport.Shared.Constants;
using GradskiTransport.Shared.Models;

namespace GradskiTransport.Data
{
    public class LinijaRepository
    {
        private readonly string connectionString;

        public LinijaRepository()
        {
            connectionString = ApplicationConstants.CONNECTION_STRING;
        }

        public async Task<List<Linija>> GetAllLinijeAsync()
        {
            var linije = new List<Linija>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_linije, broj_linije, naziv_linije, opis FROM LINIJE ORDER BY broj_linije";
            using var command = new SqlCommand(query, connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                linije.Add(new Linija
                {
                    Id = (int)reader["id_linije"],
                    BrojLinije = reader["broj_linije"].ToString() ?? "",
                    Naziv = reader["naziv_linije"].ToString() ?? "",
                    Opis = reader["opis"] == DBNull.Value ? null : reader["opis"].ToString()
                });
            }

            return linije;
        }

        public async Task<Linija?> GetLinijaByIdAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_linije, broj_linije, naziv_linije, opis FROM LINIJE WHERE id_linije = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Linija
                {
                    Id = (int)reader["id_linije"],
                    BrojLinije = reader["broj_linije"].ToString() ?? "",
                    Naziv = reader["naziv_linije"].ToString() ?? "",
                    Opis = reader["opis"] == DBNull.Value ? null : reader["opis"].ToString()
                };
            }

            return null;
        }

        public async Task<List<Linija>> GetLinijeByBrojAsync(string brojLinije)
        {
            var linije = new List<Linija>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_linije, broj_linije, naziv_linije, opis FROM LINIJE WHERE broj_linije LIKE @broj ORDER BY broj_linije";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@broj", $"%{brojLinije}%");

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                linije.Add(new Linija
                {
                    Id = (int)reader["id_linije"],
                    BrojLinije = reader["broj_linije"].ToString() ?? "",
                    Naziv = reader["naziv_linije"].ToString() ?? "",
                    Opis = reader["opis"] == DBNull.Value ? null : reader["opis"].ToString()
                });
            }

            return linije;
        }

        public async Task<List<Polazak>> GetPolasciByLinijaIdAsync(int linijaId)
        {
            var polasci = new List<Polazak>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_polaska, linija_id, vreme_polaska, tip_vozila FROM POLASCI WHERE linija_id = @linijaId ORDER BY vreme_polaska";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@linijaId", linijaId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                polasci.Add(new Polazak
                {
                    Id = (int)reader["id_polaska"],
                    LinijaId = (int)reader["linija_id"],
                    VremePolaska = (TimeSpan)reader["vreme_polaska"],
                    TipVozila = reader["tip_vozila"].ToString() ?? ""
                });
            }

            return polasci;
        }

        public async Task<bool> AddLinijaAsync(Linija linija)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "INSERT INTO LINIJE (broj_linije, naziv_linije, opis) VALUES (@broj, @naziv, @opis)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@broj", linija.BrojLinije);
            command.Parameters.AddWithValue("@naziv", linija.Naziv);
            command.Parameters.AddWithValue("@opis", linija.Opis ?? (object)DBNull.Value);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateLinijaAsync(Linija linija)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "UPDATE LINIJE SET broj_linije = @broj, naziv_linije = @naziv, opis = @opis WHERE id_linije = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", linija.Id);
            command.Parameters.AddWithValue("@broj", linija.BrojLinije);
            command.Parameters.AddWithValue("@naziv", linija.Naziv);
            command.Parameters.AddWithValue("@opis", linija.Opis ?? (object)DBNull.Value);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteLinijaAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM LINIJE WHERE id_linije = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}