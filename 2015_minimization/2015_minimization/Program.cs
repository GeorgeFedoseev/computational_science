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

        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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


            Vector3 v = new Vector3(1, 2, 3);
            Console.WriteLine((v*2).ToString());
            Console.WriteLine(v.x);
            Console.WriteLine(f(v));

            // dont exit app
           // while (true) ;            
        }


        public static double f(Vector3 v)
        {
            double N = 18;
            return 2 * v.x * v.x + (3 + 0.1 * N) * v.y * v.y 
                + (4 + 0.1 * N) * v.z * v.z
                + v.x * v.y - v.y * v.z
                + v.x * v.z + v.x
                - 2 * v.y + 3 * v.z + N;
        }
        
    }
}
