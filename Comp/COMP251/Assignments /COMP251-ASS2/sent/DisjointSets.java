import java.io.*;
import java.util.*;


/****************************
*
* COMP251 template file
*
* Assignment 2, Question 1
*
*****************************/


public class DisjointSets {

    private int[] par;
    private int[] rank;
    
    /* contructor: creates a partition of n elements. */
    /* Each element is in a separate disjoint set */
    DisjointSets(int n) {
        if (n>0) {
            par = new int[n];
            rank = new int[n];
            for (int i=0; i<this.par.length; i++) {
                par[i] = i;
            }
        }
    }
    
    public String toString(){
        int pari,countsets=0;
        String output = "";
        String[] setstrings = new String[this.par.length];
        /* build string for each set */
        for (int i=0; i<this.par.length; i++) {
            pari = find(i);
            if (setstrings[pari]==null) {
                setstrings[pari] = String.valueOf(i);
                countsets+=1;
            } else {
                setstrings[pari] += "," + i;
            }
        }
        /* print strings */
        output = countsets + " set(s):\n";
        for (int i=0; i<this.par.length; i++) {
            if (setstrings[i] != null) {
                output += i + " : " + setstrings[i] + "\n";
            }
        }
        return output;
    }
    
    /* find resentative of element i */
    public int find(int i) {

        /* Fill this method (The statement return 0 is here only to compile) */
        if (this.par[i] == i) return i;
        // par[i] = i , i is the root (representative)
        //else call recursively find on the parent of i.

        else {
            this.par[i] = find(this.par[i]);
            return this.par[i];
        }
        
    }

    /* merge sets containing elements i and j */
    public int union(int i, int j) {

       
        final int rootI = find(i);
        final int rootJ = find(j);
        if (rootI == rootJ) return -1;  //i and j are already in the same disjoint set
 
        final int child = (rank[i] <= rank[j]) ? rootJ : rootI; //the node which becomes the child of the new root
        final int parent = (child == rootI) ? rootJ : rootI;    //the root for the new tree
 
        par[child] = parent;    //make the tree with the lesser rank a child to the new root
        if (rank[i] == rank[j]) //if ranks were the same then update!
            rank[j]++;
 
        return parent;

        }

        

    
    public static void main(String[] args) {
        
        DisjointSets myset = new DisjointSets(6);
        System.out.println(myset);
        System.out.println("-> Union 2 and 3");
        myset.union(2,3);
        System.out.println(myset);
        System.out.println("-> Union 2 and 3");
        myset.union(2,3);
        System.out.println(myset);
        System.out.println("-> Union 2 and 1");
        myset.union(2,1);
        System.out.println(myset);
        System.out.println("-> Union 4 and 5");
        myset.union(4,5);
        System.out.println(myset);
        System.out.println("-> Union 3 and 1");
        myset.union(3,1);
        System.out.println(myset);
        System.out.println("-> Union 2 and 4");
        myset.union(2,4);
        System.out.println(myset);

        
    }

}
