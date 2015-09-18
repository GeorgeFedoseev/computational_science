using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using GoogleChartSharp;

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


            Interapolator interpolator = new Interapolator(f);


            // dont exit app
            while (true) ;            
        }

        public static float f(float x)
        {
            return x * x - 1 - (float)Math.Log(x);
        }

        
    }
}
