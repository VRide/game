using UnityEngine;
using System;

public class HUDScript : MonoBehaviour {

	public TextMesh clock;
	public TextMesh velocity;
	public TextMesh breath;
	public TextMesh electrodermal;
	public TextMesh info;
	public GameObject infoObject;

	void Update(){
		System.TimeSpan t = System.TimeSpan.FromSeconds (PlayerScript.time);
		clock.text = new System.DateTime(t.Ticks).ToString("mm:ss.f");

		if (PlayerInfo.mode == (int)PlayerInfo.Modes.Running)
			info.text = PlayerScript.laps + "/" + PlayerScript.totalLaps;
		else {
			info.text = Convert.ToInt32 (PlayerScript.totalDistance) + " m";
			infoObject.SetActive(false);
		}

		electrodermal.text = Convert.ToString(InputOutput.breath);

		velocity.text = InputOutput.velocity.ToString("0") + " km/h";
	}
}


