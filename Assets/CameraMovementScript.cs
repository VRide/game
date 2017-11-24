using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	// TODO: remover
	public int mode;

	public float speed;
	public float forwardSpeed = 0f;
	public float GravityModifier   = 0.379f;
	public Transform ovr;
	public GameObject lastCollided;
	public GameObject gui;
	public int totalLaps;
	private float time;
	private float startTime;
	private AudioSource audio, hitAudio;
	private float totalDistance;
	private float maxDistance;
	private Vector3 lastDistance;

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

			if(laps == totalLaps + 1 && PlayerInfo.mode == (int) PlayerInfo.Modes.Running) finished = true;
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
		PlayerInfo.mode = this.mode;

		totalDistance = 0f;
		maxDistance = 0f;
		lastDistance = gameObject.transform.position;
		InputOutput.Start ();
		if (PlayerInfo.mode == (int) PlayerInfo.Modes.Running) {
			InputOutput.Lock ();
			startTime = 2.99f;
		} else {
			startTime = 0f;	
		}
		audio = gameObject.GetComponents<AudioSource> ()[0];
		hitAudio = gameObject.GetComponents<AudioSource> ()[1];
		audio.Play ();
		laps = 1;
		time = 1;
		totalLaps = 3 + PlayerInfo.laps;
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
				gui.GetComponent<TextMesh> ().text = "GO!";
			}else{
				gui.GetComponent<TextMesh> ().text = startTime.ToString ("0");
			}
		} else {
			// FIXME: change to destroying object

			if(!finished && !InputOutput.paused){
				float currentDistance = Vector3.Distance(gameObject.transform.position, lastDistance);
				totalDistance += currentDistance;
				if(currentDistance > maxDistance) maxDistance = currentDistance;
				Debug.Log(maxDistance);
				lastDistance = gameObject.transform.position;
				time += (float) System.Math.Round (Time.deltaTime, 2);
				InputOutput.Unlock();
			}

			System.TimeSpan t = System.TimeSpan.FromSeconds (time);
			string s_laps = (PlayerInfo.mode == (int) PlayerInfo.Modes.Running) ? laps + "/" + totalLaps : "MODO LIVRE";
			
			gui.GetComponent<TextMesh> ().text = "" + s_laps + "\n" + new System.DateTime(t.Ticks).ToString("mm:ss.f") + "\n" + InputOutput.velocity.ToString("0.00") + "\n" +  totalDistance.ToString("0.00");
		}

		if (finished) {
			// FIXME: change to creating object
			InputOutput.Lock();
			System.TimeSpan t = System.TimeSpan.FromSeconds (time);
			gui.GetComponent<TextMesh> ().text = "FINISH!\nTotal time:\n" + new System.DateTime(t.Ticks).ToString("mm:ss.f") + "\nTotal distance: " + totalDistance.ToString("0.00");	
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
}
