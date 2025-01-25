using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormChanger : MonoBehaviour, IInteractable, IAlternativelyInteractible
{
    private enum MachineState
    {
        DORMANT, SPINNING
    }
    [SerializeField] private SpriteRenderer SpinnerItemSpriteRenderer = null;

    [SerializeField] private ItemSO[] ItemFormSOs;
    private MachineState _machineState = MachineState.DORMANT;
    private ItemType[] _itemTypes = null;
    private int _spinnerIndex = 0;
    private Item _consumedItem = null;

    public int SpinnerIndex => _spinnerIndex;

    private void Awake()
    {
        _itemTypes = ItemFormSOs.Select(x => x.ItemType).ToArray();
    }

    public void Interact(Player interactingPlayer)
    {
        if (_machineState == MachineState.DORMANT)
        {
            var playerHeldItem = interactingPlayer.GetHeldItem();
            if (playerHeldItem != null && playerHeldItem.CurrentItemType == ItemType.BaseFormItem)
            {
                _consumedItem = playerHeldItem;
                _spinnerIndex = 0;

                interactingPlayer.RemoveItemFromInventory();
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

                interactingPlayer.GiveItem(_consumedItem);
                _machineState = MachineState.DORMANT;
                SpinnerItemSpriteRenderer.gameObject.SetActive(false);
            }
        }
    }
    private void UpdateSpriteRenderer()
    {
        var itemComponent = ItemFormSOs[_spinnerIndex];
        SpinnerItemSpriteRenderer.color = itemComponent.ItemSpriteColor;
        SpinnerItemSpriteRenderer.sprite = itemComponent.ItemSprite;
    }

    public void AlternativelyInteract(Player interactingPlayer)
    {
        if (_machineState == MachineState.SPINNING)
        {
            if (_spinnerIndex + 1 >= ItemFormSOs.Length)
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
