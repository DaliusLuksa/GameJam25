using UnityEngine;

public class ItemSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickableItemPrefab = null;

    public void Interact(Player interactingPlayer)
    {
        if (interactingPlayer.GetHeldItem()?.CurrentItemType ==
         pickableItemPrefab?.CurrentItemType) //place down item of same type
        {
            interactingPlayer.RemoveItemFromInventory();
        }
        else if(interactingPlayer.HasSpaceInInventory()) //pickup new item
        {
            Item spawnedItem = Instantiate(pickableItemPrefab);
            interactingPlayer.GiveItem(spawnedItem);
        }
    }
}
