using UnityEngine;
using System.Collections;

public class HandleBarScript : MonoBehaviour {

	public float handlebarRotation;

	private float originalZ;
		

	// Use this for initialization
	void Start () {
		originalZ = gameObject.transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 q = gameObject.transform.localRotation.eulerAngles;

		gameObject.transform.localRotation = Quaternion.Euler (q.x, q.y, InputOutput.GetGuidonRotation());




		// gameObject.transform.rotation = Quaternion.Euler (q.x, q.y, originalZ - InputOutput.GetGuidonRotation ());
	}
}
