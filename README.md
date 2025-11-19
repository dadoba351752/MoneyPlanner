# MoneyPlanner

MoneyPlanner je osobní finanční aplikace vyvíjená v jazyce C# s využitím WPF.  
Projekt je stále ve fázi vývoje – aplikace není hotová a průběžně se mění.  
Slouží primárně ke studijním účelům (procvičení C#, WPF, práce s databází a API).

---

## Cíle a zaměření projektu

- vytvořit desktopovou WPF aplikaci pro základní správu osobních financí,
- naučit se pracovat s MVVM přístupem,
- integrovat externí API pro akciová data,
- ukládat data lokálně pomocí SQLite,
- vyzkoušet si tvorbu jednoduchých finančních kalkulaček.

---

## Hlavní funkce

### Portfolio

- základ aplikace pro správu uživatelského portfolia,
- dostupné obrazovky:
  - přehled uživatele a jeho portfolia,
  - přidání transakce,
  - zobrazení transakcí,
  - uvítací / přehledová stránka portfolia,
- některé části jsou rozpracované a budou dále rozšiřovány.

### Kalkulačky

#### Compound Interest Calculator

- kalkulačka složeného úročení,
- View: `View/Calculators/CompoundInterestCalculatorPage.xaml`
- ViewModel: `ViewModel/Calculators/CompoundInterestCalculatorViewModel.cs`

#### Net Income Calculator

- kalkulačka čistého příjmu,
- View: `View/Calculators/NetIncomeCalculatorPage.xaml`
- ViewModel: `ViewModel/Calculators/NetIncomeCalculatorViewModel.cs`

---

## Práce s API

- integrace API AlphaVantage pro získávání informací o akciích,
- klient je v `Service/Api/AlphaVantageClient.cs`,
- slouží ke studiu práce s HTTP požadavky a JSON daty.

---

## Databáze a data

Aplikace využívá lokální databáze SQLite.

Používané soubory:

- `Files/MoneyPlanner.db` – hlavní databáze,
- `Files/TSLA.json` – ukázkový JSON pro testování,
- `Database/users.db` – další databázový soubor.


---

## Struktura projektu

### Složka `MoneyPlanner/`

- `App.xaml`, `App.xaml.cs`
- `MainWindow.xaml`, `MainWindow.xaml.cs`
- `DataAccess.cs`
- `App.config`

### View – `MoneyPlanner/View/`

#### Kalkulačky (`View/Calculators/`)
- `CompoundInterestCalculatorPage.xaml`
- `NetIncomeCalculatorPage.xaml`

#### Portfolio (`View/Portfolio/`)
- `PortfolioUserPage.xaml`
- `PortfolioAddTransactionPage.xaml`
- `PortfolioUserTransactionsPage.xaml`
- `PortfolioWelcomePage.xaml`

#### Služby
- `View/Service/MessageService.cs`

### ViewModely – `MoneyPlanner/ViewModel/`

#### Kalkulačky (`ViewModel/Calculators/`)
- `CompoundInterestCalculatorViewModel.cs`
- `NetIncomeCalculatorViewModel.cs`

#### Portfolio (`ViewModel/Portfolio/`)
- `PortfolioUserViewModel.cs`
- `PortfolioAddTransactionViewModel.cs`
- `PortfolioUserTransactionsViewModel.cs`
- `PortfolioWelcomeViewModel.cs`

### Služby – `MoneyPlanner/Service/`

- `Service/Api/AlphaVantageClient.cs`

### Databáze – `MoneyPlanner/Database/`

- `users.db`

### Datové soubory – `MoneyPlanner/Files/`

- `MoneyPlanner.db`
- `TSLA.json`

---

## Použité technologie

- C# / .NET  
- WPF (Windows Presentation Foundation)  
- MVVM architektura  
- SQLite  
- externí API (AlphaVantage)  
- Visual Studio

---

## Stav projektu

Projekt je aktivně ve vývoji:

- některé části logiky a UI jsou rozpracované,
- chování aplikace není finální,
- kód bude dále refaktorován,
- projekt je určen především pro studium a získávání zkušeností s WPF a C#.
  
---

## Screenshoty z aplikace
<img width="647" height="821" alt="image" src="https://github.com/user-attachments/assets/d3b59ff3-7b6a-4ed2-afda-3dd072107e86" />
<img width="643" height="825" alt="image" src="https://github.com/user-attachments/assets/f06badeb-237c-4901-bba6-6cd1f3af839d" />
<img width="642" height="824" alt="image" src="https://github.com/user-attachments/assets/f4b76d80-96c5-4103-b8aa-2d2eee82c1ee" />
<img width="641" height="822" alt="image" src="https://github.com/user-attachments/assets/2bd9de16-64bc-4b59-8285-88b6a9079068" />

