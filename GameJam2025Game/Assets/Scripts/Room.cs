using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private ColorsEnum roomColor = ColorsEnum.UNKNOWN;
    [SerializeField] private List<GameObject> roomWalls = new List<GameObject>();

    private Player_Health player = null;

    private void Start()
    {
        UpdateRoomStuff();
    }

    private void Update()
    {
        if (player != null && player.IsAlive() && player.IsDamageable(roomColor))
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
        roomColor = newRoomColor.RoomColor;
        UpdateRoomStuff();
    }

    private void UpdateRoomStuff()
    {
        // Update wall colors
        foreach (GameObject wall in roomWalls)
        {
            SpriteRenderer wallSR = wall.GetComponent<SpriteRenderer>();
            wallSR.color = GameManager.Instance.GetRoomStruct(roomColor).RoomWallSpriteColor;
        }
    }

    public RoomStruct GetCurrentRoomStruct()
    {
        return GameManager.Instance.GetRoomStruct(roomColor);
    }

    public void SetRoomWallSpriteColor(Color roomColor)
    {
        foreach (GameObject wall in roomWalls)
        {
            SpriteRenderer wallSR = wall.GetComponent<SpriteRenderer>();
            wallSR.color = roomColor;
        }
    }
}
