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
	private MoneySettings moneySettings;

	private void Start() {
		startButton.onClick.AddListener(delegate { StartEditing(playerDeck); });
		presentEditing = playerDeck;
	}

	public void EndDeckEditing(List<CardData> deckData) {
		if (presentEditing == playerDeck) {
			playerDeck = deckData.ToList();
			StartEditing(opponentDeck);
		}
		else {
			opponentDeck = deckData.ToList();
			StartMoneyEditing();
		}
	}

	private void StartEditing(List<CardData> targetDeck) {
		presentEditing = targetDeck;
		SceneManager.LoadScene("Deck Editing Scene");
	}

	private void StartMoneyEditing() {
		SceneManager.LoadScene("Money Editing Scene");
	}

	public void EndMoneyEditing(MoneySettings moneySettings) {
		this.moneySettings = moneySettings;
		ToPlayScene();
	}

	private void ToPlayScene() {
		SceneManager.LoadScene("Play Scene");
	}

	public void DeckSetting(Deck deck, bool isPlayers) {
		List<CardData> deckData = isPlayers ? playerDeck : opponentDeck;
		foreach (CardData data in deckData) {
			Card card = data.InstantiateAsCard();
			card.owner = deck.GetComponent<Gambler>();
			deck.AddCard(card);
		}
		deck.Shuffle();
	}

	public MoneySettings GetMoneySettings() {
		return moneySettings;
	}
}
