using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace _2015_minimization
{

    static class Program
    {   
        
        static QuadraticFunc func;

        

        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            double eps = 1e-6;

            var A = new Matrix(new double[3, 3] { {4, 1, 1}, {1, 9.6, -1 }, {1, -1, 11.6}});
            var b = new Vector3(1, -2, 3);
            var K = 18;

            func = new QuadraticFunc(A, b, K);

            Minimizer minimizer = new Minimizer(func, eps);
            Console.WriteLine(
                minimizer.coordinateDescent(new Vector3(0, 0, 0))
                );
            

         /*   var interval = new Interval(1, 100);
            double step = 0.01f;

           

            var originalGraph_f = GraphGenerator.generateGraphForFunc("original", f, interval, step, Color.Red);
           


            List<GraphData> graphsList_f = new List<GraphData>() {
                originalGraph_f
            };

            Graph form_f = new Graph("Lagrange interpolation for f(x)", graphsList_f);
            form_f.Show();           


            Application.Run(form_f);*/


            //////////////


            

            // dont exit app
           // while (true) ;            
        }


        
        
    }
}
