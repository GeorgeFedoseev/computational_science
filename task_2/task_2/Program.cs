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
            Matrix A = new Matrix(new double[,]{{1, 2},{3, 4}});
            Matrix b = new Matrix(new double[,] { { 5, 7}, { 6, 8 } });
            Console.WriteLine(A);
            Console.WriteLine(b);
            Console.WriteLine(A.appendRight(b));
            Console.ReadKey();
        }
    }
}
