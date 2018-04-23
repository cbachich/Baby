using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
	Seed,
	Water
} 

public class Dispenser : MonoBehaviour {

	[SerializeField]
    private float dispenseRate = 10.0f;

	[SerializeField]
    private bool isReady = true;

	[SerializeField]
    private ResourceType resourceType;

	private float timeUntilDispense;

	public bool ResourceAvailable { get { return isReady; } }
	
	public ResourceType ResourceType {
		get { return resourceType; }
	}

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
	}

	private void Dispense()
	{
		isReady = true;

	}
}
