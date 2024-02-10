namespace FunctionLibrary
{
    public class Function : IFunction
    {
        protected FunctionDelegate _func;

        public Function(FunctionDelegate func)
        {
            if (func == null) throw new ArgumentNullException();
            _func = (x) => func(x);
        }

        public double Calculate(double x)
        {
            return _func(x);
        }
    }
}