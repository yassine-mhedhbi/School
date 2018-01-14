function m = bisection(a,b,f,tol)
%   INPUTS:     a,b - left, right endpoints
%               f   - function handle of f(x)
%               tol - tolerance
%   OUTPUTS:    m - midpoint of interval containing root
    k = 0;
    %while( b-a > tol )
    root = 2.0945514815423;
    while( abs((a+b)/2-root)>tol )
        m = (a+b)/2;
        if( f(a)*f(m)<0 )
            b = m;
        else
            a = m;
        end
        fprintf('x = %.16f for k=%d\n',m,k);
        k=k+1;
    end
    m = (a+b)/2;
    fprintf('x = %.16f for k=%d\n',m,k);
end