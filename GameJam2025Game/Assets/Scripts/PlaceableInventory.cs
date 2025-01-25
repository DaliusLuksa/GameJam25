using UnityEngine;

public abstract class PlaceableInventory : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PickedItemSpriteRenderer = null;
    protected Inventory _inventory = null;

    private void Awake()
    {
        _inventory = new Inventory(gameObject.GetComponent<PlaceableInventory>());
    }

    // Will try to place item on the placeable inventory, but if there's already an item and player can pick it up
    // it will pick it up from there instead.
    public Item PlaceItem(PlaceableInventory placer, Item item)
    {
        if (_inventory != null && _inventory.HasItem())
        {
            // Placer trying to pickup an item from us
            // only give an item if the placer has space in his inventory
            Item cur_item = _inventory.GetItem();
            if (placer.HasSpaceInInventory())
            {
                _inventory.RemoveItem();
            }

            return cur_item;
        }
        else
        {
            // We can receive a request to place item but the placer doesn't have item either
            if (item != null)
            {
                _inventory.AddItem(item);
            }
            
            return null;
        }
    }

    public void UpdateItemVisibility()
    {
        if (_inventory.HasItem())
        {
            PickedItemSpriteRenderer.sprite = _inventory.GetItem().ItemSprite;
            PickedItemSpriteRenderer.color = _inventory.GetItem().ItemSpriteColor;
            PickedItemSpriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            PickedItemSpriteRenderer.gameObject.SetActive(false);
        }
    }

    public bool HasSpaceInInventory()
    {
        return !_inventory.HasItem();
    }

    public Item GetItem() { return _inventory.GetItem(); }
    public void RemoveItem() { _inventory.RemoveItem(); }
}