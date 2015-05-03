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
            A = _A;
            b = _b;
        }

        public Matrix solveWithGauss() {
            return new Matrix(1);
        }
    }
}
