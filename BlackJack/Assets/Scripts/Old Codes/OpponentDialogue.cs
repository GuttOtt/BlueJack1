using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentDialogue : MonoBehaviour {
	[SerializeField] private Text text;

	public void ConsideringText() {
		text.text = "��...";
		StartCoroutine(EmptyDialogue());
	}

	public void CallText() {
		text.text = "��.";
		StartCoroutine(EmptyDialogue());
	}

	public void RaiseText() {
		text.text = "������.";
		StartCoroutine(EmptyDialogue());
	}

	public void FoldText() {
		text.text = "����.";
		StartCoroutine(EmptyDialogue());
	}

	public void StayText() {
		text.text = "������.";
		StartCoroutine(EmptyDialogue());
	}

	public void HitText() {
		text.text = "�� �� ��.";
		StartCoroutine(EmptyDialogue());
	}

	private IEnumerator EmptyDialogue() {
		yield return new WaitForSeconds(2f);
		text.text = "";
	}
}
