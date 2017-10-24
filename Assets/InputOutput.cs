using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;


public class InputOutput {

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

	// Use this for initialization
	public static void Start () {
		bike = GameObject.FindGameObjectWithTag("Player");
		velocity = 0f;

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
		if(locked) return;

		string m = System.Text.Encoding.UTF8.GetString(e.Message);	

		Debug.Log ("Received " + m + " on " + e.Topic);
		if (e.Topic == "bike/velocity") {
			string message = System.Text.Encoding.UTF8.GetString(e.Message);
			if(message == "FORWARD"){
				is_pressing = true;
			}else if(message == "STOP"){
				is_pressing = false;
			}
			//gameObject.transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
		} else if (e.Topic == "bike/angle") {
			float angle = System.BitConverter.ToSingle (e.Message, 0);
			guidonRotation = angle;
		}
	} 
	
	// Update is called once per frame
	public static void Update () {
		bikeXAxis = bike.transform.rotation.x;

		if(Input.GetKey(KeyCode.LeftArrow)){
			// guidonRotation = -45f;
			if(guidonRotation != -45f)
				client.Publish("bike/angle", System.BitConverter.GetBytes(-45f) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			// gameObject.transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
		}else if(Input.GetKey(KeyCode.RightArrow)){
			// guidonRotation = 45f;
			if(guidonRotation != 45f)
				client.Publish("bike/angle", System.BitConverter.GetBytes(45f) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			// gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
		}else{
			if(guidonRotation != 0f)
				client.Publish("bike/angle", System.BitConverter.GetBytes(0f) , MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			
		}



		if (Input.GetKey (KeyCode.UpArrow)) {
			if(!is_pressing)
				client.Publish ("bike/velocity", System.Text.Encoding.UTF8.GetBytes ("FORWARD"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		} else {
			if(is_pressing)
				client.Publish ("bike/velocity", System.Text.Encoding.UTF8.GetBytes ("STOP"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			
		}
	
		velocity = Mathf.Clamp (velocity - drag * Time.deltaTime, 0f, maxSpeed);
		if(is_pressing) velocity = Mathf.Clamp (velocity + acceleration * Time.deltaTime, 0f, maxSpeed);
	}

	public static void Lock(){
		is_pressing = false;
		locked = true;
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
