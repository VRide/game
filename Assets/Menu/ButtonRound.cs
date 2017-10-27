using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonRound : MenuButton {
	public Button buttonOne;
	public static int number = 1;
	public string numberTurns = "1";

	public static int Num{
		get{ return number;}
		set{number = value;}
	}
	
	public override void doAction(){
		
		string[] hiddenButtons = new string[] {"Back1", "Back2", "Arrow1", "Arrow2", "Play", "Girl", "Boy", "Running", "Free"};
		
		Dictionary<string, Vector3> showButtons = new Dictionary<string, Vector3>();
		
		Vector3 dimension = new Vector3 (2, 2, 2);
		
		showButtons.Add("NumberTurns", dimension);
		showButtons.Add("RoundMinor", dimension);
		showButtons.Add("RoundPlus", dimension);

		disableButton(hiddenButtons);
		enableButton(showButtons);

		int b;

		if(gameObject.name == "RoundMinor"){
			b = ButtonRound.Num - 1;
			if (b <= 0){
				b = 1;	
			}
			ButtonRound.Num = b;
		}
		else if (gameObject.name == "RoundPlus"){
			b = ButtonRound.Num + 1;
			ButtonRound.Num = b;
		}

		numberTurns = number.ToString ();
		buttonOne = GameObject.Find ("NumberTurns").GetComponentInChildren<Button> ();
		buttonOne.GetComponentInChildren<Text>().text = numberTurns;


		MenuButton.Options[2] = numberTurns;		
	}
}

