using UnityEngine;

[CreateAssetMenu(menuName = "Item/New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite itemSprite = null;
    [SerializeField] private Color itemSpriteColor = Color.white;
    [SerializeField] private ItemType itemType = ItemType.Circle;
    [SerializeField] private ItemColor itemColor = ItemColor.White;
    [SerializeField] private int startingUpgradeLevel = 0;

    public ItemType ItemType => itemType;
    public ItemColor ItemColor => itemColor;
    public Sprite ItemSprite => itemSprite;
    public Color ItemSpriteColor => itemSpriteColor;
    public int StartingUpgradeLevel => startingUpgradeLevel;
}
