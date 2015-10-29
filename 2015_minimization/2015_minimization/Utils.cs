using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace _2015_minimization
{


    public class Vector3{

        private Matrix m;

        public double x{
            get{return m.get(0,0);}
        }

        public double y{
            get{return m.get(1,0);}
        }

        public double z{
            get{return m.get(2,0);}
        }

        public Vector3(double _x, double _y, double _z) {            
            m = new Matrix(3, 1);
            m.set(0, 0, _x);
            m.set(1, 0, _y);
            m.set(2, 0, _z);
        }

        public Vector3(Matrix _m) {
            m = _m;
        }

        public double length() {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        static public Vector3 operator -(Vector3 v){
            return new Vector3(-v.m);
        }

        public static Vector3 operator -(Vector3 u, Vector3 v) {
            return new Vector3(u.m-v.m);
        }

        public static Vector3 operator +(Vector3 u, Vector3 v) {
            return new Vector3(u.m+v.m);
        }

        static public Vector3 operator *(Vector3 v, double k) {
            return new Vector3(v.m*k);
        }

        static public Vector3 operator /(Vector3 v, double k){
            return new Vector3(v.m/k);
        }

        public string ToString() {
            return m.ToString();
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
