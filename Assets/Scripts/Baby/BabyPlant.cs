using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPlant : MonoBehaviour {
	public event EventHandler PopOutAnimationCompleted;

	public Animator Animator { get; private set; }

	[SerializeField]
	private SpriteRenderer[] babyParts;

	private void Awake() {
		this.Animator = GetComponent<Animator>();
	}

	public void OnBabyPopOutAnimationCompleted() {
		if(PopOutAnimationCompleted != null) { PopOutAnimationCompleted(this, EventArgs.Empty); }
	}

	public void SetColor(Color skinColor) {
		foreach(var spr in babyParts) {
			spr.color = skinColor;
		}
	}

	public void Show(bool show) {
		foreach(var spr in babyParts) {
			spr.enabled = show;
		}

		if(show) {
			this.Animator.SetTrigger("Restart");
		}
	}
}
