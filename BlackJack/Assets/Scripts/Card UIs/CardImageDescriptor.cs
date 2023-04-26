using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImageDescriptor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private CardImage cardImage;
    private bool isCursorOn = true;

    private void Awake() {
        cardImage = GetComponent<CardImage>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isCursorOn = true;
        StartCoroutine(DrawDescription());
        Debug.Log("OnPointerEnter");

    }

    public void OnPointerExit(PointerEventData eventData) {
        isCursorOn = false;
        StartCoroutine(CloseDescription());
        Debug.Log("OnPointerExit");
    }

    private IEnumerator DrawDescription() {
        if (!CardDescriptionManager.Instance.isPanelOn) {
            yield return new WaitForSeconds(0.3f);
            if (!isCursorOn) yield break;
        }

        CardData data = cardImage.GetData;
        RectTransform cardImageRect = cardImage.GetComponent<RectTransform>();
        CardDescriptionManager.DrawCardImageDescription(data, cardImageRect);

        cardImage.GetComponent<Image>().color = new Color(0.8f, 0.8f, 1f);
    }

    private IEnumerator CloseDescription() {
        CardDescriptionManager.ClosePanel();
        cardImage.GetComponent<Image>().color = Color.white;

        //카드 설명을 보다가 바로 옆에 놓인 카드로 커서를 옮겼을 때,
        //딜레이 없이 바로 Description을 띄우기 위함.
        yield return new WaitForSeconds(1f);

        if (!CardDescriptionManager.IsPanelOn()) {
            CardDescriptionManager.Instance.isPanelOn = false;
        }
    }
}
