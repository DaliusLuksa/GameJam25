using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InserterMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private PlaceableInventory LeftMachineInput = null;
    [SerializeField] private PlaceableInventory RightMachineInput = null;

    [SerializeField] private ManagerSO _managerSO = null;

    public void Interact(Player interactingPlayer)
    {
        var leftBubble = LeftMachineInput.GetItem();
        var rightBubble = RightMachineInput.GetItem();
        var isLeftInputValid = Helpers.ValidateBubble(leftBubble, _managerSO);
        var isRightInputValid = Helpers.ValidateBubble(rightBubble, _managerSO);

        if (isLeftInputValid && isRightInputValid && interactingPlayer.HasSpaceInInventory())
        {
            LeftMachineInput.RemoveItem();
            RightMachineInput.RemoveItem();

            var newSprite = SpriteCombiner.InsertSprites(leftBubble.ItemSprite, rightBubble.ItemSprite, Vector2.zero, 0.6f, Color.white);

            //leftBubble.Recipe.Append(Upgrades.Insert,rightBubble.CurrentItemType); //for later :)
            leftBubble.SetItemSprite(newSprite);
            leftBubble.EnlargeItemLevel();
            leftBubble.SetItemType(ItemType.Complex);
            interactingPlayer.GiveItem(leftBubble);
        }
    }
}




