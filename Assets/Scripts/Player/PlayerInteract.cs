using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerHoldingState {
	Nothing,
	Seed,
	Water,
	Food,
	Baby
}

public class PlayerInteract : MonoBehaviour {
	[SerializeField]
	private LayerMask interactableLayerMask;

	[SerializeField]
	private int maxWaterCharges = 5;

	[SerializeField]
	private WateringCan wateringCan;

	[SerializeField]
	private SpriteRenderer seed;

	[SerializeField]
	private SpriteRenderer hangingBaby;

	private IPlayerInteractable currentInteractable;
	private Dictionary<GameObject, IPlayerInteractable> gameObjectInteractableLookup = new Dictionary<GameObject, IPlayerInteractable>();
	private PlayerHoldingState holdingState;
	private BabyMovement heldBaby;

	public PlayerHoldingState HoldingState {
		get { return holdingState; }
	}

	public int WaterCharges { get; set; }

	public void PickupResource(ResourceType resourceType) {
		if (resourceType == ResourceType.Seed) {
			holdingState = PlayerHoldingState.Seed;
		}
		else if (resourceType == ResourceType.Water) {
			holdingState = PlayerHoldingState.Water;
			WaterCharges = maxWaterCharges;
		}
	}

	public bool IsHoldingBaby() {
		return heldBaby != null;
	}

	public void PickupBaby(BabyMovement babyMovement) {
		babyMovement.WasPickedUp();
		heldBaby = babyMovement;
		holdingState = PlayerHoldingState.Baby;
	}

	public void DropResource() {
		holdingState = PlayerHoldingState.Nothing;

		if (heldBaby != null)
		{
			var position = transform.position;
			position.x -= 1.0f;
			heldBaby.transform.position = position;
			heldBaby.WasDropped();
			heldBaby = null;
		}
	}

	private void Update() {
		var interactableCollider = Physics2D.OverlapPoint(transform.position, interactableLayerMask);
		if(interactableCollider != null) {
			var interactable = GetInteractableForGameObject(interactableCollider.gameObject);

			if(currentInteractable != interactable) {
				if(currentInteractable != null) { SignalLeavingInteractable(currentInteractable); }
				if(interactable != null) { SignalEnteringInteractable(interactable); }
				currentInteractable = interactable;
			}
		}
		else if(currentInteractable != null) {
			SignalLeavingInteractable(currentInteractable);
			currentInteractable = null;
		}

		if(currentInteractable != null && Input.GetKeyDown(KeyCode.E)) {
			currentInteractable.OnPlayerInteracting(this);
		}

		if(Input.GetKeyDown(KeyCode.Q)) {
			DropResource();
		}

		wateringCan.ShowCan(holdingState == PlayerHoldingState.Water && WaterCharges > 0);
		seed.enabled = (holdingState == PlayerHoldingState.Seed);
		hangingBaby.enabled = (holdingState == PlayerHoldingState.Baby);
	}

	private void SignalLeavingInteractable(IPlayerInteractable oldInteractable) {
		oldInteractable.OnPlayerLeaving(this);
	}

	private void SignalEnteringInteractable(IPlayerInteractable newInteractable) {
		newInteractable.OnPlayerEntering(this);
	}

	private IPlayerInteractable GetInteractableForGameObject(GameObject gameObject) {
		if(gameObjectInteractableLookup.ContainsKey(gameObject)) {
			return gameObjectInteractableLookup[gameObject];
		}
		else {
			var interactable = gameObject.GetComponent<IPlayerInteractable>();
			gameObjectInteractableLookup.Add(gameObject, interactable);
			return interactable;
		}
	}
}
