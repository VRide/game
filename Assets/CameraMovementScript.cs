using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			gameObject.transform.Translate(Vector3.forward);
	 	}

		if(Input.GetKey(KeyCode.DownArrow)){
			gameObject.transform.Translate(Vector3.back);
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			print("Girando pra esquerda\n");
			gameObject.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			print("Girando pra direita\n");
			gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}
	}
}
