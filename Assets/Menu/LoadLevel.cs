using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour{
	public int timeOver = 3;
	public string level;

	private int timeRemaining;
	
	void Start(){
		timeRemaining = timeOver;
	}
	
	public void countDown(){
		timeRemaining--;
		
		if(timeRemaining <= 0){
			timeRemaining = timeOver;
			invokeNextPanel();
			CancelInvoke("countDown");
		}
	}
	
	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = timeOver;
	}
	
	private void invokeNextPanel(){
		Application.LoadLevel(level);
	}
}

