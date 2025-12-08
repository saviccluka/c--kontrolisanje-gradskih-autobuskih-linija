using Microsoft.Data.SqlClient;
using GradskiTransport.Shared.Constants;
using GradskiTransport.Shared.Models;
using GradskiTransport.Shared.Enums;

namespace GradskiTransport.Data
{
    public class KartaRepository
    {
        private readonly string connectionString;

        public KartaRepository()
        {
            connectionString = ApplicationConstants.CONNECTION_STRING;
        }

        public async Task<List<KartaDto>> GetAllKarteAsync()
        {
            var karte = new List<KartaDto>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT k.id_karte, k.putnik_id, k.tip_karte, k.status_karte, k.datum_kupovine, k.datum_vazenja, k.cena, k.broj_karte,
                       p.ime, p.prezime, p.email, p.telefon
                FROM KARTE k
                INNER JOIN PUTNICI p ON k.putnik_id = p.id_putnika
                ORDER BY k.datum_kupovine DESC";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                karte.Add(new KartaDto
                {
                    Id = (int)reader["id_karte"],
                    PutnikId = (int)reader["putnik_id"],
                    Tip = GetTipKarteFromString(reader["tip_karte"].ToString() ?? ""),
                    Status = GetStatusKarteFromString(reader["status_karte"].ToString() ?? ""),
                    DatumKupovine = (DateTime)reader["datum_kupovine"],
                    DatumVazenja = (DateTime)reader["datum_vazenja"],
                    Cena = (decimal)reader["cena"],
                    BrojKarte = reader["broj_karte"].ToString() ?? "",
                    PutnikIme = reader["ime"].ToString() ?? "",
                    PutnikPrezime = reader["prezime"].ToString() ?? "",
                    PutnikEmail = reader["email"].ToString() ?? "",
                    PutnikTelefon = reader["telefon"].ToString() ?? ""
                });
            }

            return karte;
        }

        public async Task<KartaDto?> GetKartaByIdAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT k.id_karte, k.putnik_id, k.tip_karte, k.status_karte, k.datum_kupovine, k.datum_vazenja, k.cena, k.broj_karte,
                       p.ime, p.prezime, p.email, p.telefon
                FROM KARTE k
                INNER JOIN PUTNICI p ON k.putnik_id = p.id_putnika
                WHERE k.id_karte = @id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new KartaDto
                {
                    Id = (int)reader["id_karte"],
                    PutnikId = (int)reader["putnik_id"],
                    Tip = GetTipKarteFromString(reader["tip_karte"].ToString() ?? ""),
                    Status = GetStatusKarteFromString(reader["status_karte"].ToString() ?? ""),
                    DatumKupovine = (DateTime)reader["datum_kupovine"],
                    DatumVazenja = (DateTime)reader["datum_vazenja"],
                    Cena = (decimal)reader["cena"],
                    BrojKarte = reader["broj_karte"].ToString() ?? "",
                    PutnikIme = reader["ime"].ToString() ?? "",
                    PutnikPrezime = reader["prezime"].ToString() ?? "",
                    PutnikEmail = reader["email"].ToString() ?? "",
                    PutnikTelefon = reader["telefon"].ToString() ?? ""
                };
            }

            return null;
        }

        public async Task<KartaDto?> GetKartaByBrojAsync(string brojKarte)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT k.id_karte, k.putnik_id, k.tip_karte, k.status_karte, k.datum_kupovine, k.datum_vazenja, k.cena, k.broj_karte,
                       p.ime, p.prezime, p.email, p.telefon
                FROM KARTE k
                INNER JOIN PUTNICI p ON k.putnik_id = p.id_putnika
                WHERE k.broj_karte = @brojKarte";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@brojKarte", brojKarte);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new KartaDto
                {
                    Id = (int)reader["id_karte"],
                    PutnikId = (int)reader["putnik_id"],
                    Tip = GetTipKarteFromString(reader["tip_karte"].ToString() ?? ""),
                    Status = GetStatusKarteFromString(reader["status_karte"].ToString() ?? ""),
                    DatumKupovine = (DateTime)reader["datum_kupovine"],
                    DatumVazenja = (DateTime)reader["datum_vazenja"],
                    Cena = (decimal)reader["cena"],
                    BrojKarte = reader["broj_karte"].ToString() ?? "",
                    PutnikIme = reader["ime"].ToString() ?? "",
                    PutnikPrezime = reader["prezime"].ToString() ?? "",
                    PutnikEmail = reader["email"].ToString() ?? "",
                    PutnikTelefon = reader["telefon"].ToString() ?? ""
                };
            }

            return null;
        }

        public async Task<List<KartaDto>> GetKarteByPutnikIdAsync(int putnikId)
        {
            var karte = new List<KartaDto>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT k.id_karte, k.putnik_id, k.tip_karte, k.status_karte, k.datum_kupovine, k.datum_vazenja, k.cena, k.broj_karte,
                       p.ime, p.prezime, p.email, p.telefon
                FROM KARTE k
                INNER JOIN PUTNICI p ON k.putnik_id = p.id_putnika
                WHERE k.putnik_id = @putnikId
                ORDER BY k.datum_kupovine DESC";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@putnikId", putnikId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                karte.Add(new KartaDto
                {
                    Id = (int)reader["id_karte"],
                    PutnikId = (int)reader["putnik_id"],
                    Tip = GetTipKarteFromString(reader["tip_karte"].ToString() ?? ""),
                    Status = GetStatusKarteFromString(reader["status_karte"].ToString() ?? ""),
                    DatumKupovine = (DateTime)reader["datum_kupovine"],
                    DatumVazenja = (DateTime)reader["datum_vazenja"],
                    Cena = (decimal)reader["cena"],
                    BrojKarte = reader["broj_karte"].ToString() ?? "",
                    PutnikIme = reader["ime"].ToString() ?? "",
                    PutnikPrezime = reader["prezime"].ToString() ?? "",
                    PutnikEmail = reader["email"].ToString() ?? "",
                    PutnikTelefon = reader["telefon"].ToString() ?? ""
                });
            }

            return karte;
        }

        public async Task<bool> AddKartaAsync(KartaDto karta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                INSERT INTO KARTE (putnik_id, tip_karte, status_karte, datum_kupovine, datum_vazenja, cena, broj_karte)
                VALUES (@putnikId, @tip, @status, @datumKupovine, @datumVazenja, @cena, @brojKarte)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@putnikId", karta.PutnikId);
            command.Parameters.AddWithValue("@tip", GetTipKarteAsString(karta.Tip));
            command.Parameters.AddWithValue("@status", GetStatusKarteAsString(karta.Status));
            command.Parameters.AddWithValue("@datumKupovine", karta.DatumKupovine);
            command.Parameters.AddWithValue("@datumVazenja", karta.DatumVazenja);
            command.Parameters.AddWithValue("@cena", karta.Cena);
            command.Parameters.AddWithValue("@brojKarte", karta.BrojKarte);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateKartaAsync(KartaDto karta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                UPDATE KARTE 
                SET putnik_id = @putnikId, tip_karte = @tip, status_karte = @status, 
                    datum_kupovine = @datumKupovine, datum_vazenja = @datumVazenja, 
                    cena = @cena, broj_karte = @brojKarte
                WHERE id_karte = @id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", karta.Id);
            command.Parameters.AddWithValue("@putnikId", karta.PutnikId);
            command.Parameters.AddWithValue("@tip", GetTipKarteAsString(karta.Tip));
            command.Parameters.AddWithValue("@status", GetStatusKarteAsString(karta.Status));
            command.Parameters.AddWithValue("@datumKupovine", karta.DatumKupovine);
            command.Parameters.AddWithValue("@datumVazenja", karta.DatumVazenja);
            command.Parameters.AddWithValue("@cena", karta.Cena);
            command.Parameters.AddWithValue("@brojKarte", karta.BrojKarte);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteKartaAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM KARTE WHERE id_karte = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<PutnikDto>> GetAllPutniciAsync()
        {
            var putnici = new List<PutnikDto>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT id_putnika, ime, prezime, email, telefon, datum_registracije FROM PUTNICI ORDER BY ime, prezime";
            using var command = new SqlCommand(query, connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                putnici.Add(new PutnikDto
                {
                    Id = (int)reader["id_putnika"],
                    Ime = reader["ime"].ToString() ?? "",
                    Prezime = reader["prezime"].ToString() ?? "",
                    JMBG = "", // JMBG nije u bazi
                    Email = reader["email"].ToString() ?? "",
                    Telefon = reader["telefon"].ToString() ?? "",
                    DatumRegistracije = (DateTime)reader["datum_registracije"]
                });
            }

            return putnici;
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoTipuAsync()
        {
            var statistika = new Dictionary<string, int>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    tip_karte as TipKarte,
                    COUNT(*) as BrojKarata
                FROM KARTE k
                GROUP BY k.tip_karte
                ORDER BY k.tip_karte";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                statistika[reader["TipKarte"].ToString() ?? ""] = (int)reader["BrojKarata"];
            }

            return statistika;
        }

        public async Task<Dictionary<string, int>> GetStatistikaPoStatusuAsync()
        {
            var statistika = new Dictionary<string, int>();

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    status_karte as StatusKarte,
                    COUNT(*) as BrojKarata
                FROM KARTE k
                GROUP BY k.status_karte
                ORDER BY k.status_karte";

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                statistika[reader["StatusKarte"].ToString() ?? ""] = (int)reader["BrojKarata"];
            }

            return statistika;
        }

        public async Task<decimal> GetUkupnaZaradaAsync()
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var query = "SELECT SUM(cena) as UkupnaZarada FROM KARTE";
            using var command = new SqlCommand(query, connection);

            var result = await command.ExecuteScalarAsync();
            return result == DBNull.Value ? 0 : (decimal)result;
        }

        private TipKarte GetTipKarteFromString(string tipKarte)
        {
            return tipKarte switch
            {
                "Jednokratna" => TipKarte.Jednokratna,
                "Dnevna" => TipKarte.Dnevna,
                "Nedeljna" => TipKarte.Nedeljna,
                "Mesečna" => TipKarte.Mesecna,
                "Godišnja" => TipKarte.Godisnja,
                "Studentska" => TipKarte.Jednokratna, // Studentska nije u enum-u, koristim Jednokratna
                "Penzijska" => TipKarte.Jednokratna, // Penzijska nije u enum-u, koristim Jednokratna
                _ => TipKarte.Jednokratna
            };
        }

        private StatusKarte GetStatusKarteFromString(string statusKarte)
        {
            return statusKarte switch
            {
                "Aktivna" => StatusKarte.Aktivna,
                "Iskorišćena" => StatusKarte.Iskoriscena,
                "Istekla" => StatusKarte.Istekla,
                "Otkazana" => StatusKarte.Otkazana,
                _ => StatusKarte.Aktivna
            };
        }

        private string GetTipKarteAsString(TipKarte tipKarte)
        {
            return tipKarte switch
            {
                TipKarte.Jednokratna => "Jednokratna",
                TipKarte.Dnevna => "Dnevna",
                TipKarte.Nedeljna => "Nedeljna",
                TipKarte.Mesecna => "Mesečna",
                TipKarte.Godisnja => "Godišnja",
                _ => "Jednokratna"
            };
        }

        private string GetStatusKarteAsString(StatusKarte statusKarte)
        {
            return statusKarte switch
            {
                StatusKarte.Aktivna => "Aktivna",
                StatusKarte.Iskoriscena => "Iskorišćena",
                StatusKarte.Istekla => "Istekla",
                StatusKarte.Otkazana => "Otkazana",
                _ => "Aktivna"
            };
        }
    }
}
