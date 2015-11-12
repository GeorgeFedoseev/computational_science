using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace _2015_minimization
{

    public class QuadraticFunc {
        public Matrix A;
        public Vector3 b;
        public double K;

        public QuadraticFunc(Matrix _A, Vector3 _b, double _K)
        {
            A = _A;
            b = _b;
            K = _K;
        }

        public double f(Vector3 v)
        {
            return 0.5 * (v.m.T() * A * v.m).get(0, 0) + (v.m.T() * b.m).get(0, 0) + K;
        }

        public Vector3 grad(Vector3 v) {
            return new Vector3(A * v.m + b.m); 
        }
        
    }

    public class ResultPointValue
    {
        public Vector3 point;
        public double value;

        public ResultPointValue(Vector3 _p, double _v)
        {
            point = _p;
            value = _v;
        }

        public string ToString() {
            return string.Format("func({0}, {1}, {2}) -> {3}", point.x, point.y, point.z, value);
        }
    }

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

    class GraphData
    {
        private string title;
        private Color color;
        private PointPairList pvData;


        public GraphData(string _title, PointPairList _pvData, Color _color)
        {
            title = _title;
            pvData = _pvData;
            color = _color;
        }

        public string getTitle() { return title; }
        public Color getColor() { return color; }
        public PointPairList getData() { return pvData; }
    }

    static class GraphGenerator { 
        public static GraphData generateGraphForFunc(string title, Func<double, double> _f, Interval interval, double step, Color color)
        {

            PointPairList ppList = new PointPairList();

            for (int i = 0; i < interval.length / step; i++) {
                double x = interval.from + (double)i * step;
                double y = _f(x);

                ppList.Add(x, y);
            }

            return new GraphData(title, ppList, color);
        }
    }
}
