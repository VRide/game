using UnityEngine;
using System.Collections;

public class WheelMovement : MonoBehaviour {

	public CameraMovementScript speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (new Vector3(0, 180 *  speed.forwardSpeed * Time.deltaTime, 0));
	}
}
