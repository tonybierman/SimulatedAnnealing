using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    public class City
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double DistanceTo(City other)
        {
            double dx = other.X - this.X;
            double dy = other.Y - this.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
