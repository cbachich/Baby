public interface IPlayerInteractable
{
    void OnPlayerEntering(PlayerInteract player);
    void OnPlayerLeaving(PlayerInteract player);
    void OnPlayerInteracting(PlayerInteract player);
}