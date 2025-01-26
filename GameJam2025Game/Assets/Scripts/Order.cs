using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private Item _itemGoal = null;
    private List<ItemType> possibleItemTypes = new List<ItemType>() { ItemType.Circle, ItemType.Square, ItemType.Triangle };
    private List<ItemColor> possibleItemColors = new List<ItemColor>() { ItemColor.Red, ItemColor.Green, ItemColor.Blue };

    public Order(int day)
    {
        // Use day to decide how hard this order should be
        _itemGoal = PrepareAnOrder(day);
    }

    private Item PrepareAnOrder(int day)
    {
        List<(ItemAction, Item)> recipeOrder = null;
        // Only start adding recipe stuff after day 1
        if (day > 1)
        {
            recipeOrder = new List<(ItemAction, Item)>();
            recipeOrder.Add((ItemAction.COMBINE, new Item(ItemType.Circle, ItemColor.Blue)));
        }

        var randomItemType = possibleItemTypes[Random.Range(0, possibleItemTypes.Count)];
        var randomItemColor = possibleItemColors[Random.Range(0, possibleItemColors.Count)];
        _itemGoal = new Item(randomItemType, randomItemColor, recipeOrder);
        
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
