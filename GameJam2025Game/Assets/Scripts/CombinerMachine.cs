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
        var isLeftInputValid = ValidateBubble(leftBubble);
        var isRightInputValid = ValidateBubble(rightBubble);

        if (isLeftInputValid && isRightInputValid && interactingPlayer.HasSpaceInInventory() || true)
        {
            LeftMachineInput.RemoveItem();
            RightMachineInput.RemoveItem();

            var upgradeLevel = Mathf.Max(leftBubble.CurrentItemUpgradeLevel, rightBubble.CurrentItemUpgradeLevel);
            Vector2 leftOffset = Vector2.zero, rightOffset = Vector2.zero;
            float leftScale = 0f, rightScale = 0f;
            Color leftColor = Color.white, rightColor = Color.white;
            if (upgradeLevel == 0)
            {
                leftOffset = new Vector2(-4f, 0f); rightOffset = new Vector2(4f, 0f);
                leftScale= 0.25f; rightScale = 0.25f;

            }
            else if (upgradeLevel == 1)
            {

            }

            var newSprite = SpriteCombiner.MergeSprites(leftBubble.ItemSprite, rightBubble.ItemSprite, rightOffset, rightScale, rightColor
                , leftOffset, leftScale, leftColor);

            //leftBubble.Recipe.Append(Upgrades.Combine,rightBubble.CurrentItemType); //for later :)
            leftBubble.SetItemSprite(newSprite);
            leftBubble.EnlargeItemLevel();
            leftBubble.SetItemType(ItemType.Complex);
            interactingPlayer.GiveItem(leftBubble);
        }
    }
    private bool ValidateBubble(Item bubble)
    {
        if (bubble == null)
        {
            return false;
        }
        if (bubble.CurrentItemType == ItemType.Complex)
        {
            return bubble.CurrentItemUpgradeLevel < _managerSO.MAX_UPGRADE_LEVEL;
        }
        return _managerSO.BubbleTypes.Contains(bubble.CurrentItemType);
    }
}
