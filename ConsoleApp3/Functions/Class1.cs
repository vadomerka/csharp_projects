namespace Functions
{
    public delegate double MainFunc(double x);
    public class Function
    {
        private MainFunc mainFunc;

        public Function() 
        {
            mainFunc = (double x) => x;
        }

        public Function(MainFunc newMainFunc) {
            // Костыль, могут бить.
            mainFunc = (double x) => newMainFunc?.Invoke(x) ?? x;
        }

        public double GetFuncResult(double x)
        {
            return mainFunc(x);
        }
    }

    public class CompFunction
    {
        private MainFunc mainFunc;

        public CompFunction()
        {
            mainFunc = (double x) => x;
        }

        public CompFunction(Function gFunc, Function fFunc)
        {
            mainFunc = (double x) => gFunc.GetFuncResult(fFunc.GetFuncResult(x));
        }

        public double GetFuncResult(double x)
        {
            return mainFunc(x);
        }
    }
}