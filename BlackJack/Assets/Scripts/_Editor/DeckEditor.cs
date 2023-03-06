using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckEditor : MonoBehaviour {
	[SerializeField] private List<EditingCard> deck;
	[SerializeField] private Button addButton, deleteButton, editButton;
	[SerializeField] private float cardWidth, cardHeight;
	[SerializeField] private Vector2 cardOrigin;
	private CardEditor cardEditor;
	private EditingCard selectedCard;

	private void Awake() {
		cardEditor = GetComponent<CardEditor>();
		addButton.onClick.AddListener(AddNewCard);
		deleteButton.onClick.AddListener(DeleteSelectedCard);
		editButton.onClick.AddListener(delegate {cardEditor.StartEditing(selectedCard);});
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
			int column = deck.Count - i * 7;
			column = 7 < column ? 7 : column;
			for (int j = 0; j < column; j++) {
				EditingCard card = deck[i*7 + j];
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
}
