
public class simulatedAnnealing {
	
	
	public static double func(double x){
		return Math.sin(Math.pow(x, 2.0)/2.0)/(Math.log(x+4.0)/Math.log(2));
		
	}
	
	public static double p(double newVal, double oldVal,double T){
		return Math.exp(-(oldVal-newVal)/T);
	}
	
	
	public static double searchSA(double i, double delta, double T){
		
		double coolingRate = 0.003;
		
		while(T>1){
			
			if((i+delta)>10){
				if(p(func(i-delta), func(i), T)>Math.random()){
					i-=delta;
				}
			}
			
			else if((i-delta)<0){
				if(p(func(i+delta), func(i), T)>Math.random()){
					i+=delta;
				}
			}
			
			else{
				if(func(i+delta)>=func(i-delta)){
					if(p(func(i+delta), func(i), T)>Math.random()){
						i+=delta;
					}
				}
				else{
					if(p(func(i-delta), func(i), T)>Math.random()){
						i-=delta;
					}
				}
			}
			T*=1-coolingRate;
		}
		return i;
	}
	
	
	public static void main(String args[]){
		double[][] results = new double[10][11];
		
		double delta=0.01;
		
		double T = 1000;
		
		for(int i=0; i<10; i++){
			for(int j=0; j<11; j++){
				results[i][j]=searchSA(j, delta, T);
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
