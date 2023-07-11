namespace SimpleInterestCalculator
{
    
    public class Program
    {
        static void Main(string[] args)
        {

            SimpleInterestCalculator simpleInterestCalculator= new SimpleInterestCalculator();

            Console.WriteLine("Simple Interest Calculator");
            Console.WriteLine("Principle Amount");
            int Principle = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Interest Rate");
            double IntrestRate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("TimePeriod In Years");
            double TimePeriodInYears = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Timeperiod In Months");
            double TimeperiodInMonths = (Convert.ToDouble(Console.ReadLine()))/12;

            double Timeperiod = TimePeriodInYears + TimeperiodInMonths;


            Console.Write("Your Simple Interest on Loan is : ");
            Console.WriteLine(simpleInterestCalculator.SICalculator(Principle, IntrestRate, Timeperiod));
            Console.Write("For Your Principle Amount : ");
            Console.WriteLine(Principle);
            

            
        }
    }
}