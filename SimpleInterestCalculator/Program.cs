using System.Transactions;

namespace SimpleInterestCalculator
{
    
    public class Program
    {
        static void Main(string[] args)
        {
            SimpleInterestCalculator simpleInterestCalculator = new SimpleInterestCalculator();
            Console.WriteLine("Banking Calculator");
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("1.Exit Program");
                Console.WriteLine("2.SimpleInterestCalculator");
                Console.WriteLine("3.CompoundInterestCalculator");
                Console.WriteLine("4.ReturnOnInvestment");
                Console.WriteLine("5.MonthlyEMICalculator");
                Console.WriteLine("6.LoanTenureCalculator");

                Console.WriteLine("Select Any Option");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option) 
                {
                    case 1 : flag = false;
                        break;

                    case 2 :
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                            int P;
                            double SIvalue = simpleInterestCalculator.SICalculator(out P);
                            Console.Write("Your Simple Interest on Loan is : ");
                            Console.WriteLine(SIvalue);
                            Console.WriteLine($"Initial Principle value : {P}");
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                        break;

                    case 3 :
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                            int p;
                            List<CIModel> listData = simpleInterestCalculator.CompoundInterest(out p);
                            Console.WriteLine($"Initial Principle value : {p}");
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                        break;
                    
                    case 4 :
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                            double investment;
                            double Gained;
                            double ROI = simpleInterestCalculator.ReturnOnInvestment(out investment, out Gained);
                            Console.WriteLine($"Total Gain on Investment : {Gained}");
                            Console.WriteLine($"Return on Investment : {ROI}%");
                            Console.WriteLine($"Initial Principle value : {investment}");
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                        break;

                    case 5 :
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                            double Amount;
                            double Interest;
                            double EMI = simpleInterestCalculator.LoanEMICalculator(out Amount, out Interest);
                            Console.WriteLine($"Monthly EMI For {Amount} with Annual Interest Rate {Interest*1200}% is {EMI}");
                            Console.WriteLine();
                            Console.WriteLine("--------------------------------------------");
                        break;

                    case 6 :
                        Console.WriteLine();
                        Console.WriteLine("--------------------------------------------");
                        double Principle;
                        double InterestRate;
                        double Emi;
                        double Tenure = simpleInterestCalculator.LoanTenureCalculator(out Principle, out InterestRate, out Emi);
                        Console.WriteLine($"It will take {Tenure} Months to Repay Loan Amount of {Principle} with Annual Interest Rate {InterestRate*1200}% and EMI {Emi}");
                        Console.WriteLine();
                        Console.WriteLine("--------------------------------------------");
                        break;

                }
                
            }  
        }
    }
}