using UnityEngine;
using System.Collections;
using iBoxDB.LocalServer;
using System;

public class MenuController : MonoBehaviour {

	public GameObject nextPanel;
	public int timeOver = 3;


	private int timeRemaining;

	void Start(){
		OVRDevice.ResetOrientation();
		timeRemaining = timeOver;
	}

	public void countDown(){
		timeRemaining--;
		
		if(timeRemaining <= 0){
			timeRemaining = timeOver;
			invokeNextPanel();
			CancelInvoke("countDown");
		}
	}

	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = timeOver;
	}

	private void invokeNextPanel(){

		GameObject parentPanel = gameObject.transform.parent.gameObject;
		if (parentPanel.name == "ChooseMode" && (gameObject.name == "Free" || gameObject.name == "Running"))
			PlayerInfo.mode = (int)Enum.Parse(typeof(PlayerInfo.Modes), gameObject.name);
		else if (parentPanel.name == "ChooseTurn")
			PlayerInfo.turn = (int)Enum.Parse(typeof(PlayerInfo.Turns), gameObject.name);
		else if (parentPanel.name == "ChooseGender")
			PlayerInfo.currentPlayer.gender = (int)Enum.Parse(typeof(Player.Gender), gameObject.name);

		parentPanel.SetActive (false);
		nextPanel.SetActive (true);
	}

}
