using System;

namespace ClassLibrary
{
    public class Point
    {
        private double _x;
        private double _y;

        public Point() { }

        public Point(double x, double y) 
        {
            this._x = x;
            this._y = y;
        }

        public double X => _x;
        public double Y => _y;

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator /(Point p1, double num)
        {
            if (num == 0) throw new ArgumentException();
            return new Point(p1.X / num, p1.Y / num);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public static double VectorLen(Point p1, Point p2)
        {
            Point p3 = p2 - p1;
            return Math.Sqrt(p3._x * p3._x + p3._y * p3._y);
        }

        public static Point Median(Point p1, Point p2)
        {
            return (p2 + p1) / 2;
        }

        public override string ToString()
        {
            return $"({this._x:f3};{this._y:f3})";
        }
    }
    public class Circle
    {
        private Point _p1;
        private Point _p2;
        private double _d;

        public Circle() { }

        public Circle(double x1, double y1, double x2, double y2) 
        {
            // Композиция.
            this._p1 = new Point(x1, y1);
            this._p2 = new Point(x2, y2);
            this._d = Point.VectorLen(_p1, _p2);
        }

        public Circle(string wkt)
        {
            // Конструктор работает со строками вида: "(x1;y1) (x2;y2)"
            if (!(wkt.StartsWith("(") && wkt.EndsWith(")"))) throw new ArgumentException();
            if (wkt.Split(") (").Length != 2) throw new ArgumentException();
            string textPoint1 = wkt.Split(") (")[0][1..];
            string textPoint2 = wkt.Split(") (")[1][..^1];
            
            this._p1 = new Point(double.Parse(textPoint1.Split(';')[0]), 
                                 double.Parse(textPoint1.Split(';')[1]));
            this._p2 = new Point(double.Parse(textPoint2.Split(';')[0]),
                                 double.Parse(textPoint2.Split(';')[1]));
            this._d = Point.VectorLen(_p1, _p2);
        }

        public Point Point1 => _p1;
        public Point Point2 => _p2;
        public double diametre =>_d;

        public static bool operator >(Circle c1, Circle c2)
        {
            Point c1Centre = Point.Median(c1._p1, c1._p2);
            Point c2Centre = Point.Median(c2._p1, c2._p2);
            double d = Point.VectorLen(c1Centre, c2Centre);
            double r1 = c1.diametre / 2;
            double r2 = c2.diametre / 2;
            if (d < r1 - r2)
            {
                return true;
            } 
            return false;
        }

        public static bool operator <(Circle c1, Circle c2)
        {
            return c2 > c1;
        }

        public static bool operator ==(Circle c1, Circle c2)
        {
            return c1.diametre == c2.diametre && Point.Median(c1._p1, c1._p2) == Point.Median(c2._p1, c2._p2);
        }

        public static bool operator !=(Circle c1, Circle c2)
        {
            return !(c1 == c2);
        }

        public override string ToString()
        {
            return $"{this._p1:f3} {this._p2:f3}";
        }
    }
}