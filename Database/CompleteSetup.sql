-- GradskiTransport Database Setup
-- Kreiranje baze i tabela za sistem gradskog transporta

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GradskiTransport')
BEGIN
    CREATE DATABASE GradskiTransport;
END
GO

USE GradskiTransport;
GO

-- Tabela za korisnike sistema
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KORISNICI]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[KORISNICI] (
        [id_korisnika] INT IDENTITY (1, 1) NOT NULL,
        [ime] NVARCHAR (50) NOT NULL,
        [prezime] NVARCHAR (50) NOT NULL,
        [username] NVARCHAR (50) NOT NULL,
        [password] NVARCHAR (50) NOT NULL,
        [uloga] NVARCHAR (20) NOT NULL,
        PRIMARY KEY CLUSTERED ([id_korisnika] ASC)
    );
END
GO

-- Tabela za stanice
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STANICE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[STANICE] (
        [id_stanice] INT IDENTITY (1, 1) NOT NULL,
        [naziv_stanice] NVARCHAR (100) NOT NULL,
        [adresa] NVARCHAR (200) NOT NULL,
        [latitude] DECIMAL(10, 6) NOT NULL,
        [longitude] DECIMAL(10, 6) NOT NULL,
        PRIMARY KEY CLUSTERED ([id_stanice] ASC)
    );
END
GO

-- Tabela za linije
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LINIJE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[LINIJE] (
        [id_linije] INT IDENTITY (1, 1) NOT NULL,
        [broj_linije] NVARCHAR (10) NOT NULL,
        [naziv_linije] NVARCHAR (100) NOT NULL,
        [opis] NVARCHAR (200) NULL,
        PRIMARY KEY CLUSTERED ([id_linije] ASC)
    );
END
GO

-- Tabela za veze linija-stanica
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LINIJA_STANICA]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[LINIJA_STANICA] (
        [id] INT IDENTITY (1, 1) NOT NULL,
        [linija_id] INT NOT NULL,
        [stanica_id] INT NOT NULL,
        [redni_broj] INT NOT NULL,
        PRIMARY KEY CLUSTERED ([id] ASC)
    );
END
GO

-- Tabela za polaske
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POLASCI]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[POLASCI] (
        [id_polaska] INT IDENTITY (1, 1) NOT NULL,
        [linija_id] INT NOT NULL,
        [vreme_polaska] TIME NOT NULL,
        [tip_vozila] NVARCHAR (50) NOT NULL,
        PRIMARY KEY CLUSTERED ([id_polaska] ASC)
    );
END
GO

-- Tabela za putnike
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PUTNICI]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PUTNICI] (
        [id_putnika] INT IDENTITY (1, 1) NOT NULL,
        [ime] NVARCHAR (100) NOT NULL,
        [prezime] NVARCHAR (100) NOT NULL,
        [email] NVARCHAR (200) NULL,
        [telefon] NVARCHAR (20) NULL,
        [datum_registracije] DATETIME2 NOT NULL,
        PRIMARY KEY CLUSTERED ([id_putnika] ASC)
    );
END
GO

-- Tabela za karte
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KARTE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[KARTE] (
        [id_karte] INT IDENTITY (1, 1) NOT NULL,
        [putnik_id] INT NOT NULL,
        [tip_karte] NVARCHAR (50) NOT NULL,
        [status_karte] NVARCHAR (50) NOT NULL,
        [datum_kupovine] DATETIME2 NOT NULL,
        [datum_vazenja] DATETIME2 NOT NULL,
        [cena] DECIMAL(10, 2) NOT NULL,
        [broj_karte] NVARCHAR (50) NOT NULL,
        PRIMARY KEY CLUSTERED ([id_karte] ASC)
    );
END
GO

-- Tabela za validacije
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VALIDACIJE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[VALIDACIJE] (
        [id_validacije] INT IDENTITY (1, 1) NOT NULL,
        [karta_id] INT NOT NULL,
        [stanica_id] INT NOT NULL,
        [linija_id] INT NOT NULL,
        [vreme_validacije] DATETIME2 NOT NULL,
        [broj_vozila] NVARCHAR (20) NULL,
        [uspesna] BIT NOT NULL,
        PRIMARY KEY CLUSTERED ([id_validacije] ASC)
    );
END
GO

-- Brisanje postojećih podataka
DELETE FROM VALIDACIJE;
DELETE FROM KARTE;
DELETE FROM PUTNICI;
DELETE FROM POLASCI;
DELETE FROM LINIJA_STANICA;
DELETE FROM LINIJE;
DELETE FROM STANICE;
DELETE FROM KORISNICI;
GO

-- Resetovanje ID-jeva
DBCC CHECKIDENT ('KORISNICI', RESEED, 0);
DBCC CHECKIDENT ('STANICE', RESEED, 0);
DBCC CHECKIDENT ('LINIJE', RESEED, 0);
DBCC CHECKIDENT ('LINIJA_STANICA', RESEED, 0);
DBCC CHECKIDENT ('POLASCI', RESEED, 0);
DBCC CHECKIDENT ('PUTNICI', RESEED, 0);
DBCC CHECKIDENT ('KARTE', RESEED, 0);
DBCC CHECKIDENT ('VALIDACIJE', RESEED, 0);
GO

-- Dodavanje korisnika
INSERT INTO KORISNICI (ime, prezime, username, password, uloga) 
VALUES 
('Admin', 'Admin', 'admin', 'admin123', 'admin'),
('Petar', 'Petrović', 'petar', 'petar123', 'admin'),
('Marko', 'Marković', 'marko', 'marko123', 'korisnik'),
('Ana', 'Jovanović', 'ana', 'ana123', 'korisnik'),
('Stefan', 'Nikolić', 'stefan', 'stefan123', 'korisnik'),
('Milica', 'Stojanović', 'milica', 'milica123', 'korisnik');
GO

-- Dodavanje stanica
INSERT INTO STANICE (naziv_stanice, adresa, latitude, longitude) 
VALUES 
('Trg Republike', 'Trg Republike 1', 44.817600, 20.456500),
('Zeleni Venac', 'Zeleni Venac', 44.815600, 20.456500),
('Terazije', 'Terazije 1', 44.815600, 20.456500),
('Studentski Trg', 'Studentski Trg', 44.817600, 20.456500),
('Vukov Spomenik', 'Vukov Spomenik', 44.817600, 20.456500),
('Novi Beograd', 'Novi Beograd', 44.814200, 20.418100),
('Zvezdara', 'Zvezdara', 44.804800, 20.484400),
('Voždovac', 'Voždovac', 44.788800, 20.477800),
('Karaburma', 'Karaburma', 44.825600, 20.500800),
('Banjica', 'Banjica', 44.766400, 20.452400);
GO

-- Dodavanje linija
INSERT INTO LINIJE (broj_linije, naziv_linije, opis) 
VALUES 
('15', 'Zvezdara - Novi Beograd', 'Glavna linija kroz centar grada'),
('23', 'Karaburma - Banjica', 'Linija preko Vračara'),
('31', 'Zemun - Voždovac', 'Dugačka linija kroz grad'),
('28', 'Zvezdara - Voždovac', 'Brza linija preko centra'),
('45', 'Novi Beograd - Banjica', 'Linija preko mostova'),
('67', 'Zemun - Zvezdara', 'Kružna linija');
GO

-- Dodavanje veza linija-stanica
INSERT INTO LINIJA_STANICA (linija_id, stanica_id, redni_broj) 
VALUES 
-- Linija 15: Zvezdara - Novi Beograd
(1, 7, 1), (1, 1, 2), (1, 2, 3), (1, 3, 4), (1, 6, 5),
-- Linija 23: Karaburma - Banjica
(2, 9, 1), (2, 1, 2), (2, 4, 3), (2, 8, 4), (2, 10, 5),
-- Linija 31: Zemun - Voždovac
(3, 11, 1), (3, 6, 2), (3, 1, 3), (3, 8, 4),
-- Linija 28: Zvezdara - Voždovac
(4, 7, 1), (4, 1, 2), (4, 8, 3),
-- Linija 45: Novi Beograd - Banjica
(5, 6, 1), (5, 1, 2), (5, 10, 3),
-- Linija 67: Zemun - Zvezdara
(6, 11, 1), (6, 6, 2), (6, 1, 3), (6, 7, 4);
GO

-- Dodavanje polazaka
INSERT INTO POLASCI (linija_id, vreme_polaska, tip_vozila) 
VALUES 
-- Linija 15
(1, '06:00:00', 'Autobus'),
(1, '06:30:00', 'Autobus'),
(1, '07:00:00', 'Autobus'),
(1, '07:30:00', 'Autobus'),
(1, '08:00:00', 'Autobus'),
-- Linija 23
(2, '06:15:00', 'Tramvaj'),
(2, '06:45:00', 'Tramvaj'),
(2, '07:15:00', 'Tramvaj'),
(2, '07:45:00', 'Tramvaj'),
-- Linija 31
(3, '06:20:00', 'Autobus'),
(3, '06:50:00', 'Autobus'),
(3, '07:20:00', 'Autobus'),
(3, '07:50:00', 'Autobus'),
-- Linija 28
(4, '06:10:00', 'Trolejbus'),
(4, '06:40:00', 'Trolejbus'),
(4, '07:10:00', 'Trolejbus'),
-- Linija 45
(5, '06:25:00', 'Autobus'),
(5, '06:55:00', 'Autobus'),
(5, '07:25:00', 'Autobus'),
-- Linija 67
(6, '06:05:00', 'Tramvaj'),
(6, '06:35:00', 'Tramvaj'),
(6, '07:05:00', 'Tramvaj'),
(6, '07:35:00', 'Tramvaj');
GO

-- Dodavanje putnika
INSERT INTO PUTNICI (ime, prezime, email, telefon, datum_registracije) 
VALUES 
('Nikola', 'Nikolić', 'nikola@email.com', '0641234567', GETDATE()),
('Jovana', 'Jovanović', 'jovana@email.com', '0652345678', GETDATE()),
('Aleksandar', 'Aleksandrović', 'aleksandar@email.com', '0663456789', GETDATE()),
('Marija', 'Marić', 'marija@email.com', '0674567890', GETDATE()),
('Luka', 'Lukić', 'luka@email.com', '0685678901', GETDATE());
GO

-- Dodavanje karata
INSERT INTO KARTE (putnik_id, tip_karte, status_karte, datum_kupovine, datum_vazenja, cena, broj_karte) 
VALUES 
(1, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114001'),
(2, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114002'),
(3, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -1, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -1, GETDATE())), 90.00, 'GT20250113003'),
(4, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114004'),
(5, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114005');
GO

-- Dodavanje validacija
INSERT INTO VALIDACIJE (karta_id, stanica_id, linija_id, vreme_validacije, broj_vozila, uspesna) 
VALUES 
(1, 1, 1, GETDATE(), 'A-123', 1),
(2, 2, 2, GETDATE(), 'T-456', 1),
(4, 3, 4, GETDATE(), 'TR-789', 1);
GO

PRINT 'GradskiTransport baza je uspešno kreirana sa svim tabelama i podacima!';
