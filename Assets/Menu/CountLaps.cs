using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountLaps : MonoBehaviour {
	
	private static int timeOver = 1;
	private int timeRemaining;

	void Start(){
		timeRemaining = timeOver;
		PlayerInfo.laps = 3;
	}

	public void countDown(){
		timeRemaining--;
		
		if(timeRemaining <= 0){
			timeRemaining = timeOver;
			countLaps();
		}
	}

	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = timeOver;
	}

	private void countLaps(){
		if (gameObject.name == "RoundMinor") {
				if (PlayerInfo.laps > 1) {
						PlayerInfo.laps--;	
				}
		} else if (gameObject.name == "RoundPlus")
				PlayerInfo.laps++;
		else if (gameObject.name == "Submit"){
			print ("ENTROUUUUU");
			Application.LoadLevel ("main");
		}

		Button buttonOne; 
		buttonOne = GameObject.Find ("NumberTurns").GetComponentInChildren<Button> ();
		buttonOne.GetComponentInChildren<Text>().text = PlayerInfo.laps.ToString();


	}

}
