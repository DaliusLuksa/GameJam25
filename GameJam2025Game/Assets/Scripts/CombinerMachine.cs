using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinerMachine : MonoBehaviour, IInteractable
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

            leftBubble = UpgradeItem(leftBubble, rightBubble);

            interactingPlayer.GiveItem(leftBubble);
        }
    }

    public static Item UpgradeItem(Item leftBubble, Item rightBubble)
    {
        var upgradeLevel = Mathf.Max(leftBubble.CurrentItemUpgradeLevel, rightBubble.CurrentItemUpgradeLevel);
        Vector2 leftOffset = Vector2.zero, rightOffset = Vector2.zero;
        float leftScale = 0f, rightScale = 0f;
        Color leftColor = leftBubble.ItemSpriteColor, rightColor = rightBubble.ItemSpriteColor;
        if (upgradeLevel == 0)
        {
            leftOffset = new Vector2(-8f, 0f); rightOffset = new Vector2(8f, 0f);
            leftScale = 0.5f; rightScale = 0.5f;

        }
        else if (upgradeLevel == 1)
        {
            leftOffset = new Vector2(0, -8f); rightOffset = new Vector2(0, 8f);
            leftScale = 0.5f; rightScale = 0.5f;
        }

        var newSprite = SpriteCombiner.MergeSprites(leftBubble.ItemSprite, rightBubble.ItemSprite, rightOffset, rightScale, rightColor
            , leftOffset, leftScale, leftColor);

        leftBubble.Recipe.Add((ItemAction.COMBINE, rightBubble));

        leftBubble.SetItemColor(ItemColor.White); // Dalius - Why are you setting this to white????
        leftBubble.SetItemSprite(newSprite);
        leftBubble.EnlargeItemLevel();
        leftBubble.SetItemType(ItemType.Complex);
        return leftBubble;
    }
}
