using System;

using iBoxDB.LocalServer;


public class MeasureDAO {
		
		public static bool createMeasure(Measure measure){
			return DatabaseSingleton.Instance.db.Insert("Measure", measure);
		}
		
		public static Player getMeasure(int id){
			return DatabaseSingleton.Instance.db.Get<Player>("Measure", Convert.ToInt64(id));
		}
		
		public static bool updateMeasure(Player measure){
			return DatabaseSingleton.Instance.db.Update("Measure", measure);
		}
		
		public static bool deleteMeasure(int id){
			return DatabaseSingleton.Instance.db.Delete("Measure", Convert.ToInt64(id)); 
		}
		
		public static bool deleteMeasure(long id){
			return DatabaseSingleton.Instance.db.Delete("Measure", id); 
		}

		public static long generateId(){
			return DatabaseSingleton.Instance.db.NewId ();
		}
	
}

