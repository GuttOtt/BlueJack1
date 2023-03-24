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

	protected override void Awake() {
		SceneManager.sceneLoaded += OnSceneLoaded;
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

	private static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		FindUIComponents();
		Instance.panel.SetActive(false);
	}

	private static void FindUIComponents() {
		Instance.panel = GameObject.Find("Card Description Panel");
		Instance.cardImage = Instance.panel.transform.
							Find("Card Image").GetComponent<CopyCardImage>();
		Instance.cardNameText = Instance.panel.transform.
								Find("Card Name Text").GetComponent<Text>();
		Instance.cardDescriptionText = Instance.panel.transform.
							Find("Card Description Text").GetComponent<Text>();
		Debug.Log("FindUIComponents");
	}
}