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
	public const int maxVelocity = 40;

	private static float guidonRotation = 0f;
	public static float velocity {get; private set;}
	private static float bikeXAxis = 0f;
	public static float drag = 5f;
	public static float acceleration = 8f;
	private static bool is_pressing = false;
	public static float maxSpeed = 30f;
	private static bool locked = false;

	private static GameObject bike;
	private static MqttClient client;

	private static int[] velocities = new int[maxVelocity];
	private static List<int> electrodermalActivities;
	private static List<int> heartRates;

	private static float timer;

	// Use this for initialization
	public static void Start () {
		bike = GameObject.FindGameObjectWithTag("Player");
		velocity = 0f;
		timer = 0f;

		electrodermalActivities = new List<int> ();
		heartRates = new List<int> ();
		Array.Clear(velocities, 0, maxVelocity);

		client = new MqttClient(IPAddress.Parse("127.0.0.1"), 1883 , false , null ); 
		client.Connect("vride-7qx45t");
		
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		client.Subscribe(new string[] { "bike/angle" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe(new string[] { "bike/velocity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
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

		velocity = Mathf.Clamp (velocity - drag * Time.deltaTime, 0f, maxSpeed);

		velocities[Convert.ToInt32(velocity)]++;

		if(is_pressing) velocity = Mathf.Clamp (velocity + acceleration * Time.deltaTime, 0f, maxSpeed);

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
		// Measure velocity = calculateMeasure(new List<int> (velocities));
		// Measure heartRate = calculateMeasure (heartRates);
		// Measure electrodermalActivity = calculateMeasure (electrodermalActivities);  

		// PlayerInfo.currentPlayer.velocities.Add (velocity);
		// PlayerInfo.currentPlayer.heartRates.Add (heartRate);
		// PlayerInfo.currentPlayer.electrodermalActivities.Add (electrodermalActivity);

		PlayerDAO.updatePlayer (PlayerInfo.currentPlayer);
	}

	public static Measure calculateMeasure (List<int> list)
	{
		int max = 0, min = 99999;
		long sum = 0, n = 0;

		for (int i = 1; i < list.Count; ++i) {
			n += list [i] == 0? 0: 1;
			sum += list [i] * i;
			min = Math.Min (min, Convert.ToInt32 (list [i]));
			max = Math.Max (max, Convert.ToInt32 (list [i]));
		}

		return null;
		//return new Measure (max, min, Convert.ToInt32 (sum / n));
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
}
