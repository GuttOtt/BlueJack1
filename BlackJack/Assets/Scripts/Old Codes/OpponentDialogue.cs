using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentDialogue : MonoBehaviour {
	[SerializeField] private Text text;

	public void ConsideringText() {
		text.text = "흠...";
		StartCoroutine(EmptyDialogue());
	}

	public void CallText() {
		text.text = "콜.";
		StartCoroutine(EmptyDialogue());
	}

	public void RaiseText() {
		text.text = "레이즈.";
		StartCoroutine(EmptyDialogue());
	}

	public void FoldText() {
		text.text = "폴드.";
		StartCoroutine(EmptyDialogue());
	}

	public void StayText() {
		text.text = "스테이.";
		StartCoroutine(EmptyDialogue());
	}

	public void HitText() {
		text.text = "한 장 더.";
		StartCoroutine(EmptyDialogue());
	}

	private IEnumerator EmptyDialogue() {
		yield return new WaitForSeconds(2f);
		text.text = "";
	}
}
