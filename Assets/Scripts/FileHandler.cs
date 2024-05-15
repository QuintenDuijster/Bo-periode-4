using System;
using System.IO;
using UnityEngine;

public class FileHandler
{
	public T LoadFile<T>(string dir)
	{
		try
		{
			String fileDir = Path.Combine(Application.dataPath, "/assets/" + dir);
			if (File.Exists(fileDir))
			{
				string jsonstring = File.ReadAllText(fileDir);
				var gameObject = JsonUtility.FromJson<T>(jsonstring);
				
				if (gameObject != null)
				{
					return gameObject;
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}

		return default;
	}

	internal void SaveFile<T>(T fileData, string dir)
	{
		String fileDir = Path.Combine(Application.dataPath, "/assets/" + dir);

		try
		{
			string jsonString = JsonUtility.ToJson(fileData);
			File.WriteAllText(fileDir, jsonString);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
	}
}
