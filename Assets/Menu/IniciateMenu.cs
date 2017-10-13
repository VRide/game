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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
