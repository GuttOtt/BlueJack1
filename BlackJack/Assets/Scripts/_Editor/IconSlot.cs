using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSlot: MonoBehaviour {
	private Button button;
	[SerializeField] private Image image;
	private CardIcon iconPrefab;
	private CardEditor cardEditor;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(Clicked);

		GameObject obj = new GameObject("Icon Image");
		obj.transform.SetParent(this.transform, false);
		image = obj.AddComponent<Image>() as Image;
		image.raycastTarget = false;
	}
	
	private void Clicked() {
		cardEditor.ChangeIcon(iconPrefab);
	}

	public void SetIcon(CardIcon iconPrefab) {
		this.iconPrefab = iconPrefab;
		image.sprite = iconPrefab.GetComponent<SpriteRenderer>().sprite;
	}

	public void Initialize(CardEditor cardEditor) {
		this.cardEditor = cardEditor;
	}
}
