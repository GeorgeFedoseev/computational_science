#pragma once

#include <string>
#include <iostream>
#include <cmath>
#include <stdexcept>
#include <vector>
#include <string>

using namespace std;


namespace task_2
{
	class Matrix
	{

	protected:
		std::vector<std::vector<double>> data;		

	public:

		Matrix()
		{
			Matrix(1);
		}

		Matrix(Matrix &m)
		{
			data = m.data;			
		}

		Matrix(std::vector<std::vector<double>> arr)
		{
			if (arr.size() < 1 || arr[0].size() < 1)
			{
				throw std::exception("Matrix cant be null");
			}

			data = arr;			
		}

		Matrix(int n, int m)
		{
			if (n < 1 || m < 1)
			{
				throw std::exception("Matrix cant be null");
			}

			data = std::vector<std::vector<double>>(n);
			for(int i = 0; i < n; i++){
				data[i] = std::vector<double>(m);
				for(int j = 0; j < m; j++){
					data[i][j] = 0;
				}
			}

		}

		Matrix(int n)
		{
			if (n < 1)
			{
				throw std::exception("Matrix cant be null");
			}

			auto res = new Matrix(n, n);
			data = res->data;

			for (int i = 0; i < n; i++)
			{
				data[i][i] = 1;
			}
		}


		

		Matrix  operator-()
		{

			Matrix res(*this);

			for (int i = 0; i < this->getHeight(); i++)
			{
				for (int j = 0; j < this->getWidth(); j++)
				{
					res.data[i][j] = -this->data[i][j];
				}
			}

			return res;
		}

		Matrix operator-(Matrix &m2)
		{			
			return -m2+(*this);
		}

		Matrix operator+(Matrix  &m2)
		{
			if (this->getWidth() != m2.getWidth() || this->getHeight() != m2.getHeight())
			{
				throw std::exception("Matrix must have same dim for adding");
			}

			Matrix res(this->getHeight(), this->getWidth());

			for (int i = 0; i < this->getHeight(); i++)
			{
				for (int j = 0; j < this->getWidth(); j++)
				{
					res.data[i][j] = this->data[i][j] + m2.data[i][j];
				}
			}

			return res;
		}

		Matrix operator *(double k)
		{
			Matrix res(this->getHeight(), this->getWidth());
			for (int i = 0; i < this->getHeight(); i++)
			{
				for (int j = 0; j < this->getWidth(); j++)
				{
					res.data[i][j] = this->data[i][j] * k;
				}
			}

			return res;
		}

		Matrix operator *(Matrix &m2)
		{
			if (this->getWidth() != m2.getHeight())
			{
				throw std::exception("Matrixes should: m1.w = m2.h for multpl");
			}

			Matrix res(this->getHeight(), m2.getWidth());

			for (int i = 0; i < res.getHeight(); i++)
			{
				for (int j = 0; j < res.getWidth(); j++)
				{
					double val = 0;
					for (int r = 0; r < this->getWidth(); r++)
					{
						val += this->data[i][r] * m2.data[r][j];
					}
					res.data[i][j] = val;
				}
			}

			return res;
		}

		/*Matrix *operator *(Matrix *m1)
		{
			return m1 * this;
		}*/

		Matrix operator / (double k)
		{
			return *this * (1 / k);
		}


		double det(bool debug = false)
		{
			if (getWidth() != getHeight())
			{
				throw std::exception("For det calculation matrix must be square");
			}

			if (debug)
			{
				std::wcout << std::wstring(L"det:start") << std::endl;
			}

			if (getWidth() == 1)
			{
				return get(0, 0);
			}

			double res = 0;
			for (int j = 0; j < getWidth(); j++)
			{
				res += pow(-1, j)*get(0, j) * crossOutRowAndCol(0, j).det();
			}

			if (debug)
			{
				std::wcout << std::wstring(L"det:done: ") << res << std::endl;
			}

			return res;
		}

		Matrix crossOutRowAndCol(int row, int col, bool debug = false)
		{
			if (getWidth() <= 1 || getHeight() <= 1 || row < 0 || row >= getHeight() || col < 0 || col >= getWidth())
			{
				throw std::exception("Wrong data to cross out col and row");
			}

			if (debug)
			{
				std::wcout << std::wstring(L"crossOutRowAndCol:start") << std::endl;
			}

			Matrix res(getHeight() - 1, getWidth() - 1);

			for (int i = 0; i < getHeight(); i++)
			{
				for (int j = 0; j < getWidth(); j++)
				{
					if (i != row && j != col)
					{
						auto i_m = i > row ? i - 1 : i;
						auto j_m = j > col ? j - 1 : j;

						res.data[i_m][j_m] = data[i][j];
					}
				}
			}

			if (debug)
			{
				std::wcout << std::wstring(L"crossOutRowAndCol:done") << std::endl;
			}

			return res;

		}



		Matrix inverse(bool debug = false)
		{
			if (getWidth() != getHeight())
			{
				throw std::exception("Matrix should be square to be inversed");
			}

			if (debug)
			{
				std::wcout << std::wstring(L"inverse:start") << std::endl;
			}

			double detA = det();

			if (detA == 0)
			{
				throw std::exception("Matrix isn't inversable (det = 0)");
			}


			Matrix res(getHeight(), getHeight());

			for (int i = 0; i < getHeight(); i++)
			{
				for (int j = 0; j < getWidth(); j++)
				{
					res.data[i][j] = pow(-1, i + j)*T().crossOutRowAndCol(i, j).det();
				}
			}

			if (debug)
			{
				std::wcout << std::wstring(L"inverse:done") << std::endl;
			}

			return res / detA;
		}

		double eNorm()
		{
			double sum = 0;
			for (int i = 0; i < getHeight(); i++)
			{
				for (int j = 0; j < getWidth(); j++)
				{
					sum += pow(get(i, j), 2);
				}
			}

			return sqrt(sum);
		}

		Matrix copy()
		{
			return subMatrix(0, 0, getHeight(), getWidth());
		}



		Matrix subMatrix(int startRow, int startCol, int height, int width)
		{
			if (startRow < 0 || startCol < 0 || startRow >= getHeight() || startCol >= getWidth() || startRow + height > getHeight() || startCol + width > getWidth())
			{
				throw std::exception("Wrong coordinates for submatrix");
			}

			Matrix res(height, width);

			for (int i = 0; i < res.getHeight(); i++)
			{
				for (int j = 0; j < res.getWidth(); j++)
				{
					res.data[i][j] = data[startRow + i][startCol + j];
				}
			}

			return res;

		}

		Matrix T(bool debug = false)
		{
			if (debug)
			{
				std::wcout << std::wstring(L"transpose:start") << std::endl;
			}

			Matrix res(getWidth(), getHeight());

			for (int i = 0; i < res.getHeight(); i++)
			{
				for (int j = 0; j < res.getWidth(); j++)
				{
					res.data[i][j] = data[j][i];
				}
			}

			if (debug)
			{
				std::wcout << std::wstring(L"transpose:done") << std::endl;
			}

			return res;
		}


		Matrix appendRight(Matrix b)
		{
			if (getHeight() != b.getHeight())
			{
				throw std::exception("Matrix height must match for concatination");
			}

			Matrix res(getHeight(), getWidth() + b.getWidth());

			for (int i = 0; i < res.getHeight(); i++)
			{
				for (int j = 0; j < res.getWidth(); j++)
				{
					if (j < getWidth())
					{
						// this matrix
						res.data[i][j] = data[i][j];
					}
					else
					{
						// b matrix
						res.data[i][j] = b.data[i][j - getWidth()];
					}
				}
			}

			return res;

		}

		int getRowNumberWithNotNullColumnStartingAtRow(int column, int row)
		{
			if (column < 0 || column >= getWidth())
			{
				throw std::exception("No such column ");
			}
			for (int i = row; i < getHeight(); i++)
			{
				if (data[i][column] != 0)
				{
					return i;
				}
			}

			throw std::exception("All rows have 0 in column ");
		}

		void swapRows(int a, int b)
		{
			auto tmpRow = getRow(a);
			setRow(a, getRow(b));
			setRow(a, tmpRow);
		}

		Matrix getRow(int row)
		{
			if (row < 0 || row >= getHeight())
			{
				throw std::exception("No such row number ");
			}


			Matrix res(1, getWidth());
			for (int i = 0; i < getWidth(); i++)
			{
				res.data[0][i] = data[row][i];
			}

			return res;
		}

		Matrix getColumn(int col)
		{
			if (col < 0 || col >= getWidth())
			{
				throw std::exception("No such col number ");
			}

			Matrix res(getHeight(), 1);
			for (int i = 0; i < getHeight(); i++)
			{
				res.data[i][0] = data[i][col];
			}

			return res;

		}

		void setRow(int row, Matrix val)
		{
			if (row < 0 || row >= getHeight())
			{
				throw std::exception("No such row number ");
			}

			if (val.getWidth() != getWidth())
			{
				throw std::exception("Rows width's must match");
			}

			for (int i = 0; i < getWidth(); i++)
			{
				data[row][i] = val.data[0][i];
			}
		}

		double get(int i, int j)
		{
			return data[i][j];
		}

		void set(int i, int j, double val)
		{
			data[i][j] = val;
		}

		int getHeight()
		{
			return data.size();
		}

		int getWidth()
		{
			return data[0].size();
		}


		virtual string ToString() 
		{
			string output = "";
			for (int i = 0; i < getHeight(); i++)
			{
				string row = "";
				for (int j = 0; j < getWidth(); j++)
				{
					row += " " + to_string(data[i][j]) + " ";
				}
				row = "[" + row + "]";
				output += row + "\n";
			}

			return output;
		}
	};
}
