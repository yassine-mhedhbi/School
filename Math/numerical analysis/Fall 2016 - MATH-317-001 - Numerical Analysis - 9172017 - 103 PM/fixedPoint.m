function fixedPoint(x0,g,tol)
%   Input:  x0  - initial guess
%           g   - function handle of g(x)
%           tol - tolerance
    x_old = x0;
    k = 1;
    root = 2.0945514815423;
    while( abs(x_old-root)>tol )
        x_next = g(x_old);
        fprintf('x = %.16f for k=%d\n',x_next,k);
        x_old = x_next;
        k = k+1;
    end
end