using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardDescriptor : MonoBehaviour {
    private Card card;
    private bool IsFront { get => card ? card.IsFront : false; }

    private void Awake() {
        card = GetComponent<Card>();
    }

    private void OnMouseEnter() {
        if (!IsFront || DeckListUI.IsTurnedOn) return;
        Debug.Log("OnMouseEnter" + Time.time);
        StartCoroutine(DrawDescription());

    }

    private void OnMouseExit() {
        if (DeckListUI.IsTurnedOn) return;
        Debug.Log("OnMouseExit" + Time.time);
        StartCoroutine(CloseDescription());
    }

    private IEnumerator DrawDescription() {
        if (!CardDescriptionManager.Instance.isPanelOn) {
            Debug.Log("Delay" + Time.time);
            yield return new WaitForSeconds(0.3f);
            Debug.Log("Delay Over" + Time.time);
        }

        CardDescriptionManager.DrawDescription(card.GetData(), card.transform.position);
        card.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1f);
    }

    private IEnumerator CloseDescription() {
        CardDescriptionManager.ClosePanel();
        card.GetComponent<SpriteRenderer>().color = Color.white;

        //ī�� ������ ���ٰ� �ٷ� ���� ���� ī��� Ŀ���� �Ű��� ��,
        //������ ���� �ٷ� Description�� ���� ����.
        yield return new WaitForSeconds(1f);

        if (!CardDescriptionManager.IsPanelOn()) {
            CardDescriptionManager.Instance.isPanelOn = false;
        }
    }
}
