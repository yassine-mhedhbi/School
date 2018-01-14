import java.io.*;
import java.util.*;




public class FordFulkerson {

	public static ArrayList<Integer> pathDFS(Integer source, Integer destination, WGraph graph){
		ArrayList<Integer> Stack = new ArrayList<Integer>();
		ArrayList<Integer> visitedNodes = new ArrayList<Integer>();
		Stack<Integer> stk = new Stack<Integer>();
		stk.push(source);

		while (!stk.empty()) {

			Integer node = stk.pop();
			visitedNodes.add(node);

			while(Stack.size() != 0 && (graph.getEdge(Stack.get(Stack.size() - 1), node) == null ||
					graph.getEdge(Stack.get(Stack.size() - 1), node).weight == 0)) {
					// if the edge has 0 weight, it shouldn't be in the path.
				Stack.remove(Stack.size() - 1);
			}
			Stack.add(node);
			ArrayList<Integer> adj = getAdj(node,graph);

			int i = 0;
			while (i < adj.size()) {
				if (!visitedNodes.contains(adj.get(i))) {
					if (adj.get(i) == destination ) {
						Stack.add(destination);
						stk.clear();
						break;
					}
					else {
						stk.push(adj.get(i));
					}
				}
				i++;
			}
		}
		return Stack;
	}

	public static ArrayList<Integer> getAdj(Integer node, WGraph graph){

		ArrayList<Integer> adjList = new ArrayList<Integer>();
		for (Edge e : graph.getEdges()) {
			if(e.nodes[0] == node && e.weight > 0) {
				adjList.add(e.nodes[1]);
			}
		}
		return adjList;
	}
	

	
	public static void fordfulkerson(Integer source, Integer destination, WGraph graph, String filePath){
		String answer="";
		String myMcGillID = "260673563"; //Please initialize this variable with your McGill ID
		int maxFlow = 0;

		WGraph residual = new WGraph(graph); //
		ArrayList<Integer> path = new ArrayList<Integer>();

		for(Edge e : graph.getEdges()) {
			e.weight = 0;
		}

		while ( (path=pathDFS(source, destination, residual)).get(path.size() -1 ) == destination) {

			Integer augFlow = Integer.MAX_VALUE;

			for(int i = 1; i < path.size(); i++) {
				Edge e = residual.getEdge(path.get(i - 1), path.get(i));
				if(e.weight < augFlow)
					augFlow = e.weight;
			}
			maxFlow += augFlow;

			augmentationFlow(graph, residual, augFlow, path);

		}

		answer += maxFlow + "\n" + graph.toString();	
		writeAnswer(filePath+myMcGillID+".txt",answer);
		System.out.println(answer);
	}

	public static void augmentationFlow(WGraph graph, WGraph residual, Integer augmentFlow, ArrayList<Integer> path) {

		for(int i = 1; i < path.size(); i++) {
			Edge e = residual.getEdge(path.get(i - 1), path.get(i));
			e.weight -= augmentFlow;
			Edge rBackEdge = residual.getEdge(e.nodes[1], e.nodes[0]);

			if(rBackEdge != null) {
				rBackEdge.weight += augmentFlow;
			} else {
				// add backEdge
				residual.addEdge(new Edge(e.nodes[1], e.nodes[0], augmentFlow));
			}
		}

		// add/substract the flow from the original graph
		for(int i = 1; i < path.size(); i++) {
			Edge forwardEdge = graph.getEdge(path.get(i - 1), path.get(i));
			if(forwardEdge != null) { // Forward edge
				forwardEdge.weight += augmentFlow;
			} else { // Back edge
				Edge backEdge = graph.getEdge(path.get(i), path.get(i - 1));
				backEdge.weight -= augmentFlow;
			}
		}
	}
	
	
	public static void writeAnswer(String path, String line){
		BufferedReader br = null;
		File file = new File(path);
		// if file doesnt exists, then create it
		
		try {
		if (!file.exists()) {
			file.createNewFile();
		}
		FileWriter fw = new FileWriter(file.getAbsoluteFile(),true);
		BufferedWriter bw = new BufferedWriter(fw);
		bw.write(line+"\n");	
		bw.close();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			try {
				if (br != null)br.close();
			} catch (IOException ex) {
				ex.printStackTrace();
			}
		}
	}
	
	 public static void main(String[] args){
		 String file = args[0];
		 File f = new File(file);
		 WGraph g = new WGraph(file);
		 fordfulkerson(g.getSource(),g.getDestination(),g,f.getAbsolutePath().replace(".txt",""));
	 }
}
