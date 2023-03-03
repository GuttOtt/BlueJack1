using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour {
	[SerializeField] private Canvas canvas;
	[SerializeField] private Button[] numButtons = new Button[7];
	[SerializeField] private Button[] suitButtons = new Button[4];
	[SerializeField] private Button closeButton;
	private Card card;
	private Card copy;

	private void Awake() {
		closeButton.onClick.AddListener(delegate {Close();});
		
		for (int i = 0; i < 7; i++) {
			numButtons[i].onClick.AddListener(delegate {copy.ChangeNumber(i);} );
		}

		for (int i = 0; i < 4; i++) {
			numButtons[i].onClick.AddListener(delegate {copy.ChangeSuit((Suit) i);} );
		}
	}

	private void Close() {
		Debug.Log("Close");
		canvas.gameObject.SetActive(false);
	}

	private void MakeCopy() {

	}
}
