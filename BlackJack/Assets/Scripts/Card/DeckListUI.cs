using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckListUI : Singleton<DeckListUI> {
	[SerializeField] private Vector2 _gap;
	private GameObject _panel;
	private CopyCardImage _imagePrefab;//Includes CardImageControl Component.
	private List<CopyCardImage> _cards = new List<CopyCardImage>();
	private static bool isTurnedOn = false;
	private static GameObject panel { get => Instance._panel; set => Instance._panel = value; }
	private static CopyCardImage imagePrefab { get => Instance._imagePrefab; set => Instance._imagePrefab = value; }
	private static List<CopyCardImage> cards { get => Instance._cards; set => Instance._cards = value; }
	private static Vector2 gap { get => Instance._gap; }
	public static bool IsTurnedOn { get => isTurnedOn; }

	protected void Start() {
		imagePrefab = Resources.Load<CopyCardImage>("Copy Card Image Prefab");
		panel = Instance.transform.Find("Panel").gameObject;
		panel.transform.Find("Close Button").GetComponent<Button>().onClick.AddListener(ClosePanel);
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
		isTurnedOn = true;
	}

	private void Arrange() {
		Rect rect = imagePrefab.GetComponent<RectTransform>().rect;
		float cardWidth = rect.width;
		float cardHeight = rect.height;

		for (int i = 0; i < cards.Count; i++) {
			int x = i % 7;
			int y = i / 7;

			cards[i].transform.localPosition = new Vector2((cardWidth + gap.x) * x, -(cardHeight + gap.y) * y);
		}
	}

	private void SortAscending() {
		CopyCardImage[] sorted = cards.ToArray();

		//Selection Sort
		for (int i = 0; i < sorted.Length - 1; i++) {
			int index = i;
			for (int j = i + 1; j < sorted.Length; j++) {
				if (sorted[j].GetData().number < sorted[index].GetData().number) {
					index = j;
				}
			}
			if (index != i) {
				CopyCardImage temp = sorted[i];
				sorted[i] = sorted[index];
				sorted[index] = temp;
			}
		}

		cards = sorted.ToList();
	}

	public static void ClosePanel() {
		panel.gameObject.SetActive(false);
		foreach(CopyCardImage card in cards) {
			Destroy(card.gameObject);
		}
		cards.Clear();
		isTurnedOn = false;
	}
}
