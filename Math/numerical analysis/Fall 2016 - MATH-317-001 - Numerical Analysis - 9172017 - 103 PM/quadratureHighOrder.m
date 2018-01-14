function Ih = quadratureHighOrder(a,b,f,n)
	Ih_aux1 = compositeTrap(a,b,f,n);
	Ih_aux2 = compositeTrap(a,b,f,2*n);
	Ih = (4*Ih_aux2-Ih_aux1)/3;
end