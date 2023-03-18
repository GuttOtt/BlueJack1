using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	[SerializeField] private GameObject handParent;
	[SerializeField] private GameObject hiddenParent;
	[SerializeField] private bool isPlayerHand;
	private Discards discards;
	private List<Card> cards {
		get {
			List<Card> cards = new List<Card>();
			cards.AddRange(field);
			cards.AddRange(hiddens);
			return cards;
		}
	}
	private List<Card> hiddens = new List<Card>();
	private List<Card> field = new List<Card>();

	private void Awake() {
		discards = GetComponent<Discards>();
	}

	public void AddCard(Card card) {
		field.Add(card);
		card.transform.SetParent(handParent.transform);
		card.MoveTo(handParent.transform.position + Vector3.right * (field.Count) * 2);
		card.IsFront = true;
		card.ActivateIcon(EffectCondition.OnHit);
	}

	public void AddHidden(Card card) {
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
		for (int i = field.Count - 1; i >= 0; i--) {
			Card card = field[i];
			Discard(card);
		}
		
		for (int i = hiddens.Count - 1; i >= 0; i--) {
			Card card = hiddens[i];
			Discard(card);
		}
	}

	public void Discard(Card card) {
		discards.AddCard(card);
		if (field.Contains(card)) {
			field.Remove(card);
		}
		else if (hiddens.Contains(card)) {
			hiddens.Remove(card);
		}
	}

	public void OpenHiddens() {
		foreach (Card card in hiddens) {
			card.IsFront = true;
		}
	}

	public IEnumerator ActivateAllIcon(EffectCondition condition) {
		foreach (Card card in cards) {
			if (card.ActivateIcon(condition))
				yield return new WaitForSeconds(0.5f);
		}
	}

	public IEnumerator ActivateAllField(EffectCondition condition) {
		foreach (Card card in field) {
			if (card.ActivateIcon(condition))
				yield return new WaitForSeconds(0.5f);
		}
	}

	public IEnumerator ActivateAllHidden(EffectCondition condition) {
		foreach (Card card in hiddens) {
			if (card.ActivateIcon(condition))
				yield return new WaitForSeconds(0.5f);
		}
	}

	public Card GetRandomField() {
		return field[Random.Range(0, field.Count)];
	}

	//field 에서 인자를 제외한 나머지 중 랜덤
	public Card GetRandomField(Card card) {
		List<Card> fieldExcept = field.ToList();
		fieldExcept.Remove(card);
		if (fieldExcept.Count == 0)
			return card;
		return fieldExcept[Random.Range(0, fieldExcept.Count)];
	}

	public int GetNumberOfField() {
		return field.Count;
	}
}
