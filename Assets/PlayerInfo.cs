using UnityEngine;
using System.Collections;

public class PlayerInfo {

	public enum Modes { Free, Running }
	public enum Turns { Day, Night }

	public static Player currentPlayer {get; set;}
	public static int mode {get; set;}
	public static int laps {get; set;}
	public static int turn {get; set;}
}
