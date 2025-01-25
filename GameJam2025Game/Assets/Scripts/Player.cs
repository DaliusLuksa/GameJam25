using UnityEngine;

public class Player : PlaceableInventory
{
    [SerializeField] private SpriteRenderer playerInteractPromptSprite = null;
    [SerializeField] private KeyCode playerInteractKeycode = KeyCode.E;
    [SerializeField] private KeyCode playerAltInteractKeycode = KeyCode.F;
        [SerializeField] private HandUI _handUI = null;

    private IInteractable _currInteractableObject = null;
    private IAlternativelyInteractible _currAltInteractableObject = null;
    private PlaceableInventory _currPlaceableInventoryObject = null;
    private Player_Health _player_health = null;

    // TODO: DELETE THIS SHIT LATER
    [SerializeField] private Item currentItemHeld = null;

    protected override void Awake()
    {
        base.Awake();
        _player_health = GetComponent<Player_Health>();
    }

    private void Update()
    {
        HandleInteractablePrompt();
        HandlePlayerInteractInput();
    }

    private void HandleInteractablePrompt()
    {
        playerInteractPromptSprite.gameObject.SetActive(_currInteractableObject != null);
    }

    private void HandlePlayerInteractInput()
    {
        // If player is dead, we block the input handling stuff
        if (!_player_health.IsAlive()) { return; }

        if (_currInteractableObject != null && Input.GetKeyDown(playerInteractKeycode))
        {
            _currInteractableObject.Interact(this);
        }
        if (_currAltInteractableObject != null && Input.GetKeyDown(playerAltInteractKeycode))
        {
            _currAltInteractableObject.AlternativelyInteract(this);
        }

        if (_currPlaceableInventoryObject != null && Input.GetKeyDown(playerInteractKeycode))
        {
            Item itemWereTryingToPlace = _inventory.GetItem();
            Item returnedItem = _currPlaceableInventoryObject.PlaceItem(this, itemWereTryingToPlace);
            if (returnedItem != null)
            {
                // We picked up the item, need to check if we can add it (other inventory where this item came from already handles it, too lazy to fix it in better way)
                if (!_inventory.HasItem())
                {
                    _inventory.AddItem(returnedItem);
                }
            }
            else
            {
                // If we tried to get an item from an inventory that didn't had any items (we also didn't) this shit can happen
                if (itemWereTryingToPlace != null)
                {
                    // We placed an item
                    _inventory.RemoveItem();
                }
            }
        }
    }

    public void GiveItem(Item newItem)
    {
        bool result = _inventory.AddItem(newItem);

        if (result)
        {
            currentItemHeld = newItem;
            _handUI.onChangeHandUi(newItem.ItemSprite);
        }
    }

    public void RemoveItemFromInventory()
    {
        _inventory.RemoveItem();
        currentItemHeld = null;
        _handUI.onChangeHandUi(null);
    }

    public Item? GetHeldItem() 
    {
        return _inventory.GetItem();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"Player hit interactable object - {collision.gameObject}");
            _currInteractableObject = collision.gameObject.GetComponent<IInteractable>();
        }
        
        if (collision.gameObject.GetComponent<IAlternativelyInteractible>() != null)
        {
            Debug.Log($"Player hit alternatively interactable object - {collision.gameObject}");
            _currAltInteractableObject = collision.gameObject.GetComponent<IAlternativelyInteractible>();
        }

        if (collision.gameObject.GetComponent<PlaceableInventory>() != null)
        {
            Debug.Log($"Player hit IPlaceableInventory object, he can place an item here. - {collision.gameObject}");
            _currPlaceableInventoryObject = collision.gameObject.GetComponent<PlaceableInventory>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"Player left interactable object - {collision.gameObject}");
            _currInteractableObject = null;
        }
        
        if (collision.gameObject.GetComponent<IAlternativelyInteractible>() != null)
        {
            Debug.Log($"Player left alternatively interactable object - {collision.gameObject}");
            _currAltInteractableObject = null;
        }

        if (collision.gameObject.GetComponent<PlaceableInventory>() != null)
        {
            Debug.Log($"Player left IPlaceableInventory object. - {collision.gameObject}");
            _currPlaceableInventoryObject = null;
        }
    }
}
