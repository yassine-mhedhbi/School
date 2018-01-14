tests = int(input())

parent = None
#  {x:y}, y is x's parent, if x has no parent, hence root, y is just its height, number of descendants.


def find(x):
    p = x
    while type(parent[p]) is not int:
        p = parent[p]

    while parent[x] != p:
        x, parent[x] = parent[x], p
	#path compress
    return p

def union(x, y):
    x, y = find(x), find(y)
    if x == y:
        return parent[x]
    if parent[x] < parent[y]:
        x, y = y, x
    parent[x] = parent[x] + parent[y]
    parent[y] = x
    return parent[x]
        
    
for test in range(tests):
	# get the line containing the two names.
    n = int(input())
    parent = {}

    for i in range(n):
	#get the names of 2 friends.
        a, b = input().split(" ")
        if a not in parent:
            parent[a] = 1
        if b not in parent:
            parent[b] = 1
        a = union(a, b)
        print(a)
