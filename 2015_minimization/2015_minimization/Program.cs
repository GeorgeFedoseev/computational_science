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

            /* START POINT */
            var startPoint = new Vector3(1, 1, 1);

            /* EPS */
            double eps = 1e-6;

            /* FUNCTION (N = 18) */
            var A = new Matrix(new double[3, 3] { {4, 1, 1}, {1, 9.6, -1 }, {1, -1, 11.6}});
            var b = new Vector3(1, -2, 3);
            var K = 18;
            var func = new QuadraticFunc(A, b, K);


            /* MINIMIZE */
            Minimizer minimizer = new Minimizer(func, eps);

            Console.WriteLine(
                minimizer.coordinateDescent(startPoint)
                );

            Console.WriteLine(
                minimizer.gradientDescent(startPoint)
                );
         
        }
    }
}
