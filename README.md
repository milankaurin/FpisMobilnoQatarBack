# Aplikacija za Svetsko prvenstvo u Qataru 
#IN PACKAGE MANAGER CONSOLE TYPE "Update-Database" BEFORE RUNNING THE PROJECT
Ova full-stack hibridna aplikacija, izgrađena korišćenjem ASP.NET Core Web API i Ionic sa Reactom, dizajnirana je za upravljanje i prikazivanje informacija za Svetsko prvenstvo 2024. u Kataru. Aplikacija pruža sveobuhvatnu platformu za pristup detaljima turnira, upravljanje timovima i grupama, te administraciju rasporeda mečeva i rezultata.

# Funkcionalnosti
Funkcionalnost 1: Osnovne Informacije o Svetskom Prvenstvu
Početna Strana: Prikazuje naziv turnira, lokacije i stadione gde se odvijaju mečevi.
Utakmice grupne faze: Tabele za svaku grupu koje prikazuju državu, broj odigranih mečeva, broj pobeda, nerešenih, poraza, ukupan broj datih i primljenih golova, i osvojenih poena.
Funkcionalnost 2: Administracija Grupa i Timova
Upravljanje Grupama: Korisnici mogu upravljati grupama, uključujući dodavanje, modifikaciju i brisanje grupa.
Upravljanje Timovima: Unutar svake grupe, korisnici mogu upravljati timovima, uključujući dodavanje imena timova i opcionalno ikone zastave koja predstavlja zemlju.
Funkcionalnost 3: Administracija Utakmica
Zakazivanje Utakmica: Omogućava zakazivanje utakmica, odabir stadiona na kojima se utakmice održavaju. Sistem osigurava da se na jednom stadionu u isto vreme ne mogu odigrati dve utakmice i sprečava da jedan tim igra dve utakmice u isto vreme.
Evidencija Ishoda Utakmica: Korisnici mogu evidentirati ishode utakmica, ali samo nakon predviđenog vremena početka utakmice (osim ako se utakmica ne odigra – predaja meča).
Obračun Poena: Nakon unosa rezultata utakmice, sistem automatski obračunava i ažurira poene za svaki tim kako bi se ažurirao redosled na rang listi grupe.
# Tehnički Detalji

Backend
ASP.NET Core Web API: Upravlja svim operacijama sa podacima, od čuvanja informacija o timovima i mečevima do obrade korisničkih zahteva za ažuriranje podataka. Autentikacija i autorizacija su implementirane za zaštitu endpointova, posebno onih koji modifikuju podatke.
Frontend
Ionic sa Reactom: Pruža responsivni korisnički interfejs koji je kompatibilan i sa mobilnim i sa desktop uređajima. Upotreba Ionic komponenata osigurava dosledan izgled i osećaj na različitim platformama, dok Reactove funkcije za upravljanje stanjem omogućavaju interaktivno i dinamično korisničko iskustvo.
Autentikacija
JWT Autentikacija: Osigurava API zahtevajući JSON Web Token (JWT) za pristup zaštićenim rutama. Frontend upravlja skladištenjem tokena i uključuje token u HTTP zaglavlja za autentifikovane zahteve.
Baza Podataka

Entity Framework Core: Koristi se za ORM sa SQL bazom podataka u pozadini, olakšavajući upravljanje operacijama baze podataka bez problema unutar ASP.NET okruženja.
