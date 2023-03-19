using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMoney : MonoBehaviour {
	private SpriteRenderer spr;
	private MoneyGraphic destination;
	private Money money;
	private float moveSpeed;

	private void Update() {
		if (destination) {
			Vector3 pos = destination.MoneyPosition;
			float distance = (pos - transform.position).magnitude;

			if (distance < 0.1f) {
				EndMoving();
			}

			transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed); 
		}
	}

	public static MovingMoney Instantiate(Money money, Vector3 origin, MoneyGraphic dest) {
		GameObject obj = new GameObject("Moving Money");
		MovingMoney moving = obj.AddComponent<MovingMoney>();
		moving.money = money;
		moving.destination = dest;
		moving.transform.position = origin;
		moving.moveSpeed = (dest.MoneyPosition - moving.transform.position).magnitude * Time.deltaTime * 3f;
		obj.AddComponent<SpriteRenderer>().sprite = MGContainer.MoneyToGraphic(money);
		
		return moving;
	}

	public void EndMoving() {
		destination.Deposit(money);
		Destroy(this.gameObject);
	}
}
