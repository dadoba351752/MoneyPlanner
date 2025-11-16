MoneyPlanner

MoneyPlanner je osobní finanční aplikace vyvíjená v jazyce C# s využitím WPF. Projekt je stále ve fázi vývoje a aplikace není dokončená. Slouží především ke studijním účelům, k procvičení práce s WPF, strukturou aplikace, API integrací a lokálním ukládáním dat.

Aktuální stav projektu

Aplikace je průběžně rozšiřována a některé části jsou rozpracované nebo pouze připravené pro další implementaci. Funkcionalita se bude v čase vyvíjet, kód není finální a některé části budou později refaktorovány.

Technický přehled

Platforma: .NET / C#

Uživatelské rozhraní: WPF

Databáze: SQLite (soubory uložené ve složce Files/)

API: AlphaVantage (pro načítání cenných papírů a aktuálních cen akcií)

Architektura: částečná implementace MVVM

Implementované nebo rozpracované funkce:

Zobrazení aktuálních cen akcií přes API

Práce s lokální SQLite databází

MessageService pro poskytování hlášek uživateli

Základní příprava stránky pro zobrazení transakcí

Struktura projektu

MoneyPlanner/ – hlavní projekt WPF aplikace

Service/Api/ – komunikace s externími API (např. AlphaVantageClient)

Database/ – třídy a logika pro práci s databází

Files/ – datové soubory, lokální SQLite databáze a JSON vstupy

MainWindow.xaml – hlavní okno aplikace a základní UI

DataAccess.cs – přístup k datům a jejich zpracování

MoneyPlanner.sln – solution soubor projektu

Účel projektu

Projekt slouží jako studijní materiál pro učení následujících oblastí:

tvorba desktopové aplikace ve WPF,

komunikace s REST API,

práce se soubory a SQLite databázemi,

zpracování JSON dat,

postupné budování aplikace podle vzoru MVVM,

techniky refaktoringu a práce na větším projektu.
