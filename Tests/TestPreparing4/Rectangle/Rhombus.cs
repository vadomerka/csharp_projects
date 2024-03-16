using System.Collections;

namespace Rhombus
{
    public class Rhombus
    {
        private double _d = 1;
        private double _p = 1;

        public Rhombus() { }

        public Rhombus(double x, double y) 
        {
            if (x <= 0 || y <= 0) throw new ArgumentException();
            _d = x;
            _p = y;
        }

        public Rhombus(string? wkt)
        { 
            if (string.IsNullOrEmpty(wkt)) throw new ArgumentNullException();
            try
            {
                wkt = wkt.Replace('.', ',');
                _d = double.Parse(wkt.Split(';', StringSplitOptions.RemoveEmptyEntries)[0]);
                _p = double.Parse(wkt.Split(';', StringSplitOptions.RemoveEmptyEntries)[1]);
            }
            catch 
            {
                throw new FormatException();
            }
        }

        public double Space()
        {
            double a = _p / 4;
            double b = Math.Sqrt(a * a - (_d / 2) * (_d / 2));
            return a * b * 2;
        }

        public override string ToString()
        {
            return $"{_d:f3};{_p:f3}";
        }
    }
}