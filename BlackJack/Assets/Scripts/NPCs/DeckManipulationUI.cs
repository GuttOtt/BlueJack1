using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckManipulationUI : MonoBehaviour {
	[SerializeField] private CardImage cardPrefab;
	[SerializeField] private Vector2 gap;
	[SerializeField] private SelectedCardPanel selectedCardPanel;
	private List<CardImage> cards = new List<CardImage>();
	private ICardManipulation delete, changeNumber, changeSuit, none;
	private ICardManipulation currentManipulation;
	private GameObject deckListPanel;
	private CardImage selectedCard;

	private void Awake() {
		delete = GetComponent<DeleteManipulation>();
		changeNumber = GetComponent<NumberManipulation>();
		changeSuit = GetComponent<SuitManipulation>();
		none = gameObject.AddComponent<NoneManipulation>();

		deckListPanel = transform.Find("Panel").gameObject;
	}

	private void Start() {
		deckListPanel.SetActive(false);
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
		deckListPanel.SetActive(true);
		UpdateDeckList();
	}

	public void StartChangeSuit() {
		currentManipulation = changeSuit;
		deckListPanel.SetActive(true);
		UpdateDeckList();
	}

	public void StartDelete() {
		currentManipulation = delete;
		deckListPanel.SetActive(true);
		UpdateDeckList();
	}

	public void EndManipulation() {
		currentManipulation = none;
		selectedCardPanel.ClosePanel();
		deckListPanel.SetActive(false);
		//NextButton?
	}

	public void UpdateDeckList() {
		ClearDeckList();

		List<CardData> deck = GameManager.playerDeck;

		foreach (CardData data in deck) {
			CardImage newCard = Instantiate(cardPrefab, deckListPanel.transform);
			newCard.Draw(data);
			newCard.onClick = null;
			newCard.onClick += () => Select(newCard);
			cards.Add(newCard);
		}

		Arrange();
	}

	public void Arrange() {
		cards = ExtensionsClass.SortAscending(cards);

		Rect rect = cardPrefab.GetComponent<RectTransform>().rect;
		float cardWidth = rect.width;
		float cardHeight = rect.height;

		for (int i = 0; i < cards.Count; i++) {
			int x = i % 7;
			int y = i / 7;

			cards[i].GetComponent<RectTransform>().localPosition = new Vector2((cardWidth + gap.x) * x, -(cardHeight + gap.y) * y)
																	+ new Vector2(cardWidth / 2f, -cardHeight / 2f) + gap;
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