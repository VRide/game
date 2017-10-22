using UnityEngine;
using System.Collections;

public class ButtonSubmit : MenuButton{
	public override void doAction(){
		for(int i = 0; i < MenuButton.Options.Length; i++){
			print(MenuButton.Options[i]);
		}
		Application.LoadLevel("main");
	}
}
