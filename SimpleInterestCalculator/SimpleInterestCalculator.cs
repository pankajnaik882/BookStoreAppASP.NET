using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SimpleInterestCalculator
{
    public class SimpleInterestCalculator
    {
        
        public double SICalculator( out int Principle)
        {
            Console.WriteLine("Simple Interest Calculator");
            Console.WriteLine("Principle Amount");
            Principle = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Interest Rate");
            double IntrestRate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("TimePeriod In Years");
            double TimePeriodInYears = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Timeperiod In Months");
            double TimeperiodInMonths = (Convert.ToDouble(Console.ReadLine())) / 12;

            double Timeperiod = TimePeriodInYears + TimeperiodInMonths;
            double SI = (Principle * IntrestRate * Timeperiod)/100;

            
            return SI;
        }

        public List<CIModel>  CompoundInterest(out int Principle)
        {
            List<CIModel> CImodellist = new List<CIModel>();

            Console.WriteLine("Compound Interest Calculator");
            Console.WriteLine("Principle Amount");
            Principle = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Interest Rate");
            double InterestRate = (Convert.ToDouble(Console.ReadLine())/100);
            Console.WriteLine("TimePeriod In Years");
            double TimePeriodInYears = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Number of Times Interest Compunded");
            double CompoundAnnual = Convert.ToDouble(Console.ReadLine());

            for ( int T = 1; T< TimePeriodInYears +1 ; T++ )
            {
                CIModel cIModel = new CIModel();
                cIModel.Amount = Principle * Math.Pow( 1+(InterestRate/CompoundAnnual), (CompoundAnnual*T) );

                cIModel.CI = cIModel.Amount - Principle;
                Console.WriteLine($"Your Compound Interest Generated {cIModel.CI} for Year {T}");
                Console.WriteLine($"Your Amount Generated {cIModel.Amount}");

                CImodellist.Add(cIModel);
            }
            return CImodellist;
        }

        public double ReturnOnInvestment (out double invested, out double TotalGain )
        {
            
            Console.WriteLine("Return on Investment Calculator");
            Console.WriteLine("Amount Invested");
            invested = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Amount Returned");
            double returned = Convert.ToDouble(Console.ReadLine());

            TotalGain = returned - invested;
            double ROI = ((TotalGain)/invested)*100 ;

            return ROI;
        }

        public double LoanEMICalculator(out double Amount, out double InterestRate )
        {
            Console.WriteLine("Loan Monthly EMI Calculator");
            Console.WriteLine("Loan Amount");
            Amount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Annual Interest");
            InterestRate = Convert.ToDouble(Console.ReadLine())/1200;
            Console.WriteLine("Choose Time Period to Repay Loan (IN YEARS) ");
            double time = Convert.ToDouble(Console.ReadLine());

            double timeInMonths = time * 12;

            double EMI = (InterestRate * Amount) / (1- Math.Pow((1+InterestRate), -timeInMonths));

            return EMI;
        }

        public double LoanTenureCalculator(out double Principle , out double InterestRate , out double EMI)
        {
            Console.WriteLine("Loan Tenure Calculator");
            Console.WriteLine("Loan Amount");
            Principle = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Annual Interest");
            InterestRate = Convert.ToDouble(Console.ReadLine()) / 1200;
            Console.WriteLine("Enter EMI You will Able to Pay/ Paying");
            EMI = Convert.ToDouble(Console.ReadLine());

            double Tenure = (Math.Log(EMI) - Math.Log(EMI - (Principle * InterestRate))) / Math.Log(1 + InterestRate);

            return Math.Round(Tenure);
        }
    }

    public class CIModel
    {
        public double Amount { get; set; }

        public double CI { get; set; }
    }

}
