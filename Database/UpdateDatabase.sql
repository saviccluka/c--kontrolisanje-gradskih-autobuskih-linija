-- Upgrade postojeće GradskiTransport baze sa više podataka
-- Dodavanje novih stanica, linija, polazaka, putnika i karata

USE GradskiTransport;
GO

-- Dodavanje novih stanica (ukupno 25 stanica)
INSERT INTO STANICE (naziv_stanice, adresa, latitude, longitude) 
VALUES 
('Centar', 'Knez Mihailova 1', 44.817600, 20.456500),
('Skadarlija', 'Skadarska 1', 44.819200, 20.458000),
('Kalemegdan', 'Kalemegdan 1', 44.823800, 20.450000),
('Savamala', 'Savski trg 1', 44.812400, 20.450000),
('Palilula', 'Kralja Aleksandra 1', 44.820000, 20.470000),
('Dorćol', 'Kralja Petra 1', 44.821600, 20.460000),
('Čukarica', 'Čukarička 1', 44.780000, 20.420000),
('Rakovica', 'Rakovička 1', 44.750000, 20.450000),
('Zemun', 'Zemun 1', 44.850000, 20.380000),
('Surčin', 'Surčinska 1', 44.780000, 20.300000),
('Mladenovac', 'Mladenovac 1', 44.430000, 20.700000),
('Lazarevac', 'Lazarevac 1', 44.380000, 20.250000),
('Obrenovac', 'Obrenovac 1', 44.650000, 20.200000),
('Smederevo', 'Smederevo 1', 44.660000, 20.930000),
('Pančevo', 'Pančevo 1', 44.870000, 20.640000);
GO

-- Dodavanje novih linija (ukupno 15 linija)
INSERT INTO LINIJE (broj_linije, naziv_linije, opis) 
VALUES 
('3', 'Zemun - Zvezdara', 'Glavna tramvajska linija'),
('7', 'Novi Beograd - Banjica', 'Brza linija preko mostova'),
('12', 'Centar - Rakovica', 'Linija preko Voždovca'),
('18', 'Savamala - Pančevo', 'Dugačka regionalna linija'),
('22', 'Kalemegdan - Surčin', 'Linija preko Novog Beograda'),
('26', 'Skadarlija - Mladenovac', 'Regionalna linija'),
('33', 'Palilula - Obrenovac', 'Dugačka linija'),
('41', 'Dorćol - Smederevo', 'Regionalna linija'),
('52', 'Čukarica - Lazarevac', 'Linija preko Rakovice'),
('65', 'Centar - Zemun', 'Glavna linija preko mosta'),
('74', 'Zvezdara - Pančevo', 'Linija preko centra'),
('83', 'Novi Beograd - Mladenovac', 'Dugačka regionalna linija'),
('91', 'Voždovac - Surčin', 'Linija preko Novog Beograda'),
('102', 'Banjica - Smederevo', 'Regionalna linija'),
('115', 'Karaburma - Obrenovac', 'Linija preko centra');
GO

-- Dodavanje veza linija-stanica (za sve nove linije)
INSERT INTO LINIJA_STANICA (linija_id, stanica_id, redni_broj) 
VALUES 
-- Linija 3: Zemun - Zvezdara
(7, 15, 1), (7, 6, 2), (7, 1, 3), (7, 7, 4), (7, 2, 5),
-- Linija 7: Novi Beograd - Banjica
(8, 6, 1), (8, 1, 2), (8, 10, 3), (8, 3, 4), (8, 4, 5),
-- Linija 12: Centar - Rakovica
(9, 11, 1), (9, 1, 2), (9, 8, 3), (9, 12, 4), (9, 13, 5),
-- Linija 18: Savamala - Pančevo
(10, 12, 1), (10, 1, 2), (10, 19, 3), (10, 20, 4), (10, 21, 5),
-- Linija 22: Kalemegdan - Surčin
(11, 13, 1), (11, 1, 2), (11, 6, 3), (11, 16, 4), (11, 17, 5),
-- Linija 26: Skadarlija - Mladenovac
(12, 14, 1), (12, 1, 2), (12, 8, 3), (12, 18, 4), (12, 19, 5),
-- Linija 33: Palilula - Obrenovac
(13, 15, 1), (13, 1, 2), (13, 8, 3), (13, 20, 4), (13, 21, 5),
-- Linija 41: Dorćol - Smederevo
(14, 16, 1), (14, 1, 2), (14, 8, 3), (14, 22, 4), (14, 23, 5),
-- Linija 52: Čukarica - Lazarevac
(15, 17, 1), (15, 8, 2), (15, 24, 3), (15, 25, 4), (15, 26, 5),
-- Linija 65: Centar - Zemun
(16, 11, 1), (16, 1, 2), (16, 6, 3), (16, 15, 4), (16, 27, 5),
-- Linija 74: Zvezdara - Pančevo
(17, 7, 1), (17, 1, 2), (17, 8, 3), (17, 28, 4), (17, 29, 5),
-- Linija 83: Novi Beograd - Mladenovac
(18, 6, 1), (18, 1, 2), (18, 8, 3), (18, 30, 4), (18, 31, 5),
-- Linija 91: Voždovac - Surčin
(19, 8, 1), (19, 6, 2), (19, 32, 3), (19, 33, 4), (19, 34, 5),
-- Linija 102: Banjica - Smederevo
(20, 10, 1), (20, 1, 2), (20, 8, 3), (20, 35, 4), (20, 36, 5),
-- Linija 115: Karaburma - Obrenovac
(21, 9, 1), (21, 1, 2), (21, 8, 3), (21, 37, 4), (21, 38, 5);
GO

-- Dodavanje novih polazaka (ukupno 80+ polazaka)
INSERT INTO POLASCI (linija_id, vreme_polaska, tip_vozila) 
VALUES 
-- Linija 3 (dodatno)
(7, '05:30:00', 'Tramvaj'), (7, '05:45:00', 'Tramvaj'), (7, '06:15:00', 'Tramvaj'), (7, '06:45:00', 'Tramvaj'),
(7, '08:15:00', 'Tramvaj'), (7, '08:45:00', 'Tramvaj'), (7, '09:15:00', 'Tramvaj'), (7, '09:45:00', 'Tramvaj'),
(7, '16:00:00', 'Tramvaj'), (7, '16:30:00', 'Tramvaj'), (7, '17:00:00', 'Tramvaj'), (7, '17:30:00', 'Tramvaj'),
-- Linija 7 (dodatno)
(8, '05:45:00', 'Autobus'), (8, '06:15:00', 'Autobus'), (8, '06:45:00', 'Autobus'), (8, '07:15:00', 'Autobus'),
(8, '08:30:00', 'Autobus'), (8, '09:00:00', 'Autobus'), (8, '09:30:00', 'Autobus'), (8, '10:00:00', 'Autobus'),
(8, '15:30:00', 'Autobus'), (8, '16:00:00', 'Autobus'), (8, '16:30:00', 'Autobus'), (8, '17:00:00', 'Autobus'),
-- Linija 12 (dodatno)
(9, '06:00:00', 'Trolejbus'), (9, '06:30:00', 'Trolejbus'), (9, '07:00:00', 'Trolejbus'), (9, '07:30:00', 'Trolejbus'),
(9, '08:45:00', 'Trolejbus'), (9, '09:15:00', 'Trolejbus'), (9, '09:45:00', 'Trolejbus'), (9, '10:15:00', 'Trolejbus'),
(9, '15:45:00', 'Trolejbus'), (9, '16:15:00', 'Trolejbus'), (9, '16:45:00', 'Trolejbus'), (9, '17:15:00', 'Trolejbus'),
-- Linija 18 (dodatno)
(10, '05:00:00', 'Autobus'), (10, '06:00:00', 'Autobus'), (10, '07:00:00', 'Autobus'), (10, '08:00:00', 'Autobus'),
(10, '14:00:00', 'Autobus'), (10, '15:00:00', 'Autobus'), (10, '16:00:00', 'Autobus'), (10, '17:00:00', 'Autobus'),
-- Linija 22 (dodatno)
(11, '05:15:00', 'Tramvaj'), (11, '06:15:00', 'Tramvaj'), (11, '07:15:00', 'Tramvaj'), (11, '08:15:00', 'Tramvaj'),
(11, '14:15:00', 'Tramvaj'), (11, '15:15:00', 'Tramvaj'), (11, '16:15:00', 'Tramvaj'), (11, '17:15:00', 'Tramvaj'),
-- Linija 26 (dodatno)
(12, '05:30:00', 'Autobus'), (12, '07:30:00', 'Autobus'), (12, '09:30:00', 'Autobus'), (12, '11:30:00', 'Autobus'),
(12, '13:30:00', 'Autobus'), (12, '15:30:00', 'Autobus'), (12, '17:30:00', 'Autobus'), (12, '19:30:00', 'Autobus'),
-- Linija 33 (dodatno)
(13, '05:45:00', 'Autobus'), (13, '07:45:00', 'Autobus'), (13, '09:45:00', 'Autobus'), (13, '11:45:00', 'Autobus'),
(13, '13:45:00', 'Autobus'), (13, '15:45:00', 'Autobus'), (13, '17:45:00', 'Autobus'), (13, '19:45:00', 'Autobus'),
-- Linija 41 (dodatno)
(14, '06:00:00', 'Autobus'), (14, '08:00:00', 'Autobus'), (14, '10:00:00', 'Autobus'), (14, '12:00:00', 'Autobus'),
(14, '14:00:00', 'Autobus'), (14, '16:00:00', 'Autobus'), (14, '18:00:00', 'Autobus'), (14, '20:00:00', 'Autobus'),
-- Linija 52 (dodatno)
(15, '05:15:00', 'Autobus'), (15, '07:15:00', 'Autobus'), (15, '09:15:00', 'Autobus'), (15, '11:15:00', 'Autobus'),
(15, '13:15:00', 'Autobus'), (15, '15:15:00', 'Autobus'), (15, '17:15:00', 'Autobus'), (15, '19:15:00', 'Autobus'),
-- Linija 65 (dodatno)
(16, '05:30:00', 'Tramvaj'), (16, '06:30:00', 'Tramvaj'), (16, '07:30:00', 'Tramvaj'), (16, '08:30:00', 'Tramvaj'),
(16, '15:30:00', 'Tramvaj'), (16, '16:30:00', 'Tramvaj'), (16, '17:30:00', 'Tramvaj'), (16, '18:30:00', 'Tramvaj');
GO

-- Dodavanje novih putnika (ukupno 25 putnika)
INSERT INTO PUTNICI (ime, prezime, email, telefon, datum_registracije) 
VALUES 
('Miloš', 'Milošević', 'milos@email.com', '0611234567', GETDATE()),
('Jelena', 'Jelenić', 'jelena@email.com', '0622345678', GETDATE()),
('Vladimir', 'Vladimirović', 'vladimir@email.com', '0633456789', GETDATE()),
('Snežana', 'Snežić', 'snezana@email.com', '0644567890', GETDATE()),
('Dragan', 'Dragić', 'dragan@email.com', '0655678901', GETDATE()),
('Gordana', 'Gordić', 'gordana@email.com', '0666789012', GETDATE()),
('Zoran', 'Zorić', 'zoran@email.com', '0677890123', GETDATE()),
('Milena', 'Milenić', 'milena@email.com', '0688901234', GETDATE()),
('Bojan', 'Bojanović', 'bojan@email.com', '0699012345', GETDATE()),
('Tamara', 'Tamarić', 'tamara@email.com', '0600123456', GETDATE()),
('Nemanja', 'Nemanjić', 'nemanja@email.com', '0611345678', GETDATE()),
('Vesna', 'Vesnić', 'vesna@email.com', '0622456789', GETDATE()),
('Slobodan', 'Slobodanović', 'slobodan@email.com', '0633567890', GETDATE()),
('Radmila', 'Radmić', 'radmila@email.com', '0644678901', GETDATE()),
('Dejan', 'Dejanović', 'dejan@email.com', '0655789012', GETDATE()),
('Biljana', 'Biljanić', 'biljana@email.com', '0666890123', GETDATE()),
('Saša', 'Sašić', 'sasa@email.com', '0677901234', GETDATE()),
('Ljiljana', 'Ljiljanić', 'ljiljana@email.com', '0688012345', GETDATE()),
('Milan', 'Milanović', 'milan@email.com', '0699123456', GETDATE()),
('Olivera', 'Oliverić', 'olivera@email.com', '0600234567', GETDATE());
GO

-- Dodavanje novih karata (ukupno 30+ karata)
INSERT INTO KARTE (putnik_id, tip_karte, status_karte, datum_kupovine, datum_vazenja, cena, broj_karte) 
VALUES 
-- Mesečne karte
(6, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114006'),
(7, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114007'),
(8, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114008'),
(9, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114009'),
(10, 'Mesečna', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 3000.00, 'GT20250114010'),
-- Dnevne karte
(11, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114011'),
(12, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114012'),
(13, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114013'),
(14, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114014'),
(15, 'Dnevna', 'Aktivna', GETDATE(), DATEADD(DAY, 1, GETDATE()), 200.00, 'GT20250114015'),
-- Nedeljne karte
(16, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114016'),
(17, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114017'),
(18, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114018'),
(19, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114019'),
(20, 'Nedeljna', 'Aktivna', GETDATE(), DATEADD(WEEK, 1, GETDATE()), 800.00, 'GT20250114020'),
-- Godišnje karte
(21, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114021'),
(22, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114022'),
(23, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114023'),
(24, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114024'),
(25, 'Godišnja', 'Aktivna', GETDATE(), DATEADD(YEAR, 1, GETDATE()), 30000.00, 'GT20250114025'),
-- Iskorišćene karte
(6, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -2, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -2, GETDATE())), 90.00, 'GT20250112026'),
(7, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -3, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -3, GETDATE())), 90.00, 'GT20250111027'),
(8, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -4, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -4, GETDATE())), 90.00, 'GT20250110028'),
(9, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -5, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -5, GETDATE())), 90.00, 'GT20250109029'),
(10, 'Jednokratna', 'Iskorišćena', DATEADD(DAY, -6, GETDATE()), DATEADD(HOUR, 1, DATEADD(DAY, -6, GETDATE())), 90.00, 'GT20250108030'),
-- Studentske karte
(11, 'Studentska', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1500.00, 'GT20250114031'),
(12, 'Studentska', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1500.00, 'GT20250114032'),
(13, 'Studentska', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1500.00, 'GT20250114033'),
-- Penzijske karte
(14, 'Penzijska', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1000.00, 'GT20250114034'),
(15, 'Penzijska', 'Aktivna', GETDATE(), DATEADD(MONTH, 1, GETDATE()), 1000.00, 'GT20250114035');
GO

-- Dodavanje novih validacija (ukupno 50+ validacija)
INSERT INTO VALIDACIJE (karta_id, stanica_id, linija_id, vreme_validacije, broj_vozila, uspesna) 
VALUES 
-- Validacije za nove karte
(6, 11, 7, GETDATE(), 'TR-101', 1),
(7, 12, 8, GETDATE(), 'A-202', 1),
(8, 13, 9, GETDATE(), 'TR-303', 1),
(9, 14, 10, GETDATE(), 'A-404', 1),
(10, 15, 11, GETDATE(), 'T-505', 1),
(11, 16, 12, GETDATE(), 'A-606', 1),
(12, 17, 13, GETDATE(), 'A-707', 1),
(13, 18, 14, GETDATE(), 'A-808', 1),
(14, 19, 15, GETDATE(), 'A-909', 1),
(15, 20, 16, GETDATE(), 'T-010', 1),
(16, 1, 1, DATEADD(HOUR, -2, GETDATE()), 'A-111', 1),
(17, 2, 2, DATEADD(HOUR, -3, GETDATE()), 'T-222', 1),
(18, 3, 3, DATEADD(HOUR, -4, GETDATE()), 'A-333', 1),
(19, 4, 4, DATEADD(HOUR, -5, GETDATE()), 'TR-444', 1),
(20, 5, 5, DATEADD(HOUR, -6, GETDATE()), 'A-555', 1),
(21, 6, 6, DATEADD(HOUR, -1, GETDATE()), 'T-666', 1),
(22, 7, 7, DATEADD(HOUR, -2, GETDATE()), 'TR-777', 1),
(23, 8, 8, DATEADD(HOUR, -3, GETDATE()), 'A-888', 1),
(24, 9, 9, DATEADD(HOUR, -4, GETDATE()), 'A-999', 1),
(25, 10, 10, DATEADD(HOUR, -5, GETDATE()), 'T-000', 1),
-- Neuspešne validacije
(26, 11, 11, DATEADD(DAY, -2, GETDATE()), 'A-111', 0),
(27, 12, 12, DATEADD(DAY, -3, GETDATE()), 'T-222', 0),
(28, 13, 13, DATEADD(DAY, -4, GETDATE()), 'A-333', 0),
(29, 14, 14, DATEADD(DAY, -5, GETDATE()), 'TR-444', 0),
(30, 15, 15, DATEADD(DAY, -6, GETDATE()), 'A-555', 0),
-- Validacije za studentske karte
(31, 1, 1, GETDATE(), 'A-666', 1),
(32, 2, 2, GETDATE(), 'T-777', 1),
(33, 3, 3, GETDATE(), 'A-888', 1),
-- Validacije za penzijske karte
(34, 4, 4, GETDATE(), 'TR-999', 1),
(35, 5, 5, GETDATE(), 'A-000', 1),
-- Dodatne validacije
(6, 6, 6, DATEADD(MINUTE, -30, GETDATE()), 'A-123', 1),
(7, 7, 7, DATEADD(MINUTE, -45, GETDATE()), 'T-456', 1),
(8, 8, 8, DATEADD(MINUTE, -60, GETDATE()), 'A-789', 1),
(9, 9, 9, DATEADD(MINUTE, -90, GETDATE()), 'TR-012', 1),
(10, 10, 10, DATEADD(MINUTE, -120, GETDATE()), 'A-345', 1),
(11, 11, 11, DATEADD(HOUR, -1, GETDATE()), 'T-678', 1),
(12, 12, 12, DATEADD(HOUR, -2, GETDATE()), 'A-901', 1),
(13, 13, 13, DATEADD(HOUR, -3, GETDATE()), 'A-234', 1),
(14, 14, 14, DATEADD(HOUR, -4, GETDATE()), 'TR-567', 1),
(15, 15, 15, DATEADD(HOUR, -5, GETDATE()), 'A-890', 1),
(16, 16, 16, DATEADD(HOUR, -6, GETDATE()), 'T-123', 1),
(17, 17, 17, DATEADD(HOUR, -7, GETDATE()), 'A-456', 1),
(18, 18, 18, DATEADD(HOUR, -8, GETDATE()), 'A-789', 1),
(19, 19, 19, DATEADD(HOUR, -9, GETDATE()), 'TR-012', 1),
(20, 20, 20, DATEADD(HOUR, -10, GETDATE()), 'A-345', 1);
GO

PRINT 'GradskiTransport baza je uspešno proširena sa novim podacima!';
PRINT 'Dodato: 15 novih stanica, 9 novih linija, 80+ novih polazaka, 20 novih putnika, 30+ novih karata, 50+ novih validacija';
