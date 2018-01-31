
public class hillClimbing {
	
	public static double func(double x){
		return Math.sin(Math.pow(x, 2.0)/2.0)/(Math.log(x+4.0)/Math.log(2));
		
	}
	
	public static double searchHC(double i, double delta){
		if((i+delta)>10){
			if(func(i)>func(i-delta)) return i;
			else return searchHC(i-delta, delta);
		}
		else if((i-delta)<0){
			if(func(i)>func(i+delta)) return i;
			else return searchHC(i+delta, delta);
		}
		else{
			if(func(i)>func(i+delta) && func(i)>func(i-delta)) return i;
			else if(func(i+delta)>=func(i-delta)) return searchHC(i+delta, delta);
			else return searchHC(i-delta, delta);
		}
		
	}
	
	
	public static void main(String args[]){
		double[][] results = new double[10][11];
		
		double delta=0.01;
		
		for(int i=0; i<10; i++){
			for(int j=0; j<11; j++){
				results[i][j]=searchHC(j, delta);
			}
			delta = delta+0.01;
		}
		
		for(int i=0; i<10; i++){
			for(int j=0; j<11; j++){
				if(j!=10) System.out.print(results[i][j] + ", ");
				else System.out.println(results[i][j]);
			}
		}
	}

}
