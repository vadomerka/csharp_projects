namespace PointLibrary
{
    public class Point
    {
		private double _x;
		private double _y;

		public double X
		{
			get { return _x; }
		}

		public double Y
		{
			get { return _y; }
		}

		public Point() : this(0, 0) { }

        public Point(double x, double y)
        {
			_x = x;
			_y = y;
        }

		// Сомневаюсь в реализации.
		public bool IsNearFunc(Func<double, double> func, double epsilon)
		{
			if(func == null) return false;

			double y = func(_x);
			if(y != _y) return false;

			epsilon = Math.Abs(epsilon);
            return func(_x - epsilon) <= y && y <= func(_x - epsilon);
		}
    }
}