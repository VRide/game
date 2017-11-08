using UnityEngine;
using System.Collections;

public class PeopleScript : MonoBehaviour {

	private GameObject man;
	private GameObject woman;

	// Use this for initialization
	void Start () {
		man = GameObject.Find ("man");
		woman = GameObject.Find ("woman");

		if (PlayerInfo.currentPlayer.gender == (int)Player.Gender.Boy) {
			woman.SetActive(false);
		} else if (PlayerInfo.currentPlayer.gender == (int)Player.Gender.Girl) {
			man.SetActive(false);
		}
	}

}
