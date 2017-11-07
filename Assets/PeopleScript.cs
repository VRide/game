using UnityEngine;
using System.Collections;

public class PeopleScript : MonoBehaviour {

	private GameObject man;
	private GameObject woman;

	// Use this for initialization
	void Start () {
		man = GameObject.Find ("man");
		woman = GameObject.Find ("woman");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.H)) {
			woman.SetActive(false);
		}
		if (Input.GetKey (KeyCode.M)) {
			man.SetActive(false);
		}
	}
}
