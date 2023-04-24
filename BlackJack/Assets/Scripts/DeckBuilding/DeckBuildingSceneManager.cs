using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildingSceneManager : MonoBehaviour {
    [SerializeField] AcquireCardManager acquireCardManager;
    [SerializeField] GameObject deckManipulationParent;
    [SerializeField] Button[] deckManipulationButtons = new Button[3];
    [SerializeField] Button toNextSceneButton;

    private void Start() {
        deckManipulationParent.SetActive(false);
        acquireCardManager.StartDeckBuilding(3);

        toNextSceneButton.onClick.AddListener(EndDeckBuildingScene);
    }

    public void StartDeckManipulation() {
        deckManipulationParent.SetActive(true);
    }

    public void EndDeckBuildingScene() {
        GameManager.ToChooseEnemyScene();
    }
}
