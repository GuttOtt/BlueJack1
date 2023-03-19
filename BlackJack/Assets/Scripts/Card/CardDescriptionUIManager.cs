using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionUIManager : MonoBehaviour {
	[SerializeField] private GameObject panel;
	[SerializeField] private CopyCardImage cardImage;
	[SerializeField] private Text cardNameText, cardDescriptionText;
	private Card presentCard;

	private void Awake() {
		panel.SetActive(false);
		presentCard = null;
	}

	private void Update() {
		Card card = CardOnCursor();

		if (presentCard) {
			if (!card || presentCard != card) {
				presentCard = null;
				panel.SetActive(false);
				return;
			}
		}

		if (card && card.IsFront && card.IconID != 0) {
			presentCard = card;
			panel.SetActive(true);
			DrawImage(card.GetData());
			DrawIconName(card.IconID);
			DrawIconDesc(card.IconID);
		}
	}

	private Card CardOnCursor() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (!hit.collider || !hit.collider.GetComponent<Card>()) {
			return null;
		}
		Card card = hit.collider.GetComponent<Card>();
		return card;
	}

	private void DrawImage(CardData data) {
		data.PasteTo(cardImage);
	}

	private void DrawIconName(int iconID) {
		cardNameText.text = CardIconCSV.GetNameByID(iconID);
	}

	private void DrawIconDesc(int iconID) {
		cardDescriptionText.text = CardIconCSV.GetDescByID(iconID);
	}
}