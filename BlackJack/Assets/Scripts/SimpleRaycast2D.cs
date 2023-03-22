using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRaycast2D {
	public static T OnCursor<T>() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (!hit.collider) {
			return default(T);
		}
		T target = hit.collider.GetComponent<T>();
		return target;
	}

	public static T OnCursorUI<T>() {
		Vector3 mousePos = Input.mousePosition;
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
		if (!hit.collider) {
			return default(T);
		}
		T target = hit.collider.GetComponent<T>();
		return target;
	}
}
