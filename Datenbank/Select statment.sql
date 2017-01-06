

SELECT IDKunde, Vorname,Nachname,Geschlecht, Geburtsdatum, Kinder, FKStaatsbuergerschaft,Firmenname,BeschaeftigtSeit, Kredit, Zeitraum,
Strasse,Hausnummer,EMail,Telefonnummer,Bankname
FROM tblKunde 
INNER JOIN tblArbeitgeber ON IDKunde = IDArbeitgeber
INNER JOIN tblKredit ON IDKunde = IDKredit
INNER JOIN tblKontaktdaten ON IDKunde = IDKontaktdaten
INNER JOIN tblKonto ON IDKunde = IDKonto