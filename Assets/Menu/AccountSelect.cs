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
	private static Color write = new Color (1.0f, 1.0f, 1.0f);
	private static Color black = new Color (0.0f, 0.0f, 0.0f);
	private Button currentButton;
	public Player player { get; set;}

	void Start() {
		timeRemaining = timeOver;
		currentButton = gameObject.GetComponent<Button> ();
		player = PlayerDAO.getPlayer(Int32.Parse(gameObject.name));

		if(player != null) {
			setButtonLayout (currentButton, player);
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

	public static void setButtonLayout (Button button, Player playerButton){
		button.image.color = (playerButton.gender == (int)Player.Gender.Boy ? blue : pink);
		Text buttonText = button.transform.Find("Text").GetComponentInChildren<Text>();
		buttonText.text = playerButton.name;
		buttonText.color = write;
	}

	public static void removeButtonLayout (Button button, long id){
		button.image.color = write;
		Text buttonText = button.transform.Find("Text").GetComponentInChildren<Text>();
		buttonText.text = Convert.ToString(id);
		buttonText.color = black;
	}
}
