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

         
          /* float[] datax = new float[]{0, 1, 2, 3, 4, 5, 6};
           float[] datay = new float[]{0, 1, 4, 9, 16, 25, 36};

           float[] datax2 = new float[] { 0, 1, 2, 3, 4, 5, 6 };
           float[] datay2 = new float[] { 0, 1, 2, 3, 4, 5, 6 };

           List<float[]> datasets = new List<float[]>() { datax, datay, datax2, datay2 };


           Graph gr = new Graph(datasets, (string imageUrl)=>{
               Console.WriteLine(imageUrl);
               Application.Run(new GraphForm(imageUrl));
           });*/


            var interval = new Interval(1, 200);
            double step = 0.001f;

            Interpolator interpolator = new Interpolator(f, 2, interval);

            var originalGraph = GraphGenerator.generateGraphForFunc("x^2 - 1 - ln(x)", f, interval, step, Color.Red);
            var interpolatedGraph = GraphGenerator.generateGraphForFunc("interpolated", interpolator.lagrangePolynom, interval, step, Color.Green);


            List<GraphData> graphsList = new List<GraphData>() {
                originalGraph, interpolatedGraph
            };


            Application.Run(new Graph("Lagrange interpolation", graphsList));


            // dont exit app
           // while (true) ;            
        }


        public static double f(double x)
        {
            return x * x - 1 - Math.Log(x);
        }


        
    }
}
