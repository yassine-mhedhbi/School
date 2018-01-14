f = @(x) sin(x);  %function to integrate
I = [0,pi/2];     %on interval I
a = I(1);
b = I(2);
True_soln = 1;    %analytic solution

h = (b-a)./...    %spatial resolution
      (linspace(10,1000,10));

for i = 1:length(h)
  x = a:h(i):b;   %end points of the partitions

  xmid = 1/2*(x(2:end) +... %midpoint of the partitions
                 x(1:(end-1)));

  Integral = sum(h(i)*f(xmid)); %compute integral using midpoint rule

  Err(i) = abs(Integral-True_soln); % error
end

% Now plot
loglog(h,Err,'o');
xlabel('$h$','interpreter','latex'); 
ylabel('Error','interpreter','latex')
hold on

Y = log(Err);
X = log(h);

coeffs = polyfit(X,Y,1);
order = coeffs(1);

loglog(h,exp(coeffs(2))*h.^2);
leg = legend('Computed error','$\mathcal O(h^2)$');
set(leg,'Interpreter','Latex')
hold off
