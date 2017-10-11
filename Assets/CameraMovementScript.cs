using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	public float speed;
	public float forwardSpeed = 0f;
	public float GravityModifier   = 0.379f;	
	public Transform ovr;
	public float acceleration;
	public float drag;
	public GameObject lastCollided;
	
	private float   FallSpeed 	   = 0.0f;
	protected CharacterController 	Controller 		 = null;
	protected OVRCameraController 	CameraController = null;
	protected Transform DirXform = null;

	void OnCollisionStay(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
		}
		if (collision.gameObject.CompareTag ("Respawn") && collision.contacts.Length >= 4) {
			lastCollided = collision.gameObject;
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
	}

	void Reset() {
		float height = (lastCollided.GetComponent<Collider> ().bounds.size.y / 2f + lastCollided.transform.position.y) + 0.95f;
		gameObject.transform.position = lastCollided.transform.position + new Vector3(0f, height, 0f);
		gameObject.transform.rotation = lastCollided.transform.rotation;
		forwardSpeed = 0f;
	}

	// Update is called once per frame
	void Update () {
		// FIXME: More elegant solution
		InputOutput.Update ();
		Vector3 moveDirection = Vector3.zero;

		float diff = (lastCollided.GetComponent<Collider> ().bounds.size.y /2.7f);

		print (gameObject.transform.position.y - (lastCollided.GetComponent<Collider>().bounds.size.y /2.7f));
		print (lastCollided.GetComponent<Collider>().bounds.size);

		Vector3 vel = rigidbody.velocity;
		forwardSpeed = Mathf.Clamp (forwardSpeed - drag * Time.deltaTime, 0, 10);

		if(Input.GetKey(KeyCode.UpArrow)){
			//gameObject.transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
			forwardSpeed = Mathf.Clamp (forwardSpeed + acceleration * Time.deltaTime, 0, 10);
	 	}

		if(Input.GetKey(KeyCode.DownArrow)){
			//gameObject.transform.Translate(Vector3.back * Time.deltaTime * forwardSpeed);
		}

		//rigidbody.velocity = new Vector3(vel.x, vel.y, forwardSpeed);
		gameObject.transform.Translate (Vector3.forward * Time.deltaTime * forwardSpeed);

		if(forwardSpeed != 0f){
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
