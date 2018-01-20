# -*- coding: utf-8 -*-
"""
File name : Q1
Description : First question of assignment 1
Author: Yassine Mhedhbi
"""

import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

data = pd.read_csv("Datasets/Dataset_1_train.csv",header=-1, usecols=range(2))
cols = ["x","y"]
data.columns = cols
#data.head()
x = data['x']
y = data['y']

"""plt.scatter(x,y)
plt.show()
"""
matr = []
Y = []
for a in x:
    row =[]
    for p in range(20,-1,-1): 
        row = row + [a**(p)]
    matr = matr + [row]  
print(len(matr))

for i in y:
    Y = Y +[[i]]
    
X = np.array(matr) 
#print(X)
XT = X.transpose()  
print(len(XT[5]))
A = np.dot(XT,X)
A1 = np.matrix(A)
A_I = A1.I
XTX = np.dot(A_I,XT)
W = np.dot(XTX,Y)
WT = W.transpose()
p = WT.tolist()[0]
Poly = np.poly1d(p)

x1 = x.tolist()
x1.sort()
new_y = [Poly(i) for i in x ]
plt_y = [Poly(i) for i in x1 ]
sigma = 0
for i in range(len(new_y)):
    sigma = sigma + (new_y[i] - y[i])**2 
print("The Training mean square error is:",sigma/len(y))
print("==================================================")
valid = pd.read_csv("Datasets/Dataset_1_valid.csv",header=-1,usecols=range(2))
cols = ["x","y"]
valid.columns = cols
#data.head()
u = valid['x']
v = valid['y']


new_v = [Poly(i) for i in u ]
sigma = 0
for i in range(len(new_v)):
    sigma = sigma + (new_v[i] - v[i])**2 
print("The validation mean square error is:",sigma/len(v))

plt.scatter(x,y)
plt.plot(x1,plt_y)

plt.show()

print("==================================================")

test = pd.read_csv("Datasets/Dataset_1_test.csv", header = -1,usecols=range(2))
cols = ["x","y"]
test.columns = cols
test.head() 

X = test['x'].tolist()
X.sort()
Y = [Poly(i) for i in X ]
plt.plot(X,Y)
plt.scatter(test['x'],test['y'])
plt.show()
    