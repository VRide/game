using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeightHeight : MonoBehaviour {

	public GameObject nextPanel;
	public GameObject accountPanel;

	private static int maxLengthName = 10;
	private static int timeOver = 1;
	private int timeRemaining;
	private static string label = "";
	private static string measure;
	private Button buttonOne;
	private string panelParentName;

	void Start(){
		timeRemaining = timeOver;
		panelParentName = gameObject.transform.parent.name;
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
				if(gameObject.transform.parent.gameObject.name == "PlayerWeight"){
					PlayerInfo.currentPlayer.weight = number;
					updatePanel ();
				}
				else 
					PlayerInfo.currentPlayer.height = number;
			}else
				PlayerInfo.currentPlayer.name = label;
		
			label = "";
			measure = "";
			invokeNextPanel();
			CancelInvoke("countDown");
		}
		else if(label.Length < maxLengthName)
			label += gameObject.name;

		if(GameObject.Find ("Info" + panelParentName) != null) {
			buttonOne = GameObject.Find ("Info" + panelParentName).GetComponentInChildren<Button> ();
			if (panelParentName == "PlayerHeight") measure = " cm";
			else if (panelParentName == "PlayerWeight") measure = " kg";
			buttonOne.GetComponentInChildren<Text> ().text = label + measure;
     	}
	}

	private void updatePanel ()
	{
		if (PlayerDAO.createPlayer (PlayerInfo.currentPlayer)) {
			Button button = accountPanel.transform.Find (Convert.ToString (PlayerInfo.currentPlayer.id)).GetComponent<Button> ();
			button.GetComponent<AccountSelect> ().player = PlayerInfo.currentPlayer;
			AccountSelect.setButtonLayout (button, PlayerInfo.currentPlayer);
		}
	}

	private void invokeNextPanel(){
		gameObject.transform.parent.gameObject.SetActive (false);
		nextPanel.SetActive (true);
	}
	
}
