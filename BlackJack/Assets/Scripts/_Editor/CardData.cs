using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject {
	[SerializeField] private int number;
	[SerializeField] private Suit suit;
	[SerializeField] private CardIcon icon;
}
