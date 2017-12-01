using UnityEngine;
using System.Collections;

public class GrassAudioScript : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();

	}
	void OnTriggerEnter(Collider collider){
		audio.Play ();
		InputOutput.grass = 1;
		Debug.Log ("Play t " + 1);
	}

	void OnTriggerStay(Collider collider){
		if (!audio.isPlaying) {
			audio.Play ();
			Debug.Log ("Stay t " + 1);
		}
		InputOutput.grass = 1;
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
	}

	void OnTriggerExit(Collider collider){
		audio.Pause();
		InputOutput.grass = 0;
		Debug.Log ("Exit t " + 0);
	}

	void OnCollisionEnter(Collision collision){
		audio.Play ();
		InputOutput.grass = 1;
		Debug.Log ("Play c " + 1);
	}
	
	void OnCollisionStay(Collision collision){
		if (!audio.isPlaying) {
			audio.Play ();
		}
		InputOutput.grass = 1;
		float pitch = InputOutput.velocity / 15f;
		audio.pitch = pitch;
	}
	
	void OnCollisionExit(Collision collision){
		audio.Pause();
		InputOutput.grass = 0;
		Debug.Log ("Exit c " + 0);
	}

}
