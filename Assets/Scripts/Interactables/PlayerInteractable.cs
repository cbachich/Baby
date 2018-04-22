// base class for all interactables
using System;
using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour, IPlayerInteractable
{
    public event EventHandler<PlayerInteractbleEventArgs> PlayerEntered;
    public event EventHandler<PlayerInteractbleEventArgs> PlayerLeft;

    [SerializeField]
    protected SpriteRenderer interactIndicator;

    public virtual bool CanPlayerInteract { get { return true; } }

    private void Start() {
        if(interactIndicator) {
            interactIndicator.enabled = false;
        }
    }

    #region IPlayerInteractable implementation
    public virtual void OnPlayerEntering(PlayerInteract player)
    {
        if(PlayerEntered != null) {
            PlayerEntered(this, new PlayerInteractbleEventArgs() { CanPlayerInteract = CanPlayerInteract });
        }
        if(interactIndicator && CanPlayerInteract) {
            interactIndicator.enabled = true;
        }
    }

    public virtual void OnPlayerLeaving(PlayerInteract player)
    {
        if(PlayerLeft != null) { PlayerLeft(this, new PlayerInteractbleEventArgs()); }
        if(interactIndicator) {
            interactIndicator.enabled = false;
        }
    }

    public abstract void OnPlayerInteracting(PlayerInteract player);
    #endregion
}

public class PlayerInteractbleEventArgs: EventArgs {
    public bool CanPlayerInteract;
}