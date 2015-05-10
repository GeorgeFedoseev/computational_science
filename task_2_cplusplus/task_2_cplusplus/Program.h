#pragma once

#include "Matrix.h"
#include "SLESolver.h"
#include <string>
#include <iostream>





namespace task_2
{
	class Program
	{
		static void Main(std::wstring args[])
		{


			double N = 13;
			Matrix *A = new Matrix(new double*[]
			{
				{N + 2, 1, 1},
				{1, N + 4, 1},
				{1, 1, N + 6}
			});

			Matrix *b = new Matrix(new double*[]
			{
				{N + 4},
				{N + 6},
				{N + 8}
			});


			std::wcout << std::wstring(L"A:") << std::endl;
			std::wcout << A << std::endl;

			std::wcout << std::wstring(L"b:") << std::endl;
			std::wcout << b << std::endl;




			auto solver = new SLESolver(A, b);

			std::wcout << std::wstring(L"SOVE USING GAUSS METHOD:") << std::endl;
			auto solution = solver.solveWithGauss();

			std::wcout << std::wstring(L"\nSolution:") << std::endl;
			std::wcout << solution << std::endl;
			std::wcout << std::wstring(L"Check:") << std::endl;
			std::wcout << A*solution - b << std::endl;


			std::wcout << std::wstring(L"SOVE USING ITERATION METHOD:") << std::endl;
			double eps = 0.000001;
			std::wcout << std::wstring(L"eps: ") << eps << std::endl;

			auto sol = solver.solveWithIterativeMethod(eps);

			std::wcout << std::wstring(L"\nSolution:") << std::endl;
			std::wcout << sol << std::endl;

			std::wcout << std::wstring(L"Check:") << std::endl;
			std::wcout << A*sol - b << std::endl;



			Console::ReadKey();


			std::wcout << std::wstring(L"********  ill-conditioned ********") << std::endl;


			double _eps = 0.0000001;
			auto _dim = 4;
			while (_dim < 80)
			{
				auto _A_l = new Matrix(_dim);
				for (int i = 0; i < _A_l.getHeight(); i++)
				{
					for (int j = 0; j < _A_l.getWidth(); j++)
					{
						if (j > i)
						{
							_A_l.set(i, j, -1);
						}
					}
				}

				//Console.WriteLine(_A_l);

				auto _A_r = new Matrix(_dim);
				for (int i = 0; i < _A_r.getHeight(); i++)
				{
					for (int j = 0; j < _A_r.getWidth(); j++)
					{
						if (j > i)
						{
							_A_r.set(i, j, -1);
						}
						else
						{
							_A_r.set(i, j, 1);
						}
					}
				}

				auto _b = new Matrix(_dim, 1);

				for (int i = 0; i < _b.getHeight(); i++)
				{
					if (i < _dim - 1)
					{
						_b.set(i, 0, -1);
					}
					else
					{
						_b.set(i, 0, 1);
					}
				}


				//Console.WriteLine(_A_r);

				std::wcout << std::wstring(L"[DIM=") << _dim << std::endl;
				std::wcout << std::wstring(L"eps=") << _eps << std::wstring(L"; N=") << N << std::endl;
				auto _A = _A_l + _eps * N * _A_r;

				std::wcout << std::wstring(L"A: \n") << _A << std::endl;
				std::wcout << std::wstring(L"b: \n") << _b << std::endl;

				std::wcout << std::wstring(L"Cond(A) = ") << A.eNorm()*A.inverse().eNorm() << std::endl;
				std::wcout << std::wstring(L"det(A) = ") << A.det() << std::endl;

				auto _solver = new SLESolver(_A, _b);
				auto _sol = _solver.solveWithGauss();

				std::wcout << std::wstring(L"Solution(GAUSS):\n") << _sol << std::endl;
				std::wcout << std::wstring(L"Check:\n") << (_A * _sol - _b) << std::endl;

				_sol = _solver.solveWithIterativeMethod(_eps);

				std::wcout << std::wstring(L"Solution(ITERATIVE):\n") << _sol << std::endl;
				std::wcout << std::wstring(L"Check:\n") << (_A*_sol - _b) << std::endl;


				_dim++;
			}


			Console::ReadKey();
		}
	};
}
