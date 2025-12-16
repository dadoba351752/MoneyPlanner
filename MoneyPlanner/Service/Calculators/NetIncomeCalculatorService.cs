using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyPlanner.ViewModel.Calculators;

namespace MoneyPlanner.Service.Calculators
{
    public class NetIncomeCalculatorService
    {
        public decimal GrossIncome { get; set; }
        public decimal SocialInsurance { get; set; }
        public decimal HealthInsurance { get; set; }
        public decimal TaxAdvanceBeforeAllowance { get; set; }
        public decimal TaxAdvance { get; set; }
        public decimal TaxBonus { get; set; }
        public decimal NetIncome { get; set; }
        public decimal EmployerSocialInsurance { get; set; }
        public decimal EmployerHealthInsurance { get; set; }
        public decimal EmployerCostPerEmployee { get; set; }
        public decimal TaxpayerAllowanceValue { get; set; }
        public int TaxDeductionChildrenCount { get; set; }
        public int TaxDeductionZtppChildrenCount { get; set; }
        public decimal TaxDeductionChildrenValue { get; set; }
        public decimal LowerInvalidityAllowanceValue { get; set; }
        public decimal HigherInvalidityAllowanceValue { get; set; }
        public decimal ZtppAllowanceValue { get; set; }
        public decimal SpouseAllowanceValue { get; set; }
        public decimal ZtppSpouseAllowanceValue { get; set; }

        //Procenta odvodů na pojištění z hrubé mzdy
        private const decimal socialInsurance = 0.065M;
        private const decimal sickInsurance = 0.006M;
        private const decimal healthInsurance = 0.045M;

        //Hodnoty pro daňové výpočty
        private const decimal taxRateReduced = 0.15M;
        private const decimal taxRateStandard = 0.23M;
        private const decimal taxLimit = 139671M;

        //Procenta odvodů zaměstnavatele
        private const decimal employerSocialInsurance = 0.248M;
        private const decimal employerHealthInsurance = 0.09M;

        //Slevy a úlevy
        private const decimal taxpayerAllowance = 2570M;
        private const decimal firstChildAllowance = 1267M;
        private const decimal secondChildAllowance = 1860M;
        private const decimal thirdPlusChildAllowance = 2320M;
        private const decimal lowerInvalidityAllowance = 210M;
        private const decimal higherInvalidityAllowance = 420M;
        private const decimal ztppAllowance = 1345M;
        private const decimal spouseAllowanceValue = 2070M;
        private const decimal ztppSpouseAllowanceValue = 4140M;

        public NetIncomeCalculatorService()
        {

        }

        public void Calculate
            (
            decimal grossIncome,
            bool taxpayerAllowanceBool,
            int childrenCount,
            int ztppChildrenCount,
            bool lowerInvalidityBool,
            bool higherInvalidityBool,
            bool ztppAllowanceBool,
            bool spouseAllowanceBool,
            bool ztppSpouseAllowanceBool
            ) 
        {
            GrossIncome = grossIncome;
            //Spočítá sociální a nemocenské pojištění placené zaměstnancem
            SocialInsurance = decimal.Round(grossIncome * (socialInsurance + sickInsurance));
            //Spočítá zdravotní pojištění placené zaměstnancem
            HealthInsurance = decimal.Round(grossIncome * healthInsurance);
            //Spočítá zálohu na daň před odečtením slev
            if (grossIncome <= taxLimit)
            {
                TaxAdvanceBeforeAllowance = decimal.Round(grossIncome * taxRateReduced);
            }
            else TaxAdvanceBeforeAllowance = decimal.Round(grossIncome * taxRateStandard);
            //Na základě selectu true/false slevy na poplatníka přiřadí hodnotu
            if (taxpayerAllowanceBool)
            {
                TaxpayerAllowanceValue = taxpayerAllowance;
            }
            else TaxpayerAllowanceValue = 0;
            //Na základě selectu true/false slevy na invaliditu 1. a 2. stupně přiřadí hodnotu
            if (lowerInvalidityBool)
            {
                LowerInvalidityAllowanceValue = lowerInvalidityAllowance;
            }
            else LowerInvalidityAllowanceValue = 0;
            //Na základě selectu true/false slevy na invaliditu 3. stupně přiřadí hodnotu
            if (higherInvalidityBool)
            {
                HigherInvalidityAllowanceValue = higherInvalidityAllowance;
            }
            else HigherInvalidityAllowanceValue = 0;
            //Na základě selectu true/false slevy na držitele ZTP/P přiřadí hodnotu
            if (ztppAllowanceBool)
            {
                ZtppAllowanceValue = ztppAllowance;
            }
            else ZtppAllowanceValue = 0;
            //Na základě selectu true/false slevy na držitele ZTP/P přiřadí hodnotu
            if (spouseAllowanceBool)
            {
                SpouseAllowanceValue = spouseAllowanceValue;
            }
            else SpouseAllowanceValue = 0;
            //Na základě selectu true/false slevy na držitele ZTP/P přiřadí hodnotu
            if (ztppSpouseAllowanceBool)
            {
                ZtppSpouseAllowanceValue = ztppSpouseAllowanceValue;
            }
            else ZtppSpouseAllowanceValue = 0;
            //Spočítá slevu na dani za děti a za děti s průkazem ZTP/P
            TaxDeductionChildrenValue = decimal.Round(CalculateChildAllowance(childrenCount, ztppChildrenCount));
            //Spočítá zálohu na daň po odečtení slev a daňový bonus
            decimal totalAllowance = TaxAdvanceBeforeAllowance - TaxpayerAllowanceValue - TaxDeductionChildrenValue - LowerInvalidityAllowanceValue - HigherInvalidityAllowanceValue - ZtppAllowanceValue - SpouseAllowanceValue - ZtppSpouseAllowanceValue;
            if (totalAllowance < 0)
            {
                TaxAdvance = decimal.Round(0);
                TaxBonus = decimal.Round(totalAllowance * (-1));
            } else 
                TaxAdvance = decimal.Round(totalAllowance);
            //Spočítá čistou mzdu
            NetIncome = decimal.Round(grossIncome - SocialInsurance - HealthInsurance - TaxAdvance);
            //Spočítá sociální pojištění placené zaměstnavatelem
            EmployerSocialInsurance = decimal.Round(grossIncome * employerSocialInsurance);
            //Spočítá zdravotní pojištění placené zaměstnavatelem
            EmployerHealthInsurance = decimal.Round(grossIncome * employerHealthInsurance);
            //Spočítá celkové náklady zaměstnavatele na zaměstnance
            EmployerCostPerEmployee = decimal.Round(grossIncome + EmployerSocialInsurance + EmployerHealthInsurance);
        }    

        //Metoda pro výpočet slevy na dani za děti
        private decimal CalculateChildAllowance(int childCount, int ztppCount)
        {
            if (childCount < ztppCount) { ztppCount = childCount; }
            if ((childCount == 1) && (ztppCount == 1))//Pokud child = 1 a ztpp = 1
            {
                return firstChildAllowance * 2;
            } else if ((childCount == 1) && !(ztppCount == 1))//Pokud child = 1 a ztpp = 0
            {
                return firstChildAllowance;
            }
            if ((childCount == 2) && (ztppCount ==2))//Pokud child = 2 a ztpp = 2
            {
                return (firstChildAllowance * 2) + (secondChildAllowance * 2);
            } else if ((childCount == 2) && (ztppCount == 1))//Pokud child = 2 a ztpp = 1
            {
                return (firstChildAllowance) + (secondChildAllowance * 2);
            } else if ((childCount == 2) && (ztppCount == 0))//Pokud child = 2 a ztpp = 0
            {
                return (firstChildAllowance) + (secondChildAllowance);
            }
            if ((childCount >= 3) && (childCount - ztppCount == 0))//Pokud je child >= 3 a ztpp = 0
            {
                return (firstChildAllowance * 2) + (secondChildAllowance * 2) + ((thirdPlusChildAllowance * 2) * (childCount - 2));
            }
            else if ((childCount >= 3) && (childCount - ztppCount == 1))//Pokud je child >= 3 a ztpp = 1
            {
                return (firstChildAllowance) + (secondChildAllowance * 2) + ((thirdPlusChildAllowance * 2) * (childCount - 2));
            }
            else if ((childCount >= 3) && (childCount - ztppCount >= 2))//Pokud je child >= 3 a ztpp >= 2
            {
                return (firstChildAllowance) + (secondChildAllowance) + (thirdPlusChildAllowance * (childCount - 2)) + (thirdPlusChildAllowance * ztppCount);
            }
            else return 0;
        }
    }
}
