using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour {
	public static CardIcon[] cardIconPrefabs = Resources.LoadAll<CardIcon>("CardIcons");

	public static CardIcon GetRandomIconPrefab() {
		int r = Random.Range(0, cardIconPrefabs.Length);
		return cardIconPrefabs[r];
	}

	public static Card CreateCard(int number, Suit suit, CardIcon iconPrefab) {
		GameObject obj = new GameObject("Card");
		Card card = obj.AddComponent<Card>() as Card;
		card.AddIcon(iconPrefab);
		card.Initialize(number, suit);

		return card;
	}

	public static CardData RandomCardData() {
		int number = Random.Range(1, 8);
		Suit suit = (Suit) Random.Range(0, 4);
		CardIcon iconPrefab = cardIconPrefabs[Random.Range(0, cardIconPrefabs.Length)];

		return new CardData(number, suit, iconPrefab);
	}
}
