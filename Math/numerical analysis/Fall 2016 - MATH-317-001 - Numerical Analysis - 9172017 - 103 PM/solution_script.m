f = @(x)x^3-2*x-5;
fprime = @(x)3*x^2-2;
bisection(2,3,f,1e-10);
g = @(x)nthroot(5+2*x,3);
fixedPoint(2.5,g,1e-10);
newton(2.5,f,fprime,1e-10);
secant(2.5,4,f,1e-10);
