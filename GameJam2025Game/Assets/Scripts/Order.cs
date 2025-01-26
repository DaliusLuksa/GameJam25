using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private Item _itemGoal = null;

    public Order(int day)
    {
        // Use day to decide how hard this order should be
        _itemGoal = PrepareAnOrder(day);
    }

    private Item PrepareAnOrder(int day)
    {
        List<(ItemAction, Item)> recipeOrder = new List<(ItemAction, Item)>();
        recipeOrder.Add((ItemAction.COMBINE, new Item(ItemType.Circle, ItemColor.Blue)));

        _itemGoal = new Item(ItemType.Square, ItemColor.Green, recipeOrder);
        
        return _itemGoal;
    }

    public bool IsOrderFinished(Item item)
    {
        if (item == null)
        {
            return false;
        }

        // If item goal has Recipe than we need to check if it matches with the item we trying to send (finish contract)
        if (_itemGoal.Recipe != null)
        {
            // Return false if capacities doesn't match (no point in checking anything)
            if (item.Recipe.Count != _itemGoal.Recipe.Count) { return false; }

            // Need to loop through all recipes and check if stuff matches.
            for (int i = 0; i < item.Recipe.Count; i++)
            {
                // If at least one doesn't match, just return false then.
                if (item.Recipe[i].Item1 != _itemGoal.Recipe[i].Item1 || item.Recipe[i].Item2.CompareItem(_itemGoal.Recipe[i].Item2) == false)
                {
                    return false;
                }
            }
        }

        return item.CompareItem(_itemGoal);
    }
}
