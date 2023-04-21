using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildingSceneManager : Singleton<DeckBuildingSceneManager> {
    [SerializeField] AcquireCardManager acquireCardManager;
    [SerializeField] GameObject deckManipulationObejct;
    [SerializeField] Button[] deckManipulationButtons = new Button[3];

    private void Start() {
        deckManipulationObejct.SetActive(false);
        acquireCardManager.StartDeckBuilding(3);
    }

    public void StartDeckManipulation() {
        deckManipulationObejct.SetActive(true);
    }

    public void EndDeckBuildingScene() {

    }
}
