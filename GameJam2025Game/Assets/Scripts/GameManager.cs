using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoomStruct
{
    public ColorsEnum RoomColor;
    public Color RoomWallSpriteColor;
}

[System.Serializable]
public struct PlayerShitStruct
{
    public Player PlayerPrefab;
    public Transform PlayerSpawnPosition;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField] private List<RoomStruct> roomStruct = new List<RoomStruct>();
    [SerializeField] private List<Room> roomList = new List<Room>();
    [SerializeField] private List<PlayerShitStruct> playerInitList = new List<PlayerShitStruct>();

    private List<Player> playersList = new List<Player>();

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

    private void Start()
    {
        foreach (PlayerShitStruct player in playerInitList)
        {
            Player newPlayer = Instantiate(player.PlayerPrefab, player.PlayerSpawnPosition.position, Quaternion.identity);
            playersList.Add(newPlayer);
        }
    }
}
