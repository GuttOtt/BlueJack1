using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardDescriptionManager : Singleton<CardDescriptionManager> {
	public bool isPanelOn;
	private GameObject panel;
	private Text cardNameText, cardDescriptionText;
	private Vector2 cardSize;

	protected void Start() {
		Instance.panel = Instance.transform.Find("Panel").gameObject;

		Instance.cardNameText = Instance.panel.transform.
								Find("Card Name Text").GetComponent<Text>();
		Instance.cardDescriptionText = Instance.panel.transform.
							Find("Card Description Text").GetComponent<Text>();

        Sprite cardSprite = Resources.Load<Sprite>("Sprites/BlankCard");
        cardSize = cardSprite.bounds.size;

        ClosePanel();
	}

	private void AdjustPanelPosition() {
		RectTransform panelRect = Instance.panel.GetComponent<RectTransform>();
		Vector2 panelPos = panelRect.position;
		Vector2 panelSize = panelRect.sizeDelta * panelRect.lossyScale;
		float xPanelMax = panelPos.x + panelSize.x / 2;
		float yPanelMin = panelPos.y - panelSize.y / 2;

		RectTransform cameraRect = Camera.main.GetComponent<RectTransform>();

		if (yPanelMin < 0) {
			panelRect.position -= Vector3.up * yPanelMin;
		}
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

        panelRect.position += new Vector3(panelSize.x, -panelSize.y, 0) / 2f;
		panelRect.anchoredPosition += new Vector2(Instance.cardSize.x, 0) / 2f;

		Debug.Log(Instance.cardSize.x / 2f);

		Instance.AdjustPanelPosition();
	}

	public static void ClosePanel() {
		Instance.panel.gameObject.SetActive(false);
	}

	public static bool IsPanelOn() {
		return Instance.panel.activeSelf;
	}
}