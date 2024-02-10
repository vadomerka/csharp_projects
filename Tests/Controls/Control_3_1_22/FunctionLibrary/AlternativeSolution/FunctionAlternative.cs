namespace FunctionLibrary.AlternativeSolution
{
    public class FunctionAlternative : IFunction
    {
        protected IFunction _func;

        public FunctionAlternative(IFunction func)
        {
            if (func == null) throw new ArgumentNullException();
            _func = func;
        }

        public double Calculate(double x)
        {
            return _func.Calculate(x);
        }
    }
}
