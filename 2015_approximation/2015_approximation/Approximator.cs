using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _2015_approximation
{

    class Approximator
    {
        private Func<double, double> f;
        private int nodes_count;
        Interval interval;
        bool equidistant;

        public Approximator(Func<double, double> _f, int _nodes_count, Interval _interval, bool _equidistant = true) {
            init(_f, _nodes_count, _interval, _equidistant);
        }

        private void init(Func<double, double> _f, int _nodes_count,  Interval _interval, bool _equidistant = true){
            f = _f;
            nodes_count = _nodes_count;
            interval = _interval;
            equidistant = _equidistant;               
        }






        #region LEAST SQUARE

        /*** LEAST SQUARE ***/

        public Func<double, double> leastSquare() {
            List<Func<double, double>> phi = new List<Func<double, double>> { 
                _x => Math.Pow(_x, 3),
                _x => Math.Pow(_x, 2),
                _x => _x,
                _x => 1
            };

            var nodes = generateEquidistantInterpolationNodes();

            var Q = makeQ(phi, nodes);
            var y = makeY(nodes);
            SLESolver slvr = new SLESolver(Q.T() * Q, Q.T() * y);
            var a = slvr.solveWithGauss(); // вектор-столбец оптимальных коэфф

            return x => {
                double res = 0;
                for (int i = 0; i < phi.Count; i++)
                    res += a.get(i, 0) * phi[i](x);
                return res;
            };
        }

        private Matrix makeY(List<double> nodes) {
            Matrix Y = new Matrix(nodes.Count, 1);
            for (int i = 0; i < nodes.Count; i++)
                Y.set(i, 0, f(nodes[i]));
            return Y;
        }

        private Matrix makeQ(List<Func<double, double>> phi, List<double> nodes) {
            Matrix Q = new Matrix(nodes.Count, phi.Count);
            for(int i = 0; i < Q.getHeight(); i++)
                for(int j = 0; j < Q.getWidth(); j++)
                    Q.set(i, j, phi[j](nodes[i]));
            return Q;
        }
        #endregion

        #region NODES GENERATORS
        private List<double> generateSpecialInterpolationNodes(){
            List<double> nodes = new List<double>();
            
            for (var i = 0; i <= nodes_count; i++) {
                nodes.Insert(0, 
                        (double)0.5 * 
                            (interval.length * 
                                (double)Math.Cos((2 * (double)i + 1) / (2 * (nodes_count + 1)) * (double)Math.PI)
                                + interval.from + interval.to)
                    );
            }

            return nodes;
        }

        private List<double> generateEquidistantInterpolationNodes()
        {
            List<double> nodes = new List<double>();

            for (var i = 0; i <= nodes_count; i++)
            {
                nodes.Add(interval.from + (double)i * interval.length / nodes_count);
            }

            return nodes;
        }
        #endregion

    }
}
