namespace FunctionLibrary.AlternativeSolution
{
    public class CompFunctionAlternative
    {
        protected FunctionAlternative _f;
        protected FunctionAlternative _g;

        public CompFunctionAlternative(FunctionAlternative f, FunctionAlternative g)
        {
            if (f == null || g == null) throw new ArgumentNullException();
            _f = f;
            _g = g;
        }

        public double Calculate(double x)
        {
            return _g.Calculate(_f.Calculate(x));
        }
    }
}
