using UnityEngine;
using System.Collections;

public class PlayerInfo {

	public enum Modes { Free, Running }

	public static Player currentPlayer {get; set;}
	public static int mode {get; set;}
	public static int laps {get; set;}
}
