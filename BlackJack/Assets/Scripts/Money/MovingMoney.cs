using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMoney : MonoBehaviour {
	private SpriteRenderer spr;
	private MoneyGraphic destination;
	private Money money;

	private void Update() {
		if (destination) {
			Vector3 pos = destination.transform.position;
			float distance = (pos - transform.position).magnitude;

			if (distance < 0.1f) {
				EndMoving();
			}

			float moveSpeed = distance * Time.deltaTime * 3f;
			transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed); 
		}
	}

	public static MovingMoney Instantiate(Money money, MoneyGraphic dest) {
		GameObject obj = new GameObject("Moving Money");
		MovingMoney moving = obj.AddComponent<MovingMoney>();
		moving.Initialize(money, dest);
		return moving;
	}

	public void Initialize(Money money, MoneyGraphic dest) {
		destination = dest;
		this.money = money;
		spr.sprite = MGContainer.MoneyToGraphic(money);
	}

	public void EndMoving() {
		destination.Deposit(money);
		Destroy(this.gameObject);
	}
}
