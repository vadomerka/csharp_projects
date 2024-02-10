namespace ObjectClass
{
    public class _3DShape
    {
        protected double _side;
        protected string _type = "shape";

        public _3DShape(double side) 
        { 
            _side = side;
        }

        public _3DShape() 
        { 
            Random random = new Random();
            _side = random.Next(1, 100 + 1) * random.NextDouble();
        }

        public double Space()
        {
            return _side * _side;
        }

        public double Value()
        {
            return _side * _side * _side;
        }

        public override string ToString()
        {
            return $"тип фигуры: {_type}; " +
                $"сторона: {_side.ToString("0.000")}; " + 
                $"площадь: {this.Space().ToString("0.000")}; " 
                + $"объем: {this.Value().ToString("0.000")}; ";
        }
    }

    public class Cube : _3DShape 
    {
        protected string _type = "Cube";

        public double Space()
        { 
            return 6 * base.Space();
        }
    }

    public class Sphere : _3DShape
    {
        protected string _type = "Sphere";

        public double Space()
        {
            return 4 * Math.PI * _side * _side;
        }

        public double Value()
        {
            return 4 * Math.PI * _side * _side * _side / 3;
        }

        public override string ToString()
        {
            return $"тип фигуры: {_type}; " +
                $"радиус: {_side.ToString("0.000")}; " +
                $"площадь: {this.Space().ToString("0.000")}; "
                + $"объем: {this.Value().ToString("0.000")}; ";
        }
    }
}