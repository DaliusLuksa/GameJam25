using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PainterMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private PlaceableInventory LeftMachineInput = null;
    [SerializeField] private PlaceableInventory RightMachineInput = null;

    [SerializeField] private ManagerSO _managerSO = null;

    public void Interact(Player interactingPlayer)
    {
        var leftItem = LeftMachineInput.GetItem();
        var rightItem = RightMachineInput.GetItem();
        
        var bubble = GetBubble(leftItem, rightItem);
        var paint = GetPaint(leftItem, rightItem);

        if (bubble != null && paint != null && interactingPlayer.HasSpaceInInventory())
        {
            LeftMachineInput.RemoveItem();
            RightMachineInput.RemoveItem();

            if (!_managerSO.PaintTypeToItemColorMap.TryGetValue(paint.CurrentItemType, out var itemColor))
            {
                Debug.LogError($"Failed to find {paint.CurrentItemType} in {nameof(_managerSO)}, values {_managerSO.PaintTypeToItemColorMap.Select(x => $"{x.Key}: {x.Value}")}");
                return;
            }

            bubble.SetItemColor(itemColor);

            //bubble.Recipe.Append(Upgrades.Paint, itemColor); //for later :)

            interactingPlayer.GiveItem(bubble);
        }
    }

    private Item GetPaint(Item item1, Item item2)
    {
        if (_managerSO.PaintTypes.Contains(item1.CurrentItemType))
        {
            return item1;
        }
        if (_managerSO.PaintTypes.Contains(item2.CurrentItemType))
        {
            return item2;
        }
        return null;
    }

    private Item GetBubble(Item item1, Item item2)
    {
        if (_managerSO.BubbleTypes.Contains(item1.CurrentItemType))
        {
            return item1;
        }
        if (_managerSO.BubbleTypes.Contains(item2.CurrentItemType))
        {
            return item2;
        }
        return null;
    }

}
