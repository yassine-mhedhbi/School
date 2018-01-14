function secant(x0,x1,f,tol)
%   Input:  x0  - initial guess
%           x1  - second initial guess
%           f   - function handle of f(x)
%           tol - tolerance
    x_old0 = x0;
    x_old1 = x1;
    k = 1;
    root = 2.0945514815423;
    while( abs(x_old1-root)>tol )
        x_next = x_old1 - f(x_old1)*(x_old1-x_old0)/(f(x_old1)-f(x_old0));
        fprintf('x = %.16f for k=%d\n',x_next,k);
        x_old0 = x_old1;
        x_old1 = x_next;
        k = k+1;
    end
end