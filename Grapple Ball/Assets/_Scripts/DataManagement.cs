using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/**
 * Non-monobehaviour class for data management
 */
public static class DataManagement {
	private static readonly string path = Application.persistentDataPath + "/playerData.json";

	/**
	 * Save player data into binary file.
	 */
	public static void SaveData(PlayerStatus player) {
		using (var file = new FileStream(path, FileMode.Create)) {
			var formatter = new BinaryFormatter();
			var data = new PlayerData(player);

			formatter.Serialize(file, data);
			file.Close();
		}
	}

	/**
	 * Delete the binary file containing the data.
	 */
	public static void DeleteData() {
		try {
			File.Delete(path);
		}
		catch (Exception e) {
			Debug.LogException(e);
			throw;
		}
	}

	/**
	 * Load player data from binary file.
	 */
	public static PlayerData LoadData() {
		if (File.Exists(path))
			using (var file = new FileStream(path, FileMode.Open)) {
				var formatter = new BinaryFormatter();
				var data = formatter.Deserialize(file) as PlayerData;
				file.Close();

				return data;
			}

		Debug.LogError("File not found at " + path);
		return null;
	}
}
