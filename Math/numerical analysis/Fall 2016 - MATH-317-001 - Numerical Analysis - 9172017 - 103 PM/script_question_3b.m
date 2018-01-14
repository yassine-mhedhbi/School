
A = [3 -1 0;-1 3 -1;0 -1 3];
b = [2;2;-1];
x = [1;0;0];



L = tril(A,-1);%lower triangle matrix
U = triu(A,1);%upper triangle matrix
D = A-L-U;%diagonal matrix

%% Jacobi
for k=1:1
x = D\(b-(L+U)*x);
%x = (L+D)\(b-U*x);
end
x

x = [1;0;0];
%% Gauss-Seidel
for k=1:1
x = (L+D)\(b-U*x);
end
x
