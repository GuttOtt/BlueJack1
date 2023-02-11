using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	[SerializeField] Discards discards;
	[SerializeField] Text totalText;
	private List<Card> cards = new List<Card>();

	public int NumberOfSynergy(ICardSynergy synergy) {
		int count = 0;	

		foreach (Card card in cards) {
			if (synergy.IsSatisfiedBy(card)) {
				count++;
			}
		}

		return count;
	}

	public void AddCard(Card card) {
		cards.Add(card);
		card.transform.SetParent(this.transform);
		card.transform.localPosition = Vector2.zero + Vector2.right * cards.Count * 2;
		
		if (GetTotal() <= 21) {
			totalText.text = "гу╟Х: " + GetTotal().ToString();
		}
		else {
			FindObjectOfType<RoundManager>().EndRound(this.GetComponent<Betting>());
		}
	}

	public int GetTotal() {
		int total = 0;
		foreach (Card card in cards) {
			total += card.GetNumber();
		}
		return total;
	}

	public void DiscardAll() {
		for (int i = cards.Count - 1; i >= 0; i--) {
			discards.AddCard(cards[i]);
			cards.RemoveAt(i);
		}
	}

	public void Discard(Card card) {
		discards.AddCard(card);
		cards.Remove(card);
	}
}
