using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEffect: MonoBehaviour, ICardEffect {
	[SerializeField] private float percent;

	public void Activate() {
		Gambler owner = transform.parent.GetComponent<Card>().owner;
		owner.Save(percent);
	}
}
