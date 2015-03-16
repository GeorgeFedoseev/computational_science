using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {

        static double my_factorial(long num){
            if (num < 2)
                return 1;
            return num*my_factorial(num - 1);
        }

        static double my_sgn(double num) {
            return num == 0 ? 0 : (num > 0 ? 1 : -1);
        }

        static double my_abs(double num) {
            return num >= 0 ? num : -num;
        }

        static double my_pow(double num, int pow) { 
            double res = 1;
            while(pow > 0){
                res *= num;
                pow--;
            }

            return res;
        }


        static double my_arctg(double num, double eps) {
            double sum = 0;
            double R = double.MaxValue;
            
            if (my_abs(num) < 1) {
                int k = 0;
                while (my_abs(R) > eps) {
                    R = Math.Pow(-1, k) * Math.Pow(num, 2 * k + 1) / (2 * k + 1);
                    sum += R;
                    k++;
                }

                return sum;
            }else{
                int k = 0;
                while (my_abs(R) > eps) {
                    R = Math.Pow(-1, k) * Math.Pow(num, -(2 * k + 1)) / (2 * k + 1);
                    sum += R;
                    k++;
                }

                return (Math.PI/2)*my_sgn(num) - sum;
            }
        }

        static double my_sin(double num, double eps) {
            /*if(my_abs(num) > Math.PI/4)
                throw new Exception("Abs argument of my_sin must be between 0 and pi/4");*/

            double sum = 0;
            double R = double.MaxValue;
            int k = 0;
            while (my_abs(R) > eps) {
                R = Math.Pow(-1, k) * Math.Pow(num, 2 * k + 1) / my_factorial(2 * k + 1);
                sum += R;
                k++;
            }

            return sum;
        }

        static double my_sqrt(double num, double eps){
            if(num < 0)
                throw new Exception("Argument must be positive value");
            double delta = double.MaxValue;
            double val = num, prev_val = 1;
            while (delta > eps) { 
                val = 0.5*(prev_val + num/prev_val);
                delta = my_abs(val - prev_val);
                prev_val = val;
            }

            return val;
        }

        static void Main(string[] args)
        {

            double eps = 0.000001;
            double epsu = eps / 2;
            double epsv = eps / 2;
            double epsk = epsu / 3 / 0.48;
            double epsSqrt = epsv / 2;

            Console.WriteLine("Precision: " + Math.Log10(1 / eps).ToString()+" decimals after dot");

            Console.WriteLine("    My        Std    ");

            for (double x = 0.2; x <= 0.3; x += 0.01) {
                double m = 1 - Math.Pow(x, 2);
                double k = my_sqrt(0.9 * x + 1, epsk / 2);
                double u = my_arctg(k / m, epsu / 3);
                double v = my_sin(3 * x + 0.6, epsv / 2);
                double my_value = u + v;
                double std_value = Math.Atan(Math.Sqrt(0.9*x+1)/(1-x*x)) + Math.Sin(3*x+0.6);

                Console.Write(String.Format("{0:F" + Math.Log10(1 / eps).ToString() + "}    ", my_value));
                Console.Write(String.Format("{0:F" + Math.Log10(1 / eps).ToString() + "}\n", std_value));
            }

            

            
        }


    }
}
