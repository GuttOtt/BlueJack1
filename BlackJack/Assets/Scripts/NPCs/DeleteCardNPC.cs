using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteCardNPC : MonoBehaviour, INPC, IPointerClickHandler {
    private DeckManipulationUI manipulationUI;

    private void Awake() {
        manipulationUI = FindObjectOfType<DeckManipulationUI>() as DeckManipulationUI;
    }

    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("Clicked");
        Interact();
    }

    public void Interact() {
        manipulationUI.StartDelete();
    }
}
