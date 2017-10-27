using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuButton : MonoBehaviour{
	public int timeRemaining = 3;
	public static string[] options = new string[5];
	public Button button;
	public string time = "";
	
	public int nextScreen;

	private List< List <string> > screens = new List<List<string>>();
	private Button buttonOne;
	private static int number = 1;
	private string numberTurns = "1";
	

	void Start(){
		initializeScreens();

		if (gameObject.name == screens[0][0]) {
			dodoAction (0);
		}
	}

	void initializeScreens(){
		List<string> screen0 = new List<string>(new string[] { "Play" });
		screens.Add (screen0);
		List<string> screen1 = new List<string>(new string[] { "Girl", "Boy", "Back1" });
		screens.Add (screen1);
		List<string> screen2 = new List<string>(new string[] { "Running", "Free", "Back2" });
		screens.Add (screen2);
		List<string> screen3 = new List<string>(new string[] { "NumberTurns", "RoundMinor", "RoundPlus", "Submit", "Back3" });
		screens.Add (screen3);	
	}

	public static string[] Options{
		get{ return options;}
		set{options = value;}
	}
	
	public void countDown(){
		GameObject.Find("Time").GetComponent<Button>().enabled = true;
		time = timeRemaining.ToString ();
		button = GameObject.Find ("Time").GetComponentInChildren<Button> ();
		button.GetComponentInChildren<Text>().text = time;
		timeRemaining--;

		if(timeRemaining <= 0){
			timeRemaining = 3;
			dodoAction(nextScreen);
			//if(gameObject.name != "RoundPlus") CancelInvoke("countDown");
		}

	}

	public virtual void doAction(){
		}

	
	public void dodoAction(int screen){
		List<string> hiddenButtons = new List<string>();

		for (int i = 0; i < screens.Count; ++i) {
			if (i != screen) {

				List<string> aux = screens[i];
				for(int j = 0; j < aux.Count; ++j){
					hiddenButtons.Add(aux[j]);
				}
			}
		}
				
		disableButton(hiddenButtons.ToArray());
		enableButton(screens[screen].ToArray());

		int b;

		if(gameObject.name == "Submit")
			Application.LoadLevel("main");
		else if(gameObject.name == "RoundMinor"){
			b = number - 1;
			if (b <= 0){
				b = 1;	
			}
			number = b;
		}
		else if (gameObject.name == "RoundPlus"){
			b = number + 1;
			number = b;
		}
		
		numberTurns = number.ToString ();
		buttonOne = GameObject.Find ("NumberTurns").GetComponentInChildren<Button> ();
		buttonOne.GetComponentInChildren<Text>().text = numberTurns;
		
		Options[2] = numberTurns;	
	}
	
	public void enableButton(string[] names){
		for (int i=0; i<names.Length; i++) {
			GameObject.Find(names[i]).GetComponent<Button>().enabled = true;
			GameObject.Find(names[i]).transform.localScale = new Vector3(2, 2, 2);
		}

	}
	
	public void disableButton(string[] names){
		for (int i=0; i<names.Length; i++) {
			GameObject.Find (names[i]).GetComponent<Button> ().enabled = false;
			GameObject.Find (names[i]).transform.localScale = new Vector3 (0, 0, 0);	
		}
	}

	public void enableButton(Dictionary<string, Vector3> names){
	}
	
	public void MouseOver(){
		InvokeRepeating("countDown", 1,1);
	}

	public void MouseOut(){
		CancelInvoke("countDown");
		timeRemaining = 3;
	}
}


