using UnityEngine;

public class ItemSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickableItemPrefab = null;

    public void Interact(Player interactingPlayer)
    {
        if (interactingPlayer.GetHeldItem()?.CurrentItemType ==
         pickableItemPrefab?.CurrentItemType) //place down item of same type
        {
            Debug.Log($"Table removed item from player... Started by - {interactingPlayer}");
            interactingPlayer.RemoveItemFromInventory();
        }
        else if(interactingPlayer.HasSpaceInInventory()) //pickup new item
        {
            Debug.Log($"Table gave item to the player... Started by - {interactingPlayer}");
            Item spawnedItem = Instantiate(pickableItemPrefab);
            interactingPlayer.GiveItem(spawnedItem);
        }
    }
}
