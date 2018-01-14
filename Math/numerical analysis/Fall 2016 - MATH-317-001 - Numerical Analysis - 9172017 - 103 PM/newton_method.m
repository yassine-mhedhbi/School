function x = newton(x0,F,dF,numIter,tol)
x = x0;
k = 0;
while (abs(F(x))>tol || k<=numIter)
    x = x-F(x)/dF(x);
    k=k+1;
end

end