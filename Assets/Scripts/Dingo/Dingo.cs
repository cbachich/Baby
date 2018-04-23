using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DingoBehaviorState {
	TrackingBaby,
	LeavingWithBaby,
	WaitingForBaby,
	RunningAway
}

public class DingoReturnedFromHuntEventArgs: EventArgs {
	public bool WithBaby;
}

public class Dingo : MonoBehaviour {
	[SerializeField]
	private float creepSpeed = 1f;
	[SerializeField]
	private float runSpeed = 1.3f;
	[SerializeField]
	private SpriteRenderer hangingBabySprite;

	private Vector3 startingPoint;
	private DingoBehaviorState currentBehavior = DingoBehaviorState.TrackingBaby;
	private BabyMovement heldBaby;

	public event EventHandler<DingoReturnedFromHuntEventArgs> ReturnedFromHunt;

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
			case DingoBehaviorState.RunningAway:
			RunAway();
			break;
		}

		hangingBabySprite.enabled = currentBehavior == DingoBehaviorState.LeavingWithBaby;
	}

	private void TrackBaby() {
		var targetBaby = FindClosestBaby();
		if(targetBaby == null) {
			currentBehavior = DingoBehaviorState.WaitingForBaby;
			return;
		}

		var targetPosition = targetBaby.transform.position;
		var direction = (targetPosition - transform.position).normalized;
		Move(direction * creepSpeed * Time.deltaTime);
	}

	private void LeaveWithBaby() {
		var targetPosition = startingPoint;
		var direction = (targetPosition - transform.position).normalized;
		Move(direction * creepSpeed * Time.deltaTime);

		if((transform.position - targetPosition).sqrMagnitude <= 1f) {
			if(ReturnedFromHunt != null) { ReturnedFromHunt(this, new DingoReturnedFromHuntEventArgs() { WithBaby = true}); }
			heldBaby.HandleBeingEaten();
			heldBaby = null;
		}
	}

	private void WaitForBaby() {
		var baby = FindClosestBaby();
		if(baby) {
			currentBehavior = DingoBehaviorState.TrackingBaby;
			TrackBaby();
		}
	}

	private void RunAway() {
		var targetPosition = startingPoint;
		var direction = (targetPosition - transform.position).normalized;
		Move(direction * runSpeed * Time.deltaTime);

		if((transform.position - targetPosition).sqrMagnitude <= 1f) {
			if(ReturnedFromHunt != null) { ReturnedFromHunt(this, new DingoReturnedFromHuntEventArgs() { WithBaby = false}); }
		}
	}

	private void Move(Vector3 delta) {
		transform.position = transform.position + delta;
		var scaleSign = Mathf.Sign(transform.localScale.x);
		var moveSign = Mathf.Sign(delta.x);
		if(scaleSign != moveSign) {
			var localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}
	}

	private BabyMovement FindClosestBaby() {
		var allBabies = FindObjectsOfType<BabyMovement>();
		if(allBabies == null) { return null; }

		float minDistSqrd = 100000f;
		BabyMovement closeBaby = null;
		for(int i=0; i<allBabies.Length; i++) {
			if(allBabies[i].BabyState == BabyState.Carried) { continue; }

			float distSqrd = (transform.position - allBabies[i].transform.position).sqrMagnitude;
			if(distSqrd < minDistSqrd) {
				minDistSqrd = distSqrd;
				closeBaby = allBabies[i];
			}
		}
		return closeBaby;
	}

	private void DropBaby() {
		if (heldBaby != null)
		{
			heldBaby.transform.position = transform.position;
			heldBaby.WasDropped();
			heldBaby = null;
		}
	}
	
	private void PickupBaby(BabyMovement babyMovement) {
		babyMovement.WasPickedUp();
		heldBaby = babyMovement;
		currentBehavior = DingoBehaviorState.LeavingWithBaby;
	}

	public void ScareOff() {
		if(currentBehavior == DingoBehaviorState.LeavingWithBaby) {
			DropBaby();
		}
		currentBehavior = DingoBehaviorState.RunningAway;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		var baby = other.gameObject.GetComponent<BabyMovement>();
		if(baby && currentBehavior == DingoBehaviorState.TrackingBaby && baby.BabyState != BabyState.Carried) {
			PickupBaby(baby);
		}
	}

	public void Reset() {
		currentBehavior = DingoBehaviorState.TrackingBaby;
		transform.position = startingPoint;
	}
}
