using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using GoogleChartSharp;

namespace task1_interpolation
{

    public class Dataset
    {
        public float[] nodes, values;

        public Dataset(float[] _nodes, float[] _values)
        {
            nodes = _nodes;
            values = _values;
        }
    }


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


            var interval = new Interval(1, 20);
            float step = 1f;

            Interpolator interpolator = new Interpolator(f, 10, interval);

            var originalFunctionDataset = generateDatasetsForFunc(f, interval, step);
            var interpolatedFunctionDataset = generateDatasetsForFunc(interpolator.lagrangePolynom, interval, step);            


            List<float[]> datasets = new List<float[]>() {
                originalFunctionDataset.nodes, originalFunctionDataset.values,
                interpolatedFunctionDataset.nodes, interpolatedFunctionDataset.values
            };


            Graph gr = new Graph(datasets, (string imageUrl) =>
            {
                Console.WriteLine(imageUrl);
                Application.Run(new GraphForm(imageUrl));
            });


            // dont exit app
            while (true) ;            
        }
               

        public static float f(float x)
        {
            return x * x - 1 - (float)Math.Log(x);
        }


        /* UTILS */
        private static Dataset generateDatasetsForFunc(Func<float, float> _f, Interval interval, float step)
        {           
            List<float> nodes = new List<float>();
            List<float> values = new List<float>();

            for (int i = 0; i < interval.length / step; i++) {
                float node = interval.from + (float)i * step;
                float value = _f(node);

                nodes.Add(node);
                values.Add(value);
            }

            return new Dataset(nodes.ToArray(), values.ToArray());
        }

        
    }
}
