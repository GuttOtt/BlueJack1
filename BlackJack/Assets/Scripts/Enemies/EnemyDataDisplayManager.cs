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
    [SerializeField] Button selectButton, closeButton;
    private GameObject panel;
    private ChooseEnemySceneManager sceneManager;
    private EnemyData enemyData;

    private void Awake() {
        panel = transform.Find("Panel").gameObject;
        panel.SetActive(false);
        selectButton.onClick.AddListener(SelectEnemy);
        closeButton.onClick.AddListener(ClosePanel);
        
        sceneManager = FindObjectOfType<ChooseEnemySceneManager>();
    }

    public void DrawEnemyData(EnemyData enemyData) {
        panel.SetActive(true);
        deckListDisplay.gameObject.SetActive(true);

        nameText.text = enemyData.enemyName;
        portraitImage.sprite = enemyData.portrait;
        hpText.text = enemyData.hp.ToString();
        deckListDisplay.DrawDeckList(enemyData.deckData);

        this.enemyData = enemyData;
    }

    public void ClosePanel() {
        panel.SetActive(false);
        deckListDisplay.ClosePanel();
    }

    public void SelectEnemy() {
        sceneManager.EndEnemyChoiceScene(enemyData);
    }
}