using System.ComponentModel;
using MoneyPlanner.Service.Calculators;

namespace MoneyPlanner.ViewModel.Calculators
{
    public class NetIncomeCalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private NetIncomeCalculatorService _calculatorService;
        public NetIncomeCalculatorViewModel(NetIncomeCalculatorService calculatorService)
        {
            _calculatorService = _calculatorService;
        }

        private string _grossIncome;
        private decimal _employeeSocialInsurance;
        private decimal _employeeHealthInsurance;
        private decimal _taxAdvanceBeforeAllowance;
        private decimal _taxAdvance;
        private decimal _taxBonus;
        private decimal _netIncome;
        private decimal _employerSocialInsurance;
        private decimal _employerHealthInsurance;
        private decimal _employerCostPerEmployee;
        private bool _taxpayerAllowanceCheckbox;
        private decimal _taxpayerAllowanceValue;
        private string _taxDeductionChildrenCount;
        private decimal _taxDeductionChildrenValue;
        private string _taxDeductionZtppChildrenCount;
        private bool _lowerInvalidityAllowanceCheckbox;
        private decimal _lowerInvalidityAllowanceValue;
        private bool _higherInvalidityAllowanceCheckbox;
        private decimal _higherInvalidityAllowanceValue;
        private bool _ztppAllowanceCheckbox;
        private decimal _ztppAllowanceValue;
        private bool _spouseAllowanceCheckbox;
        private decimal _spouseAllowanceValue;
        private bool _ztppSpouseAllowanceCheckbox;
        private decimal _ztppSpouseAllowanceValue;
        private decimal grossIncome = 0;
        private void RefreshForm()
        {
            //NetIncomeCalculatorService calculatorService = new NetIncomeCalculatorService
            //    (
            //    grossIncome, 
            //    TaxpayerAllowanceCheckbox, 
            //    GetChildrenCount(TaxDeductionChildrenCount), 
            //    GetChildrenCount(TaxDeductionZtppChildrenCount),
            //    LowerInvalidityAllowanceCheckbox,
            //    HigherInvalidityAllowanceCheckbox,
            //    ZtppAllowanceCheckbox,
            //    SpouseAllowanceCheckbox,
            //    ZtppSpouseAllowanceCheckbox
            //    );
            _calculatorService.Calculate
                (
                grossIncome,
                TaxpayerAllowanceCheckbox, 
                GetChildrenCount(TaxDeductionChildrenCount), 
                GetChildrenCount(TaxDeductionZtppChildrenCount),
                LowerInvalidityAllowanceCheckbox,
                HigherInvalidityAllowanceCheckbox,
                ZtppAllowanceCheckbox,
                SpouseAllowanceCheckbox,
                ZtppSpouseAllowanceCheckbox
                );
            EmployeeSocialInsurance = _calculatorService.SocialInsurance;
            EmployeeHealthInsurance = _calculatorService.HealthInsurance;
            TaxAdvanceBeforeAllowance = _calculatorService.TaxAdvanceBeforeAllowance;
            TaxAdvance = _calculatorService.TaxAdvance;
            TaxBonus = _calculatorService.TaxBonus;
            NetIncome = _calculatorService.NetIncome;
            EmployerSocialInsurance = _calculatorService.EmployerSocialInsurance;
            EmployerHealthInsurance = _calculatorService.EmployerHealthInsurance;
            EmployerCostPerEmployee = _calculatorService.EmployerCostPerEmployee;
            TaxpayerAllowanceValue = _calculatorService.TaxpayerAllowanceValue;
            TaxDeductionChildrenValue = _calculatorService.TaxDeductionChildrenValue;
            LowerInvalidityAllowanceValue = _calculatorService.LowerInvalidityAllowanceValue;
            HigherInvalidityAllowanceValue = _calculatorService.HigherInvalidityAllowanceValue;
            ZtppAllowanceValue = _calculatorService.ZtppAllowanceValue;
            SpouseAllowanceValue = _calculatorService.SpouseAllowanceValue;
            ZtppSpouseAllowanceValue = _calculatorService.ZtppSpouseAllowanceValue;
        }
        public string GrossIncome
        {
            get { return _grossIncome; }
            set
            {
                _grossIncome = value;
                decimal.TryParse(_grossIncome, out grossIncome);
                OnPropertyChanged(nameof(GrossIncome));
                RefreshForm();
            }
        }
        public decimal EmployeeSocialInsurance
        {
            get { return _employeeSocialInsurance; }
            set
            {
                _employeeSocialInsurance = value;
                OnPropertyChanged(nameof(EmployeeSocialInsurance));
            }
        }
        public decimal EmployeeHealthInsurance
        { 
            get { return _employeeHealthInsurance; }
            set
            {
                _employeeHealthInsurance = value;
                OnPropertyChanged(nameof(EmployeeHealthInsurance));
            }
        }
        public decimal TaxAdvanceBeforeAllowance
        {
            get { return _taxAdvanceBeforeAllowance; }
            set
            {
                _taxAdvanceBeforeAllowance = value;
                OnPropertyChanged(nameof(TaxAdvanceBeforeAllowance));
            }
        }
        public decimal TaxAdvance
        {
            get { return _taxAdvance; }
            set
            {
                _taxAdvance = value;
                OnPropertyChanged(nameof(TaxAdvance));
            }
        }
        public decimal TaxBonus
        {
            get { return _taxBonus; }
            set
            {
                _taxBonus = value;
                OnPropertyChanged(nameof(TaxBonus));
            }
        }
        public decimal NetIncome
        {
            get { return _netIncome; }
            set
            {
                _netIncome = value;
                OnPropertyChanged(nameof(NetIncome));
            }
        }
        public decimal EmployerSocialInsurance
        {
            get { return _employerSocialInsurance; }
            set
            {
                _employerSocialInsurance = value;
                OnPropertyChanged(nameof(EmployerSocialInsurance));
            }
        }
        public decimal EmployerHealthInsurance
        {
            get { return _employerHealthInsurance; }
            set
            {
                _employerHealthInsurance = value;
                OnPropertyChanged(nameof(EmployerHealthInsurance));
            }
        }
        public decimal EmployerCostPerEmployee
        {
            get { return _employerCostPerEmployee; }
            set
            {
                _employerCostPerEmployee = value;
                OnPropertyChanged(nameof(EmployerCostPerEmployee));
            }
        }
        public bool TaxpayerAllowanceCheckbox
        {
            get { return _taxpayerAllowanceCheckbox; }
            set
            {
                _taxpayerAllowanceCheckbox = value;
                OnPropertyChanged(nameof(TaxpayerAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal TaxpayerAllowanceValue
        {
            get { return _taxpayerAllowanceValue; }
            set
            {
                _taxpayerAllowanceValue = value;
                OnPropertyChanged(nameof(TaxpayerAllowanceValue));
            }

        }
        public string TaxDeductionChildrenCount
        {
            get { return _taxDeductionChildrenCount; }
            set
            {
                _taxDeductionChildrenCount = value;
                OnPropertyChanged(nameof(TaxDeductionChildrenCount));
                RefreshForm();
            }
        }
        public decimal TaxDeductionChildrenValue
        {
            get { return _taxDeductionChildrenValue; }
            set
            {
                _taxDeductionChildrenValue = value;
                OnPropertyChanged(nameof(TaxDeductionChildrenValue));
            }
        }
        public string TaxDeductionZtppChildrenCount
        {
            get { return _taxDeductionZtppChildrenCount; }
            set
            {
                _taxDeductionZtppChildrenCount = value;
                OnPropertyChanged(nameof(TaxDeductionZtppChildrenCount));
                RefreshForm();
            }
        }
        public bool LowerInvalidityAllowanceCheckbox
        {
            get { return _lowerInvalidityAllowanceCheckbox; }
            set
            {
                _lowerInvalidityAllowanceCheckbox = value;
                OnPropertyChanged(nameof(LowerInvalidityAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal LowerInvalidityAllowanceValue
        {
            get { return _lowerInvalidityAllowanceValue; }
            set
            {
                _lowerInvalidityAllowanceValue = value;
                OnPropertyChanged(nameof(LowerInvalidityAllowanceValue));
            }

        }
        public bool HigherInvalidityAllowanceCheckbox
        {
            get { return _higherInvalidityAllowanceCheckbox; }
            set
            {
                _higherInvalidityAllowanceCheckbox = value;
                OnPropertyChanged(nameof(HigherInvalidityAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal HigherInvalidityAllowanceValue
        {
            get { return _higherInvalidityAllowanceValue; }
            set
            {
                _higherInvalidityAllowanceValue = value;
                OnPropertyChanged(nameof(HigherInvalidityAllowanceValue));
            }

        }
        public bool ZtppAllowanceCheckbox
        {
            get { return _ztppAllowanceCheckbox; }
            set
            {
                _ztppAllowanceCheckbox = value;
                OnPropertyChanged(nameof(ZtppAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal ZtppAllowanceValue
        {
            get { return _ztppAllowanceValue; }
            set
            {
                _ztppAllowanceValue = value;
                OnPropertyChanged(nameof(ZtppAllowanceValue));
            }

        }
        public bool SpouseAllowanceCheckbox
        {
            get { return _spouseAllowanceCheckbox; }
            set
            {
                _spouseAllowanceCheckbox = value;
                OnPropertyChanged(nameof(SpouseAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal SpouseAllowanceValue
        {
            get { return _spouseAllowanceValue; }
            set
            {
                _spouseAllowanceValue = value;
                OnPropertyChanged(nameof(SpouseAllowanceValue));
            }

        }
        public bool ZtppSpouseAllowanceCheckbox
        {
            get { return _ztppSpouseAllowanceCheckbox; }
            set
            {
                _ztppSpouseAllowanceCheckbox = value;
                OnPropertyChanged(nameof(ZtppSpouseAllowanceCheckbox));
                RefreshForm();
            }
        }
        public decimal ZtppSpouseAllowanceValue
        {
            get { return _ztppSpouseAllowanceValue; }
            set
            {
                _ztppSpouseAllowanceValue = value;
                OnPropertyChanged(nameof(ZtppSpouseAllowanceValue));
            }

        }

        //Metoda pro převod hodnoty z ComboBoxu s počtem dětí na int
        public int GetChildrenCount(string comboValue)
        {
            switch (comboValue)
            {
                case "Žádné":
                    return 0;

                case "1 dítě":
                    return 1;

                case "2 děti":
                    return 2;

                case "3 děti":
                    return 3;

                case "4 děti":
                    return 4;

                case "5 dětí":
                    return 5;

                case "6 dětí":
                    return 6;

                case "7 dětí":
                    return 7;

                case "8 dětí":
                    return 8;

                case "9 dětí":
                    return 9;

                case "10 dětí":
                    return 10;

                default:
                    return 0;
            }
        }
    }
}

