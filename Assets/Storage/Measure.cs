using System;

public class Measure {
	public int max  {get; set;}
	public int min  {get; set;}
	public int average {get; set;}

	public Measure(){}
	
	public Measure(int max, int min, int average){
		this.max = max;
		this.min = min;
		this.average = average;
	}

}


