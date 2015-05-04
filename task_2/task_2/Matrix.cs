using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2
{
    class Matrix
    {

        protected double[,] data;       


        public Matrix(Matrix m)
        {
            data = m.data;
        }

        public Matrix(double[,] arr)
        {
            if (arr.GetLength(0) < 1 && arr.GetLength(1) < 0)
            {
                throw new Exception("Matrix cant be null");
            }

            data = arr;
        }

        public Matrix(int n, int m)
        {
            if (n < 1 || m < 1)
            {
                throw new Exception("Matrix cant be null");
            }
            data = new double[n, m];
        }

        public Matrix(int n)
        {
            if (n < 1)
            {
                throw new Exception("Matrix cant be null");
            }

            data = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                data[i, i] = 1;
            }
        }




        static public Matrix operator -(Matrix m1)
        {

            Matrix res = new Matrix(m1.getHeight(), m1.getWidth());

            for (int i = 0; i < m1.getHeight(); i++)
            {
                for (int j = 0; j < m1.getWidth(); j++)
                {
                    res.data[i, j] = -m1.data[i, j];
                }
            }

            return res;
        }

        static public Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + (-m2);
        }

        static public Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.getWidth() != m2.getWidth() || m1.getHeight() != m2.getHeight())
            {
                throw new Exception("Matrix must have same dim for adding");
            }

            Matrix res = new Matrix(m1.getHeight(), m1.getWidth());

            for (int i = 0; i < m1.getHeight(); i++)
            {
                for (int j = 0; j < m1.getWidth(); j++)
                {
                    res.data[i, j] = m1.data[i, j] + m2.data[i, j];
                }
            }

            return res;
        }

        static public Matrix operator *(Matrix m1, double k)
        {
            Matrix res = new Matrix(m1.getHeight(), m1.getWidth());
            for (int i = 0; i < m1.getHeight(); i++)
            {
                for (int j = 0; j < m1.getWidth(); j++)
                {
                    res.data[i, j] = m1.data[i, j] * k;
                }
            }

            return res;
        }

        static public Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.getWidth() != m2.getHeight())
            {
                throw new Exception("Matrixes should: m1.w = m2.h for multpl");
            }

            Matrix res = new Matrix(m1.getHeight(), m2.getWidth());

            for (int i = 0; i < res.getHeight(); i++)
            {
                for (int j = 0; j < res.getWidth(); j++)
                {
                    double val = 0;
                    for (int r = 0; r < m1.getWidth(); r++)
                    {
                        val += m1.data[i, r] * m2.data[r, j];
                    }
                    res.data[i, j] = val;
                }
            }

            return res;
        }

        static public Matrix operator *(double k, Matrix m1)
        {
            return m1 * k;
        }

        static public Matrix operator /(Matrix m1, double k)
        {
            return m1 * (1/k);
        }


        public double det(bool debug = false) {
            if (getWidth() != getHeight()) {
                throw new Exception("For det calculation matrix must be square");
            }

            if (debug)
                Console.WriteLine("det:start");

            if (getWidth() == 1) {
                return get(0, 0);
            }

            double res = 0;
            for (int j = 0; j < getWidth(); j++) {
                res += Math.Pow(-1, j)*get(0, j) * crossOutRowAndCol(0, j).det();
            }

            if (debug)
                Console.WriteLine("det:done: " + res);

            return res;
        }

        public Matrix crossOutRowAndCol(int row, int col, bool debug = false) {
            if (getWidth() <= 1 || getHeight() <= 1
                || row < 0 || row >= getHeight() || col < 0 || col >= getWidth()) 
            {
                throw new Exception("Wrong data to cross out col and row");
            }

            if (debug)
                Console.WriteLine("crossOutRowAndCol:start");

            Matrix res = new Matrix(getHeight()-1, getWidth()-1);

            for (int i = 0; i < getHeight(); i++) {
                for (int j = 0; j < getWidth(); j++) {
                    if (i != row && j != col) {
                        var i_m = i > row ? i - 1 : i;
                        var j_m = j > col ? j - 1 : j;

                        res.data[i_m, j_m] = data[i, j];
                    }
                }
            }

            if (debug)
                Console.WriteLine("crossOutRowAndCol:done");

            return res;

        }



        public Matrix inverse(bool debug = false) {
            if (getWidth() != getHeight()) {
                throw new Exception("Matrix should be square to be inversed");
            }

            if (debug)
                Console.WriteLine("inverse:start");

            double detA = det();

            if (detA == 0) {
                throw new Exception("Matrix isn't inversable (det = 0)");
            }


            Matrix res = new Matrix(getHeight(), getHeight());

            for (int i = 0; i < getHeight(); i++) {
                for (int j = 0; j < getWidth(); j++) {                    
                    res.data[i, j] = Math.Pow(-1, i+j)*T().crossOutRowAndCol(i, j).det();
                }
            }

            if (debug)
                Console.WriteLine("inverse:done");

            return res/detA;
        }

        public double eNorm() {
            double sum = 0;
            for (int i = 0; i < getHeight(); i++) {
                for (int j = 0; j < getWidth(); j++) {
                    sum += Math.Pow(get(i, j), 2);
                }
            }

            return Math.Sqrt(sum);
        }

        public Matrix copy() {
            return subMatrix(0, 0, getHeight(), getWidth());
        }



        public Matrix subMatrix(int startRow, int startCol, int height, int width) {
            if (startRow < 0 || startCol < 0 || startRow >= getHeight() || startCol >= getWidth()
                || startRow + height > getHeight() || startCol + width > getWidth()) 
            {
                throw new Exception(String.Format("Wrong coordinates for submatrix: (x: {0}, y:{1}, w:{2}, h:{3})",
                                                        startCol, startRow, width, height));
            }

            Matrix res = new Matrix(height, width);

            for (int i = 0; i < res.getHeight(); i++) {
                for (int j = 0; j < res.getWidth(); j++) {
                    res.data[i, j] = data[startRow + i, startCol + j];
                }
            }

            return res;

        }

        public Matrix T(bool debug = false)
        {
            if (debug)
                Console.WriteLine("transpose:start");

            Matrix res = new Matrix(getWidth(), getHeight());

            for (int i = 0; i < res.getHeight(); i++)
            {
                for (int j = 0; j < res.getWidth(); j++)
                {
                    res.data[i, j] = data[j, i];
                }
            }

            if (debug)
                Console.WriteLine("transpose:done");

            return res;
        }


        public Matrix appendRight(Matrix b) {
            if (getHeight() != b.getHeight()) {
                throw new Exception("Matrix height must match for concatination");
            }

            Matrix res = new Matrix(getHeight(), getWidth()+b.getWidth());

            for (int i = 0; i < res.getHeight(); i++) {
                for (int j = 0; j < res.getWidth(); j++) {
                    if (j < getWidth()) { 
                        // this matrix
                        res.data[i, j] = data[i, j];
                    }else{
                        // b matrix
                        res.data[i, j] = b.data[i, j-getWidth()];
                    }
                }
            }

            return res;

        }

        public int getRowNumberWithNotNullColumnStartingAtRow(int column, int row) { 
            if(column < 0 || column >= getWidth()){
                throw new Exception("No such column "+column);
            }
            for (int i = row; i < getHeight(); i++) {
                if (data[i, column] != 0) {
                    return i;
                }
            }

            throw new Exception("All rows have 0 in column "+column);
        }

        public void swapRows(int a, int b) {
            var tmpRow = getRow(a);
            setRow(a, getRow(b));
            setRow(a, tmpRow);
        }

        public Matrix getRow(int row) {
            if (row < 0 || row >= getHeight()) {
                throw new Exception("No such row number "+row);
            }


            Matrix res = new Matrix(1, getWidth());
            for(int i = 0; i < getWidth(); i++){
                res.data[0, i] = data[row, i];
            }

            return res;
        }

        public Matrix getColumn(int col) { 
            if (col < 0 || col >= getWidth()) {
                throw new Exception("No such row number " + col);
            }

            Matrix res = new Matrix(getHeight(), 1);
            for(int i = 0; i < getHeight(); i++){
                res.data[i, 0] = data[i, col];
            }

            return res;

        }

        public void setRow(int row, Matrix val) { 
            if (row < 0 || row >= getHeight()) {
                throw new Exception("No such row number "+row);
            }

            if (val.getWidth() != getWidth()) {
                throw new Exception("Rows width's must match");
            }

            for(int i = 0; i < getWidth(); i++){
                data[row, i] = val.data[0, i];
            }
        }

        public double get(int i, int j)
        {
            return data[i, j];
        }

        public void set(int i, int j, double val)
        {
            data[i, j] = val;
        }

        public int getHeight()
        {
            return data.GetLength(0);
        }

        public int getWidth()
        {
            return data.GetLength(1);
        }


        public override string ToString()
        {
            var output = "";
            for (int i = 0; i < getHeight(); i++)
            {
                var row = "";
                for (int j = 0; j < getWidth(); j++)
                {
                    row += " " + data[i, j].ToString() + " ";
                }
                row = "[" + row + "]";
                output += row + "\n";
            }

            return output;
        }
    }
}
