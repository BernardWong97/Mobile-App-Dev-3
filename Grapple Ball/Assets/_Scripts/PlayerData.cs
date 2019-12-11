using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
	public int playerHealth;
	public int playerGemCount;

	public PlayerData(PlayerStatus player) {
		playerHealth = player.health;
		playerGemCount = player.gemCount;
	}
}
