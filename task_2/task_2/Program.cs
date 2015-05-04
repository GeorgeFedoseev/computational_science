using System;




namespace task_2
{
    class Program
    {
        static void Main(string[] args)
        {


            double N = 13;
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

            var sol = solver.solveWithIterativeMethod(eps);
            
            Console.WriteLine("\nSolution:");
            Console.WriteLine(sol);

            Console.WriteLine("Check:");
            Console.WriteLine(A*sol-b);



            Console.ReadKey();


            Console.WriteLine("********  ill-conditioned ********");


            double _eps = 0.0000001;
            var _dim = 4;
            while (_dim < 80) {
                var _A_l = new Matrix(_dim);
                for (int i = 0; i < _A_l.getHeight(); i++) { 
                    for(int j = 0; j < _A_l.getWidth(); j++){
                        if (j > i)
                            _A_l.set(i, j, -1);
                    }
                }

                //Console.WriteLine(_A_l);

                var _A_r = new Matrix(_dim);
                for (int i = 0; i < _A_r.getHeight(); i++) { 
                    for(int j = 0; j < _A_r.getWidth(); j++){
                        if (j > i)
                            _A_r.set(i, j, -1);
                        else
                            _A_r.set(i, j, 1);
                    }
                }

                var _b = new Matrix(_dim, 1);

                for(int i = 0; i < _b.getHeight(); i++){
                    if (i < _dim - 1)
                        _b.set(i, 0, -1);
                    else
                        _b.set(i, 0, 1);
                }

                
                //Console.WriteLine(_A_r);

                Console.WriteLine("[DIM={0}]", _dim);
                Console.WriteLine("eps={0}; N={1};", _eps, N);
                var _A = _A_l + _eps * N * _A_r;

                Console.WriteLine("A: \n"+_A);
                Console.WriteLine("b: \n" + _b);

                Console.WriteLine("Cond(A) = {0}", A.eNorm()*A.inverse().eNorm());
                Console.WriteLine("det(A) = {0}", A.det());

                var _solver = new SLESolver(_A, _b);
                var _sol = _solver.solveWithGauss();
                
                Console.WriteLine("Solution(GAUSS):\n" + _sol);
                Console.WriteLine("Check:\n" + (_A * _sol - _b));

                _sol = _solver.solveWithIterativeMethod(_eps);

                Console.WriteLine("Solution(ITERATIVE):\n"+_sol);
                Console.WriteLine("Check:\n" + (_A*_sol-_b));


                _dim++;
            }
                       

            Console.ReadKey();
        }
    }
}
