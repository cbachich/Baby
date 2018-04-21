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

	private IPlayerInteractable currentInteractable;
	private Dictionary<GameObject, IPlayerInteractable> gameObjectInteractableLookup = new Dictionary<GameObject, IPlayerInteractable>();
	private PlayerHoldingState holdingState;

	public PlayerHoldingState HoldingState {
		get { return holdingState; }
		set {
			// notify holding state changed
			holdingState = value;
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
