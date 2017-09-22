using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	public float speed;
	public float GravityModifier   = 0.379f;	
	public Transform ovr;

	private float   FallSpeed 	   = 0.0f;
	protected CharacterController 	Controller 		 = null;
	protected OVRCameraController 	CameraController = null;
	protected Transform DirXform = null;

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
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = Vector3.zero;

		if(Input.GetKey(KeyCode.UpArrow)){
			gameObject.transform.Translate(Vector3.forward);
	 	}

		if(Input.GetKey(KeyCode.DownArrow)){
			gameObject.transform.Translate(Vector3.back);
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			gameObject.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}

		if(Input.GetKey(KeyCode.Space)){
			gameObject.transform.Translate(Vector3.up);
		}

		if (Controller.isGrounded && FallSpeed <= 0)
			FallSpeed = ((Physics.gravity.y * (GravityModifier * 0.002f)));	
		else
			FallSpeed += ((Physics.gravity.y * (GravityModifier * 0.002f)) * OVRDevice.SimulationRate * Time.deltaTime);	
		
		moveDirection.y += FallSpeed * OVRDevice.SimulationRate * Time.deltaTime;
		Controller.Move(moveDirection);
	}
}
