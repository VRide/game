using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmulateValues : MonoBehaviour{
		void Start (){
			Measure velocity = new Measure(1, 0, 8, 13, 6, 1);
			Measure velocity2 = new Measure(2, 0, 6, 12, 18, 1);
			Measure velocity3 = new Measure(3, 0, 8, 14, 2, 1);
			Measure velocity4 = new Measure(4, 0, 9, 18 ,27, 1);

			//print(DatabaseSingleton.Instance.db.Insert ("Measure", velocity, velocity2, velocity3, velocity4));

			/*
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
			*/
		}
	
		void Update (){
	
		}
}

