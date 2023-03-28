using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckManipulationUI : MonoBehaviour {
	[SerializeField] private ClickableCardImage cardPrefab;
	[SerializeField] private Vector2 gap;
	private List<ClickableCardImage> cards = new List<ClickableCardImage>();
	private ICardManipulation delete, changeNumber, changeSuit, none;
	private ICardManipulation currentManipulation;
	private GameObject deckListPanel;
	//private SelectedCardPanel selectedCardPanel;
	private ClickableCardImage selectedCard;

	private void Awake() {
		delete = gameObject.AddComponent<DeleteManipulation>();
		changeNumber = gameObject.AddComponent<ChangeNumberManipulation>();
		changeSuit = gameObject.AddComponent<ChangeSuitManipulation>();
		none = gameObject.AddComponent<NoneManipulation>();

		deckListPanel = transform.Find("Panel").gameObject;
		deckListPanel.SetActive(false);
	}

	private void Manipulate() {
		currentManipulation.Manipulate(selectedCard.Data);
		UpdateDeckList();
		//Coroutine Delay
		EndManipulation();
	}

	private void ClearDeckList() {
		foreach (ClickableCardImage card in cards) {
			Destroy(card.gameObject);
		}

		cards.Clear();
	}

	private void Select(ClickableCardImage card) {
		selectedCard = card;
		OpenSelectedCardPanel(card.Data);
	}

	private void OpenSelectedCardPanel(CardData data) {
		//selectedCardPanel.gameObject.SetActive(true);
		//selectedCardPanel.SetCardData(data);
		//selectedCardPanel.SetButtonAction(0, delegate { Manipulate(); });

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
		deckListPanel.SetActive(false);
		//selectedCardPanel.Close();
	}

	public void UpdateDeckList() {
		ClearDeckList();

		List<CardData> deck = GameManager.playerDeck;

		foreach (CardData data in deck) {
			ClickableCardImage newCard = Instantiate(cardPrefab, deckListPanel.transform);
			newCard.Draw(data);
			newCard.onClick = null;
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

			cards[i].transform.localPosition = new Vector2((cardWidth + gap.x) * x, -(cardHeight + gap.y) * y);
		}
	}
}

public interface ICardManipulation {
	void Manipulate(CardData data);
}

public class DeleteManipulation: MonoBehaviour, ICardManipulation {
	public void Manipulate(CardData data) {
		GameManager.RemovePlayerDeck(data);
		GetComponent<DeckManipulationUI>().UpdateDeckList();
	}
}

public class ChangeNumberManipulation: MonoBehaviour, ICardManipulation {
	public void Manipulate(CardData data) {
		data.ChangeNumber(Random.Range(1, 8));
	}
}

public class ChangeSuitManipulation: MonoBehaviour, ICardManipulation {
	public void Manipulate(CardData data) {
		data.ChangeSuit( (Suit) Random.Range(0, 4));
	}
}

public class NoneManipulation: MonoBehaviour, ICardManipulation {
	public void Manipulate(CardData data) {
		//Empty method.
	}
}