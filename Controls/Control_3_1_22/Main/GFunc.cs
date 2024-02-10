using FunctionLibrary;

namespace Main
{
    public class GFunc : IFunction
    {
        public double Calculate(double x)
        {
            return x * x + x;
        }
    }
}
