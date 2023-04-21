using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDataDisplayManager : MonoBehaviour {
    [SerializeField] DeckListDisplay deckListDisplay;
    [SerializeField] Text nameText;
    [SerializeField] Image portraitImage;
    [SerializeField] Text hpText;
    private GameObject panel;

    private void Awake() {
        panel = transform.Find("Panel").gameObject;
        panel.SetActive(false);
    }

    public void DrawEnemyData(string enemyName, Sprite portrait
        , HP hp, List<CardData> deckData) {
        panel.SetActive(true);
        deckListDisplay.gameObject.SetActive(true);

        nameText.text = enemyName;
        portraitImage.sprite = portrait;
        hpText.text = hp.ToString();
        deckListDisplay.DrawDeckList(deckData);
    }

    public void ClosePanel() {
        panel.SetActive(true);
        deckListDisplay.ClosePanel();
    }
}