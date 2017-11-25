using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResumeScript : MonoBehaviour{
	
	void Start(){
		Button[] buttons = gameObject.GetComponentsInChildren<Button> ();

		for (int i = 0; i < buttons.Length; ++i) {
			string text = buttons [i].GetComponentInChildren<Text> ().text;

			if(buttons[i].name == "InfoTime")
				buttons [i].GetComponentInChildren<Text> ().text = Convert.ToString(PlayerInfo.currentPlayer.time) + "  " + text;
			else if(buttons[i].name == "InfoDistance")
				buttons [i].GetComponentInChildren<Text> ().text = Convert.ToString(PlayerInfo.currentPlayer.distance) + "  " + text;
			else if(buttons[i].name == "InfoFree")
				buttons [i].GetComponentInChildren<Text> ().text = Convert.ToString(PlayerInfo.currentPlayer.free) + "  " + text;
			else if(buttons[i].name == "InfoRunning")
				buttons [i].GetComponentInChildren<Text> ().text = Convert.ToString(PlayerInfo.currentPlayer.running) + "  " + text;
		}
	}
}

