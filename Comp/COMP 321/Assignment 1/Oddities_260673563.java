import java.util.*;
import java.io.*;

public class Oddities_260673563 {
    public static void main(String[] args){
	// set up the scanner, n which is the value, and length is just how many values we have
        Scanner sc = new Scanner(System.in);
        int n = 0;
        int length = 0; 
	// get the number of elements or values we gonna classify
        while ( length <= 0)  length = sc.nextInt();
        for (int i = 0; i < length; i++) {
	    // read the next value	
            n = sc.nextInt();
            if ( n % 2 == 0 ) { System.out.println(n + " is even");}
            else {System.out.println(n + " is odd");}
        }
    }
}
