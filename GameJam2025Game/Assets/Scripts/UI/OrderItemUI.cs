using UnityEngine;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;

    public Item _item { get; private set; }

    public void SetupOrderItemUI(Item item)
    {
        _item = item;
        itemSprite.sprite = item.ItemData.ItemSprite;
        item.ItemData.ManagerSO.ItemColorToHexColorMap.TryGetValue(item.CurrentItemColor, out var color);
        itemSprite.color = color;
        itemSprite.gameObject.SetActive(true);
    }
}
