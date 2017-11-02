using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using iBoxDB.LocalServer;

public class DatabaseSingleton {

	private static DatabaseSingleton instance;
	public DB server = null;
	public DB.AutoBox db = null;
	
	private DatabaseSingleton() {
		if (db == null)
		{	
			DB.Root(Application.persistentDataPath);
			
			server = new DB(3);
			server.GetConfig().EnsureTable<Player>("Player", "id");	
			db = server.Open();
		}
	}
	
	public static DatabaseSingleton Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new DatabaseSingleton();
			}
			return instance;
		}
	}
}

public static class IDHelper
{
    public static int Id(this AutoBox auto, byte pos, int step = 1)
    {
        return (int)auto.NewId(pos, step);
    }
}