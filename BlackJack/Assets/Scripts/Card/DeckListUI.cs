using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckListUI : Singleton<DeckListUI> {
	private GameObject _panel;
	private CopyCardImage _imagePrefab;//Includes CardImageControl Component.
	private List<CopyCardImage> _cards = new List<CopyCardImage>();
	private static GameObject panel { get => Instance._panel; set => Instance._panel = value; }
	private static CopyCardImage imagePrefab { get => Instance._imagePrefab; set => Instance._imagePrefab = value; }
	private static List<CopyCardImage> cards { get => Instance._cards; set => Instance._cards = value; }

	protected void Start() {
		imagePrefab = Resources.Load<CopyCardImage>("Copy Card Image Prefab");
		panel = Instance.transform.Find("Panel").gameObject;
		ClosePanel();
	}

	public static void DrawDeckList(List<CardData> deckData) {
		panel.gameObject.SetActive(true);
		cards.Clear();
		foreach (CardData data in deckData) {
			CopyCardImage card = Instantiate(imagePrefab, panel.transform);
			data.PasteTo(card);
			cards.Add(card);
		}

		Instance.SortAscending();
		Instance.Arrange();
	}

	private void Arrange() {
		for (int i = 0; i < cards.Count; i++) {
			int x = i % 8;
			int y = i / 8;

			cards[i].transform.localPosition = new Vector2(x * 90f, -y * 110f) + new Vector2(-400f, 300f) + new Vector2(45f + 5f, -55f - 5f);
		}
	}

	private void SortAscending() {
		CopyCardImage[] arranged = cards.ToArray();

		//Selection Sort
		for (int i = 0; i < arranged.Length - 1; i++) {
			int index = i;
			for (int j = i + 1; j < arranged.Length; j++) {
				if (arranged[j].GetData().number < arranged[index].GetData().number) {
					index = j;
				}
			}
			if (index != i) {
				CopyCardImage temp = arranged[i];
				arranged[i] = arranged[index];
				arranged[index] = temp;
			}
		}

		cards = arranged.ToList();
	}

	public static void ClosePanel() {
		panel.gameObject.SetActive(false);
		foreach(CopyCardImage card in cards) {
			Destroy(card.gameObject);
		}
		cards.Clear();
	}
}
