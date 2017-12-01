using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
			if (PlayerDAO.deletePlayer(PlayerInfo.currentPlayer.id))
			{
				var measures = DatabaseSingleton.Instance.db.Select<Measure>("from Measure where playerId==?", Convert.ToInt64(PlayerInfo.currentPlayer.id));
				foreach(var measure in measures)
					MeasureDAO.deleteMeasure(measure.id);

				Button button = nextPanelYes.transform.Find(Convert.ToString(PlayerInfo.currentPlayer.id)).GetComponent<Button>();
				button.GetComponent<AccountSelect>().player = null;
				AccountSelect.removeButtonLayout(button, PlayerInfo.currentPlayer.id);
			}
			nextPanelYes.SetActive (true);
			//Application.LoadLevelAsync("Menu");
		}
	}
}
