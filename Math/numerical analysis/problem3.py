###########################################################
# Description : A script to run the secant method and     #
# Newton-Raphson of the function f(x) = sqrt(x) - e^(-x)  #
#                                                         #
# Author : Yassine Mhedhbi                                #
###########################################################

""" Variable description, f: function , fd = derivative of f, T : tolerance, MAX: maximum number of iterations."""

###########################################################
# function name : secant                                  #
# Description : starts at x0, x1,(initial guesses) tries  #
#to find the root  of f useing secant method.             #
###########################################################

import math 


def secant(f,x0,x1, T=0.00000001, MAX=1000000):
    
    i=0 # number of iteration done.
    while i < MAX:
        x2 = x1 - f(x1)*((x1-x0)/(f(x1)-f(x0))) # get the vaue of X_2
        print("iteration # %d: %f" % (i, x2))
        if abs(x2-x1) < T:
            return x2
        else:
            x0 = x1
            x1 = x2
        i += 1
    return False # in case of no root 

###########################################################
# function name : newtonraphson                           #
# Description : find the root of a function using an      #
# initial guess, a function and its derivative            #
###########################################################

def newtonraphson(f, fd, x0, T=0.00000001, MAX=1000000):
    i = 0
    while i < MAX:

        x1 = x0 - (f(x0)/fd(x0))
        print("iteration # %d: %f" %  (i, x1))
        if abs(x1 - x0) < T:
            return x1
        else:
            x0 = x1
        i += 1
    return False

"""---------------------------------------------------------"""
"""                        MAIN                             """
"""---------------------------------------------------------"""
if __name__ == "__main__":
    
    import math 
    
    def f(x):
        y = math.sqrt(x) - math.exp(-1*x)
        return y 
    def fd(x):
        yd = math.pow(2*math.sqrt(x),-1) +  math.exp(-1*x)
        return yd

    print "\n CALL THE Secant METHOD "
    secant(f,0,1)
    
    print "\n CALL Newton-Raphson METHOD "
    newtonraphson(f, fd, 0.5)