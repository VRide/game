﻿using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	public float speed;
	public float forwardSpeed = 0f;
	public float GravityModifier   = 0.379f;
	public Transform ovr;
	public GameObject lastCollided;
	public GameObject gui;
	public int totalLaps;
	private float time;
	
	private float   FallSpeed 	   = 0.0f;
	protected CharacterController 	Controller 		 = null;
	protected OVRCameraController 	CameraController = null;
	protected Transform DirXform = null;

	public int laps { get; set; }
	public int nextCheckpoint = 1;
	private bool finished = false;

	void OnCollisionStay(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
		}
		if (collision.gameObject.CompareTag ("Respawn") && collision.contacts.Length >= 4) {
			lastCollided = collision.gameObject;
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Terrain")) {
			Reset ();
		} else {
			print(collider.gameObject.name);
			int actualCheckpoint = System.Convert.ToInt32(collider.gameObject.name);

			if(actualCheckpoint == nextCheckpoint && nextCheckpoint == 0){ 
				laps++;
				print("Lap " + laps);
			} 
			nextCheckpoint = (actualCheckpoint + 1)%4;

			if(laps == totalLaps + 1) finished = true;
		}
	}

	/*
	void OnCollisionStay()
	{
	}
	*/
	void Awake()
	{		
		// We use Controller to move player around
		Controller = gameObject.GetComponent<CharacterController>();
		
		if(Controller == null)
			Debug.LogWarning("OVRPlayerController: No CharacterController attached.");
		
		// We use OVRCameraController to set rotations to cameras, 
		// and to be influenced by rotation
		OVRCameraController[] CameraControllers;
		CameraControllers = gameObject.GetComponentsInChildren<OVRCameraController>();
		
		if(CameraControllers.Length == 0)
			Debug.LogWarning("OVRPlayerController: No OVRCameraController attached.");
		else if (CameraControllers.Length > 1)
			Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraController attached.");
		else
			CameraController = CameraControllers[0];	
		
		// Instantiate a Transform from the main game object (will be used to 
		// direct the motion of the PlayerController, as well as used to rotate
		// a visible body attached to the controller)
		DirXform = null;
		Transform[] Xforms = gameObject.GetComponentsInChildren<Transform>();
		
		for(int i = 0; i < Xforms.Length; i++)
		{
			if(Xforms[i].name == "ForwardDirection")
			{
				DirXform = Xforms[i];
				break;
			}
		}
		
		if(DirXform == null)
			Debug.LogWarning("OVRPlayerController: ForwardDirection game object not found. Do not use.");
	}

	// Use this for initialization
	void Start () {
		InputOutput.Start ();
		laps = 1;
		time = 1;
	}

	void Reset() {
		float height = (lastCollided.GetComponent<Collider> ().bounds.size.y / 2f + lastCollided.transform.position.y) + 0.95f;
		Vector3 orientation = lastCollided.GetComponent<Collider> ().transform.rotation.eulerAngles;

		rigidbody.velocity = new Vector3(0f,0f,0f); 
		rigidbody.angularVelocity = new Vector3(0f,0f,0f);

		gameObject.transform.position = lastCollided.transform.position + new Vector3(0f, height, 0f);
		gameObject.transform.rotation = Quaternion.Euler(orientation + new Vector3(0, 270, 0));
		gameObject.transform.FindChild ("MountainBike_01").transform.localRotation = Quaternion.Euler(new Vector3(270, 270, 0));

		forwardSpeed = 0f;
	}

	// Update is called once per frame
	void Update () {
		time += (float) System.Math.Round (Time.deltaTime, 2);
		System.TimeSpan t = System.TimeSpan.FromSeconds (time);
		gui.GetComponent<TextMesh> ().text = finished ? "FINISH" : "" + laps + "/" + totalLaps + "\n" + new System.DateTime(t.Ticks).ToString("mm:ss.f") + "\n" + forwardSpeed.ToString("0.00");

		// FIXME: More elegant solution
		InputOutput.Update ();
		Vector3 moveDirection = Vector3.zero;

		float diff = (lastCollided.GetComponent<Collider> ().bounds.size.y /2.7f);


		gameObject.transform.Translate (Vector3.forward * Time.deltaTime * InputOutput.velocity);

		if(InputOutput.velocity != 0f){
			gameObject.transform.Rotate(new Vector3(0, InputOutput.GetGuidonRotation() * Time.deltaTime, 0));
		}

		float angle = Mathf.Abs(gameObject.transform.rotation.eulerAngles.z);
		if(angle >= 45f && angle <= 315f){
			Reset();
		}
		return;
		if (Controller.isGrounded && FallSpeed <= 0)
			FallSpeed = ((Physics.gravity.y * (GravityModifier * 0.002f)));	
		else
			FallSpeed += ((Physics.gravity.y * (GravityModifier * 0.002f)) * OVRDevice.SimulationRate * Time.deltaTime);	
		
		moveDirection.y += FallSpeed * OVRDevice.SimulationRate * Time.deltaTime;
		Controller.Move(moveDirection);
	}
}
