function Ih = compositeTrap(a,b,f,n)
    h = (b-a)/n;
    x = linspace(a,b,n+1);
    Ih = sum(h/2*(f(x(1:n))+f(x(2:(n+1)))));
end