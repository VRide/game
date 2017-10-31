using UnityEngine;
using System.Collections;

public class GrassAudioScript : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();

	}
	void OnTriggerEnter(Collider collider){
		Debug.Log("Enter");
		audio.Play ();

	}

	void OnTriggerStay(Collider collider){
		Debug.Log("Exit");
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
	}

	void OnTriggerExit(Collider collider){
		Debug.Log("Exit");
		audio.Pause();
	}

}
