using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardDescriptor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Card card;
    private bool IsFront { get => card ? card.IsFront : false; }
    private bool isCursorOn = false;

    private void Awake() {
        card = GetComponent<Card>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!IsFront) return;
        isCursorOn = true;
        StartCoroutine(DrawDescription());

    }

    public void OnPointerExit(PointerEventData eventData) {
        isCursorOn = false;
        StartCoroutine(CloseDescription());
    }

    private IEnumerator DrawDescription() {
        if (!CardDescriptionManager.Instance.isPanelOn) {
            yield return new WaitForSeconds(0.3f);
            if (!isCursorOn) yield break;
        }

        CardDescriptionManager.DrawDescription(card.GetData(), card.transform.position);
        card.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1f);
    }

    private IEnumerator CloseDescription() {
        CardDescriptionManager.ClosePanel();
        card.GetComponent<SpriteRenderer>().color = Color.white;

        //카드 설명을 보다가 바로 옆에 놓인 카드로 커서를 옮겼을 때,
        //딜레이 없이 바로 Description을 띄우기 위함.
        yield return new WaitForSeconds(1f);

        if (!CardDescriptionManager.IsPanelOn()) {
            CardDescriptionManager.Instance.isPanelOn = false;
        }
    }
}
