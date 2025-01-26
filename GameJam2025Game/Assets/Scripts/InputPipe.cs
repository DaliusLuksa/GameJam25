using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPipe : MonoBehaviour, IInteractable
{
    [SerializeField] private PlaceableInventory _outputInventory = null;
    public void Interact(Player interactingPlayer)
    {
        if (_outputInventory != null && _outputInventory.HasSpaceInInventory() && !interactingPlayer.HasSpaceInInventory())
        {
            var item = interactingPlayer.GetHeldItem();
            var itemWereTryingToPlace = _outputInventory.PlaceItem(interactingPlayer, item);
            if (itemWereTryingToPlace == null)
            {
                // We placed an item
                interactingPlayer.RemoveItemFromInventory();
            }
        }
    }
    public void LinkInventory(PlaceableInventory inventory)
    {
        if (inventory != null)
        {
            _outputInventory = inventory;
        }
    }

    public void ResetLink()
    {
        _outputInventory = null;
    }
}
