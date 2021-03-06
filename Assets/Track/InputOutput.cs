﻿using UnityEngine;
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
	public static int breath {get; private set;}
	private static float bikeXAxis = 0f;
	public static float drag = 5f;
	public static float acceleration = 8f;
	private static bool locked = false;
	public static bool paused;
	public static int grass = 0;
	public static bool quit;
	public static bool rightHand;
	public static bool leftHand;

	private static int discard = 0;
	private static int quantity = 0;
	private static int oldElevation;
	private static int oldDifficulty;
	private static long sum = 0;
	private static float constant = 0.0006f * 31.4159265358979f;
	private static float average = 0f;
	private static GameObject bike;
	private static MqttClient client;
	private static bool mqtt;
	private static List<int> velocities;
	private static List<int> electrodermalActivities;
	private static List<int> breathRates;

	private static float timer;

	// Use this for initialization
	public static void Start () {
		paused = false;
		quit = false;
		rightHand = false;
		leftHand = false;
		bike = GameObject.Find("Player");
		velocity = 0f;
		timer = 0f;
		discard = 0;

		electrodermalActivities = new List<int> ();
		breathRates = new List<int> ();
		velocities = new List<int> ();

		try{
			client = new MqttClient(IPAddress.Parse("10.0.0.1"), 1883 , false , null ); 
			client.Connect("vride-7qx45t");
			
			client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

			client.Subscribe(new string[] { "bike/angle" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
			client.Subscribe(new string[] { "bike/elevation" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
			client.Subscribe(new string[] { "bike/difficulty" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
			client.Subscribe(new string[] { "bike/velocity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
			client.Subscribe(new string[] { "bike/hand/right" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
			client.Subscribe(new string[] { "bike/hand/left" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
			client.Subscribe(new string[] { "user/breath" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			mqtt = true;
		} catch(System.Exception e){
			mqtt = false;	
		}
	}

	public static void Reset(){
		velocity = 0f;
		guidonRotation = 0f;
	}

	static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e){ 
		Debug.Log ("Receiving " + e.Topic);
		//string m = System.Text.Encoding.UTF8.GetString(e.Message);
		string message = System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log ("Received on " + e.Topic + ": " + message);

		if (e.Topic == "bike/velocity") {
			long instant = Convert.ToInt64 (message); 
			if(instant < 3f*average || discard > 5) {
				discard = 0;
				sum += instant;
				quantity++;
				if(quantity == 5){
					average = sum/5f;
					sum = 0;
					quantity = 0;
				}
				Debug.Log(average);
				velocity =  (Convert.ToInt64 (message) + average) * constant;
			}else discard++;
		} else if (e.Topic == "bike/angle") {	
			long angle = Convert.ToInt64 (message);
			guidonRotation = angle;
		} else if (e.Topic == "bike/hand/right") {
			bool hand = (message == "1");
			rightHand = hand;
		} else if (e.Topic == "bike/hand/left") {
			bool hand = (message == "1");
			leftHand = hand;
		} else if (e.Topic == "user/breath") {
			breath = Convert.ToInt32 (message);
			breathRates.Add(breath);
		} else if (e.Topic == "bike/electrodermal") {
			int activity = System.BitConverter.ToInt32 (e.Message, 0);
			electrodermalActivities.Add(activity);
		}

		rightHand = leftHand = true;
	} 
	
	// Update is called once per frame
	public static void Update () {
		bikeXAxis = bike.transform.rotation.eulerAngles.x;
		int elevation;

		while (bikeXAxis < 0f) {
			bikeXAxis += 360f;
		}

		if (bikeXAxis >= 359f || bikeXAxis <= 1f) {
			elevation = 0;
		} else if (bikeXAxis >= 1f && bikeXAxis <= 180f) {
			elevation = -1;
		} else {
			elevation = 1;
		}

		if (oldElevation != elevation) {
			client.Publish ("bike/elevation", System.Text.Encoding.UTF8.GetBytes (elevation.ToString()), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		}

		oldElevation = elevation;

		int difficulty = (elevation + 1) * 2 + grass;
		if (oldDifficulty != difficulty) {
			client.Publish ("bike/difficulty", System.Text.Encoding.UTF8.GetBytes (difficulty.ToString()), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		}
		
		oldDifficulty = difficulty;
		
		Debug.Log (elevation + " " + grass + " " + difficulty.ToString());

		AudioListener.volume = paused ? 0f : 1f;

		paused = (!rightHand || !leftHand);
		quit = (!rightHand && !leftHand);

		if (mqtt)
			MqttUpdate ();
		else
			TestUpdate ();

		velocities.Add(Convert.ToInt32(velocity));

		timer += Time.deltaTime;
		if (timer >= 0.050f) {
			timer = 0;
		} else {
			return;
		}

	}

	private static void MqttUpdate(){
		int elevation = Mathf.RoundToInt(bike.transform.rotation.x);
		Debug.Log (elevation);
	}

	private static void TestUpdate(){
		rightHand = Input.GetKey (KeyCode.X);
		//leftHand = Input.GetKey (KeyCode.Z);
		leftHand = Input.GetKey (KeyCode.X);

		if(Input.GetKey(KeyCode.LeftArrow))
			guidonRotation = -45;
		else if(Input.GetKey(KeyCode.RightArrow))
			guidonRotation = 45;
		else
			guidonRotation = 0;

		velocity = Mathf.Clamp (velocity - drag * Time.deltaTime, 0f, 30f);
		if (Input.GetKey (KeyCode.UpArrow)) {
			velocity = Mathf.Clamp (velocity + acceleration * Time.deltaTime, 0f, 30f);
		}
	}

	public static void Lock(){
		locked = true;
		guidonRotation = 0;
		drag = 15f;
		Debug.Log ("Lock()");
	}

	public static void Unlock(){
		locked = false;
		drag = 5f;
	}

	public static void Data(){
		Measure velocity = calculateMeasure(new List<int> (velocities), (int)Measure.Type.Velocity);
		Measure heartRate = calculateMeasure (breathRates, (int)Measure.Type.HeartRate);
		//Measure electrodermalActivity = calculateMeasure (electrodermalActivities, (int)Measure.Type.ElectrodermalActivity);  

		MeasureDAO.createMeasure (velocity);
		MeasureDAO.createMeasure (heartRate);
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

		Debug.Log ("Sum " + sum + " List " + list.Count);
		//int track = PlayerInfo.currentPlayer.free + PlayerInfo.currentPlayer.running; 
		int track = 0;
		return new Measure (track, type, max, min, Convert.ToInt32 (list.Count == 0 ? 0 : sum / list.Count), 0);
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
			return breathRates;
	}
}
