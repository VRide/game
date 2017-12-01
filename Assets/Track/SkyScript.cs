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
	private GameObject goalRoad;
	private GameObject humanCheer;
	private GameObject fence;

	// Use this for initialization
	void Start () {
		stars = GameObject.FindGameObjectWithTag("Star");
		cloud = GameObject.FindGameObjectWithTag("Cloud");
		sun = GameObject.FindGameObjectWithTag("Sun");
		moon = GameObject.Find("Moon");
		streetlightGroup = GameObject.Find ("Streetlight Group");
		streetLights = streetlightGroup.GetComponentsInChildren<Light>();
		directionalLight = GameObject.Find("Directional light");
		fenceGroup = GameObject.Find("Run Fence Group");
		goalRoad = GameObject.Find("goal road prefab");
		humanCheer = GameObject.Find ("HumanCheer");
		fence = GameObject.FindGameObjectWithTag("ee");
		

		if (PlayerInfo.mode == (int)PlayerInfo.Modes.Free) {
			Free();
		}
		if (PlayerInfo.turn == (int)PlayerInfo.Turns.Day) {
			Day ();
		} else if (PlayerInfo.turn == (int)PlayerInfo.Turns.Night) {
			Night ();
		}
	}

	void Free() {
		fenceGroup.SetActive (false);
		goalRoad.SetActive (false);
		humanCheer.SetActive (false);
		fence.collider.isTrigger = true;
	}

	void Day(){
		stars.SetActive (false);
		foreach (Light light in streetLights) {
			light.enabled = false;
		}
		moon.SetActive (false);

		sun.SetActive (true);
		cloud.SetActive (true);
	}

	void Night(){
		Debug.Log ("Entrou NIGHT");
		stars.SetActive (true);
		foreach (Light light in streetLights){
			Debug.Log (light);
			light.enabled = true;
		}
		moon.SetActive (true);
		
		sun.SetActive (false);
		cloud.SetActive (false);
		Debug.Log ("Desativou");
		directionalLight.GetComponent<Light> ().intensity = 0.2f;
	}
}
