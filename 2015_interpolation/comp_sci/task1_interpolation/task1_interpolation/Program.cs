using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace task1_interpolation
{

    static class Program
    {

        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var interval = new Interval(-1, 1);
            double step = 0.01f;

            /* FOR f(x) */
            Interpolator interpolator_f = new Interpolator(f, 20, interval, false);
            Interpolator interpolator_f_eq = new Interpolator(f, 20, interval, true);

            var originalGraph_f = GraphGenerator.generateGraphForFunc("original", f, interval, step, Color.Red);
            var interpolatedGraph_f = GraphGenerator.generateGraphForFunc("interpolated", interpolator_f.lagrangePolynom, interval, step * 10, Color.Green);
            var interpolatedGraph_f_eq = GraphGenerator.generateGraphForFunc("interpolated_eq", interpolator_f_eq.lagrangePolynom, interval, step * 10, Color.Blue);


            List<GraphData> graphsList_f = new List<GraphData>() {
                originalGraph_f, interpolatedGraph_f, interpolatedGraph_f_eq
            };

            Graph form_f = new Graph("Lagrange interpolation for f(x)", graphsList_f);
            form_f.Show();



            /* FOR h(x) */
            Interpolator interpolator_h = new Interpolator(h, 8, interval, false);
            Interpolator interpolator_h_eq = new Interpolator(h, 8, interval, true);

            var originalGraph_h= GraphGenerator.generateGraphForFunc("original", h, interval, step, Color.Red);
            var interpolatedGraph_h = GraphGenerator.generateGraphForFunc("interpolated", interpolator_h.lagrangePolynom, interval, step * 10, Color.Green);
            var interpolatedGraph_h_eq = GraphGenerator.generateGraphForFunc("interpolated_eq", interpolator_h_eq.lagrangePolynom, interval, step * 10, Color.Blue);

            List<GraphData> graphsList_h = new List<GraphData>() {
                originalGraph_h, interpolatedGraph_h, interpolatedGraph_h_eq
            };


            Graph form_h = new Graph("Lagrange interpolation for h(x)", graphsList_h);
            form_h.Show();


            Application.Run(form_f);

            // dont exit app
           // while (true) ;            
        }


        public static double f(double x)
        {
            return x * x - Math.Log(x+2);
        }

        public static double h(double x) {
            return Math.Abs(x) * f(x);
        }


        
    }
}
