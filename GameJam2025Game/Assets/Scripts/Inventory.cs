using UnityEngine;

public class Inventory
{
    [SerializeField] private Item _inventoryItem = null;

    private PlaceableInventory _owner = null;

    public Inventory(PlaceableInventory owner)
    {
        _owner = owner;
    }

    public bool HasItem()
    {
        return _inventoryItem;
    }

    public Item GetItem()
    {
        return _inventoryItem;
    }

    public bool AddItem(Item item)
    {
        if (_inventoryItem != null)
        {
            Debug.LogError($"Trying to add an item to {_owner} inventory when [{_inventoryItem.CurrentItemType}] item already exists there");
            return false;
        }

        _inventoryItem = item;
        _owner.UpdateItemVisibility();
        return true;
    }

    public void RemoveItem()
    {
        if (_inventoryItem == null)
        {
            Debug.LogError($"Tried to remove item from {_owner.name} inventory when there was no item");
            return;
        }

        _inventoryItem = null;
        _owner.UpdateItemVisibility();
    }
}
