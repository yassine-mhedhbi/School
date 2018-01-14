function c = bisectionTutorial(f,a,b,Tol,Nmax)
% c = bisection(f,a,b,Tol,Nmax)
% 
% Compute the root of the function given by
% function handle f over the interval [a,b]. 

if sign(f(a))==sign(f(b))
  warning('f(a) and f(b) have same sign')
end

for n = 1:Nmax
  c = (a+b)/2;
  if (f(c) == 0) | (b-a)/2 < Tol
    return;
  end

  if sign(f(c)) == sign(f(a))
    a=c;
  else
    b=c;
  end
end

error('Method failed!')
