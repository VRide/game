using UnityEngine;
using System.Collections;

public class KneeScript : MonoBehaviour {

	public GameObject pedal;
	private float heightOffset;
	// Use this for initialization
	void Start () {
		heightOffset = gameObject.transform.position.y - pedal.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float x = gameObject.transform.position.x, z = gameObject.transform.position.z;
		gameObject.transform.position = new Vector3 (pedal.transform.position.x, pedal.transform.position.y + heightOffset, z);
	}
}
