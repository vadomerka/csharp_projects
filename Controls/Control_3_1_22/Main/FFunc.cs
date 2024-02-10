using FunctionLibrary;

namespace Main
{
    public class FFunc : IFunction
    {
        public double Calculate(double x)
        {
            return Math.Sin(x);
        }
    }
}
