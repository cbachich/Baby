using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour {
	private SpriteRenderer[] sprites;

	private void Awake() {
		sprites = GetComponentsInChildren<SpriteRenderer>();
	}

	public void ShowCan(bool show) {
		foreach(var spr in sprites) {
			spr.enabled = show;
		}
	}
}