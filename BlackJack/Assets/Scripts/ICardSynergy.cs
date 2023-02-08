using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardSynergy {
	bool IsSatisfiedBy(Card card);
}

public class NumberSynergy: ICardSynergy {
	int number;

	public bool IsSatisfiedBy(Card card) {
		return card.IsEqualNumber(number);
	}
}

public class SuitSynergy: ICardSynergy {
	Suit suit;

	public bool IsSatisfiedBy(Card card) {
		return card.IsEqualSuit(suit);
	}
}
