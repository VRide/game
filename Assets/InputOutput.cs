using UnityEngine;
using System.Collections;

public class InputOutput {

	private static float guidonRotation = 0f;
	private static float velocity = 0f;
	private static float bikeXAxis = 0f;

	private static GameObject bike;

	// Use this for initialization
	public static void Start () {
		bike = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	public static void Update () {
		bikeXAxis = bike.transform.rotation.x;

		guidonRotation = 0f;
		if(Input.GetKey(KeyCode.LeftArrow)){
			guidonRotation = -45f;
			// gameObject.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
		}else if(Input.GetKey(KeyCode.RightArrow)){
			guidonRotation = 45f;
			// gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}

	}

	public static float GetGuidonRotation() {
		return guidonRotation;
	}
	
	public static float getVelocity() {
		return velocity;
	}
}
