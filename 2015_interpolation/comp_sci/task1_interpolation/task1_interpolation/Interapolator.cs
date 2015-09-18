using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace task1_interpolation
{

    public class Interval {
        public float from, to;

        public Interval(float _from, float _to){
            from = _from;
            to = _to;
        }
    }

    class Interapolator
    {
        private Func<float, float> f;
        private int nodes_count;
        Interval interval;
               

        public Interapolator(Func<float, float> _f, int _nodes_count, Interval _interval) {
            init(_f, _nodes_count, _interval);
        }


        private void init(Func<float, float> _f, int _nodes_count,  Interval _interval){
            f = _f;
            nodes_count = _nodes_count;
            interval = _interval;
        }


        private List<float> generateEquidistantInterpolationNodes(){
            return null;
        }

        
    }
}
