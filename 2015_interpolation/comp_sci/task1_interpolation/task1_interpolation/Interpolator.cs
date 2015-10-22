using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace task1_interpolation
{

    public class Interval {
        public double from, to;

        public double length { 
            get {
                return getlength();
             }
        }

        public Interval(double _from, double _to){
            from = _from;
            to = _to;          
        }

        private double getlength()
        {
            return to - from;
        }
    }

    class Interpolator
    {
        private Func<double, double> f;
        private int nodes_count;
        Interval interval;
        bool equidistant;
               

        public Interpolator(Func<double, double> _f, int _nodes_count, Interval _interval, bool _equidistant = true) {
            init(_f, _nodes_count, _interval, _equidistant);
        }


        private void init(Func<double, double> _f, int _nodes_count,  Interval _interval, bool _equidistant = true){
            f = _f;
            nodes_count = _nodes_count;
            interval = _interval;
            equidistant = _equidistant;

            var nodes = generateSpecialInterpolationNodes();

          //  Console.WriteLine(string.Join(", ", nodes));            
        }


        public double lagrangePolynom(double x) {           

            List<double> nodes;
            if (equidistant)
                nodes = generateEquidistantInterpolationNodes();
            else
                nodes = generateSpecialInterpolationNodes();


            double result = 0;
            for (int i = 0; i <= nodes_count; i++) {
                double summand = 1;
                for (int j = 0; j <= nodes_count; j++) {
                    if (i != j)
                        summand *= (x - nodes[j]) / (nodes[i] - nodes[j]);
                }

                summand *= f(nodes[i]);

                result += summand;
            }

            return result;
        }


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

    }
}
