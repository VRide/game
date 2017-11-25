using System;
using System.Collections;
using System.Collections.Generic;

public class Player {

	public enum Gender { Boy, Girl }

	public long id;
	public string name;
	public int height;
	public int weight;
	public int gender;
	public int free;
	public int running;
	public long time;
	public long distance;

	public Player(){}

	public Player(int id){
		this.id = Convert.ToInt64(id);
		this.time = 0;
		this.distance = 0;
		this.free = 0;
		this.running = 0;
	}

	public Player(int id, string name, int height, int weight, int gender){
		this.id = Convert.ToInt64(id);
		this.name = name;
		this.height = height;
		this.weight = weight;
		this.gender = gender;
		this.time = 0;
		this.distance = 0;
		this.free = 0;
		this.running = 0;
	}
}

