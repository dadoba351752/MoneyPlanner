MoneyPlanner

MoneyPlanner je osobní finanční aplikace vyvíjená v jazyce C# s využitím WPF.
Projekt je stále ve fázi vývoje – aplikace není hotová a průběžně se mění.
Slouží primárně ke studijním účelům (procvičení C#, WPF, práce s databází a API).

Cíle a zaměření projektu

vytvořit desktopovou WPF aplikaci pro základní správu osobních financí,

naučit se pracovat s MVVM přístupem,

integrovat externí API pro akciová data,

ukládat data lokálně pomocí SQLite,

vyzkoušet si tvorbu jednoduchých finančních kalkulaček.

Hlavní funkce
Portfolio

základ aplikace pro správu uživatelského portfolia,

obrazovky pro:

přehled uživatele a jeho portfolia,

přidání transakce,

zobrazení transakcí,

uvítací / přehledovou stránku portfolia,

datová logika je postupně rozšiřována, některé části jsou rozpracované.

Kalkulačky

V aplikaci jsou připravené jednoduché finanční kalkulačky:

Compound Interest Calculator

kalkulačka složeného úročení (dlouhodobé investice, úrok, období, růst hodnoty apod.),

View: View/Calculators/CompoundInterestCalculatorPage.xaml,

ViewModel: ViewModel/Calculators/CompoundInterestCalculatorViewModel.cs.

Net Income Calculator

kalkulačka čistého příjmu, vhodná pro hrubé odhady čisté mzdy / čistého příjmu,

View: View/Calculators/NetIncomeCalculatorPage.xaml,

ViewModel: ViewModel/Calculators/NetIncomeCalculatorViewModel.cs.

Obě kalkulačky jsou zatím ve vývoji – logika i UI se mohou měnit.

Práce s API

integrace s externím API (AlphaVantage) pro získání informací o akciích,

klient je umístěn v Service/Api/AlphaVantageClient.cs,

slouží ke studiu práce s HTTP požadavky a zpracováním JSON odpovědí.

Databáze a data

použití SQLite pro lokální ukládání dat,

soubory s databází a daty:

Files/MoneyPlanner.db – hlavní databáze aplikace,

Files/TSLA.json – ukázkový JSON soubor s daty pro testování,

Database/users.db – další databázový soubor používaný v projektu,

přístup k datům je řešen přes třídu DataAccess.cs (a související logiku).

Struktura projektu

Hlavní složky:

MoneyPlanner/

WPF projekt aplikace

App.xaml, MainWindow.xaml – start aplikace a hlavní okno

DataAccess.cs – přístup k datům a databázová logika

App.config – konfigurace aplikace

MoneyPlanner/View/

WPF stránky (Views)

View/Calculators/ – stránky kalkulaček

CompoundInterestCalculatorPage.xaml

NetIncomeCalculatorPage.xaml

View/Portfolio/ – stránky pro práci s portfoliem

PortfolioUserPage.xaml

PortfolioAddTransactionPage.xaml

PortfolioUserTransactionsPage.xaml

PortfolioWelcomePage.xaml

View/Service/MessageService.cs – služba pro uživatelské zprávy

MoneyPlanner/ViewModel/

ViewModely pro MVVM

ViewModel/Calculators/

CompoundInterestCalculatorViewModel.cs

NetIncomeCalculatorViewModel.cs

ViewModel/Portfolio/

PortfolioUserViewModel.cs

PortfolioAddTransactionViewModel.cs

PortfolioUserTransactionsViewModel.cs

PortfolioWelcomeViewModel.cs

MoneyPlanner/Service/

Service/Api/AlphaVantageClient.cs – klient pro externí API

MoneyPlanner/Database/

users.db – databázový soubor (SQLite)

MoneyPlanner/Files/

MoneyPlanner.db – databázový soubor (SQLite)

TSLA.json – ukázkový JSON soubor s daty

Kořen repozitáře:

MoneyPlanner.sln – solution soubor pro Visual Studio

.gitignore, .gitattributes – nastavení Git repozitáře

Použité technologie

C# / .NET

WPF (Windows Presentation Foundation)

MVVM (architektonický přístup – postupně aplikován)

SQLite (lokální databáze)

externí API (AlphaVantage)

Visual Studio jako hlavní IDE

Stav projektu

Projekt je aktivně ve vývoji:

některé části logiky i UI jsou rozpracované nebo pouze připravené,

chování aplikace není finální,

struktura kódu může být dále refaktorována,

projekt slouží především pro osobní studium a průběžné zlepšování.
techniky refaktoringu a práce na větším projektu.
