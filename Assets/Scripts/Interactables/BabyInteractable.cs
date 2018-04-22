using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BabyMovement))]
public class BabyInteractable : PlayerInteractable {

	private BabyMovement babyMovement;

	private void Awake() {
		babyMovement = GetComponent<BabyMovement>();
	}

	public override void OnPlayerInteracting(PlayerInteract player)
    {
		if (!player.IsHoldingBaby()) {
			player.PickupBaby(babyMovement);
		}	
    }
}
