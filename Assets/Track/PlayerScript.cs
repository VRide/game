﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {

	// TODO: remover
	public int mode;
	public int turn;
	public bool test;

	public float speed;
	public float forwardSpeed = 0f;
	public float GravityModifier   = 0.379f;
	public Transform ovr;
	public GameObject lastCollided;
	public GameObject gui;
	public static int totalLaps;
	public static float time;
	private float startTime;
	private AudioSource audio, hitAudio;
	public static float totalDistance;
	private float maxDistance;
	private Vector3 lastDistance;

	private float   FallSpeed 	   = 0.0f;
	protected CharacterController 	Controller 		 = null;
	protected OVRCameraController 	CameraController = null;
	protected Transform DirXform = null;

	public static int laps { get; set; }
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

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("hittable")) hitAudio.Play ();
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Terrain")) {
			Reset ();
		} else if(collider.gameObject.CompareTag("Lap")){
			print(collider.gameObject.name + " " + collider.gameObject.tag);
			int actualCheckpoint = System.Convert.ToInt32(collider.gameObject.name);

			if(actualCheckpoint == nextCheckpoint && nextCheckpoint == 0){ 
				laps++;
				print("Lap " + laps);
			} 
			nextCheckpoint = (actualCheckpoint + 1)%4;

			if(laps == totalLaps + 1 && PlayerInfo.mode == (int) PlayerInfo.Modes.Running) { 
				finished = true;
				laps = totalLaps;
			}
		}
	}
	
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
		//TODO: remover
		OVRDevice.ResetOrientation ();
		if (test) {
			PlayerInfo.mode = this.mode;
			PlayerInfo.turn = this.turn;
		}

		totalDistance = 0f;
		maxDistance = 0f;
		lastDistance = gameObject.transform.position;
		startTime = 2.99f;	
		audio = gameObject.GetComponents<AudioSource> ()[0];
		hitAudio = gameObject.GetComponents<AudioSource> ()[1];
		audio.Play ();
		audio.pitch = 0f;
		laps = 1;
		time = 1;
		totalLaps = PlayerInfo.laps;
		if (test) totalLaps++;
		InputOutput.Start ();
		InputOutput.Lock ();
	}

	void Reset() {
		float height = 2.44f + 0.95f;
		Debug.Log ("HEIGHT: " + (lastCollided.GetComponent<Collider> ().bounds.size.y / 2f));
		Vector3 orientation = lastCollided.GetComponent<Collider> ().transform.rotation.eulerAngles;

		rigidbody.velocity = new Vector3(0f,0f,0f); 
		rigidbody.angularVelocity = new Vector3(0f,0f,0f);

		gameObject.transform.position = lastCollided.transform.position + new Vector3(0f, height, 0f);
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, orientation.y + 270, 0));
		gameObject.transform.FindChild ("MountainBike_01").transform.localRotation = Quaternion.Euler(new Vector3(270, 270, 0));

		lastDistance = gameObject.transform.position;
		InputOutput.Reset();
	}

	// Update is called once per frame
	void Update () {
		if (InputOutput.paused) {
			Time.timeScale = 0.0001f;
		}else{
			Time.timeScale = 1f;
		}
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
		if (startTime >= 0) {
			startTime -= (float) System.Math.Round (Time.deltaTime, 2);
			 
			if(startTime.ToString("0") == "0"){
				gui.GetComponent<TextMesh> ().text = "COMEÇOU!";
			}else{
				gui.GetComponent<TextMesh> ().text = startTime.ToString ("0");
			}
		} else {
			// FIXME: change to destroying object
			gui.GetComponent<TextMesh> ().text = "";

			if(!finished && !InputOutput.paused){
				float currentDistance = Vector3.Distance(gameObject.transform.position, lastDistance);
				totalDistance += currentDistance;
				if(currentDistance > maxDistance) maxDistance = currentDistance;
				lastDistance = gameObject.transform.position;
				time += (float) System.Math.Round (Time.deltaTime, 2);
				InputOutput.Unlock();
			}
		}

		if (finished) {
			// FIXME: change to creating object
			InputOutput.Lock();
			
			System.TimeSpan t = System.TimeSpan.FromSeconds (time);
			gui.GetComponent<TextMesh> ().text = "FIM!\nTempo total:\n" + new System.DateTime(t.Ticks).ToString("mm:ss.f") + "\nDistancia: " + totalDistance.ToString("0.00");	
		}

		if (InputOutput.paused) {
			if (startTime < 0) {
				gui.GetComponent<TextMesh> ().text = "Levante\nas duas\nmaos para sair";
				if (InputOutput.quit) {
					Finish ();
				}
			} else {
				gui.GetComponent<TextMesh> ().text = "Segure os\ndois guidaos\npara começar";
			}
		}

		// FIXME: More elegant solution
		InputOutput.Update ();
		Vector3 moveDirection = Vector3.zero;

		gameObject.transform.Translate (Vector3.forward * Time.deltaTime * InputOutput.velocity);

		if(InputOutput.velocity != 0f){
			gameObject.transform.Rotate(new Vector3(0, InputOutput.GetGuidonRotation() * Time.deltaTime, 0));
		}

		float angle = Mathf.Abs(gameObject.transform.rotation.eulerAngles.z);
		if(angle >= 45f && angle <= 315f){
			Reset();
		}
	}

	void Finish(){
		InputOutput.Data();

		if(PlayerInfo.mode == (int)PlayerInfo.Modes.Free)
			PlayerInfo.currentPlayer.free += 1;
		else if(PlayerInfo.mode == (int)PlayerInfo.Modes.Running)
			PlayerInfo.currentPlayer.running += 1;

		PlayerInfo.currentPlayer.distance += Convert.ToInt64(totalDistance);
		PlayerInfo.currentPlayer.time += Convert.ToInt64(time);
		
		PlayerDAO.updatePlayer(PlayerInfo.currentPlayer);

		Application.LoadLevel("finalstatistic");
	}
}
