N = 100;    % Number of grid points
a = -1;     % Left end point
b = 0;      % Right end point
h = (b-a)/N;% step size
alpha = 1/2;% desired solution at left end point
beta = 1/3; % desired solution at right end point
tol = 1e-2; % stopping criteria
x = a:h:b;  % the grid points

f = @(x,y,yprime) 2*y.^3; % the ODE's operator

fy = @(x,y,yprime) 6*y.^2;% derivative of operator
                          % with respect to solution y

fyprime = @(x,y,yprime) zeros(size(y)); 
                          % derivative wrt y'

yexact = 1./(x+3); % exact solution

M = N;           % number of gridpoints
s = zeros(M,1);  % allocate memory
s(1) = 0;        % initial guess for root solver

plot(x,yexact,'k.','MarkerSize',5);
hold on

k = 1;             % while loop index
yN = zeros(M,1);   % memory allocation
zN = zeros(M,1);   % ditto

while( k==1 || abs(yN(k-1)-beta)>tol && k<=M )

    % allocate memory and put in initial values
    y = zeros(2,N+1);
    y(:,1) = [alpha; s(k)];
    z = zeros(2,N+1);
    z(:,1) = [0; 1];
    
    % Forward Euler
    F1 = @(Y,x)[Y(2); f(x,Y(1),Y(2))];  
        %the 1st order ODE system for y

    F2 = @(Y,Z,x)[Z(2); ...
                  fy(x,Y(1),Y(2)).*Z(1) + ...
                  fyprime(x,Y(1),Y(2)).*Z(2)]; 
        % the 1st order system for z

    for i=1:N
        y(:,i+1) = y(:,i)+h*F1(y(:,i),x(i));
        z(:,i+1) = z(:,i)+h*F2(y(:,i),z(:,i),x(i));
    end
    plot(x,y(1,:))
    
    axis([a b 0.3 0.8])
    yN(k) = y(1,N+1); %ie, phi(s_k)-beta from the notes
    zN(k) = z(1,N+1); %ie, phi'(s_k) from the notes

    % Update slope using Newton's method
    if( k>=1 )
        s(k+1) = s(k)-(yN(k)-beta)/zN(k);
    end
    fprintf('k = %d\n',k);
    fprintf('yN = %1.3f\n\n',yN(k));
    k = k+1;
    pause
end
hold off
print(gcf, '-depsc2', 'Q2');











