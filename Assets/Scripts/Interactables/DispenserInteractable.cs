using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dispenser))]
public class DispenserInteractable : PlayerInteractable {

	private Dispenser dispenser;

	private void Awake() {
		dispenser = GetComponent<Dispenser>();
	}

	public override void OnPlayerInteracting(PlayerInteract player)
    {
		if(dispenser.ResourceAvailable) {
			dispenser.TakeResource();
		}
    }
}
