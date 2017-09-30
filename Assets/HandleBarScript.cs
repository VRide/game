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
		Vector3 q = gameObject.transform.rotation.eulerAngles;

		gameObject.transform.rotation = Quaternion.Euler (q.x, q.y, originalZ);

		if(Input.GetKey(KeyCode.LeftArrow)){
			Quaternion newQ = Quaternion.Euler(q.x, q.y, originalZ - 30);
			gameObject.transform.rotation = newQ;
		}
		
		if(Input.GetKey(KeyCode.RightArrow)){
			gameObject.transform.rotation = Quaternion.Euler(q.x, q.y, originalZ + 30);
		}	
	}
}
