#pragma once

#include "Matrix.h"
#include <string>
#include <iostream>
#include <cmath>
#include <stdexcept>


namespace task_2
{
	class SLESolver
	{

	private:
		Matrix A, b;

	public:
		SLESolver(Matrix _A, Matrix _b){

			if (_A.getWidth() < 1 || _A.getHeight() < 1 || _A.getHeight() != _A.getWidth() || _A.getHeight() != _b.getHeight() || _b.getWidth() != 1)
			{
				throw std::exception("Wrong input for SLE solver");
			}

			if (_A.det() == 0)
			{
				throw std::exception("Matrix should be invertable for SLE solver");
			}

			A = _A;
			b = _b;
		}


		Matrix solveWithIterativeMethod(double eps, bool debug = false)
		{

			Matrix delta(A.getWidth());
			delta = delta/(2*A.eNorm());

			Matrix D = A.inverse() - delta;

			Matrix alpha = delta * A;
			Matrix beta = D * b;

			if (debug)
			{
				cout << "alpha:" << std::endl;
				cout << alpha.ToString() << std::endl;
				std::cout << "beta:" << std::endl;
				std::cout << beta.ToString() << std::endl;

				std::wcout << std::wstring(L"||alpha|| = ") << alpha.eNorm() << std::endl;
			}



			Matrix x = beta.copy();
			Matrix x_next;

			auto k = 0;
			while (true)
			{
				x_next = beta + alpha * x;

				if (debug)
				{
					std::wcout << std::wstring(L"[k=") << k << std::wstring(L" norm: ") << (x_next - x).eNorm() << std::endl;
				}

				if ((alpha.eNorm()/(1-alpha.eNorm()))*(x_next - x).eNorm() <= eps)
				{
					return x_next;
				}

				x = x_next;
				k++;
			}


		}

	private:
		static bool checkIterativeMethodCondition(Matrix &alpha)
		{
			for (int i = 0; i < alpha.getHeight(); i++)
			{
				double sum = 0;
				for (int j = 0; j < alpha.getWidth(); j++)
				{
					sum += abs(alpha.get(i, j));
				}

				if (sum >= 1)
				{
					return false;
				}
			}

			return true;
		}

	public:
		Matrix solveWithGauss()
		{
			auto Ab = A.appendRight(b);

			//cout << Ab.ToString() << endl;

			// straight
			for (int i = 0; i < Ab.getHeight(); i++)
			{
				Ab.swapRows(i, Ab.getRowNumberWithNotNullColumnStartingAtRow(i, i));
				Ab.setRow(i, Ab.getRow(i) / Ab.getRow(i).get(0, i));
				for (int k = i + 1; k < Ab.getHeight(); k++)
				{
					Matrix modRow = Ab.getRow(k);
					Ab.setRow(k, modRow - Ab.getRow(i) * modRow.get(0, i));
				}

			}


			//cout << Ab.ToString() << endl;

			//cout << "reverse" << endl;

			// reverse
			for (int i = Ab.getHeight() - 2; i >= 0; i--)
			{

				for (int k = A.getWidth() - 1; k > i; k--)
				{
					auto modRow = Ab.getRow(i);
					
					Ab.setRow(i, modRow - Ab.getRow(k) * modRow.get(0, k));
					
				}

				//cout << Ab.ToString() << endl;

			}

			//cout << Ab.ToString() << endl;

			return Ab.getColumn(Ab.getWidth() - 1);
		}

	};
}
