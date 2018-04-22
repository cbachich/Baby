using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkInteract : MonoBehaviour {

	private int numberOfBabiesPickedUp = 0;

	public int NumberOfBabiesCollected { get { return numberOfBabiesPickedUp; } }

	void OnTriggerEnter2D(Collider2D other) {
        var baby = other.gameObject.GetComponent<BabyMovement>();

		if (baby != null)
		{
			numberOfBabiesPickedUp += 1;
			Destroy(other.gameObject);
		}
    }
}
