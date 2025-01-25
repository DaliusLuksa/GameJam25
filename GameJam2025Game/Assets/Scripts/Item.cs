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

    private int _currentItemUpgradeLevel = 0;

    public ItemType CurrentItemType => _currentItemType;
    public ItemColor CurrentItemColor => _currentItemColor;
    public Sprite ItemSprite => _currentSprite;
    public Color ItemSpriteColor => _currentSpriteColor;

    public void SetItemType(ItemType itemType)
    {
        if (!itemData.ManagerSO.ItemTypeToSpriteMap.TryGetValue(itemType, out var sprite))
        {
            Debug.LogError($"Failed to find {itemType} in {nameof(itemData.ManagerSO)}, values {itemData.ManagerSO.ItemTypeToSpriteMap.Select(x => $"{x.Key}: {x.Value}")}");
            return;
        }

        _currentItemType = itemType;
        _currentSprite = sprite;
    }

    private void Awake()
    {
        _currentItemType = itemData.ItemType;
        _currentItemColor = itemData.ItemColor;
        _currentItemUpgradeLevel = itemData.StartingUpgradeLevel;
        _currentSprite = itemData.ItemSprite;
        _currentSpriteColor = itemData.ItemSpriteColor;
    }
}

