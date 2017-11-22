using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyScript : MonoBehaviour {

	public GameObject streetlightGroup; 

	private GameObject stars;
	private GameObject cloud;
	private GameObject sun;
	private GameObject moon;
	private Light[] streetLights;
	private GameObject directionalLight;
	private GameObject fenceGroup;

	// Use this for initialization
	void Start () {
		stars = GameObject.FindGameObjectWithTag("Star");
		cloud = GameObject.FindGameObjectWithTag("Cloud");
		sun = GameObject.FindGameObjectWithTag("Sun");
		moon = GameObject.FindGameObjectWithTag("Moon");
		streetlightGroup = GameObject.Find ("Streetlight Group");
		streetLights = streetlightGroup.GetComponentsInChildren<Light>();
		directionalLight = GameObject.FindGameObjectWithTag("directionalLight");
		fenceGroup = GameObject.Find("Inner Fence Group");

		if (PlayerInfo.mode == (int)PlayerInfo.Modes.Free) {
			FreeAfternoon();
		} else if (PlayerInfo.turn == (int)PlayerInfo.Turns.Day) {
			Day ();
		} else if (PlayerInfo.turn == (int)PlayerInfo.Turns.Night) {
			Night ();
		}
	}

	void FreeAfternoon() {
		directionalLight.transform.rotation = Quaternion.Euler (new Vector3(20, 0, 90));
		directionalLight.GetComponent<Light> ().color = new Color(1F, 0.3921F, 0F, 1F);
		directionalLight.GetComponent<Light> ().intensity = 2f;
		stars.SetActive (false);
		moon.SetActive (false);
		fenceGroup.SetActive (false);
	}

	void Day(){
		stars.SetActive (false);
		foreach (Light light in streetLights) {
			light.enabled = false;
		}
		moon.SetActive (false);

		sun.SetActive (true);
		cloud.SetActive (true);
		directionalLight.SetActive (true);
	}

	void Night(){
		stars.SetActive (true);
		foreach (Light light in streetLights){
			Debug.Log (light);
			light.enabled = true;
		}
		moon.SetActive (true);
		
		sun.SetActive (false);
		cloud.SetActive (false);
		directionalLight.SetActive (false);
	}



}
