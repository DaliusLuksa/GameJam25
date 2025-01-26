using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool ShouldDamagePlayer { get; private set; } = true;
    // This shit decides if the room damages the player or not. Probably will also be used to see if we need to set room wall colors to white because
    // it's suppose to be neutral room when it's disabled.
    public bool IsRoomEnabled { get; private set; } = true;

    [SerializeField] private ColorsEnum roomColor = ColorsEnum.UNKNOWN;
    [SerializeField] private List<GameObject> roomWalls = new List<GameObject>();

    private Player_Health player = null;

    public ColorsEnum RoomColor => roomColor;

    public void SetShouldDamagePlayer(bool value)
    {
        ShouldDamagePlayer = value;
    }

    public void SetIsRoomEnabled(bool value)
    {
        // Only proceed if it's a different value for the currenty set one.
        if (IsRoomEnabled == value) { return; }

        IsRoomEnabled = value;
        UpdateRoomStuff();
    }

    private void Start()
    {
        UpdateRoomStuff();
    }

    private void Update()
    {
        if (IsRoomEnabled && ShouldDamagePlayer && player != null && player.IsAlive() && player.IsDamageable(roomColor))
        {
            player.TakeDamage(Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player_Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    public void ChangeRoomColor(RoomStruct newRoomColor)
    {
        if (!IsRoomEnabled) { return; }

        roomColor = newRoomColor.RoomColor;
        UpdateRoomStuff();
    }

    private void UpdateRoomStuff()
    {
        // Update wall colors
        foreach (GameObject wall in roomWalls)
        {
            SpriteRenderer wallSR = wall.GetComponent<SpriteRenderer>();
            wallSR.color = IsRoomEnabled ? GameManager.Instance.GetRoomStruct(roomColor).RoomWallSpriteColor : Color.white;
        }
    }

    public RoomStruct GetCurrentRoomStruct()
    {
        return GameManager.Instance.GetRoomStruct(roomColor);
    }

    public void SetRoomWallSpriteColor(Color roomColor)
    {
        if (!IsRoomEnabled) { return; }

        foreach (GameObject wall in roomWalls)
        {
            SpriteRenderer wallSR = wall.GetComponent<SpriteRenderer>();
            wallSR.color = roomColor;
        }
    }
}
