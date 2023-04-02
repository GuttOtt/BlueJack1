using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteManipulation: MonoBehaviour, ICardManipulation {
	[SerializeField] Button deleteButton;
	private CardData data;
	private DeckManipulationUI deckManipulation;

	private void Awake() {
		deleteButton.gameObject.SetActive(true);
		deleteButton.onClick.AddListener(Delete);
		deleteButton.gameObject.SetActive(false);
	}

	private void Delete() {
		GameManager.playerDeck.Remove(data);
		GetComponent<DeckManipulationUI>().EndManipulation();
		deleteButton.gameObject.SetActive(false);
	}

	public void StartManipulation(CardData data) {
		this.data = data;
		deleteButton.gameObject.SetActive(true);
	}
}
