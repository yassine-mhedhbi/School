a = 0;
b = 1;
h = 1/2^3;
f = @(x) 3-2*x;
alpha = 1;
beta = 1+exp(1);
u = @(x) x+exp(x);

error = zeros(1,8);
for k = 1:8
    h(k) = 2^-k;
    uh = BVP(a,b,h(k),f,alpha,beta);

    N = (b-a)/h(k);
    x = linspace(a,b,N+1);
   
    uexact = u(x(2:N))';
    
    error(k) = norm(uh-uexact);
end
    
loglog(h,error)
print(gcf, '-depsc2', 'loglog_BVP_error');

i = 1:7;
log(error(i+1)./error(i))./log(h(i+1)./h(i))
