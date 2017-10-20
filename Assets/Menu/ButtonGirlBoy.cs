using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonGirlBoy : MenuButton {
	
	public override void doAction(){
		
		string[] hiddenButtons = new string[] {"Arrow2", "Play", "Arrow1", "Girl", "Boy", "NumberTurns", "RoundMinor", "RoundPlus"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3 (2, 2, 2);
		
		showButtons.Add("Running", dimension);
		showButtons.Add("Free", dimension);
		
		disableButton(hiddenButtons);
		enableButton(showButtons);
	}
}
		

