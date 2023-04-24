using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckListDisplay : MonoBehaviour {
    [SerializeField] private Vector2 boundGap;
    [SerializeField] private Vector2 gap;
    [SerializeField] private Vector2 cardSize = new Vector2(160, 220);
    [SerializeField] private CardImage cardImagePrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private int numberOfColumn = 7;
    [SerializeField] private GameObject panel;
    private List<CardImage> cards = new List<CardImage>();

    private void Awake() {
        panel = transform.Find("Panel").gameObject;
        if (closeButton) closeButton.onClick.AddListener(ClosePanel);
        ClosePanel();
    }


    private void ClearCards() {
        foreach (CardImage card in cards) {
            Destroy(card.gameObject);
        }
        cards.Clear();
    }

    private void Arrange() {
        Rect rect = cardImagePrefab.GetComponent<RectTransform>().rect;
        float cardWidth = rect.width;
        float cardHeight = rect.height;

        for (int i = 0; i < cards.Count; i++) {
            int x = i % numberOfColumn;
            int y = i / numberOfColumn;

            cards[i].transform.localPosition = 
                boundGap + new Vector2((cardWidth + gap.x) * x, -(cardHeight + gap.y) * y);
        }
    }

    public void DrawDeckList(List<CardData> deckData) {
        panel.gameObject.SetActive(true);
        ClearCards();

        foreach (CardData data in deckData) {
            CardImage card = Instantiate(cardImagePrefab, panel.transform);
            card.Draw(data);
            card.GetComponent<RectTransform>().sizeDelta = cardSize;

            cards.Add(card);
        }

        ExtensionsClass.SortAscending(cards);
        Arrange();
    }
    public void ClosePanel() {
        panel.SetActive(false);
    }
}
