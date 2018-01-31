import math 

def f(x):
	return math.sin(x**2/2)/math.log(x+4,2)


def search(ind, d,s):
	if ind+d > 10:
		if f(ind) > f(ind-d):
			return (ind,s)
		else:
			return search(ind-d,d,s+1)

	elif ind - d < 0 :
		if f(ind) > f(ind+d) :
			return (ind,s)
		else:
			return search(ind+d, d,s+1)

	else : 
		if f(ind) > f(ind+d) and f(ind) > f(ind - d):
			return (ind,s)

		elif f(ind+d) >= f(ind-d):
			return search(ind+d,d,s+1)

		else:
			return search(ind-d,d,s+1)

res = [[0 for i in range(11)] for j in range(10)] 
conv = [[0 for i in range(11)] for j in range(10)] 
delta = 0.01

for i in range(10):
	for j in range(11):
		res[i][j],conv[i][j] = search(j,delta,1)

	delta = delta + 0.01

D = ["DX=0.01","DX=0.02","DX=0.03","DX=0.04","DX=0.05","DX=0.06","DX=0.07","DX=0.08","DX=0.09","DX=0.1"]
for i in range(10):
	print(D[i])
	print(res[i])
	print()
print("The steps of convergence are:")
for i in range(10):
	print(D[i])
	print(conv[i])
	print()

	
