#include <iostream>
#include <cmath>
#include <vector>

using namespace std;


double f(double x){
	return x*log(x+1) - 0.3;
}

double f_shtrix(double x){
	return log(x+1) + x/(x+1);
}

void main(void){

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
	

}