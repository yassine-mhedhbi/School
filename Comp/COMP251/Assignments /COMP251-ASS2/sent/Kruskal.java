import java.util.*;

public class Kruskal{

    public static WGraph kruskal(WGraph g){
        WGraph MST = new WGraph() ; // set the mst graph, Empty
        DisjointSets myset = new DisjointSets(g.getNbNodes());
        //Use a	disjointSet to determine whether an edge connects vertices in different components.
        for (Edge e : g.listOfEdgesSorted()){
            if (IsSafe(myset,e)) {
                MST.addEdge(e);
                myset.union(e.nodes[0],e.nodes[1]);
            }
        }
        return MST;
    }

    public static Boolean IsSafe(DisjointSets p, Edge e){

        if (p.find(e.nodes[0]) == p.find(e.nodes[1])) return false;
        else {
            // since getListOfEdges return edges in increasing order, means the first edge
            // of non-connected sets is safe.
            return true;
        }
    }

    public static void main(String[] args){

        String file = args[0];
        WGraph g = new WGraph(file);
        WGraph t = kruskal(g);
        System.out.println(t);

   } 
}
