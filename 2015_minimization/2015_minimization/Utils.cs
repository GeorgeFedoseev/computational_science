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

        public static double drv(Func<Vector3, double> f, Vector3 p, Vector3 dir)
        {
            double h = 0.000001;
            return (-f(new Vector3(p.x + 2 * h * dir.x, p.y + 2 * h * dir.y, p.z + 2 * h * dir.z))
                    + 8 * f(new Vector3(p.x + h * dir.x, p.y + h * dir.y, p.z + h * dir.z))
                    - 8 * f(new Vector3(p.x - h * dir.x, p.y - h * dir.y, p.z - h * dir.z))
                    + f(new Vector3(p.x - 2 * h * dir.x, p.y - 2 * h * dir.y, p.z - 2 * h * dir.z))) / (12 * h);
        }

        public static Vector3 grad(Func<Vector3, double> f, Vector3 p)
        {
            return new Vector3(
                drv(f, p, new Vector3(1, 0, 0)),
                drv(f, p, new Vector3(0, 1, 0)),
                drv(f, p, new Vector3(0, 0, 1))
            );
        }

        public static Vector3 grad_n(Func<Vector3, double> f, Vector3 p)
        {
            var gr = grad(f, p);
            var n = gr.length();
            return new Vector3(
                    gr.x / n,
                    gr.y / n,
                    gr.z / n
                );
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

        static public Vector3 operator *(double k, Vector3 v)
        {
            return v * k;
        }

        static public Vector3 operator /(Vector3 v, double k){
            return new Vector3(v.m/k);
        }

        public override string  ToString() {
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
