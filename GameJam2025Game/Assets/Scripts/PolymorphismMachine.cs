using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PolymorphismMachine : MonoBehaviour, IInteractable, IAlternativelyInteractible
{
    private enum MachineState
    {
        DORMANT, SPINNING
    }
    [SerializeField] private SpriteRenderer SpinnerItemSpriteRenderer = null;
    [SerializeField] private GameObject MachineInput;
    [SerializeField] private ItemSO[] ItemSOs;
    private MachineState _machineState = MachineState.DORMANT;
    private ItemType[] _itemTypes = null;
    private int _spinnerIndex = 0;
    private Item _consumedItem = null;

    public int SpinnerIndex => _spinnerIndex;

    private void Awake()
    {
        _itemTypes = ItemSOs.Select(x => x.ItemType).ToArray();
    }

    public void Interact(Player interactingPlayer)
    {
        if (_machineState == MachineState.DORMANT)
        {
            var inputPlaceableInventory = MachineInput.GetComponent<PlaceableInventory>();
            if (inputPlaceableInventory != null && inputPlaceableInventory.GetItem() != null
                && _itemTypes.Contains(inputPlaceableInventory.GetItem().CurrentItemType))
            {
                _consumedItem = inputPlaceableInventory.GetItem();
                _spinnerIndex = Array.FindIndex(_itemTypes, itemType => itemType == _consumedItem.CurrentItemType);

                inputPlaceableInventory.RemoveItem();
                _machineState = MachineState.SPINNING;
                SpinnerItemSpriteRenderer.gameObject.SetActive(true);
                UpdateSpriteRenderer();
            }
        }
        else if (_machineState == MachineState.SPINNING)
        {
            if (interactingPlayer.HasSpaceInInventory())
            {
                _consumedItem.SetItemType(_itemTypes[_spinnerIndex]);
                //TODO: make colors carry over from last item.
                //newItem.CurrentItemColor = _consumedItem.CurrentItemColor;
                interactingPlayer.GiveItem(_consumedItem);
                _machineState = MachineState.DORMANT;
                SpinnerItemSpriteRenderer.gameObject.SetActive(false);
            }
        }
    }
    private void UpdateSpriteRenderer()
    {
        var itemComponent = ItemSOs[_spinnerIndex];
        SpinnerItemSpriteRenderer.color = itemComponent.ItemSpriteColor;
        SpinnerItemSpriteRenderer.sprite = itemComponent.ItemSprite;
    }

    public void AlternativelyInteract(Player interactingPlayer)
    {
        if (_machineState == MachineState.SPINNING)
        {
            if (_spinnerIndex + 1 >= ItemSOs.Length)
            {
                _spinnerIndex = 0;
            }
            else
            {
                _spinnerIndex++;
            }
            UpdateSpriteRenderer();
        }
    }
}
