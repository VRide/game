﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonPlay : MenuButton {

	public override void doAction(){
		
		string[] hiddenButtons = new string[] {"Arrow2", "Play", "Arrow1", "Running", "Free", "NumberTurns", "RoundMinor", "RoundPlus"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3(2, 2, 2);
		
		showButtons.Add("Girl", dimension);
		showButtons.Add("Boy", dimension);
		
		disableButton(hiddenButtons);
		enableButton(showButtons);
	}
}
