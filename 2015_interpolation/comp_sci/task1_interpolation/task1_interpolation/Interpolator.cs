using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace task1_interpolation
{

    public class Interval {
        public float from, to;

        public float length { 
            get {
                return getlength();
             }
        }

        public Interval(float _from, float _to){
            from = _from;
            to = _to;          
        }

        private float getlength()
        {
            return to - from;
        }
    }

    class Interpolator
    {
        private Func<float, float> f;
        private int nodes_count;
        Interval interval;
        bool equidistant;
               

        public Interpolator(Func<float, float> _f, int _nodes_count, Interval _interval, bool _equidistant = true) {
            init(_f, _nodes_count, _interval, _equidistant);
        }


        private void init(Func<float, float> _f, int _nodes_count,  Interval _interval, bool _equidistant = true){
            f = _f;
            nodes_count = _nodes_count;
            interval = _interval;
            equidistant = _equidistant;

            var nodes = generateSpecialInterpolationNodes();

            Console.WriteLine(string.Join(", ", nodes));            
        }


        public float lagrangePolynom(float x) {           

            List<float> nodes;
            if (equidistant)
                nodes = generateEquidistantInterpolationNodes();
            else
                nodes = generateSpecialInterpolationNodes();


            float result = 0;
            for (int i = 0; i <= nodes_count; i++) {
                float summand = 1;
                for (int j = 0; j <= nodes_count; j++) {
                    if (i != j)
                        summand *= (x - nodes[j]) / (nodes[i] - nodes[j]);
                }

                summand *= f(nodes[i]);

                result += summand;
            }

            return result;
        }


        private List<float> generateSpecialInterpolationNodes(){
            List<float> nodes = new List<float>();
            
            for (var i = 0; i <= nodes_count; i++) {
                nodes.Insert(0, 
                        (float)0.5 * 
                            (interval.length * 
                                (float)Math.Cos((2 * (float)i + 1) / (2 * (nodes_count + 1)) * (float)Math.PI)
                                + interval.from + interval.to)
                    );
            }

            return nodes;
        }

        private List<float> generateEquidistantInterpolationNodes()
        {
            List<float> nodes = new List<float>();

            for (var i = 0; i <= nodes_count; i++)
            {
                nodes.Add(interval.from + (float)i * interval.length / nodes_count);
            }

            return nodes;
        }

        

        
    }
}
