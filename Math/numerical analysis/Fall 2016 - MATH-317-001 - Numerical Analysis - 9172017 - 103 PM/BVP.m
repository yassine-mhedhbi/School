function uh = BVP(a,b,h,f,alpha,beta)

N = (b-a)/h;
x = linspace(a,b,N+1);

Ah = diag((2-2*h^2)*ones(1,N-1))+diag((-1+3/2*h)*ones(1,N-2),1)+diag((-1-3/2*h)*ones(1,N-2),-1);
fh = h^2*f(x(2:N))';
fh(1) = fh(1)+(1+3/2*h)*alpha;
fh(end) = fh(end)+(1-3/2*h)*beta;

uh = Ah\fh;


end

