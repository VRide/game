using System;

public class Player {

	public enum Gender { Boy, Girl }

	public long id;
	public string name;
	public int height;
	public int weight;
	public int gender;

	public Player(){}

	public Player(int id){
		this.id = Convert.ToInt64(id);
	}

	public Player(int id, string name, int height, int weight, int gender){
		this.id = Convert.ToInt64(id);
		this.name = name;
		this.height = height;
		this.weight = weight;
		this.gender = gender;
	}
}
