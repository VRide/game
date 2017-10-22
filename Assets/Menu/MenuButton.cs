using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuButton : MonoBehaviour{
	public int timeRemaining = 3;
	public static string[] options = new string[5];
	
	public static string[] Options{
		get{ return options;}
		set{options = value;}
	}
	
	public void countDown(){
		timeRemaining--;
		print(timeRemaining);
		if(timeRemaining <= 0){
			doAction();
			CancelInvoke("countDown");
			timeRemaining = 3;
		}
	}
	
	public virtual void doAction(){
		//do something when the button is pressed
	}
	
	public void enableButton(Dictionary<string, Vector3> buttons){
		foreach (string key in buttons.Keys){
			GameObject.Find(key).GetComponent<Button>().enabled = true;
			GameObject.Find(key).transform.localScale = buttons[key];
		}

	}
	
	public void disableButton(string[] names){
		for (int i=0; i<names.Length; i++) {
			GameObject.Find (names[i]).GetComponent<Button> ().enabled = false;
			GameObject.Find (names[i]).transform.localScale = new Vector3 (0, 0, 0);	
		}
	}
	
	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}

	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = 3;
	}
}


