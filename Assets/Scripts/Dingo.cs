using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DingoBehaviorState {
	TrackingBaby,
	LeavingWithBaby,
	WaitingForBaby
}

public class Dingo : MonoBehaviour {
	[SerializeField]
	private float creepSpeed = 1f;
	[SerializeField]
	private float runSpeed = 1.3f;

	private Vector3 startingPoint;
	private DingoBehaviorState currentBehavior = DingoBehaviorState.TrackingBaby;

	public event EventHandler BabyCaptured;

	private void Start() {
		startingPoint = transform.position;
	}

	private void Update() {

		switch(currentBehavior) {
			case DingoBehaviorState.TrackingBaby:
			TrackBaby();
			break;
			case DingoBehaviorState.LeavingWithBaby:
			LeaveWithBaby();
			break;
			case DingoBehaviorState.WaitingForBaby:
			WaitForBaby();
			break;
		}
	}

	private void TrackBaby() {
		var targetBaby = FindClosestBaby();
		var targetPosition = targetBaby != null ? targetBaby.transform.position : startingPoint;
		var direction = (targetPosition - transform.position).normalized;
		transform.position = transform.position + (direction * creepSpeed * Time.deltaTime);

		if((transform.position - targetPosition).sqrMagnitude <= 1f) {
			Debug.Log("dingo: pick up baby");
			currentBehavior = DingoBehaviorState.LeavingWithBaby;
			// todo: pick up baby
		}
	}

	private void LeaveWithBaby() {
		var targetPosition = startingPoint;
		var direction = (targetPosition - transform.position).normalized;
		transform.position = transform.position + (direction * creepSpeed * Time.deltaTime);

		if((transform.position - targetPosition).sqrMagnitude <= 1f) {
			if(BabyCaptured != null) { BabyCaptured(this, EventArgs.Empty); }
			GameObject.Destroy(gameObject, 0.2f);
		}
	}

	private void WaitForBaby() {

	}

	private BabyGrowth FindClosestBaby() {
		var allBabies = FindObjectsOfType<BabyGrowth>();
		if(allBabies == null) { return null; }

		float minDistSqrd = 100000f;
		BabyGrowth closeBaby = null;
		for(int i=0; i<allBabies.Length; i++) {
			float distSqrd = (transform.position - allBabies[i].transform.position).sqrMagnitude;
			if(distSqrd < minDistSqrd) {
				minDistSqrd = distSqrd;
				closeBaby = allBabies[i];
			}
		}
		return closeBaby;
	}
}
