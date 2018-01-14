f = @(x)exp(-x);
a = 0;
b = 1;
I_exact = 1-exp(-1);

L = 8;
h = zeros(L,1);
I_CMP = zeros(L,1);
I_Richardson = zeros(L,1);
error_CT = zeros(L,1);
error_Richardson = zeros(L,1);
for k=1:8
    N = 2^k;
    h(k) = (b-a)/N;
    I_CMP(k) = compositeTrap(a,b,f,N);
    I_Richardson(k) = quadratureHighOrder(a,b,f,N);
    error_CT(k) = abs(I_CMP(k)-I_exact);
    error_Richardson(k) = abs(I_Richardson(k)-I_exact);
end


%% Loglog plot
loglog(h,error_CT,'-o',h,error_Richardson,'-o')
legend('Composite Trapezoidal Rule','Richardon''s Rule','location','northwest')

%% Table of errors
table(:,1) = h;
table(:,2) = error_CT;
table(2:length(h),3) = (log(error_CT(2:length(h)))-log(error_CT(1:length(h)-1)))./(log(h(2:length(h)))-log(h(1:length(h)-1)));
table(:,4) = error_Richardson;
table(2:length(h),5) = (log(error_Richardson(2:length(h)))-log(error_Richardson(1:length(h)-1)))./(log(h(2:length(h)))-log(h(1:length(h)-1)));

table
