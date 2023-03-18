using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGraphic : MonoBehaviour {
	private SpriteRenderer spr;
	private Money money;
	[SerializeField] private Text text;
	[SerializeField] private Vector3 moneyPosition;

	private void Awake() {
		GameObject obj = new GameObject("MoneyGraphic");
		obj.transform.parent = this.transform;
		obj.transform.position = moneyPosition;
		spr = obj.AddComponent<SpriteRenderer>();

		money = Money.zero;
		text.transform.position = Camera.main.WorldToScreenPoint(moneyPosition);
	}

	public void SetMoney(Money money) {
		this.money = money.Times(1f);
		UpdateGraphic();
	}

	public void Send(Money money, MoneyGraphic dest) {
		this.money = this.money.Minus(money);
		UpdateGraphic();
		MovingMoney.Instantiate(money, moneyPosition, dest);
	}

	public void UpdateGraphic() {
		spr.sprite = MGContainer.MoneyToGraphic(money);
		text.text = money.AmountToInt().ToString();
	}

	public void Deposit(Money money) {
		this.money = this.money.Plus(money);
		UpdateGraphic();
	}
}

public class MGContainer {
	public static Sprite MoneyToGraphic(Money money) {
		int amount = money.AmountToInt();
		Sprite sprite;

		if (amount <= 10) {
			sprite = Resources.Load<Sprite>("Sprites/MoneyGraphics/MG1");
		}
		else if (amount <= 50) {
			sprite = Resources.Load<Sprite>("Sprites/MoneyGraphics/MG2");
		}
		else if (amount <= 100) {
			sprite = Resources.Load<Sprite>("Sprites/MoneyGraphics/MG3");
		}
		else if (amount <= 500) {
			sprite = Resources.Load<Sprite>("Sprites/MoneyGraphics/MG4");
		}
		else {
			sprite = Resources.Load<Sprite>("Sprites/MoneyGraphics/MG5");
		}

		return sprite;
	}
}