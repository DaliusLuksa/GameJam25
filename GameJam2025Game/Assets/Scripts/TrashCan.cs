using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void Interact(Player interactingPlayer)
    {
        if (!interactingPlayer.HasSpaceInInventory())
        {
            interactingPlayer.RemoveItemFromInventory();
        }
    }
}
