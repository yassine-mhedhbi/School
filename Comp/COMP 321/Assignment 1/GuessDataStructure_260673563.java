import java.io.*;
import java.util.*;


public class GuessDataStructure_260673563 {

	public static void main (String [] abc) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		String s;
		//check the end of file
		while ((s=br.readLine())!=null) {
			int opCount=Integer.parseInt(s);
			//new stack
			Stack<Integer> st=new Stack<>(); 
			boolean isS = true;
			//new Queue
			Queue<Integer> q=new LinkedList<>(); 
			boolean isQ = true;
			//new priority queue
			PriorityQueue<Integer> pq=new PriorityQueue<>(Collections.reverseOrder()); 
			boolean isPQ = true;
			int n = Integer.parseInt(s);
			for (int i = 0; i < n; i++) {
                s = br.readLine();
                if (!isQ && !isS && !isPQ) {
                    continue;
                }
                StringTokenizer temp = new StringTokenizer(s, " ");
		// parsing, the operation t and the value(element) x
                int t = Integer.parseInt(temp.nextToken());
                int x = Integer.parseInt(temp.nextToken());
                if (t == 1) {
                    if (isQ) {q.offer(x);}
                    if (isS) {st.add(x);}
                    if (isPQ) {pq.offer(x);}
                } else {
                    if (isQ && !q.isEmpty()) {
                        int y1 = q.poll();
                        if (y1 != x) {isQ = false;}
                    } else {isQ = false;}
                    if (isS && !st.isEmpty()) {
                        int y2 = st.pop();
                        if (y2 != x) {isS = false;}
                    } else {isS = false;}
                    if (isPQ && !pq.isEmpty()) {
                        int y3 = pq.poll();
                        if (y3 != x) {isPQ = false;}
                    } else {isPQ = false;}
                }
            }
	    // If all are false, then impossible
            if (!isQ && !isS && !isPQ) {
                System.out.println("impossible\n");
            } else if (isQ && !isS && !isPQ) {
                System.out.println("queue\n");
            } else if (isS && !isQ && !isPQ) {
                System.out.println("stack\n");
            } else if (isPQ && !isS && !isQ) {
                System.out.println("priority queue\n");
            } else {
                System.out.println("not sure\n");
            }
        }
}
}
