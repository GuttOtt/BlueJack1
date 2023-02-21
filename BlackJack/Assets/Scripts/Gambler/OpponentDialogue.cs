using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentDialogue : MonoBehaviour {
	[SerializeField] SpeechBubble speechBubble;
	[SerializeField] string callText, raiseText, foldText, hitText, stayText;

	private void Awake() {
		speechBubble.gameObject.SetActive(false);

	}

	public void Raise() {
		HandleSpeechBubble(raiseText);
	}

	public void Call() {
		HandleSpeechBubble(callText);
	}

	public void Fold() {
		HandleSpeechBubble(foldText);
	}

	public void Hit() {
		HandleSpeechBubble(hitText);
	}

	public void Stay() {
		HandleSpeechBubble(stayText);
	}

	private void HandleSpeechBubble(string text) {
		speechBubble.gameObject.SetActive(true);
		speechBubble.Speech(text);
	}
}