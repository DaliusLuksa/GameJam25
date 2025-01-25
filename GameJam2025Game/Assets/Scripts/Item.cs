using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemData = null;

    private ItemType _currentItemType = ItemType.Circle;
    private ItemColor _currentItemColor = ItemColor.White;
    private Sprite _currentSprite;
    private Color _currentSpriteColor;

    public int CurrentItemUpgradeLevel { get; private set; } = 0;

    public ItemType CurrentItemType => _currentItemType;
    public ItemColor CurrentItemColor => _currentItemColor;
    public Sprite ItemSprite => _currentSprite;
    public Color ItemSpriteColor => _currentSpriteColor;

    public void SetItemType(ItemType itemType)
    {
        _currentItemType = itemType;

        if(itemType == ItemType.Complex)
        {
            return;
        }

        if (!itemData.ManagerSO.ItemTypeToSpriteMap.TryGetValue(itemType, out var sprite))
        {
            Debug.LogError($"Failed to find {itemType} in {nameof(itemData.ManagerSO)}, values {itemData.ManagerSO.ItemTypeToSpriteMap.Select(x => $"{x.Key}: {x.Value}")}");
            return;
        }

        _currentSprite = sprite;
    }

    public void SetItemColor(ItemColor itemColor)
    {
        if(_currentItemType == ItemType.Complex)
        {
            Debug.LogError("what the fuck are you doing");
            return;
        }

        _currentItemColor = itemColor;

        if (!itemData.ManagerSO.ItemColorToHexColorMap.TryGetValue(itemColor, out var color))
        {
            Debug.LogError($"Failed to find {itemColor} in {nameof(itemData.ManagerSO)}, values {itemData.ManagerSO.ItemColorToHexColorMap.Select(x => $"{x.Key}: {x.Value}")}");
            return;
        }
        _currentSpriteColor = color;
    }

    public void EnlargeItemLevel()
    {
        CurrentItemUpgradeLevel++;
    }

    //SHOULD ONLY BE USED WITH COMPLEX TYPES.
    public void SetItemSprite (Sprite sprite)
    {
        _currentSprite = sprite;
    }

    private void Awake()
    {
        _currentItemType = itemData.ItemType;
        _currentItemColor = itemData.ItemColor;
        CurrentItemUpgradeLevel = itemData.StartingUpgradeLevel;
        _currentSprite = itemData.ItemSprite;
        _currentSpriteColor = itemData.ItemSpriteColor;
    }
}

