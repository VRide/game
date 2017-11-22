using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmulateValues : MonoBehaviour{
		void Start (){
			Measure velocity = new Measure(30, 2, 14);
			Measure velocity2 = new Measure(35, 1, 24);
			Measure velocity3 = new Measure(40, 3, 20);
			Measure velocity4 = new Measure(38, 1, 16);

			Measure heart = new Measure (14, 10, 9);
			Measure heart2 = new Measure (11, 3, 10);
			Measure heart3 = new Measure (14, 4, 9);
			Measure heart4 = new Measure (18, 5, 11);

			Measure skin = new Measure (20, 12, 8);
			Measure skin2 = new Measure (21, 13, 11);
			Measure skin3 = new Measure (24, 14, 7);
			Measure skin4 = new Measure (28, 15, 11);

			Player renata = new Player(8);
			
			renata.electrodermalActivities = new List<Measure> ();
			renata.electrodermalActivities.Add (skin);
			renata.electrodermalActivities.Add (skin2);
			renata.electrodermalActivities.Add (skin3);
			renata.electrodermalActivities.Add (skin4);

			renata.velocities = new List<Measure> ();
			renata.velocities.Add (velocity);
			renata.velocities.Add (velocity2);
			renata.velocities.Add (velocity3);
			renata.velocities.Add (velocity4);

			renata.heartRates = new List<Measure> ();
			renata.heartRates.Add(heart);
			renata.heartRates.Add(heart2);
			renata.heartRates.Add(heart3);
			renata.heartRates.Add(heart4);

			bool ok = PlayerDAO.createPlayer (renata);
			
		}
	
		void Update (){
	
		}
}

