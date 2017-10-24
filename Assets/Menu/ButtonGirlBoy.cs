using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonGirlBoy : MenuButton {
	
	public override void doAction(){
		
		string[] hiddenButtons = new string[] {"Submit", "Back1", "Back3", "Play", "Girl", "Boy", "NumberTurns", "RoundMinor", "RoundPlus"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3 (2, 2, 2);
		
		showButtons.Add("Running", dimension);
		showButtons.Add("Free", dimension);
		showButtons.Add("Back2", dimension);
		
		disableButton(hiddenButtons);
		enableButton(showButtons);

		MenuButton.Options[0] = gameObject.name;		
	}
}
		

