using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_minimization
{

    public class ResultPointValue{
        public Vector3 point;
        public double value;

        public ResultPointValue(Vector3 _p, double _v){
            point = _p;
            value = _v;
        }
    }

    public class Minimizer
    {
        //private Func<Vector3, double> f;

        public ResultPointValue coordinateDescent(Func<Vector3, double> f, Vector3 startPoint) {
            Vector3 currentPoint, prevPoint;
            currentPoint = startPoint;

            do{

            }while();

            var grad_n = Vector3.grad_n(f, currentPoint);
        }

        private ResultPointValue linearDescent(Func<Vector3, double> f, Vector3 startPoint, Vector3 coordinate, 
                                                Interval range, double step, double accuracy) 
        {
            double minVal = double.MaxValue;
            Vector3 minPoint = startPoint;
            for (int i = 0; i < range.length / step; i++) {
                var point = startPoint + (range.from + i*step)*coordinate;
                if (f(point) < minVal) {
                    minPoint = point;
                }
            }

            return new ResultPointValue(minPoint, minVal);
        }

    }
}
