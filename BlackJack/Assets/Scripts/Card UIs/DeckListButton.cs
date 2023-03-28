using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListButton : MonoBehaviour {
	private Button button;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => DeckListUI.DrawDeckList(GameManager.playerDeck));
	}
}
