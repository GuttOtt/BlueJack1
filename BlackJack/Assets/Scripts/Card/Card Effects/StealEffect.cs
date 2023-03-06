using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealEffect : MonoBehaviour, ICardEffect {
	[SerializeField] private float percent;

	public void Activate() {
		Money ante = PlaySceneManager.moneySettings.ante;
		Money stealAmount = ante.Times(percent);

		Gambler owner = transform.parent.GetComponent<Card>().owner;
		owner.Steal(stealAmount);

		Debug.Log("Steal "+ stealAmount.AmountToInt());
	}
}
