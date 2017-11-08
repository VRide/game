using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AccountSelect : MonoBehaviour {

	public int timeOver = 3;
	public GameObject nextPanel;
	public GameObject nextPanelInfo;

	private int timeRemaining;
	private static Color pink = new Color (0.925f, 0.251f, 0.478f);
	private static Color blue = new Color (0.008f, 0.467f, 0.741f);
	private static Color write = new Color (1.0f, 1.0f, 1.01f);
	private Button currentButton;
	private Player player;

	void Start() {
		timeRemaining = timeOver;
		currentButton = gameObject.GetComponent<Button> ();
		print (currentButton.name);
		player = PlayerDAO.getPlayer(Int32.Parse(gameObject.name));
		if(player != null) {
			currentButton.image.color = (player.gender == (int)Player.Gender.Boy? blue: pink);
			Text buttonText = currentButton.GetComponentInChildren<Text> ();
			buttonText.text = player.name;
			buttonText.color = write;
		}
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
		if (player == null) {
			PlayerInfo.currentPlayer = new Player(Int32.Parse(gameObject.name));
			nextPanelInfo.SetActive (true);
		} else {
			PlayerInfo.currentPlayer = player;
			nextPanel.SetActive (true);
		}
	}
}
