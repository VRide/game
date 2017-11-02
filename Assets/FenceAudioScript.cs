using UnityEngine;
using System.Collections;

public class FenceAudioScript : MonoBehaviour {

	private AudioSource audio;
	
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		
	}
	void OnCollisionEnter(Collision collision){
		Debug.Log("cerca");
		Debug.Log (audio.name);
		audio.Play ();
	}
}
