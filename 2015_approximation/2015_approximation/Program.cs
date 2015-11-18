using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2015_approximation
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ZedGraph;

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
            Approximator interpolator_f = new Approximator(f, 20, interval, false);
            Approximator interpolator_f_eq = new Approximator(f, 20, interval, true);

            var originalGraph_f = GraphGenerator.generateGraphForFunc("original", f, interval, step, Color.Red);
            var interpolatedGraph_f = GraphGenerator.generateGraphForFunc("interpolated", interpolator_f.lagrangePolynom, interval, step * 10, Color.Green);
            var interpolatedGraph_f_eq = GraphGenerator.generateGraphForFunc("interpolated_eq", interpolator_f_eq.lagrangePolynom, interval, step * 10, Color.Blue);


            List<GraphData> graphsList_f = new List<GraphData>() {
            originalGraph_f, interpolatedGraph_f, interpolatedGraph_f_eq
        };

            Graph form_f = new Graph("Lagrange interpolation for f(x)", graphsList_f);
            form_f.Show();


            Application.Run(form_f);

            // dont exit app
            // while (true) ;            
        }


        public static double f(double x)
        {
            return (x + 3) * Math.Cos(x);
        }



    }
    

}
