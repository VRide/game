using UnityEngine;
using System.Collections;

public class RunningMode : MenuButton {
	
	public override void doAction(){
		Application.LoadLevel("main");
	}
}



