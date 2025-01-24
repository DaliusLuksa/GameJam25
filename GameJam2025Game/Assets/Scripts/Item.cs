using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemData = null;
    private ItemType _currentItemType = ItemType.Circle;
    private ItemColor _currentItemColor = ItemColor.White;
    private int _currentItemUpgradeLevel = 0;

    public ItemType CurrentItemType => _currentItemType;
    public ItemColor CurrentItemColor => _currentItemColor;
    public Sprite ItemSprite => itemData.ItemSprite;
    public Color ItemSpriteColor => itemData.ItemSpriteColor;

    private void Awake()
    {
        _currentItemType = itemData.ItemType;
        _currentItemColor = itemData.ItemColor;
        _currentItemUpgradeLevel = itemData.StartingUpgradeLevel;
    }
}
