using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dingo))]
public class DingoInteractable : PlayerInteractable {

	private Dingo dingo;

	private void Awake() {
		dingo = GetComponent<Dingo>();
	}

	public override void OnPlayerInteracting(PlayerInteract player)
    {
		dingo.ScareOff();	
    }
}
