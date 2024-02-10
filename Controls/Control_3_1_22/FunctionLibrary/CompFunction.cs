namespace FunctionLibrary
{
    public class CompFunction : IFunction
    {
        protected Function _f;
        protected Function _g;

        public CompFunction(Function f, Function g)
        {
            if(f == null || g == null) throw new ArgumentNullException();
            _f = f;
            _g = g;
        }

        public double Calculate(double x)
        {
            return _g.Calculate(_f.Calculate(x));
        }
    }
}
