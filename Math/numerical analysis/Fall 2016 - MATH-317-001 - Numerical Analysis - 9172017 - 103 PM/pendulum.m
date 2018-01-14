T = 30;
N = 1000;
h = T/N;
t = 0:h:T;
f = @(X) [X(2); -sin(X(1))];
 
% Euler's method
X = zeros(2,N+1);
X(:,1) = [pi/4; 0];
for i=1:N
    X(:,i+1) = X(:,i) + h*f(X(:,i));
end
X1 = X(1,:);
Y1 = X(2,:);

% Improved Euler method
X = zeros(2,N+1);
X(:,1) = [pi/4; 0];
M = 1;
for i=1:N
    Xprev = X(:,i);
    Xpred = Xprev + h*f(Xprev);
    for j=1:M
        Xpred = Xprev+0.5*h*(f(Xprev)+f(Xpred));
    end
    X(:,i+1) = Xpred;
end
X2 = X(1,:);
Y2 = X(2,:);

figure(1)
clf
hold on
plot(t, X1,'.');
plot(t, X2,'.');
legend('Euler','Improved Euler')
xlabel('t')
ylabel('\theta(t)')
hold off

figure(2)
clf
hold on
plot(X1, Y1,'.');
plot(X2, Y2,'.');
legend('Euler','Improved Euler')
xlabel('\theta(t)')
ylabel('\theta''(t)')
hold off    

figure(3)
clf
hold on
plot(t, 0.5*Y1.^2-cos(X1),'.');
plot(t, 0.5*Y2.^2-cos(X2),'.');
legend('Euler','Improved Euler')
xlabel('t')
ylabel('E(t)')
hold off
