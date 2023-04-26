using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
	[SerializeField] public static List<CardData> playerDeck = new List<CardData>();
	[SerializeField] private Button normalModeButton;
	public static int currentFloor = 0;
	public static EnemyData currentEnemyData;
	public static int act = 1;


	private void Start() {
		if (normalModeButton) normalModeButton.onClick.AddListener(StartNormalMode);
	}

	private void StartNormalMode() {
        //Set Vanila Deck
        CardIcon noneIconPrefab = Resources.Load<CardIcon>("CardIcons/None Icon");
        for (int i = 0; i < 2; i++)
		for (int j = 1; j <= 7; j++) {
			playerDeck.Add(new CardData(j, (Suit) Random.Range(0, 4), noneIconPrefab));
		}
		for (int i = 0; i < 2; i++)
            playerDeck.Add(new CardData(7, (Suit)Random.Range(0, 4), noneIconPrefab));

        ToDeckBuildingScene();
	}

    public static void AddToPlayerDeck(CardData data) {
		playerDeck.Add(data);
	}

	public static void RemovePlayerDeck(CardData data) {
		playerDeck.Remove(data);
	}

	public static void ToChooseEnemyScene() {
		currentFloor++;
        SceneManager.LoadScene("Choose Enemy Scene");
    }

	public static void ToDeckBuildingScene() {
        SceneManager.LoadScene("Deck Building Scene");
    }

	public static void ToBlackjackScene(EnemyData enemyData) {
		currentEnemyData = enemyData;
		SceneManager.LoadScene("Blackjack Scene");
	}
}
