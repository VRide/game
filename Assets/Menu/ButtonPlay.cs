using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour {
	public int timeRemaining = 5;
	
	void countDown(){
		timeRemaining--;
		print(timeRemaining);
		if(timeRemaining <= 0){
			goToScene();
			CancelInvoke("countDown");
			timeRemaining = 5;
		}
	}
	
	void goToScene(){
		print("entrouu");
		Application.LoadLevel ("main");
	}
	
	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = 5;
	}
}
