clear
clc
%figuredefaults
N = 100;
a = 0;
b = 1;
h = (b-a)/N;
alpha = 0;
beta = 0;
tol = 1e-10;
x = a:h:b;
aux = @(x) 2*x.*(1-x);
f = @(x,y,yprime) -aux(x).*(1+yprime.^2).^(3/2);
fy = @(x,y,yprime) zeros(size(y));
fyprime = @(x,y,yprime) -3/2.*aux(x).*2.*yprime.*sqrt(1+yprime.^2);


M = 10;
s = zeros(M,1);
s(1) = 0;

clf
hold on
k = 1;
yN = zeros(M,1);
zN = zeros(M,1);
while( k==1 || abs(yN(k-1)-beta)>tol && k<=M )
    y = zeros(2,N+1);
    y(:,1) = [alpha; s(k)];
    z = zeros(2,N+1);
    z(:,1) = [0; 1];
    
    % Forward Euler
    F1 = @(Y,x)[Y(2); f(x,Y(1),Y(2))];
    F2 = @(Y,Z,x)[Z(2); fy(x,Y(1),Y(2)).*Z(1)+fyprime(x,Y(1),Y(2)).*Z(2)];
    for i=1:N
        y(:,i+1) = y(:,i)+h*F1(y(:,i),x(i));
        z(:,i+1) = z(:,i)+h*F2(y(:,i),z(:,i),x(i));
    end
    plot(x,y(1,:),'.')
    
    axis([a b -0.2 0.1])
    yN(k) = y(1,N+1);
    zN(k) = z(1,N+1);
    % Update slope using Newton's method
    if( k>=1 )
        s(k+1) = s(k)-(yN(k)-beta)/zN(k);
    end
    fprintf('k = %d\n',k);
    fprintf('yN = %1.3f\n',yN(k));
    k = k+1;
    pause(0.1)
end
hold off
k
print(gcf, '-depsc2', 'shooting_Newton');