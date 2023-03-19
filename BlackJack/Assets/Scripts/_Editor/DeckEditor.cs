using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckEditor : MonoBehaviour {
	[SerializeField] private List<EditingCard> deck;
	[SerializeField] private Button addButton, deleteButton, editButton, completeButton, r1To7Button, r1To77Button, randomIconButton;
	[SerializeField] private float cardWidth, cardHeight;
	[SerializeField] private Vector2 cardOrigin;
	private CardEditor cardEditor;
	private EditingCard selectedCard;

	private void Awake() {
		cardEditor = GetComponent<CardEditor>(); 
		addButton.onClick.AddListener(AddNewCard);
		r1To7Button.onClick.AddListener(AddRandom1To7);
		r1To77Button.onClick.AddListener(AddRandom1To77);
		randomIconButton.onClick.AddListener(SetAllToRandomIcon);
		deleteButton.onClick.AddListener(DeleteSelectedCard);
		editButton.onClick.AddListener(delegate {cardEditor.StartEditing(selectedCard);});
		completeButton.onClick.AddListener(CompleteEditing);
	}

	private void Update() {
		SelectCard();
	}

	public void AddNewCard() {
		GameObject obj = new GameObject("Editing Card");
		EditingCard card = obj.AddComponent<EditingCard>() as EditingCard;
		card.Initialize(1, (Suit) 0);
		
		deck.Add(card);
		Arrange();
	}

	public void DeleteSelectedCard() {
		if (!selectedCard)
			return;

		deck.Remove(selectedCard);
		Destroy(selectedCard.gameObject);
		selectedCard = null;
		Arrange();
	}

	private void Arrange() {
		for (int i = 0; i < deck.Count; i++) {
			int column = deck.Count - i * 8;
			column =8 < column ? 8 : column;
			for (int j = 0; j < column; j++) {
				EditingCard card = deck[i*8 + j];
				card.transform.position = cardOrigin + new Vector2(j * cardWidth, -i * cardHeight);
			}
		}
	}

	private void SelectCard() {
		if (!Input.GetMouseButtonDown(0)) return;

		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward);

		if (hit) {
			if (selectedCard)
				selectedCard.GetComponent<SpriteRenderer>().color = Color.white;

			selectedCard = hit.collider.GetComponent<EditingCard>();
			selectedCard.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1f);
		}
	}

	private void CompleteEditing() {
		List<CardData> deckData = new List<CardData>();
		foreach (EditingCard card in deck) {
			deckData.Add(card.GetData());
		}

		SandboxManager.Instance.EndDeckEditing(deckData);
	}

	private void AddRandom1To7() {
		for (int i = 1; i <= 7; i++) {
			GameObject obj = new GameObject("Editing Card");
			EditingCard card = obj.AddComponent<EditingCard>() as EditingCard;
			card.Initialize(i, (Suit) Random.Range(0, 4));
			deck.Add(card);
		}
		Arrange();
	}

	private void AddRandom1To77() {
		AddRandom1To7();
		GameObject obj = new GameObject("Editing Card");
		EditingCard card = obj.AddComponent<EditingCard>() as EditingCard;
		card.Initialize(7, (Suit) Random.Range(0, 4));
		deck.Add(card);
		Arrange();
	}

	private void SetAllToRandomIcon() {
		foreach (EditingCard card in deck) {
			card.ChangeIcon(cardEditor.GetRandomIcon());
		}
	}
}
