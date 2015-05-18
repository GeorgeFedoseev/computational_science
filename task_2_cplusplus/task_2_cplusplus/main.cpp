#include <iostream>
#include <cstdarg>
#include <vector>
#include <array>

#include "Matrix.h"
#include "SLESolver.h"
 

using namespace std;
using namespace task_2;




void main(void){	
	int m=0, n = 0;

	

	double m2_arr[][3] = {{2, 3, 4},
						{2, 3, 7}};
	n = 2;
	m = 3;

	vector<vector<double>> m2_vec(n);
	for(int i = 0; i < n; i++){
		m2_vec[i] = vector<double>(m);
		for(int j = 0; j < m; j++){
			m2_vec[i][j] = m2_arr[i][j];
		}
	}
	

	double N = 13;

	double A_arr[][3] = {
		{N + 2, 1, 1},
		{1, N + 4, 1},
		{1, 1, N + 6}
	};
	n = m = 3;
	vector<vector<double>> A_vec(n);
	for(int i = 0; i < n; i++){
		A_vec[i] = vector<double>(m);
		for(int j = 0; j < m; j++){
			A_vec[i][j] = A_arr[i][j];
		}
	}
	Matrix A(A_vec);


	double b_arr[][1] = {
		{N + 4},
		{N + 6},
		{N + 8}
	};
	n = 3;
	m = 1;
	vector<vector<double>> b_vec(n);
	for(int i = 0; i < n; i++){
		b_vec[i] = vector<double>(m);
		for(int j = 0; j < m; j++){
			b_vec[i][j] = b_arr[i][j];
		}
	}
	Matrix b(b_vec);



	cout << "A:" << std::endl;
	cout << A.ToString() << std::endl;

	cout << "b:" << std::endl;
	cout << b.ToString() << std::endl;

	auto solver = new SLESolver(A, b);
	std::wcout << std::wstring(L"SOVE USING GAUSS METHOD:") << std::endl;
	auto solution = solver->solveWithGauss();

	std::cout << "\nSolution:" << std::endl;
	std::cout << solution.ToString() << std::endl;
	std::cout << "Check:" << std::endl;
	std::cout << (A*solution - b).ToString() << std::endl;


	cout << "SOVE USING ITERATION METHOD:" << endl;
	double eps = 0.0001;
	cout << "eps: " << eps << std::endl;

	auto sol = solver->solveWithIterativeMethod(eps);
	cout << "\nSolution:" << std::endl;
	cout << sol.ToString() << std::endl;

	cout << "Check:" << std::endl;
	cout << (A*sol - b).ToString() << std::endl;

	getchar();

	std::wcout << std::wstring(L"********  ill-conditioned ********") << std::endl;


	double _eps = 0.0001;
	auto _dim = 4;

	while (_dim < 6)
	{
		Matrix _A_l(_dim);
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

		

		Matrix _A_r(_dim);
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

		Matrix _b(_dim, 1);

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

		std::wcout << std::wstring(L"[DIM=") << _dim << "]" << std::endl;
		std::wcout << std::wstring(L"eps=") << _eps << std::wstring(L"; N=") << N << std::endl;
		Matrix _A = _A_l + (_A_r * N)*_eps;

		cout << "A: \n" << _A.ToString() << std::endl;
		cout << "b: \n" << _b.ToString() << std::endl;

		std::wcout << std::wstring(L"Cond(A) = ") << A.eNorm()*A.inverse().eNorm() << std::endl;
		std::wcout << std::wstring(L"det(A) = ") << A.det() << std::endl;

		auto _solver = new SLESolver(_A, _b);
		auto _sol = _solver->solveWithGauss();

		std::cout << "Solution(GAUSS):\n" << _sol.ToString() << std::endl;
		std::cout << "Check:\n" << (_A * _sol - _b).ToString() << std::endl;

		_sol = _solver->solveWithIterativeMethod(_eps);

		std::cout << "Solution(ITERATIVE):\n" << _sol.ToString() << std::endl;
		std::cout << "Check:\n" << (_A*_sol - _b).ToString() << std::endl;


		_dim++;

	
		
	}


			
	
	getchar();
}