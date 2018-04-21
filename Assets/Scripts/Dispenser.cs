using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {

	[SerializeField]
    private float dispenseRate = 10.0f;

	[SerializeField]
    private bool isReady = true;

	private float timeUntilDispense;

	void Start () {
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

	private void CountDownDispenser() {
		timeUntilDispense -= Time.deltaTime;
	}

	private void BuildResource()
	{
		isReady = false;
		timeUntilDispense = dispenseRate;
	}

	private void Dispense()
	{
		isReady = true;

		GetComponent<SpriteRenderer>().color = Color.green;
	}
}
