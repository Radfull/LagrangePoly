using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangePoly.Model
{
    public class PointPairs
    {
        public int IterNum { get; set; }
        public double X_point { get; set; }
        public double y_point { get; set; }

        public PointPairs(int iterNum, double x, double y)
        {
            IterNum = iterNum;
            X_point = x;
            y_point = y;
        }
    }
}