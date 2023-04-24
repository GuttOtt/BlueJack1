using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckManipulationUI : MonoBehaviour {
	[SerializeField] private DeckListDisplay deckListDisplay;
	[SerializeField] private Vector2 gap;
	[SerializeField] private SelectedCardPanel selectedCardPanel;
	private List<CardImage> cards = new List<CardImage>();
	private ICardManipulation delete, changeNumber, changeSuit, none;
	private ICardManipulation currentManipulation;
	private CardImage selectedCard;

	private void Awake() {
		delete = GetComponent<DeleteManipulation>();
		changeNumber = GetComponent<NumberManipulation>();
		changeSuit = GetComponent<SuitManipulation>();
		none = gameObject.AddComponent<NoneManipulation>();
	}

	private void Start() {
		deckListDisplay.gameObject.SetActive(false);
		selectedCardPanel.ClosePanel();

	}

	private void ClearDeckList() {
		foreach (CardImage card in cards) {
			Destroy(card.gameObject);
		}

		cards.Clear();
	}

	private void Select(CardImage card) {
		selectedCard = card;
		selectedCardPanel.OpenPanel(card.GetData);
		currentManipulation.StartManipulation(card.GetData);
	}

	public void StartChangeNumber() {
		currentManipulation = changeNumber;
		deckListDisplay.gameObject.SetActive(true);
		UpdateDeckList();
	}

	public void StartChangeSuit() {
		currentManipulation = changeSuit;
		deckListDisplay.gameObject.SetActive(true);
		UpdateDeckList();
	}

	public void StartDelete() {
		currentManipulation = delete;
		deckListDisplay.gameObject.SetActive(true);
		UpdateDeckList();
	}

	public void EndManipulation() {
		currentManipulation = none;
		selectedCardPanel.ClosePanel();
		deckListDisplay.gameObject.SetActive(false);
		//NextButton?
	}

	public void UpdateDeckList() {
		deckListDisplay.ClosePanel();
		deckListDisplay.DrawDeckList(GameManager.playerDeck);
		List<CardImage> cards = deckListDisplay.GetCards;

		foreach (CardImage card in cards) {
			card.onClick = null;
			card.onClick += () => Select(card);
		}
	}
}

public interface ICardManipulation {
	void StartManipulation(CardData data);
}

public class NoneManipulation: MonoBehaviour, ICardManipulation {
	public void StartManipulation(CardData data) {
		//Empty method.
	}
}