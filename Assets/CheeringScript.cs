using UnityEngine;
using System.Collections;

public class CheeringScript : MonoBehaviour {

	float totalTime, time;
	Animation animation;

	// Use this for initialization
	void Start () {
		time = 0;
		totalTime = Random.Range (0, 3);
		animation = gameObject.GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (time < totalTime) {
			time += Time.deltaTime;
			if(time >= totalTime){
				animation.Play();
			}
		}
	}
}
