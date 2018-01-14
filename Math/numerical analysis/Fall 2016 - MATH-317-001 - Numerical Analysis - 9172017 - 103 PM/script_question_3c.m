
A = [3 -1 0;-1 3 -1;0 -1 3];
b = [2;2;-1];
x = [1;0;0];



L = tril(A,-1);%lower triangle matrix
U = triu(A,1);%upper triangle matrix
D = A-L-U;%diagonal matrix

%% Richardson
w = 0.2;

x = [1;0;0];

relative_error = 1;

k = 0;
while relative_error > 10^-10
	xold = x;
    x = w*(b-A*x)+x;
	relative_error = norm(x-xold)/norm(xold);
    k = k+1;
end

k


%% Jacobi
x = [1;0;0];

relative_error = 1;

k = 0;
while relative_error > 10^-10
	xold = x;
    x = D\(b-(L+U)*x);
	relative_error = norm(x-xold)/norm(xold);
    k = k+1;
end

k


%% Gauss-Seidel
x = [1;0;0];

relative_error = 1;

k = 0;
while relative_error > 10^-10
	xold = x;
    x = (L+D)\(b-U*x);
	relative_error = norm(x-xold)/norm(xold);
    k = k+1;
end

k

%% SOR
theta = 1.1;
x = [1;0;0];

relative_error = 1;

k = 0;
while relative_error > 10^-10
	xold = x;
    x = (1-theta)*x+theta*((L+D)\(b-U*x));
	relative_error = norm(x-xold)/norm(xold);
    k = k+1;
end

k
