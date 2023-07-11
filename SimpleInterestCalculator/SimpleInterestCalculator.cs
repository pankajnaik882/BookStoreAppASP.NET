using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterestCalculator
{
    public class SimpleInterestCalculator
    {
        
        public double SICalculator(int principle, double interestRate, double timePeriod)
        {
            double SI = (principle * interestRate * timePeriod)/100;
            return SI;
        }
    }
}
