using UnityEngine;
using System.Collections;

public class GrassAudioScript : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();

	}
	void OnTriggerEnter(Collider collider){
		audio.Play ();

	}

	void OnTriggerStay(Collider collider){
		if (!audio.isPlaying)
			audio.Play ();
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
	}

	void OnTriggerExit(Collider collider){
		audio.Pause();
	}

	void OnCollisionEnter(Collision collision){
		audio.Play ();
		
	}
	
	void OnCollisionStay(Collision collision){
		if (!audio.isPlaying)
			audio.Play ();
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
	}
	
	void OnCollisionExit(Collision collision){
		audio.Pause();
	}

}
