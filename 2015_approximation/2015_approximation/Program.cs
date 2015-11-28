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
            
            Approximator approximator = new Approximator(f, 5, interval);

            var originalGraph_f = GraphGenerator.generateGraphForFunc("исходная ф-ция", f, interval, step, Color.Red);
            var leastSquareGraph_f = GraphGenerator.generateGraphForFunc("наименьшие квадраты", approximator.leastSquare(), interval, step, Color.Green);
            
            var legendreGraph_f = GraphGenerator.generateGraphForFunc("Лежандр", approximator.legendre(), interval, step, Color.Orange);



            List<GraphData> graphsList_f = new List<GraphData>() {
                originalGraph_f, leastSquareGraph_f, legendreGraph_f
            };

            Graph form_f = new Graph("Аппроксимация f(x) = (x+3)cos(x)", graphsList_f);
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
