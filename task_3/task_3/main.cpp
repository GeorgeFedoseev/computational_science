#include <iostream>
#include <cmath>
#include <vector>

#include "Matrix.h"

using namespace std;


double f(double x){
	return x*log(x+1) - 0.3;
}

double f_shtrix(double x){
	return log(x+1) + x/(x+1);
}

void main(void){


	// EQUATION
	cout << "EQUATION:" << endl << endl;
	auto intervals = vector<vector<double>>();
	// localize root 
	int N = 1000000;
	double from = -1000, to = 1000;
	double a, b;
	a = from;
	for(int i = 0; i < N; i++){
		b = from + i*(to-from)/N;
		
		if(f(a)*f(b) < 0){
			auto interval = vector<double>();			
			interval.push_back(a);
			interval.push_back(b);
			intervals.push_back(interval);			
		}

		a = b;
	}


	double eps = 0.0001;

	cout << "eps: " << eps << endl;
	
	cout << "ROOTS:" << endl;
	for(int i = 0; i < intervals.size(); i++){		
		double ai = intervals[i][0];
		double bi = intervals[i][1];
		double xk = ai;
		double xk_next = bi;

		while(1){
			xk_next = xk - f(xk)/f_shtrix(xk);
			if(abs(xk_next - xk) >= eps)
				break;
			xk = xk_next;
		}
		cout << xk_next << endl;
	}

	cout << endl << endl << "LINEAR SYSTEM:" << endl;

	double _eps = 0.001;	
	cout << "eps: " << _eps << endl;

	double xk = 1.5, yk = 0;
	double gk = 1, hk = 1;
	int i = 0;
	while(abs(gk) > _eps || abs(hk) > _eps){
		hk = (-sin(xk-1) - yk + 1.3 + xk*cos(xk-1) - cos(xk-1)*sin(yk+1) - 0.5*cos(xk-1))
					/ (1 + cos(xk-1)*cos(yk + 1));
		gk = -xk + sin(yk+1) + 0.5 + cos(yk+1)*hk;

		cout << "ITERATION " << ++i << endl;
		cout << "gk = " << gk << "; hk = " << hk << ";" << endl;

		xk += gk;
		yk += hk;
		cout << "xk = " << xk << "; yk = " << yk << ";" << endl;
	}
	cout << endl << "SOLUTION:" << endl;
	cout << "(" << xk <<", "<< yk << ")";


	cin.get();
	

}