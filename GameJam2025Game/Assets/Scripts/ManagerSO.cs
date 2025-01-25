using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ManagerSO/New Item")]
public class ManagerSO : ScriptableObject
{
    [SerializedDictionary]
    public SerializedDictionary<ItemType, Sprite> ItemTypeToSpriteMap = null;
    [SerializedDictionary]
    public SerializedDictionary<ItemType, ItemType> FormToBubbleTypesMap = null;
    [SerializedDictionary]
    public SerializedDictionary<ItemType, ItemColor> PaintTypeToItemColorMap = null;
    [SerializedDictionary]
    public SerializedDictionary<ItemColor, Color> ItemColorToHexColorMap = null;

    [SerializeField]
    public ItemType[] FormTypes = null;
    [SerializeField]
    public ItemType[] BubbleTypes = null;
    [SerializeField]
    public ItemType[] PaintTypes = null;

    [SerializeField]
    public int MAX_UPGRADE_LEVEL = 2;

}