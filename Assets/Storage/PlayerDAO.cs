using System;

using iBoxDB.LocalServer;

public class PlayerDAO {

	
	public static bool createPlayer(Player player){
		return DatabaseSingleton.Instance.db.Insert("Player", player);
	}

	public static Player getPlayer(int id){
		return DatabaseSingleton.Instance.db.Get<Player>("Player", Convert.ToInt64(id));
	}

	public static bool updatePlayer(Player player){
		return DatabaseSingleton.Instance.db.Update("Player", player);
	}

	public static bool deletePlayer(int id){
		return DatabaseSingleton.Instance.db.Delete("Player", Convert.ToInt64(id)); 
	}

}


