using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGraphic : MonoBehaviour {
	private SpriteRenderer spr;
	private Money money;
	[SerializeField] private Text text;
	[SerializeField] private Vector3 moneyPosition;
	public Vector3 MoneyPosition { get => moneyPosition;}

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
		if (money.IsEqualWith(Money.zero)) {
			spr.sprite = null;
			text.text = "";
			return;
		}
		spr.sprite = MGContainer.MoneyToGraphic(money);
		text.text = money.AmountToInt().ToString();
	}

	public void Deposit(Money money) {
		this.money = this.money.Plus(money);
		UpdateGraphic();
	}
}

public class MGContainer {
	private static List<Sprite> sprites = new List<Sprite>();
	
	public static void Initialize() {
		sprites.Add(Resources.Load<Sprite>("Sprites/MoneyGraphics/MG1"));
		sprites.Add(Resources.Load<Sprite>("Sprites/MoneyGraphics/MG2"));
		sprites.Add(Resources.Load<Sprite>("Sprites/MoneyGraphics/MG3"));
		sprites.Add(Resources.Load<Sprite>("Sprites/MoneyGraphics/MG4"));
		sprites.Add(Resources.Load<Sprite>("Sprites/MoneyGraphics/MG5"));
	}

	public static Sprite MoneyToGraphic(Money money) {
		int amount = money.AmountToInt();
		Sprite sprite;

		if (amount <= 0) {
			sprite = null;
		}
		if (amount <= 10) {
			sprite = sprites[0];
		}
		else if (amount <= 50) {
			sprite = sprites[1];
		}
		else if (amount <= 100) {
			sprite = sprites[2];
		}
		else if (amount <= 500) {
			sprite = sprites[3];
		}
		else {
			sprite = sprites[4];
		}

		return sprite;
	}
}