using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Calculators
{
    public class CompoundInterestCalculatorService
    {
        public decimal TotalAmount { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }

        public CompoundInterestCalculatorService
            (
                decimal initialDeposit,
                decimal regularDeposit,
                decimal depositFrequency,
                decimal annualInterestRate,
                decimal termYears
            )
        {
            //Výpočet složeného úročení
            decimal x = (1 + ((annualInterestRate / 100) / depositFrequency));
            decimal y = (depositFrequency * termYears);

            decimal helper = (decimal)Math.Pow((double)x, (double)y);
            decimal totalAmountWithoutInterest = initialDeposit + (regularDeposit * (depositFrequency * termYears));
            if (annualInterestRate != 0)
            {
                TotalAmount = (initialDeposit * helper) + (regularDeposit * ((helper - 1) / ((annualInterestRate / 100) / depositFrequency)));
            } else
            {
                //Pokud je úroková sazba nulová
                TotalAmount = totalAmountWithoutInterest;
            }
            PrincipalAmount = totalAmountWithoutInterest;
            InterestAmount = TotalAmount - totalAmountWithoutInterest;
        }
    }
}
