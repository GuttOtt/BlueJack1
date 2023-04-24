using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCardPanel : MonoBehaviour {
	[SerializeField] private Text nameText, descText;
	[SerializeField] private CardImage image;
	private CardData data;
	private GameObject panel;
	public CardData Data { get => data; }

	private void Awake() {
		panel = transform.Find("Panel").gameObject;
	}

	public void ClosePanel() {
		panel.SetActive(false);
	}

	public void OpenPanel() {
		panel.SetActive(true);
	}

	public void OpenPanel(CardData data) {
		panel.SetActive(true);
		SetCardData(data);
	}

	public void SetCardData(CardData data) {
		this.data = data;
		image.Draw(data);

		nameText.text = image.Name;
		descText.text = image.Desc;
	}
}
