using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDescriptionManager : Singleton<CardDescriptionManager> {
	[SerializeField] private GameObject panel;
	[SerializeField] private CopyCardImage cardImage;
	[SerializeField] private Text cardNameText, cardDescriptionText;

	protected override void Awake() {
		panel.SetActive(false);
	}

	private void Update() {

	}

	public static void DrawDescription(CardData data) {
		Instance.panel.SetActive(true);
		data.PasteTo(Instance.cardImage);
		Instance.cardNameText.text = CardIconCSV.GetNameByID(data.iconPrefab.ID);
		Instance.cardDescriptionText.text = CardIconCSV.GetDescByID(data.iconPrefab.ID);
	}

	public static void ClosePanel() {
		Instance.panel.SetActive(false);
	}
}