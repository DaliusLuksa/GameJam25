using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FormUser : MonoBehaviour, IInteractable
{

    [SerializeField] private PlaceableInventory LeftMachineInput = null;
    [SerializeField] private PlaceableInventory RightMachineInput = null;

    [SerializeField] private ManagerSO _managerSO = null;


    private void Awake()
    {

    }

    public void Interact(Player interactingPlayer)
    {
        var form = LeftMachineInput.GetItem();
        var bubble = RightMachineInput.GetItem();
        var isLeftInputValid =  form != null && _managerSO.FormTypes.Contains(form.CurrentItemType);
        var isRightInputValid = bubble !=null && _managerSO.BubbleTypes.Contains(bubble.CurrentItemType);

        if (isLeftInputValid && isRightInputValid && interactingPlayer.HasSpaceInInventory())
        {
            LeftMachineInput.RemoveItem();
            RightMachineInput.RemoveItem();
            var builtItemType = _managerSO.FormToBubbleTypesMap.FirstOrDefault(x => x.Key == form.CurrentItemType).Value;
            if (builtItemType == ItemType.None)
            {
                Debug.LogError($"Failed to build bubble from form, cannot find mapping of {form.CurrentItemType} to bubble.");
                return;
            }
            bubble.SetItemType(builtItemType);
            bubble.DaliusSetItemType(builtItemType);
            interactingPlayer.GiveItem(bubble);
        }
    }
}
