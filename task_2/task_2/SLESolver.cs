using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2
{
    class SLESolver
    {

		Matrix A, b;

        public SLESolver(Matrix _A, Matrix _b) {
            if (_A.getWidth() < 1 || _A.getHeight() < 1
				|| _A.getHeight() != _A.getWidth()
                || _A.getHeight() != _b.getHeight() || _b.getWidth() != 1) {
                throw new Exception("Wrong input for SLE solver");
            }

            if (_A.det() == 0) {
                throw new Exception("Matrix should be invertable for SLE solver");
            }

            A = _A;
            b = _b;
        }


        public Matrix solverWithIterativeMethod(double eps) {

			// restruct matrix so that aii != 0
            for (int i = 0; i < A.getHeight(); i++) {
                A.swapRows(i, A.getRowNumberWithNotNullColumnStartingAtRow(i, i));
            }

            Matrix delta = new Matrix(A.getWidth()) / (2*A.eNorm());
            Matrix D = A.inverse() - delta;

            Matrix alpha = delta * A;
            Matrix beta = D * b;

            Console.WriteLine("alpha:");
            Console.WriteLine(alpha);
            Console.WriteLine("beta:");
            Console.WriteLine(beta);

            Console.WriteLine("||alpha|| = {0}", alpha.eNorm());


            Matrix x = beta.copy();
			Matrix x_next;

            var k = 0;
            while(true){
                x_next = beta + alpha * x;                

                Console.WriteLine("[k={0} norm: {1}]", k, (x_next - x).eNorm());

                if((x_next - x).eNorm() <= eps)
                    return x_next;

                x = x_next;
                k++;
            }  

            
        }

		private static bool checkIterativeMethodCondition(Matrix alpha){
            for (int i = 0; i < alpha.getHeight(); i++) {
                double sum = 0;
                for (int j = 0; j < alpha.getWidth(); j++) {
                    sum += Math.Abs(alpha.get(i, j));
                }

                if (sum >= 1)
                    return false;
            }

            return true;
        }

        public Matrix solveWithGauss() {
            var Ab = A.appendRight(b);

			// straight
            for (int i = 0; i < Ab.getHeight(); i++) {
                Ab.swapRows(i, Ab.getRowNumberWithNotNullColumnStartingAtRow(i, i));
                Ab.setRow(i, Ab.getRow(i) / Ab.getRow(i).get(0, i));                
                for (int k = i + 1; k < Ab.getHeight(); k++) {
                    Matrix modRow = Ab.getRow(k);                    
                    Ab.setRow(k, modRow - Ab.getRow(i) * modRow.get(0, i));
                }

                //Console.WriteLine(Ab);
            }
            //Console.WriteLine("reverse");
			// reverse
			for (int i = Ab.getHeight()-2; i >= 0; i--) {

                for (int k = A.getWidth() - 1; k > i; k--) {
                    var modRow = Ab.getRow(i);
                    Ab.setRow(i, modRow - Ab.getRow(k) * modRow.get(0, k));
                }


                    //Console.WriteLine(Ab);
            }

            return Ab.getColumn(Ab.getWidth()-1);
        }

    }
}
