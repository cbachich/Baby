// base class for all interactables
using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour, IPlayerInteractable
{
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
        if(interactIndicator && CanPlayerInteract) {
            interactIndicator.enabled = true;
        }
    }

    public virtual void OnPlayerLeaving(PlayerInteract player)
    {
        if(interactIndicator) {
            interactIndicator.enabled = false;
        }
    }

    public abstract void OnPlayerInteracting(PlayerInteract player);
    #endregion
}