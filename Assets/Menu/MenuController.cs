using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject nextPanel;
	public int timeOver = 3;

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
		gameObject.transform.parent.gameObject.SetActive (false);
		nextPanel.SetActive (true);
	}

}
