using UnityEngine;
using System.Collections;

public class ButtonHover : MonoBehaviour {
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
		print("Selecionou");
	}
	
	public void MouseOver(){
		print("entrouu");
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = 5;
	}
}
