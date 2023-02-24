using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNumberEffect : MonoBehaviour, ICardEffect {
	[SerializeField] private int number;

	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		card.ChangeNumber(number);
	}
}
