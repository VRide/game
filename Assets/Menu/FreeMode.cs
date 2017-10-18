using UnityEngine;
using System.Collections;

public class FreeMode : MenuButton {
	
	public override void doAction(){
		Application.LoadLevel("main");
	}
}


