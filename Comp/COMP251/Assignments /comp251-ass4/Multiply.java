import java.util.*;
import java.io.*;

public class Multiply{

    private static int randomInt(int size) {
        int maxval = (1 << size) - 1;
        return (int)(Math.random()*maxval);
    }
    
    public static int[] naive(int size, int x, int y) {
        int[] result = new int[2];
        // YOUR CODE GOES HERE  (Note: Change return statement)

        if(size == 1) {
                result[0] = x * y;
                result[1] = 1;
            }
        else {
            int newSize = (int) Math.ceil(size / 2.0);
            int a = (int) Math.floor(x >> newSize);
            int b = x % (1 << newSize);
            int c = (int) Math.floor(y >> newSize);
            int d = y % (1 << newSize);

                //-------------------------------
                if(b < 0) b += 1 << newSize;
                if(d < 0) d += 1 << newSize;
                //-------------------------------
            int[] e = naive(newSize, a, c);
            int[] f = naive(newSize, b, d);
            int[] g = naive(newSize, b, c);
            int[] h = naive(newSize, a, d);
            result[0] = (e[0]   << (2*newSize)) + ((g[0] + h[0]) << newSize) + f[0];
            result[1] = e[1] + f[1] + g[1] + h[1] + 3 * newSize;
            }

            return result;
        
    }

    public static int[] karastuba(int size, int x, int y) {
        int[] result = new int[2];

        if(size == 1) {
            result[0] = x * y;
            result[1] = 1;
        }
        else {
            int newSize = (int) Math.ceil(size / 2.0);

            int a = (int) Math.floor(x >> newSize);
            int b = x % (1 << newSize);
            int c = (int) Math.floor(y >> newSize);
            int d = y % (1 << newSize);

                //------------------------
                if(b < 0) b += 1 << newSize;
                if(d < 0) d += 1 << newSize;
                //-------------------------

            int[] e = karastuba(newSize, a, c);
            int[] f = karastuba(newSize, b, d);
            int[] g = karastuba(newSize, a - b, c - d);

            result[0] = (e[0] << (2*newSize)) + ((e[0] + f[0] - g[0]) << newSize) + f[0];
            result[1] = e[1] + f[1] + g[1] + 6 * newSize;
        }

        return result;




        
    }
    
    public static void main(String[] args){

        try{
            int maxRound = 20;
            int maxIntBitSize = 16;
            for (int size=1; size<=maxIntBitSize; size++) {

                int sumOpNaive = 0;
                int sumOpKaratsuba = 0;
                for (int round=0; round<maxRound; round++) {
                    int x = randomInt(size);
                    int y = randomInt(size);
                    int[] resNaive = naive(size,x,y);
                    int[] resKaratsuba = karastuba(size,x,y);
            
                    if (resNaive[0] != resKaratsuba[0]) {
                        throw new Exception("Return values do not match! (x=" + x + "; y=" + y + "; Naive=" + resNaive[0] + "; Karatsuba=" + resKaratsuba[0] + ")");
                    }
                    
                    if (resNaive[0] != (x*y)) {
                        int myproduct = x*y;
                        throw new Exception("Evaluation is wrong! (x=" + x + "; y=" + y + "; Your result=" + resNaive[0] + "; True value=" + myproduct + ")");
                    }
                    
                    sumOpNaive += resNaive[1];
                    sumOpKaratsuba += resKaratsuba[1];
                }
                int avgOpNaive = sumOpNaive / maxRound;
                int avgOpKaratsuba = sumOpKaratsuba / maxRound;
                System.out.println(size + "," + avgOpNaive + "," + avgOpKaratsuba);
            }
            
        }
        catch (Exception e){
            System.out.println(e);
        }

   } 
}
