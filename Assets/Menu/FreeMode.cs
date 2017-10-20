using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreeMode : MenuButton {
	
	public override void doAction(){

		string[] hiddenButtons = new string[] {"Arrow2", "Play", "Arrow1", "Running", "Free", "Boy", "Girl"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3(2, 2, 2);
		
		showButtons.Add("RoundMinor", dimension);
		showButtons.Add("RoundPlus", dimension);
		showButtons.Add("NumberTurns", dimension);
		
		disableButton(hiddenButtons);
		enableButton(showButtons);
	}
}


