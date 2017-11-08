using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccountDelete : MonoBehaviour {
	
	public int timeOver = 3;
	public GameObject nextPanelYes;
	public GameObject nextPanelNo;
	
	private int timeRemaining;
	
	void Start () {
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
		if (gameObject.name == "No") {
			nextPanelNo.SetActive (true);
		} else if(gameObject.name == "Yes") {
			print (PlayerInfo.currentPlayer.id);
			PlayerDAO.deletePlayer(PlayerInfo.currentPlayer.id);
			//nextPanelYes.SetActive (true);
			Application.LoadLevel("Menu");
		}
	}
}
