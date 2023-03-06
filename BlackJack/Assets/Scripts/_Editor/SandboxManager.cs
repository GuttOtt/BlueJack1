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
	public Money playerStartMoney = Money.zero;
	private Money opponentStartMoney = Money.zero;
	public Money ante = Money.zero;
	public float raiseRatio = 2f;

	private void Start() {
		startButton.onClick.AddListener(delegate { StartEditing(playerDeck); });
	}

	public void EndDeckEditing(List<CardData> deckData) {
		presentEditing = deckData.ToList();

		if (presentEditing == playerDeck) {
			StartEditing(opponentDeck);
		}
		else {
			//ToMoneyEditing();
			ToPlayScene();
		}
	}

	private void StartEditing(List<CardData> targetDeck) {
		presentEditing = targetDeck;
		SceneManager.LoadScene("Deck Editing Scene");
	}

	private void ToMoneyEditing() {
		SceneManager.LoadScene("Money Editing Scene");
	}

	private void ToPlayScene() {
		SceneManager.LoadScene("Play Scene");
	}

	public void SetDeck(Deck deck, bool isPlayers) {
		List<CardData> deckData = isPlayers ? playerDeck : opponentDeck;
		foreach (CardData data in deckData) {
			Card card = data.InstantiateAsCard();
			deck.AddCard(card);
		}
	}

	public void SetMoney(Wallet wallet, bool isPlayers) {
		Money startMoney = isPlayers ? playerStartMoney : opponentStartMoney;
		wallet.Deposit(startMoney);
	}
}
