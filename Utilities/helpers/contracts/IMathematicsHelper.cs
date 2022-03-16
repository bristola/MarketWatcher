using Data.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.helpers.contracts
{
    public interface IMathematicsHelper
    {
        decimal LeastSquaresSlope(List<Coordinate> coordinates);
    }
}
