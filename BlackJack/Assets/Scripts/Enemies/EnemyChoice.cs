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
    private CardIcon noneIcon;
    private EnemyDataDisplayManager enemyDisplay;
    private ChooseEnemySceneManager sceneManager;

    [SerializeField] EnemyArchetype testEnemyData;

    private void Awake() {
        noneIcon = Resources.Load<CardIcon>("CardIcons/None Icon");
        enemyDisplay = FindObjectOfType<EnemyDataDisplayManager>();
        enemyData = EnemyData.CreateInstance<EnemyData>();

        //For debug
        Initialize(testEnemyData, 1);
    }

    private List<CardData> SetDeck(CardIcon[] cardIcons) {
        List<CardData> deckData = new List<CardData>();

        //Set Vanilla Cards, 1~7 + 7
        for (int j = 0; j < 2; j++)
        for (int i = 1; i <= 7; i++) {
            CardData data = new CardData(i, (Suit) Random.Range(0, 4), noneIcon);
            deckData.Add(data);
        }
        for (int i = 0; i < 2; i++) {
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

        return deckData;
    }
    public void Initialize(EnemyArchetype archetype, int difficulty) {
        enemyData.enemyName = archetype.enemyName;
        enemyData.portrait = archetype.portrait;
        enemyData.hp = new HP(archetype.hpAmountForDifficulty[difficulty]);
        enemyData.deckData = SetDeck(archetype.iconsForDifficulty[difficulty]);
    }

    public void DisplayEnemyData() {
        enemyDisplay.DrawEnemyData(enemyData);
    }

    public void OnPointerClick(PointerEventData eventData) {
        DisplayEnemyData();
    }
}
