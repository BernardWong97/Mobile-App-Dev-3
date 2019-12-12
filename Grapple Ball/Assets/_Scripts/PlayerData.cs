using System;

/**
 * A serializable class container to serialize player data
 */
[Serializable]
public class PlayerData {
	public int playerGemCount;

	// Public variables
	public float playerHealth;

	// Constructor with player arg
	public PlayerData(PlayerStatus player) {
		playerHealth = player.health;
		playerGemCount = player.gemCount;
	}
}
