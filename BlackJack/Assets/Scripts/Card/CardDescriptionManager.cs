using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardDescriptionManager : Singleton<CardDescriptionManager> {
	[SerializeField] private Vector2 cardSize;
	public bool isPanelOn;
	private GameObject panel;
	private Text cardNameText, cardDescriptionText;

	protected void Start() {
		Instance.panel = Instance.transform.Find("Panel").gameObject;

		Instance.cardNameText = Instance.panel.transform.
								Find("Card Name Text").GetComponent<Text>();
		Instance.cardDescriptionText = Instance.panel.transform.
							Find("Card Description Text").GetComponent<Text>();

		ClosePanel();
	}

	public static void DrawDescription(CardData data) {
		Instance.panel.gameObject.SetActive(true);
		Instance.cardNameText.text = CardIconCSV.GetNameByID(data.iconPrefab.ID);
		Instance.cardDescriptionText.text = CardIconCSV.GetDescByID(data.iconPrefab.ID);

        Instance.isPanelOn = true;
    }

	public static void DrawDescription(CardData data, Vector2 cardPosition) {
		DrawDescription(data);

		Vector2 cardScreenPos = Camera.main.WorldToScreenPoint(cardPosition);
		RectTransform panelRect = Instance.panel.GetComponent<RectTransform>();
		panelRect.position = cardScreenPos;

        Vector2 panelSize = panelRect.sizeDelta * panelRect.lossyScale;
		panelRect.position += new Vector3(panelSize.x, -panelSize.y, 0) / 2;
		panelRect.position += new Vector3(Instance.cardSize.x, 0) / 2;
	}

	public static void ClosePanel() {
		Instance.panel.gameObject.SetActive(false);
	}

	public static bool IsPanelOn() {
		return Instance.panel.activeSelf;
	}
}