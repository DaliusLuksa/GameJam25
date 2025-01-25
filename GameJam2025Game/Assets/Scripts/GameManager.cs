using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoomStruct
{
    public ColorsEnum RoomColor;
    public Color RoomWallSpriteColor;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField] private List<RoomStruct> roomStruct = new List<RoomStruct>();
    [SerializeField] private List<Room> roomList = new List<Room>();

    public RoomStruct GetRoomStruct(ColorsEnum roomColor) => roomStruct.Find(o => o.RoomColor == roomColor);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
