using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClient : MonoBehaviour {
	[SerializeField] private Gambler player;
	[SerializeField] private Gambler opponent;
	[SerializeField] private TurnSystem turnSystem;
	[SerializeField] private List<CardIcon> testIcons = new List<CardIcon>();
	[SerializeField] private CardIcon noneIcon;

	private void Start() {
		player.GetComponent<Wallet>().Deposit(Money.wons(500));
		opponent.GetComponent<Wallet>().Deposit(Money.wons(500));

		//SetBasicDeck(player);
		SetBasicDeck(opponent);

		for (int i = 0; i < 7; i++) {
			AddIconCard(player, testIcons[0]);
		}

		turnSystem.ToStartPhase();
	}

	private void SetBasicDeck(Gambler gambler) {
		Deck deck = gambler.GetComponent<Deck>();
		for (int j = 0; j < 2; j++) {
			for (int i = 1; i <= 7; i++) {
				Card card = CardGenerator.CreateCard(i, (Suit) j, noneIcon);
				card.owner = gambler;
				deck.AddCard(card);
			}
		}
		deck.Shuffle();
	}

	private void AddIconCard(Gambler gambler, CardIcon iconPrefab) {
		Deck deck = gambler.GetComponent<Deck>();

		Card card = CardGenerator.CreateCard(Random.Range(7, 8), (Suit) Random.Range(0, 4), iconPrefab);
		deck.AddCard(card);
		card.owner = gambler;

		deck.Shuffle();
	}
}
