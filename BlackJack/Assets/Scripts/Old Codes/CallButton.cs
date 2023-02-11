using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallButton : MonoBehaviour {
	private Button button;
	[SerializeField] private Betting playerBetting;

	private void Start() {
		button = GetComponent<Button>();
	}

	public void OnClickCall() {
		playerBetting.Call();
	}
}
