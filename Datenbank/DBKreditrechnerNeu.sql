
--KARL Manuel
USE MASTER
GO
DROP DATABASE dbKreditInstitut
GO
CREATE DATABASE dbKreditInstitut
GO
USE dbKreditInstitut
GO

CREATE TABLE tblEinstellungen(
	IDEinstellungen INT IDENTITY(1,1)  PRIMARY KEY NOT NULL,
	NormalZins decimal(3,2)  NOT NULL,
	EffektiverZins decimal(3,2)  NOT NULL,
	Datum datetime  NULL
)
GO

CREATE TABLE tblWohnart(
	IDWohnart INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Wohnart VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblTitel(
	IDTitel INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Titel VARCHAR(20) NOT NULL
)
GO


CREATE TABLE tblAusbildung(
	IDAusbildung INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Ausbildung VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblLand(
	IDLand CHAR(3) PRIMARY KEY NOT NULL,
	Land VARCHAR(50) NOT NULL
)
GO



CREATE TABLE tblBranche(
	IDBranche INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Branche VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblOrt(
	IDOrt INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PLZ CHAR(5) NOT NULL,
	Ort VARCHAR(50) NOT NULL,
	FKLand CHAR(3) FOREIGN KEY REFERENCES tblLand
)
GO



CREATE TABLE tblIdentifikationsArt(
	IDIdArt INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	IdArt VARCHAR(30) NOT NULL	
)
GO

CREATE TABLE tblFamilienstand(
	IDFamilienstand INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Familienstand VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblBeschaeftigungsart(
	IDBeschaeftigungsart INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	BeschaeftigungsArt VARCHAR(30) NOT NULL
)
GO

CREATE TABLE tblArbeitgeber(
	IDArbeitgeber INT IDENTITY(1,1) PRIMARY KEY  NOT NULL,
	FKBeschaeftigungsart INT FOREIGN KEY REFERENCES tblBeschaeftigungsart NULL,
	FKBranche INT FOREIGN KEY REFERENCES tblBranche NULL,
	Firmenname VARCHAR(30) NULL,
	BeschaeftigtSeit DATE NULL
)
GO

CREATE TABLE tblKunde(
	IDKunde INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Vorname VARCHAR(30) NOT NULL,
	Nachname VARCHAR(30) NOT NULL,
	Geburtsdatum DATE NOT NULL,
	Kinder INT NOT NULL,
	FKTitel INT FOREIGN KEY REFERENCES tblTitel NULL,
	FKArbeitgeber INT FOREIGN KEY REFERENCES tblArbeitgeber NULL,
	FKFamilienstand INT FOREIGN KEY REFERENCES tblFamilienstand NULL,
	FKStaatsbuergerschaft CHAR(3) FOREIGN KEY REFERENCES tblLand NULL,
	FKWohnart INT FOREIGN KEY REFERENCES tblWohnart NULL,
	FKAusbildung INT FOREIGN KEY REFERENCES tblAusbildung NULL,
	Idendifikationsnummer VARCHAR(50) NULL,
	FKIdentifikationsArt INT FOREIGN KEY REFERENCES tblIdentifikationsArt NULL,
	Geschlecht VARCHAR(20) NULL
	
)
GO
CREATE TABLE tblKontaktdaten(
	IDKontaktdaten INT PRIMARY KEY REFERENCES tblKunde  NOT NULL,
	FKOrt INT FOREIGN KEY REFERENCES tblOrt NULL,
	Strasse VARCHAR(40) NOT NULL,
	Hausnummer VARCHAR(10) NOT NULL,
	Stiege VARCHAR(10) NULL,
	Tuer VARCHAR(10) NULL,
	EMail VARCHAR(50) NULL,
	Telefonnummer VARCHAR(50) NULL
)
GO


CREATE TABLE tblFinanzielleSituation(
	IDFinanzielles INT PRIMARY KEY REFERENCES tblKunde  NOT NULL,
	NettoEinkommen DECIMAL(10,2) NULL,
	Wohnkosten DECIMAL(10,2) NULL,
	EinkünfteAlimente DECIMAL(10,2) NULL,
	Unterhaltszahlung DECIMAL(10,2) NULL,
	Ratenverpflichtung DECIMAL(10,2) NULL

)
GO

CREATE TABLE tblKredit(
	IDKredit INT PRIMARY KEY REFERENCES tblKunde NOT NULL,
	Kredit DECIMAL(10,2) NULL,
	Zeitraum INT NULL,
	Bewilligt BIT NULL
)
GO



CREATE TABLE tblKonto(
	IDKonto INT PRIMARY KEY REFERENCES tblKunde  NOT NULL,
	KontoNeu BIT NULL,
	IBAN VARCHAR(30) NULL,
	BIC VARCHAR(20) NULL,
	Bankname VARCHAR(50) NULL
)
GO


/*
INSERT INTO tblLand(IDLand,Land)
VALUES
('AUT','Österreich')
*/

INSERT INTO tblLand
SELECT [Spalte 0],[Spalte 1] 
FROM landort.dbo.Länderneu
WHERE "Spalte 0" <> ''
GO

INSERT INTO tblOrt
SELECT PLZ,Ort, 'AUT'
FROM landort.dbo.tblOrt
	

GO



INSERT INTO tblTitel(Titel)
VALUES
('Kein Titel'),
('Dr'),
('Mag')
GO

INSERT INTO tblFamilienstand(Familienstand)
VALUES
('Ledig'),
('Verheiratet'),
('Verwitwet'),
('in Partnerschaft')
GO

INSERT INTO tblWohnart(Wohnart)
VALUES
('Mietwohnung'),
('EigentumsWohnung'),
('Haus')
GO

INSERT INTO tblAusbildung(Ausbildung)
VALUES
('Hauptschlussabschluss'),
('Lehre')
GO

INSERT INTO tblIdentifikationsArt(IdArt)
VALUES
('Führerschein'),
('Reisepass')
GO

INSERT INTO tblBeschaeftigungsart(BeschaeftigungsArt)
VALUES
('Angestellter'),
('Arbeiter')
GO

INSERT INTO tblBranche(Branche)
VALUES
('Bau'),
('Metal'),
('Kaufmänisch')
GO