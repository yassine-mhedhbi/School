function p = horner(a,x)
% p = horner(a,x)
%
% Use horner's method to compute polynomial
%  p(x) = a(1) + a(2)x + ... + a(n)x^(n-1)

n = length(a); %degree of polynomial + 1 

p = a(n);      %initialize while loop
i = n-1;

while i>=1
  p = p*x+a(i); %compute next iterate
  i = i-1;
end
