using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonRoundMinor : MenuButton {
	public Button buttonOne;
	public int number = 1;
	public string numberTurns = "1";
	
	public override void doAction(){
		
		string[] hiddenButtons = new string[] {"Arrow1", "Arrow2", "Play", "Girl", "Boy", "Running", "Free"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3 (2, 2, 2);
		
		showButtons.Add("NumberTurns", dimension);
		showButtons.Add("RoundMinor", dimension);
		showButtons.Add("RoundPlus", dimension);
		
		disableButton(hiddenButtons);
		enableButton(showButtons);
		
		number -= 1;
		if (number < 0)
			number = 1;	

		numberTurns = number.ToString ();
		buttonOne = GameObject.Find ("NumberTurns").GetComponentInChildren<Button> ();
		buttonOne.GetComponentInChildren<Text>().text = numberTurns;
	}
}
