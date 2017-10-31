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

	void OnTriggerExit(Collider collider){
		Debug.Log("Exit");
		audio.Pause();
	}

}
