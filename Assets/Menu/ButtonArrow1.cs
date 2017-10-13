using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonArrow1 : MonoBehaviour {
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
		//pplication.LoadLevel ("main");

		GameObject.Find("Arrow1").GetComponent<Button>().enabled = false;
		GameObject.Find("Arrow1").transform.localScale = new Vector3(0,0,0);

		GameObject.Find("Arrow2").GetComponent<Button>().enabled = true;
		GameObject.Find("Arrow2").transform.localScale = new Vector3(1,1,1);

		GameObject.Find("Play").GetComponent<Button>().enabled = true;
		GameObject.Find("Play").transform.localScale = new Vector3(1,1,1);

		//GameObject.FindGameObjectWithTag("Options").GetComponent<Button>().enabled = false;
		//GameObject.Find("Options").GetComponent<Button>().enabled = false;
		//GameObject.Find("Options").transform.localScale = new Vector3(0,0,0);
		//GameObject.Find("InstructionsButton").GetComponent<Button>().enabled = true;
		//GameObject.Find("InstructionsButton").transform.localScale = new Vector3(1,1,1);
	}
	
	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = 5;
	}
}
