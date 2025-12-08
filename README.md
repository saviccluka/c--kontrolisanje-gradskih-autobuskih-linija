# Gradski Transport - Sistem upravljanja

Windows Forms aplikacija za upravljanje gradskim transportom, napravljena po uzoru na EvidencijaTimova projekat.

## Opis aplikacije

Aplikacija omogućava:
- Prijavu korisnika sa različitim ulogama (admin/korisnik)
- Pregled linija gradskog transporta
- Pregled rasporeda polazaka
- Upravljanje kartama (u razvoju)
- Statistiku sistema (u razvoju)

## Struktura projekta

Projekat je organizovan u slojeve (layered architecture):

- **GradskiTransport.Shared** - zajednički modeli i konstante
- **GradskiTransport.Data** - sloj za pristup bazi podataka (Repository pattern)
- **GradskiTransport.Business** - poslovna logika
- **GradskiTransport.Presentation** - korisnički interfejs (Windows Forms)

## Potrebni alati

- Visual Studio 2019 ili noviji
- Microsoft SQL Server (LocalDB ili Express)
- .NET 6.0 ili noviji

## Instalacija i pokretanje

1. Klonirajte ili preuzmite projekat
2. Otvorite `GradskiTransport.sln` u Visual Studio-u
3. Kreirajte bazu podataka:
   - Otvorite SQL Server Management Studio
   - Povežite se na **localhost** (SQL Server)
   - Pokrenite skript `Database/CompleteSetup.sql`
4. Pokrenite aplikaciju (F5)

## Načini pokretanja aplikacije

### 1. Visual Studio (preporučeno)
- Otvorite `GradskiTransport.sln` u Visual Studio-u
- Pritisnite **F5** ili kliknite **Start Debugging**

### 2. Command Line (.NET CLI)
```bash
# Navigirajte do glavnog direktorijuma projekta
cd "C:\Users\lukas\OneDrive\Desktop\VP Anchi"

# Pokrenite aplikaciju
dotnet run --project GradskiTransport.Presentation
```

## Podaci za prijavu

### Administratori
- **admin** / **admin123** (Admin Admin)
- **petar** / **petar123** (Petar Petrović)
- Uloga: Administrator (puni pristup)

### Obični korisnici
- **marko** / **marko123** (Marko Marković)
- **ana** / **ana123** (Ana Jovanović)
- **stefan** / **stefan123** (Stefan Nikolić)
- **milica** / **milica123** (Milica Stojanović)
- Uloga: Korisnik (samo pregled)

## Funkcionalnosti

### Pregled linija
- Prikaz svih linija gradskog transporta
- Informacije o broju linije, nazivu i opisu
- Sortiranje po broju linije

### Raspored polazaka
- Izbor linije iz dropdown liste
- Prikaz svih polazaka za odabranu liniju
- Prikaz vremena polaska i tipa vozila
- Real-time ažuriranje trenutnog vremena

### Upravljanje kartama (u razvoju)
- Generisanje novih karata
- Validacija postojećih karata
- Pregled istorije validacija
- Upravljanje tipovima karata

### Statistika sistema (u razvoju)
- Statistika prodaje karata
- Analiza validacija po linijama
- Izveštaji o prometu
- Grafikoni i dijagrami

## Baza podataka

Aplikacija koristi SQL Server bazu sa sledećim tabelama:
- `KORISNICI` - korisnici sistema sa ulogama (admin/korisnik)
- `STANICE` - stanice gradskog transporta
- `LINIJE` - linije gradskog transporta
- `LINIJA_STANICA` - veze između linija i stanica
- `POLASCI` - raspored polazaka
- `PUTNICI` - putnici sistema
- `KARTE` - karte za prevoz
- `VALIDACIJE` - istorija validacija karata

### Sadržaj baze
- **6 linija** gradskog transporta (15, 23, 28, 31, 45, 67)
- **10 stanica** u Beogradu
- **Polasci** za sve linije
- **Test korisnici** za različite uloge

## Tehnologije

- C# (.NET 6.0)
- Windows Forms
- SQL Server
- Microsoft.Data.SqlClient
- Layered Architecture (Repository Pattern)

## Arhitektura

Projekat koristi layered architecture sa sledećim slojevima:

1. **Presentation Layer** - Windows Forms korisnički interfejs
2. **Business Layer** - Poslovna logika i validacija
3. **Data Layer** - Repository pattern za pristup bazi
4. **Shared Layer** - Zajednički modeli i konstante

## Razvoj

Aplikacija je u aktivnom razvoju. Trenutno su implementirane:
- ✅ Prijava korisnika
- ✅ Pregled linija
- ✅ Raspored polazaka
- 🔄 Upravljanje kartama (u razvoju)
- 🔄 Statistika sistema (u razvoju)

## Podrška

Za pitanja ili probleme, molimo kontaktirajte tim za razvoj.