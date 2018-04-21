using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {

	[SerializeField]
    private float dispenseRate = 10.0f;

	[SerializeField]
    private bool isReady = true;

	[SerializeField]
    private Sprite emptySprite;

	[SerializeField]
    private Sprite filledSprite;

	private float timeUntilDispense;
	private SpriteRenderer spriteRenderer;

	public bool ResourceAvailable { get { return isReady; } }

	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		if (isReady) {
			Dispense();
		 }
		 else {
			BuildResource();
		 }
	}
	
	void Update () {
		if (!isReady)
		{
			CountDownDispenser();

			if (timeUntilDispense <= 0.0)
			{
				Dispense();
			}
		}
	}

	public void TakeResource() {
		BuildResource();
	}

	private void CountDownDispenser() {
		timeUntilDispense -= Time.deltaTime;
	}

	private void BuildResource()
	{
		isReady = false;
		timeUntilDispense = dispenseRate;
		spriteRenderer.sprite = emptySprite;
	}

	private void Dispense()
	{
		isReady = true;

		spriteRenderer.sprite = filledSprite;
	}
}
