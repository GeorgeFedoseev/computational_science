using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_minimization
{   

    public class Minimizer
    {

        QuadraticFunc func;
        double accuracy;

        public Minimizer(QuadraticFunc _func, double _accuracy)
        {
            func = _func;
            accuracy = _accuracy;
        }



        public ResultPointValue coordinateDescent(Func<Vector3, double> f, Vector3 startPoint) {
            Vector3 currentPoint, prevPoint;
            currentPoint = startPoint;

            do{

            }while();

            var grad_n = Vector3.grad_n(f, currentPoint);

            return new ResultPointValue(new Vector3(new Matrix(3)), 0);
        }

        private ResultPointValue linearDescent(Func<Vector3, double> f, Vector3 startPoint, Vector3 direction, 
                                                Interval range, double step, double accuracy) 
        {
            double minVal = double.MaxValue;
            Vector3 minPoint = startPoint;
            for (int i = 0; i < range.length / step; i++) {
                var point = startPoint + (range.from + i*step)*direction.normalized();
                if (f(point) < minVal) {
                    minPoint = point;
                }
            }

            return new ResultPointValue(minPoint, minVal);
        }

    }
}
