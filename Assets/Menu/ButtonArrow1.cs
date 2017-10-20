﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonArrow1 : MenuButton {

	public override void doAction(){

		string[] hiddenButtons = new string[] {"Arrow1", "Girl", "Boy", "Running", "Free", "NumberTurns", "RoundMinor", "RoundPlus"};

		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();

		Vector3 dimension = new Vector3 (2, 2, 2);
		
		showButtons.Add("Arrow2", dimension);
		showButtons.Add("Play", dimension);
	
		disableButton(hiddenButtons);
		enableButton(showButtons);
	}
}
