using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace task_2
{
    class Program
    {
        static void Main(string[] args)
        {


            double N = 0;
            Matrix A = new Matrix(new double[,] {  {N+2, 1, 1},
                                                    {1, N+4, 1},
                                                    {1, 1, N+6}
            });

            Matrix b = new Matrix(new double[,] {   { N+4 },
                                                    { N+6 },
                                                    { N+8 }
            });


            Console.WriteLine("A:");
            Console.WriteLine(A);

            Console.WriteLine("b:");
            Console.WriteLine(b);

            
           
            
            var solver = new SLESolver(A, b);

            Console.WriteLine("SOVE USING GAUSS METHOD:");
            var solution = solver.solveWithGauss();

            Console.WriteLine("\nSolution:");
            Console.WriteLine(solution);
            Console.WriteLine("Check:");
            Console.WriteLine(A*solution-b);


            Console.WriteLine("SOVE USING ITERATION METHOD:");
            double eps = 0.000001;
            Console.WriteLine("eps: {0}\n", eps);

            var sol = solver.solverWithIterativeMethod(eps);
            
            Console.WriteLine("\nSolution:");
            Console.WriteLine(sol);

            Console.WriteLine("Check:");
            Console.WriteLine(A*sol-b);

            Console.ReadKey();
        }
    }
}
