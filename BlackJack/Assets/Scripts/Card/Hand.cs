using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	[SerializeField] private GameObject handParent;
	[SerializeField] private GameObject hiddenParent;
	[SerializeField] private bool isPlayerHand;
	private Discards discards;
	private List<Card> cards = new List<Card>(); 
	private List<Card> hiddens = new List<Card>();

	private void Awake() {
		discards = GetComponent<Discards>();
	}

	public void AddCard(Card card) {
		cards.Add(card);
		card.transform.SetParent(handParent.transform);
		card.MoveTo(handParent.transform.position + Vector3.right * (cards.Count - hiddens.Count -1) * 2);
		card.IsFront = true;
		card.ActivateIcon(EffectCondition.OnHit);
	}

	public void AddHidden(Card card) {
		cards.Add(card);
		hiddens.Add(card);
		card.transform.SetParent(hiddenParent.transform);
		card.MoveTo(hiddenParent.transform.position + Vector3.right * (hiddens.Count -1) * 2);

		if (isPlayerHand) {
			card.IsFront = true;
		}
		else {
			card.IsFront = false;
		}
	}

	public int GetTotal() {
		int total = 0;
		foreach (Card card in cards) {
			total += card.GetNumber();
		}
		return total;
	}

	public bool IsBursted() {
		if (GetTotal() > 21)
			return true;
		return false;
	}

	public void DiscardAll() {
		for (int i = cards.Count - 1; i >= 0; i--) {
			Card card = cards[i];
			Discard(card);
		}
	}

	public void Discard(Card card) {
		discards.AddCard(card);
		cards.Remove(card);
		if (hiddens.Contains(card)) {
			hiddens.Remove(card);
		}
	}

	public void OpenHiddens() {
		foreach (Card card in hiddens) {
			card.IsFront = true;
		}
	}

	public void ActivateAllIcon(EffectCondition condition) {
		foreach (Card card in cards) {
			card.ActivateIcon(condition);
		}
	}
}
