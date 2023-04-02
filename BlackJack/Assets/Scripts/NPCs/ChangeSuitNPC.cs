using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeSuitNPC : MonoBehaviour, INPC, IPointerClickHandler {
    private DeckManipulationUI manipulationUI;

    private void Awake() {
        manipulationUI = FindObjectOfType<DeckManipulationUI>() as DeckManipulationUI;
    }

    public void OnPointerClick(PointerEventData eventData){
        Interact();
    }

    public void Interact() {
        manipulationUI.StartChangeSuit();
    }
}
