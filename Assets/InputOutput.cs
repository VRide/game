using UnityEngine;
using System.Collections;

public class InputOutput {

	private static float guidonRotation = 0f;
	public static float velocity {get; private set;}
	private static float bikeXAxis = 0f;
	public static float drag = 1f;
	public static float acceleration = 5f;
	public static float maxSpeed = 30f;

	private static GameObject bike;

	// Use this for initialization
	public static void Start () {
		bike = GameObject.FindGameObjectWithTag("Player");
		velocity = 0f;
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

		velocity = Mathf.Clamp (velocity - drag * Time.deltaTime, 0f, maxSpeed);

		if(Input.GetKey(KeyCode.UpArrow)){
			Debug.Log("apertou " + velocity);
			//gameObject.transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
			velocity = Mathf.Clamp (velocity + acceleration * Time.deltaTime, 0f, maxSpeed);
		}

	}

	public static float GetGuidonRotation() {
		return guidonRotation;
	}
	
	public static float getVelocity() {
		return velocity;
	}
}
