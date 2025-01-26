using UnityEngine;

public class OrderGiveawaySpot : MonoBehaviour, IInteractable
{
    public void Interact(Player interactingPlayer)
    {
        if (interactingPlayer.GetHeldItem() != null)
        {
            GameManager.Instance.TryToSubmitOrder(interactingPlayer.GetHeldItem());
            interactingPlayer.RemoveItem();
        }
    }
}
