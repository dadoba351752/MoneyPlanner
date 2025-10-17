using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyPlanner.Service.Calculators;
using System.Globalization;

namespace MoneyPlanner.ViewModel.Calculators
{
    public class CompoundInterestCalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _initialDeposit;
        private string _regularDeposit;
        private string _depositFrequency = "Roční";
        private string _annualInterestRate;
        private string _termYears;
        private string _totalAmount;
        private string _principalAmount;
        private string _interestAmount;

        private decimal initialDeposit;
        private decimal regularDeposit;
        private decimal annualInterestRate;
        private decimal termYears;

        public void Calculate()
        {
            CompoundInterestCalculatorService calculatorService = new CompoundInterestCalculatorService
                (
                initialDeposit,
                regularDeposit,
                getDepositFrequency(DepositFrequency),
                annualInterestRate,
                termYears
                );
            TotalAmount = calculatorService.TotalAmount.ToString("N1", CultureInfo.GetCultureInfo("cs-CZ"));
            PrincipalAmount = calculatorService.PrincipalAmount.ToString("N1", CultureInfo.GetCultureInfo("cs-CZ"));
            InterestAmount = calculatorService.InterestAmount.ToString("N1", CultureInfo.GetCultureInfo("cs-CZ"));
        }

        public string InitialDeposit 
        { 
            get { return _initialDeposit; }
            set
            {
                _initialDeposit = value;
                decimal.TryParse(InitialDeposit, out initialDeposit);
                OnPropertyChanged(nameof(InitialDeposit));
            }
        }

        public string RegularDeposit
        {
            get { return _regularDeposit; }
            set
            {
                _regularDeposit = value;
                decimal.TryParse(RegularDeposit, out regularDeposit);
                OnPropertyChanged(nameof(RegularDeposit));
            }
        }

        public string DepositFrequency
        {
            get { return _depositFrequency; }
            set
            {
                _depositFrequency = value;
                OnPropertyChanged(nameof(DepositFrequency));
            }
        }

        public string AnnualInterestRate
        {
            get { return _annualInterestRate; }
            set
            {
                _annualInterestRate = value;
                decimal.TryParse(AnnualInterestRate, out annualInterestRate);
                OnPropertyChanged(nameof(AnnualInterestRate));
            }
        }

        public string TermYears
        {
            get { return _termYears; }
            set
            {
                _termYears = value;
                decimal.TryParse(TermYears, out termYears);
                OnPropertyChanged(nameof(TermYears));
            }
        }

        public string TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public string PrincipalAmount
        {
            get { return _principalAmount; }
            set
            {
                _principalAmount = value;
                OnPropertyChanged(nameof(PrincipalAmount));
            }
        }

        public string InterestAmount
        {
            get { return _interestAmount; }
            set
            {
                _interestAmount = value;
                OnPropertyChanged(nameof(InterestAmount));
            }
        }

        //Metoda pro převod pravidelnosti vkladu
        public int getDepositFrequency(string comboValue)
        {
            switch (comboValue)
            {
                case "Roční":
                    return 1;

                case "Půlroční":
                    return 2;

                case "Čtvrtletní":
                    return 4;

                case "Měsíční":
                    return 12;

                case "Týdenní":
                    return 52;

                default:
                    throw new ApplicationException("Invalid deposit frequency.");
            }
        }
    }
}
