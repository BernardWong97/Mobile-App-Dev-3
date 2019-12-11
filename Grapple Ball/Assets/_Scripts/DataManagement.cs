using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataManagement {
	private static string path = Application.persistentDataPath + "/playerData.json";

	public static void SaveData(PlayerStatus player) {
		using (FileStream file = new FileStream(path, FileMode.Create)) {
			BinaryFormatter formatter = new BinaryFormatter();
			PlayerData data = new PlayerData(player);

			formatter.Serialize(file, data);
			file.Close();
		}
	}

	public static PlayerData LoadData() {
		if (File.Exists(path)) {
			using (FileStream file = new FileStream(path, FileMode.Open)) {
				BinaryFormatter formatter = new BinaryFormatter();
				PlayerData data = formatter.Deserialize(file) as PlayerData;
				file.Close();

				return data;
			}
		}
		else {
			Debug.LogError("File not found at " + path);
			return null;
		}
	}
}
