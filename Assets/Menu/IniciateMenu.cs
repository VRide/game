using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IniciateMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("Arrow2").GetComponent<Button>().enabled = false;
		GameObject.Find("Arrow2").transform.localScale = new Vector3(0,0,0);

		GameObject.Find("Play").GetComponent<Button>().enabled = false;
		GameObject.Find("Play").transform.localScale = new Vector3(0,0,0);

		GameObject.Find ("Girl").GetComponent<Button> ().enabled = false;
		GameObject.Find("Girl").transform.localScale = new Vector3(0,0,0);
		
		GameObject.Find("Boy").GetComponent<Button>().enabled = false;
		GameObject.Find("Boy").transform.localScale = new Vector3(0,0,0);

		GameObject.Find ("Running").GetComponent<Button> ().enabled = false;
		GameObject.Find("Running").transform.localScale = new Vector3(0,0,0);
		
		GameObject.Find("Free").GetComponent<Button>().enabled = false;
		GameObject.Find("Free").transform.localScale = new Vector3(0,0,0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
