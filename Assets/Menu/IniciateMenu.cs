using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IniciateMenu : MenuButton {

	void Start (){
		string[] hiddenButtons = new string[] {"Submit", "Back1", "Back2", "Back3", "Arrow2", "Play", "Girl", "Boy", "Running", "Free", "NumberTurns", "RoundMinor", "RoundPlus"};
		
		disableButton(hiddenButtons);
	}
}
