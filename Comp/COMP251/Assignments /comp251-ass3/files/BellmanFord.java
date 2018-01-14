import java.util.*;

public class BellmanFord{

    private int[] distances = null;
    private int[] predecessors = null;
    private int source;

    BellmanFord(WGraph g, int source) throws Exception{
        /* Constructor, input a graph and a source
         * Computes the Bellman Ford algorithm to populate the
         * attributes 
         *  distances - at position "n" the distance of node "n" to the source is kept
         *  predecessors - at position "n" the predecessor of node "n" on the path
         *                 to the source is kept
         *  source - the source node
         *
         *  If the node is not reachable from the source, the
         *  distance value must be Integer.MAX_VALUE
         */
        
        //code
        distances = new int[g.getNbNodes()];
        predecessors = new int[g.getNbNodes()];

        HashSet<Integer> set = new HashSet<Integer>();

        for (Edge e :g.getEdges()) {
            set.add(e.nodes[0]);
            set.add(e.nodes[1]);
        }

        for (Integer node : set) {
            if (node == source) distances[node] = 0;
            else distances[node] = Integer.MAX_VALUE;
            predecessors[node] = -1;

        }
        for(int i = 1; i < distances.length; i++) {
            for(Edge e : g.getEdges()) {
                int weight = 0;
                if( distances[e.nodes[0]] != Integer.MAX_VALUE && (weight = distances[e.nodes[0]] + e.weight) < distances[e.nodes[1]] ) {
                    distances[e.nodes[1]] = weight ;
                    predecessors[e.nodes[1]] = e.nodes[0];

                }
            }
        }


        for(Edge e : g.getEdges()) {
            if(distances[e.nodes[0]] != Integer.MAX_VALUE && distances[e.nodes[0]] + e.weight < distances[e.nodes[1]]) {
                throw new Exception("Graph contains a negative weight cycle");
            }
        }


       
    }

    public int[] shortestPath(int destination) throws Exception{
        /*Returns the list of nodes along the shortest path from 
         * the object source to the input destination
         * If not path exists an Error is thrown
         */
        int[] Rpath = new int[this.predecessors.length];
        Rpath[0] = destination;
        int pre = destination;
        int length = 1;

        while((pre = this.predecessors[pre]) != -1) {
           Rpath[length] = pre;
           length++;
        }
        if(Rpath[length - 1] != this.source) {
            throw new Exception("No path exists from source to destination");
        }

        // Reverse
        int[] path = new int[length];
        for(int i = 0; i < length; i++) {
            path[i] = Rpath[length - i - 1];
        }

        return path;
    }
        



    public void printPath(int destination){
        /*Print the path in the format s->n1->n2->destination
         *if the path exists, else catch the Error and 
         *prints it
         */
        try {
            int[] path = this.shortestPath(destination);
            for (int i = 0; i < path.length; i++){
                int next = path[i];
                if (next == destination){
                    System.out.println(destination);
                }
                else {
                    System.out.print(next + "-->");
                }
            }
        }
        catch (Exception e){
            System.out.println(e);
        }
    }

    public static void main(String[] args){

        String file = args[0];
        WGraph g = new WGraph(file);
        try{
            BellmanFord bf = new BellmanFord(g, g.getSource());
            bf.printPath(g.getDestination());
        }
        catch (Exception e){
            System.out.println(e);
        }

   } 
}
