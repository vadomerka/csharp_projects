using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Circle
    {
        private double _diametr;

        public Circle() { _diametr = 0; }

        public Circle(double d) {
            if (d > 0)
            {
                _diametr = d;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public double Diametr
        {
            get { return _diametr; }
            set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _diametr = value; 
            }
        }

        public double Radius()
        {
            return _diametr / 2;
        }

        public double Space()
        {
            return Math.PI * _diametr / 2 * _diametr / 2;
        }

        public override string ToString() 
        {
            return $"d = {_diametr.ToString("#.###")} r = {this.Radius().ToString("#.###")} S = {this.Space().ToString("#.###")}";
        }
    }
}
