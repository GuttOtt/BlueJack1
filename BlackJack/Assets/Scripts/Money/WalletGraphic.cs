using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletGraphic : MonoBehaviour {
	private Text text;

	private void Awake() {
		text = GetComponent<Text>() as Text;
		//text.text =  "";
	}

	public void Plus(Money money) {
		text.gameObject.SetActive(true);
		text.color = Color.red;
		int amount = money.AmountToInt();
		text.text = "+" + amount;
		StartCoroutine(TextDisappear());
	}

	public void Minus(Money money) {
		text.gameObject.SetActive(true);
		text.color = Color.blue;
		int amount = money.AmountToInt();
		text.text = "-" + amount;
		StartCoroutine(TextDisappear());
	}

	private IEnumerator TextDisappear() {
		yield return new WaitForSeconds(3f);

		text.gameObject.SetActive(false);
	}
}