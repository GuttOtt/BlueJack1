using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSlot: MonoBehaviour {
	private Button button;
	private Image image;
	private CardIcon iconPrefab;
	private CardData data;
	private CopyCardImage copyImg;

	private void Awake() {
		button = GetComponent<Button>();
		GameObject obj = new GameObject("Icon Image");
		obj.transform.SetParent(this.transform, false);
		image = obj.AddComponent<Image>() as Image;
	}

	private void Clicked() {
		data.ChangeIcon(iconPrefab);
		data.PasteTo(copyImg);
	}

	public void SetIcon(CardIcon iconPrefab) {
		this.iconPrefab = iconPrefab;
		image.sprite = iconPrefab.GetComponent<SpriteRenderer>().sprite;
	}

	public void Initialize(CardData data, CopyCardImage copyImg) {
		this.data = data;
		this.copyImg = copyImg;
	}
}
