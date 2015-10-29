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

            var interval = new Interval(1, 100);
            double step = 0.01f;

           

            var originalGraph_f = GraphGenerator.generateGraphForFunc("original", f, interval, step, Color.Red);
           


            List<GraphData> graphsList_f = new List<GraphData>() {
                originalGraph_f
            };

            Graph form_f = new Graph("Lagrange interpolation for f(x)", graphsList_f);
            form_f.Show();           


            Application.Run(form_f);

            // dont exit app
           // while (true) ;            
        }


        public static double f(double x)
        {
            return x * x - 1 - Math.Log(x);
        }
        
    }
}
