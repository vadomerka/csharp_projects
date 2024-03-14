using System.Collections;

namespace Rectangle
{
    public class Rectangle
    {
        private double _x = 1;
        private double _y = 1;

        public Rectangle() { }

        public Rectangle(double x, double y) 
        {
            if (x <= 0 || y <= 0) throw new ArgumentException();
            _x = x;
            _y = y;
        }

        public Rectangle(string? wkt)
        { 
            if (string.IsNullOrEmpty(wkt)) throw new ArgumentNullException();
            try
            {
                wkt = wkt.Replace('.', ',');
                _x = double.Parse(wkt.Split(';', StringSplitOptions.RemoveEmptyEntries)[0]);
                _y = double.Parse(wkt.Split(';', StringSplitOptions.RemoveEmptyEntries)[1]);
            }
            catch 
            {
                throw new FormatException();
            }
        }

        public double Perimetre()
        {
            return (_x + _y) * 2;
        }

        public override string ToString()
        {
            return $"{_x:f3};{_y:f3}";
        }
    }
}