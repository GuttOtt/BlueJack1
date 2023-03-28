using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	[SerializeField] public static List<CardData> playerDeck = new List<CardData>();

	public static void AddToPlayerDeck(CardData data) {
		playerDeck.Add(data);
	}

	public static void RemovePlayerDeck(CardData data) {
		playerDeck.Remove(data);
	}
}
