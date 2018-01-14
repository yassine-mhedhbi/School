%% Parameters
T= 20;
y0 = 0.1;
epsilon = 0.03;
f = @(y) y^2-epsilon*y^3;
N = 500;

%% Part (a)

% Euler's method
h = T/N;
t = 0:h:T;
Y_Euler = zeros(N,1);
Y_Euler(1) = y0;
for n=1:N
    Y_Euler(n+1) = Y_Euler(n)+h*f(Y_Euler(n));
end

% Trapezoidal method
h = T/N;
t = 0:h:T;
Y_trapezoidal = zeros(N,1);
Y_trapezoidal(1) = y0;

% for each iteration of the trapezoidal method we
% need to find the root of F
% here y = y_{n+1} and yp = y_n
% We will use Newton's method, so we will need
% F and its derivative
F = @(y,yp,h,c) y-yp-0.5*h*(yp^2-c*yp^3+y^2-c*y^3);
dF = @(y,h,c) 1-0.5*h*(2*y-3*c*y^2);


% maximum number of iteration for Newton's method
numIter = 20;
% tolerance for Newton's method
tol = 1e-10;
for n=1:N
      k=0;
      yp = Y_trapezoidal(n);
      Y_trapezoidal(n+1) = newton_method(yp,@(x)F(x,yp,h,epsilon),@(x)dF(x,h,epsilon),numIter,tol);
end

clf
hold on
plot(t,Y_Euler,'b.')
plot(t,Y_trapezoidal,'r.')
hold off


