#!/usr/bin/env python3

"""
Created on Sat Feb 24 2018

@author: ymhedh
"""
import numpy as np
import pandas as pd
from collections import Counter



def bagofword_yelp(words,data,mode):
	if mode == "t":
		x = 7000
	elif mode == "v":
		x = 1000
	else :
		x = 2000
	l=np.zeros((x,10000))
	count=0;
	for lines in data:
		ind = 0
		for w in words:
			if(w in lines):
				l[count][ind]=1
			ind+=1
		count+=1
	return l

def bagofword_imdb(words,data,mode):
	if mode == "t":
		x = 15000
	elif mode == "v":
		x = 10000
	else :
		x = 25000
	l=np.zeros((x,10000))
	count=0;
	for lines in data:
		ind = 0
		for w in words:
			if(w in lines):
				l[count][ind]=1
			ind+=1
		count+=1
	return l




# first we get the top 10k words for IMDB
l = open("Datasets/IMDB-train.txt","r").read()
l = l.replace("<br /><br />"," ")
l = l.replace("<br /><br /","")
l = l.replace("\n"," ")
l = l.replace("'s","")
l = l.replace("\t","\t")
a = '?!.",()'
for i in a:
	l = l.replace(i,"")

T = l.lower()
Text = T.split(" ")
IMDB_top = Counter(Text).most_common(10000)

#print(IMDB_top)

# first we get the top 10k words for yelp
lm = open("Datasets/yelp-train.txt","r").read()
lm = lm.replace("<br /><br />"," ")
lm = lm.replace("<br /><br /","")
lm = lm.replace("\n"," ")
lm = lm.replace("'s","")
lm = lm.replace("\t","")  
lm = lm.replace("\t","\t")
a = '?!.",()'
for i in a:
	lm = lm.replace(i,"")

R = lm.lower()
Text = R.split(" ")
yelp_top = Counter(Text).most_common(10000)

imdb_v = open("IMDB-vocab.txt","a")
for i in range(10000):
	line = str(i+1) + "\t" + IMDB_top[i][0] + "\t" + str(IMDB_top[i][1]) + "\n"
	imdb_v.write(line)

yelp_v = open("yelp-vocab.txt","a")
for i in range(10000):
	line = str(i+1) + "\t" + yelp_top[i][0] + "\t" + str(yelp_top[i][1]) + "\n"
	yelp_v.write(line)

# ----------------- done with vocab.txt files ---------------------------------------#

words_i = [i[0] for i in IMDB_top]
words_y = [i[0] for i in yelp_top]

# now write the Train/Valid/test files
movie_r = []
f = open("Datasets/IMDB-train.txt","r").read()
x = f.split("\n")

IMBD_matrix_train = bagofword_imdb(words_i,x,"t")

movie_r_v = []
f = open("Datasets/IMDB-valid.txt","r").read()
x = f.split("\n")

IMBD_matrix_valid = bagofword_imdb(words_i,x,"v")

movie_r_ts =[]
f = open("Datasets/IMDB-test.txt","r").read()
x = f.split("\n")

IMBD_matrix_test = bagofword_imdb(words_i,x,"test")





