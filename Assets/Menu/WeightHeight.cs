using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeightHeight : MonoBehaviour {
	
	private static int timeOver = 1;
	private int timeRemaining;
	private static string label;
	private static string measure;
	public GameObject nextPanel;
	Button buttonOne;

	void Start(){
		//print ("entrou");
		timeRemaining = timeOver;
	}

	public void countDown(){
		timeRemaining--;
		
		if(timeRemaining <= 0){
			timeRemaining = timeOver;
			defineLabel();
		}
	}

	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}
	
	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = timeOver;
	}

	private void defineLabel(){

		if (gameObject.name == "Delete"){
			int tam = label.Length;
			if(tam != 0)
				label = label.Remove(tam - 1);
		}
		else if (gameObject.name == "Ok"){
			int number;
			if(int.TryParse(label, out number)){
				print (number);
			}
			label = "";
			measure = "";
			buttonOne = GameObject.Find ("Info").GetComponentInChildren<Button> ();
			buttonOne.GetComponentInChildren<Text> ().text = label + measure;
			CancelInvoke("countDown");
			invokeNextPanel();
		}else
			label += gameObject.name;

		buttonOne = GameObject.Find ("Info").GetComponentInChildren<Button> ();
		if (gameObject.transform.parent.name == "PlayerHeight") measure = " cm";
		else if (gameObject.transform.parent.name == "PlayerWeight") measure = " kg";
		buttonOne.GetComponentInChildren<Text> ().text = label + measure;
	}

	private void invokeNextPanel(){
		gameObject.transform.parent.gameObject.SetActive (false);
		nextPanel.SetActive (true);

	}
	
}
