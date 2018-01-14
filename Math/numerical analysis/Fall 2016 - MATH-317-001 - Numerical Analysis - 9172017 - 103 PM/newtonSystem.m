function newtonSystem(X0,tol)
%   Input:  x0  - initial guess vector
%           tol - tolerance
    X_old = X0;
    k = 1;
    while( norm(F(X_old))>tol )
        Y_new = JF(X_old)\(-F(X_old)');
        X_new = X_old+Y_new;
        fprintf('(x,y) = (%.16f,%.16f) for k=%d\n',X_new(1),X_new(2),k);
        X_old = X_new;
        k = k+1;
    end
end

function Y=F(X)
    Y(1) = X(1)^2+X(2)^2-4;
    Y(2) = X(1)+sin(X(1)*X(2))-X(2);
end

function A=JF(X)
    A(1,1) = 2*X(1);
    A(1,2) = 2*X(2);
    A(2,1) = 1+X(2)*cos(X(1)*X(2));
    A(2,2) = X(1)*cos(X(1)*X(2))-1;
end