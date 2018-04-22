using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {

	[SerializeField]
    private Sprite atRest;

	[SerializeField]
    private Sprite holdingSeed;

	[SerializeField]
    private Sprite holdingWater;

	[SerializeField]
	private PlayerInteract playerInteract;

	private SpriteRenderer spriteRenderer;

	void Awake () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInteract.HoldingState == PlayerHoldingState.Seed) {
			spriteRenderer.sprite = holdingSeed;
		}
		else if (playerInteract.HoldingState == PlayerHoldingState.Water) {
			spriteRenderer.sprite = holdingWater;
		}
		else {
			spriteRenderer.sprite = atRest;
		}
	}
}
