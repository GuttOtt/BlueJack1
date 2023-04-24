using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SandboxManager : Singleton<SandboxManager> {
	[SerializeField] Button startButton;
	private List<CardData> playerDeck;
	private List<CardData> opponentDeck;
	private List<CardData> presentEditing;
	private Sprite sandboxEnemyPortrait;

	private void Start() {
		startButton.onClick.AddListener(delegate { StartEditing(playerDeck); });
		presentEditing = playerDeck;
		sandboxEnemyPortrait = Resources.Load<Sprite>("Sprites/BasicPortrait");
	}

	public void EndDeckEditing(List<CardData> deckData) {
		if (presentEditing == playerDeck) {
			playerDeck = deckData.ToList();
			StartEditing(opponentDeck);
		}
		else {
			opponentDeck = deckData.ToList();
			ToPlayScene();
		}
	}

	private void StartEditing(List<CardData> targetDeck) {
		presentEditing = targetDeck;
		SceneManager.LoadScene("Deck Editing Scene");
	}

	private void StartMoneyEditing() {
		SceneManager.LoadScene("Money Editing Scene");
	}

	private void ToPlayScene() {
		EnemyData enemyData = EnemyData.CreateInstance<EnemyData>();
		enemyData.enemyName = "Sandbox Enemy";
		enemyData.hp = new HP(200);
		enemyData.portrait = sandboxEnemyPortrait;
		enemyData.deckData = opponentDeck;

		GameManager.playerDeck = playerDeck;

		GameManager.ToBlackjackScene(enemyData);
	}

	public void DeckSetting(Deck deck, bool isPlayers) {
		List<CardData> deckData = isPlayers ? playerDeck : opponentDeck;
		foreach (CardData data in deckData) {
			Card card = data.InstantiateAsCard();
			card.owner = deck.GetComponent<BlackJacker>();
			deck.AddCard(card);
		}
		deck.Shuffle();
	}
}
