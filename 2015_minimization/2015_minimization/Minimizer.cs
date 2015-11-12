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

        public MinimizationResult coordinateDescent(Vector3 startPoint) {
            Vector3 currentPoint, prevPoint;
            currentPoint = startPoint;

            int _iter_count = 0;

            int i = 1;
            do{
                var ei = new Vector3(Matrix.unitVector(3, i));
                double step = -ei*new Vector3(func.A*currentPoint.m + func.b.m)
                                / (ei*new Vector3(func.A*ei.m)); // мю k-ое

                prevPoint = currentPoint;
                currentPoint = currentPoint+ei*step;
                //Console.WriteLine(string.Format("accuracy: {0} (need: {1})", (func.A * currentPoint.m + func.b.m).eNorm(), accuracy));

                i++; if(i > 3) i = 1;                
                _iter_count++;
            } while ((func.A * currentPoint.m + func.b.m).eNorm() > accuracy);

            return new MinimizationResult(currentPoint, func.f(currentPoint), _iter_count, "Coordinate Descent");
        }

        public MinimizationResult gradientDescent(Vector3 startPoint)
        {
            Vector3 currentPoint, prevPoint;
            currentPoint = startPoint;

            int _iter_count = 0;

            int i = 1;
            do
            {
                var q = func.grad(currentPoint); // q = A*xk + b = grad(xk)
                double step = -Math.Pow(q.norm(), 2)
                                / (q.m.T()*func.A*q.m).scalar(); // мю k-ое

                prevPoint = currentPoint;
                currentPoint = currentPoint + q * step;
                //Console.WriteLine(string.Format("accuracy: {0} (need: {1})", (func.A * currentPoint.m + func.b.m).eNorm(), accuracy));

                i++; if (i > 3) i = 1;
                _iter_count++;
            } while ((func.A * currentPoint.m + func.b.m).eNorm() > accuracy);

            return new MinimizationResult(currentPoint, func.f(currentPoint), _iter_count, "Gradient Descent");
        }

        private MinimizationResult linearDescent(Func<Vector3, double> f, Vector3 startPoint, Vector3 direction, 
                                                Interval range, double step, double accuracy) 
        {
            double minVal = double.MaxValue;
            Vector3 minPoint = startPoint;
            int _iter_count = 0;
            for (int i = 0; i < range.length / step; i++) {
                var point = startPoint + (range.from + i*step)*direction.normalized();
                if (f(point) < minVal) {
                    minPoint = point;
                }
                _iter_count++;
            }

            return new MinimizationResult(minPoint, minVal, _iter_count, "Linear Descent");
        }

    }
}
