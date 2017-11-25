using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;
using System.Collections.Generic;

public class InputOutput {

	// public const int maxHeartRate = 200;
	// public const int maxElectrodermalActivity = 100;

	private static float guidonRotation = 0f;
	public static float velocity {get; private set;}
	private static float bikeXAxis = 0f;
	public static float drag = 5f;
	public static float acceleration = 8f;
	private static bool is_pressing = false;
	public static float maxSpeed = 30f;
	private static bool locked = false;
	public static bool paused;
	public static bool quit;
	public static bool rightHand;
	public static bool leftHand;

	private static GameObject bike;
	private static MqttClient client;

	private static List<int> velocities;
	private static List<int> electrodermalActivities;
	private static List<int> heartRates;

	private static float timer;

	// Use this for initialization
	public static void Start () {
		paused = false;
		quit = false;
		rightHand = true;
		leftHand = true;
		bike = GameObject.FindGameObjectWithTag("Player");
		velocity = 0f;
		timer = 0f;

		electrodermalActivities = new List<int> ();
		heartRates = new List<int> ();
		velocities = new List<int> ();

		client = new MqttClient(IPAddress.Parse("127.0.0.1"), 1883 , false , null ); 
		client.Connect("vride-7qx45t");
		
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		client.Subscribe(new string[] { "bike/angle" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "bike/velocity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "bike/hand/right" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "bike/hand/left" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
	}

	public static void Reset(){
		velocity = 0f;
		guidonRotation = 0f;
	}

	static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e){ 
		Debug.Log ("Receiving " + e.Topic);
		//if(locked) return;

		//string m = System.Text.Encoding.UTF8.GetString(e.Message);	

		//Debug.Log ("Received " + m + " on " + e.Topic);
		if (e.Topic == "bike/velocity") {

			string message = System.Text.Encoding.UTF8.GetString(e.Message);
			if(message == "F"){
				is_pressing = true;
			}else if(message == "S"){
				is_pressing = false;
			}
			//gameObject.transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
		} else if (e.Topic == "bike/angle") {
			Debug.Log(e.Message.ToString());
			long angle = System.BitConverter.ToInt64 (e.Message, 0);
			Debug.Log ("Recebeu bike/angle o valor de " + angle);
			guidonRotation = angle;
		}else if (e.Topic == "bike/hand/right") {
			Debug.Log("Enter right hand");
			bool hand = System.BitConverter.ToBoolean(e.Message, 0);
			rightHand = hand;
		}else if (e.Topic == "bike/hand/left") {
			Debug.Log("Enter left hand");
			bool hand = System.BitConverter.ToBoolean(e.Message, 0);
			leftHand = hand;
		}
		/*
		else if (e.Topic == "bike/heart") {
			int rate = System.BitConverter.ToInt64 (e.Message, 0);
			heartRates.Add(rate);
		} else if (e.Topic == "bike/electrodermal") {
			int activity = System.BitConverter.ToInt64 (e.Message, 0);
			electrodermalActivities.Add(activity);
		}
		*/
	} 
	
	// Update is called once per frame
	public static void Update () {
		bikeXAxis = bike.transform.rotation.x;
		AudioListener.volume = paused ? 0f : 1f;

		paused = (!rightHand || !leftHand);
		quit = (!rightHand && !leftHand);

		velocity = Mathf.Clamp (velocity - drag * Time.deltaTime, 0f, maxSpeed);


		velocities.Add(Convert.ToInt32(velocity));

		if(is_pressing) velocity = Mathf.Clamp (velocity + acceleration * Time.deltaTime, 0f, maxSpeed);

		if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyUp(KeyCode.X))
			client.Publish ("bike/hand/right", System.BitConverter.GetBytes (Input.GetKey (KeyCode.X)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.Z))
			client.Publish ("bike/hand/left", System.BitConverter.GetBytes (Input.GetKey (KeyCode.Z)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

		timer += Time.deltaTime;
		if (timer >= 0.050f) {
			timer = 0;
		} else {
			return;
		}

		long angle = 0;
		if(Input.GetKey(KeyCode.LeftArrow)){
			// guidonRotation = -45f;
			angle = -45;
			client.Publish("bike/angle", System.BitConverter.GetBytes(angle) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			// gameObject.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
		}else if(Input.GetKey(KeyCode.RightArrow)){
			// guidonRotation = 45f;
			angle = 45;
			client.Publish("bike/angle", System.BitConverter.GetBytes(angle) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			// gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}else{
			client.Publish("bike/angle", System.BitConverter.GetBytes(angle) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
				client.Publish ("bike/velocity", System.Text.Encoding.UTF8.GetBytes ("F"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		} else {
				client.Publish ("bike/velocity", System.Text.Encoding.UTF8.GetBytes ("S"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		}

	}

	public static void Lock(){
		is_pressing = false;
		locked = true;
	}

	public static void Data(){
		Measure velocity = calculateMeasure(new List<int> (velocities), (int)Measure.Type.Velocity);
		//Measure heartRate = calculateMeasure (heartRates, (int)Measure.Type.HeartRate);
		//Measure electrodermalActivity = calculateMeasure (electrodermalActivities, (int)Measure.Type.ElectrodermalActivity);  

		MeasureDAO.createMeasure (velocity);
		//MeasureDAO.createMeasure (heartRate);
		//MeasureDAO.createMeasure (electrodermalActivity);
	}

	public static Measure calculateMeasure (List<int> list, int type){
		int max = 0, min = 99999;
		long sum = 0;

		for (int i = 1; i < list.Count; ++i) {
			sum += list [i];
			min = Math.Min (min, Convert.ToInt32 (list [i]));
			max = Math.Max (max, Convert.ToInt32 (list [i]));
		}

		int track = PlayerInfo.currentPlayer.free + PlayerInfo.currentPlayer.running; 

		return new Measure (track, type, max, min, Convert.ToInt32 (sum / list.Count), PlayerInfo.currentPlayer.id);
	}

	public static void Unlock(){
		locked = false;
	}
	
	public static float GetGuidonRotation() {
		return guidonRotation;
	}
	
	public static float getVelocity() {
		return velocity;
	}

	public static List<int> getVelocities() {
		return velocities;
	}

	public static List<int> getElectrodermalActivities(){
		return electrodermalActivities;
	}

	public static List<int> getHeartRates(){
			return heartRates;
	}
}
