using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckListDisplay : MonoBehaviour {
    [SerializeField] private Vector2 boundGap;
    [SerializeField] private Vector2 gap;
    [SerializeField] private Vector2 cardSize = new Vector2(160, 220);
    [SerializeField] private Button closeButton;
    [SerializeField] private int numberOfColumn = 7;
    [SerializeField] private GameObject panel;
    private List<CardImage> cards = new List<CardImage>();
    private CardImage cardImagePrefab;
    public List<CardImage> GetCards { get => cards.ToList(); }

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
        float cardWidth = cardSize.x;
        float cardHeight = cardSize.y;

        for (int i = 0; i < cards.Count; i++) {
            int x = i % numberOfColumn;
            int y = i / numberOfColumn;

            cards[i].transform.localPosition = 
                boundGap + new Vector2((cardWidth + gap.x) * x, -(cardHeight + gap.y) * y);
        }
    }

    public void DrawDeckList(List<CardData> deckData) {
        panel.SetActive(true);
        ClearCards();

        foreach (CardData data in deckData) {
            GameObject cardObject = new GameObject("Card Image");
            cardObject.transform.SetParent(panel.transform);
            CardImage card = cardObject.AddComponent<CardImage>();
            card.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            card.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            card.Draw(data);
            card.ImageSize = cardSize;

            cards.Add(card);
        }

        cards = ExtensionsClass.SortAscending(cards);
        Arrange();
    }
    public void ClosePanel() {
        panel.SetActive(false);
    }

    public List<CardImage> GetAllCardImage() {
        return cards;
    }
}
