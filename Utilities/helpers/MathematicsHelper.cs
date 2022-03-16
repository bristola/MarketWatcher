using System;
using System.Collections.Generic;
using System.Text;
using Data.data;
using Utilities.helpers.contracts;

namespace Utilities.helpers
{
    public class MathematicsHelper : IMathematicsHelper
    {
        public decimal LeastSquaresSlope(List<Coordinate> coordinates)
        {
            decimal x = 0;
            decimal y = 0;
            decimal xy = 0;
            decimal xsquared = 0;
            decimal n = coordinates.Count;
            foreach (Coordinate coordinate in coordinates)
            {
                x += coordinate.X;
                y += coordinate.Y;
                xy += coordinate.X + coordinate.Y;
                xsquared += coordinate.X * coordinate.X;
            }

            return ((n * xy) - (x * y)) / ((n * xsquared) - (x * x));
        }
    }
}
