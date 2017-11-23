using System;

public class Measure {

	public enum Type { Velocity, HeartRate, ElectrodermalActivity }

	public long id; 
	public long track;
	public int type;
	public int max;
	public int min;
	public int average;
	public long playerId;

	public Measure(){}
	
	public Measure(long track, int type, int max, int min, int average, long playerId){
		this.id = DatabaseSingleton.Instance.db.NewId ();
		this.track = track;
		this.type = type;
		this.max = max;
		this.min = min;
		this.average = average;
		this.playerId = playerId;
	}

}


