using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardImageClicker : MonoBehaviour, IPointerClickHandler {
	public Action onPointerClick;

	public void OnPointerClick(PointerEventData eventData) {
		onPointerClick();
	}
}
