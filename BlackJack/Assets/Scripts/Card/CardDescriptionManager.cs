using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardDescriptionManager : Singleton<CardDescriptionManager> {
	private GameObject panel;
	private CopyCardImage cardImage;
	private Text cardNameText, cardDescriptionText;

	protected void Start() {
		Instance.panel = Instance.transform.Find("Panel").gameObject;

		Instance.cardImage = Instance.panel.transform.
							Find("Card Image").GetComponent<CopyCardImage>();
		Instance.cardNameText = Instance.panel.transform.
								Find("Card Name Text").GetComponent<Text>();
		Instance.cardDescriptionText = Instance.panel.transform.
							Find("Card Description Text").GetComponent<Text>();

		ClosePanel();
	}

	public static void DrawDescription(CardData data) {
		Instance.panel.gameObject.SetActive(true);
		data.PasteTo(Instance.cardImage);
		Instance.cardNameText.text = CardIconCSV.GetNameByID(data.iconPrefab.ID);
		Instance.cardDescriptionText.text = CardIconCSV.GetDescByID(data.iconPrefab.ID);
	}

	public static void ClosePanel() {
		Instance.panel.gameObject.SetActive(false);
	}
}