using UnityEngine;
using System.Collections;

public class ThighScript : MonoBehaviour {

	public GameObject calf;
	private float originalAngle;
	// Use this for initialization
	void Start () {
		originalAngle = gameObject.transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		float calfX = calf.transform.position.x;
		float calfY = calf.transform.position.y;
		float thighX = gameObject.transform.position.x;
		float thighY = gameObject.transform.position.y;

		float angle = Mathf.Atan2 (Mathf.Abs (calfY - thighY), Mathf.Abs(calfX - thighX)) * Mathf.Rad2Deg;
		float x = gameObject.transform.rotation.eulerAngles.x, y = gameObject.transform.rotation.eulerAngles.y;
		gameObject.transform.rotation = Quaternion.Euler (new Vector3(x, y, angle));
	}
}
