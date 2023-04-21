using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyChoice : MonoBehaviour, IPointerClickHandler { 
    private EnemyData enemyData;
    private string enemyName;
    private Sprite portrait;
    private HP hp;
    private List<CardData> deckData = new List<CardData>();
    private CardIcon noneIcon;
    private EnemyDataDisplayManager enemyDisplay;

    [SerializeField] EnemyData testEnemyData;

    private void Awake() {
        noneIcon = Resources.Load<CardIcon>("CardIcons/None Icon");
        enemyDisplay = FindObjectOfType<EnemyDataDisplayManager>();

        //For debug
        Initialize(testEnemyData, 1);
    }

    private void SetDeck(CardIcon[] cardIcons) {
        deckData = new List<CardData>();

        //Set Vanilla Cards, 1~7 + 7
        for (int i = 1; i <= 7; i++) {
            CardData data = new CardData(i, (Suit) Random.Range(0, 4), noneIcon);
            deckData.Add(data);
        }
        for (int i = 0; i < 4; i++) {
            CardData data = new CardData(7, (Suit)Random.Range(0, 4), noneIcon);
            deckData.Add(data);
        }

        //Attach Icons To Random Cards
        List<CardData> tempDeck = deckData.ToList();
        foreach (CardIcon icon in cardIcons) {
            CardData randomCardData = tempDeck[Random.Range(0, tempDeck.Count - 1)];
            randomCardData.ChangeIcon(icon);
            tempDeck.Remove(randomCardData);
        }
    }
    public void Initialize(EnemyData data, int difficulty) {
        this.enemyName = data.enemyName;
        this.portrait = data.portrait;
        this.hp = new HP(data.hpAmountForDifficulty[difficulty]);
        SetDeck(data.iconsForDifficulty[difficulty]);
    }

    public void DisplayEnemyData() {
        enemyDisplay.DrawEnemyData(enemyName, portrait, hp, deckData);
    }

    public void OnPointerClick(PointerEventData eventData) {
        DisplayEnemyData();
        Debug.Log("Clicked");
    }
}
