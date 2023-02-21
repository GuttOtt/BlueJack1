using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {
	[SerializeField] Text text;

	private void OnEnable() {
		GetComponent<Image>().color = Color.white;
		text.GetComponent<Text>().color = Color.black;
		StartCoroutine(Disappear());
	}


	private IEnumerator Disappear() {
		yield return new WaitForSeconds(1f);

		while (GetComponent<Image>().color.a > 0.1) {
			GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime);
			text.GetComponent<Text>().color -= new Color(0, 0, 0, Time.deltaTime);
			yield return null;
		}

		gameObject.SetActive(false);
	}

	public void Speech(string speech) {
		text.text = speech;
	}
}