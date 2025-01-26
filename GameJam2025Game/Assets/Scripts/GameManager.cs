using System.Collections;
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

[System.Serializable]
public struct OrderItemShitter
{
    public ItemType ItemType;
    public ItemSO ItemData;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField] private List<OrderItemShitter> orderItemShitterList = new List<OrderItemShitter>();
    [SerializeField] private OrderUIScript orderUIScript = null;
    [SerializeField] private DamageIndicator damageIndicator = null;
    [SerializeField] private List<RoomStruct> roomStruct = new List<RoomStruct>();
    [SerializeField] private List<Room> roomList = new List<Room>();
    [SerializeField] private List<PlayerShitStruct> playerInitList = new List<PlayerShitStruct>();
    // Timer for how long the day should be. In this case 3 minutes
    [SerializeField] private int currentDay = 1;
    [SerializeField] private float maxDayTimer = 180;
    [SerializeField] private float currentGameTimer = 0f;
    [SerializeField] private float changeRoomColorAfterTimer = 60;
    [SerializeField] private float currentRoomChangeTimer = 0;
    [SerializeField] private int finishedContracts = 0;
    [SerializeField] private float maxTimerForNewOrder = 20f;
    [SerializeField] private float currentTimerForNewOrder = 0f;

    private List<Order> ordersList = null;
    private bool isGameInProgress = false;
    private List<Player> playersList = new List<Player>();
    private Dictionary<Room, RoomStruct> roomTargetColors = new Dictionary<Room, RoomStruct>();
    private Dictionary<Room, RoomStruct> roomOriginalColors = new Dictionary<Room, RoomStruct>();
    // Define the available colors and shuffle them
    List<ColorsEnum> availableColors = new List<ColorsEnum> { ColorsEnum.RED, ColorsEnum.GREEN, ColorsEnum.BLUE };

    public OrderUIScript OrderUIScript => orderUIScript;
    public DamageIndicator DamageIndicator => damageIndicator;
    public int FinishedContracts => finishedContracts;
    public Player GetPlayer(int index)
    {
        if (playerInitList.Count <= 0) { return null; }

        return playersList.Find(o => o.PlayerIndex == index);
    }
    public bool IsGameInProgress() => isGameInProgress;
    public float GetCurrentDayProgressNormalized()
    {
        return currentGameTimer / maxDayTimer;
    }
    public int CurrentDay => currentDay;

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

        isGameInProgress = true;
        ordersList = new List<Order>();
        // Create first order when the game starts
        CreateNewOrder();
    }

    private void Update()
    {
        // Don't execute Update() if the game is not in progress
        if (!isGameInProgress) { return; }

        HandleGameTimer();
    }

    public void DisableRoomOnPlayerDeath(ColorsEnum color)
    {
        roomList.Find(o => o.RoomColor == color).SetIsRoomEnabled(false);
        availableColors.Remove(color);
    }

    public void ReenableAllDisabledRooms()
    {
        foreach (var room in roomList)
        {
            room.SetIsRoomEnabled(true);
        }

        // Restore available colors list
        availableColors = new List<ColorsEnum> { ColorsEnum.RED, ColorsEnum.GREEN, ColorsEnum.BLUE };
    }

    public void TryToSubmitOrder(Item item)
    {
        foreach (var order in ordersList)
        {
            if (order.IsOrderFinished(item))
            {
                finishedContracts++;
                orderUIScript.DestroyuBaduOrderu(item);
                ordersList.Remove(order);
                return;
            }
        }
    }

    private void CreateNewOrder()
    {
        var newOrdaru = new Order(currentDay, orderItemShitterList);
        orderUIScript.CreateNewOrder(newOrdaru.ItemGoal);
        ordersList.Add(newOrdaru);
    }

    private void HandleGameTimer()
    {
        currentGameTimer += Time.deltaTime;
        currentRoomChangeTimer += Time.deltaTime;
        currentTimerForNewOrder += Time.deltaTime;
        if (currentGameTimer >= maxDayTimer)
        {
            // Finished the day (level completed)
            isGameInProgress = false;
            StartCoroutine(PrepareTheNextDay());
        }

        if (isGameInProgress && currentRoomChangeTimer >= changeRoomColorAfterTimer)
        {
            currentRoomChangeTimer = 0;
            // Should change the room colors
            InitiateRoomColorSwitch();
        }

        if (isGameInProgress && currentTimerForNewOrder >= maxTimerForNewOrder)
        {
            currentTimerForNewOrder = 0;
            CreateNewOrder();
        }
    }

    private IEnumerator PrepareTheNextDay()
    {
        yield return new WaitForSeconds(1.5f);

        // Revive all dead players
        foreach (var player in playersList)
        {
            var player_health = player.GetComponent<Player_Health>();
            if (!player_health.IsAlive())
            {
                player_health.Revive();
            }
        }

        // Reenable all rooms
        ReenableAllDisabledRooms();

        yield return new WaitForSeconds(5f);

        currentDay++;
        currentGameTimer = 0;
        // Force rooms to change color at the start of the next day
        currentRoomChangeTimer = changeRoomColorAfterTimer;
        isGameInProgress = true;
    }

    private void InitiateRoomColorSwitch()
    {
        // Assign new colors for each room
        AssignNewRoomColors();

        // Start coroutine to animate color switching
        StartCoroutine(FlashRoomColors());
    }

    private void AssignNewRoomColors()
    {
        var copyOfAvailableColors = new List<ColorsEnum>(availableColors);
        ShuffleList(copyOfAvailableColors);

        // Ensure we do not exceed the number of available colors
        //int maxRoomsToColor = Mathf.Min(roomList.Count, copyOfAvailableColors.Count);
        int maxRoomsToColor = roomList.Count;

        // Assign a unique color to each room
        for (int i = 0; i < maxRoomsToColor; i++)
        {
            // Skip disabled room
            if (!roomList[i].IsRoomEnabled) { continue; }

            // Select the first color
            ColorsEnum selectedColor = copyOfAvailableColors[0];
            // Delete it afterwards
            copyOfAvailableColors.RemoveAt(0);
            RoomStruct newRoomStruct = GetRoomStruct(selectedColor);

            // Save the original color
            roomOriginalColors[roomList[i]] = roomList[i].GetCurrentRoomStruct();

            // Save the target color
            roomTargetColors[roomList[i]] = newRoomStruct;

            // Disable room damage while it's changing the color
            roomList[i].SetShouldDamagePlayer(false);
        }
    }

    private IEnumerator FlashRoomColors()
    {
        float totalDuration = 5f; // Total duration of the flashing effect
        float interval = 1f;    // Time between flashes
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            foreach (var roomPair in roomTargetColors)
            {
                Room room = roomPair.Key;
                RoomStruct originalRoomStruct = roomOriginalColors[room];
                RoomStruct targetRoomStruct = roomTargetColors[room];

                // Lerp to the target color
                StartCoroutine(LerpRoomColor(room, originalRoomStruct, targetRoomStruct, interval));
            }

            yield return new WaitForSeconds(interval * 2);
            elapsedTime += interval * 2; // Account for both lerp intervals
        }

        // After flashing, set the rooms to their final colors
        foreach (var roomPair in roomTargetColors)
        {
            Room room = roomPair.Key;
            room.ChangeRoomColor(roomTargetColors[room]);
            // Reenable room damage
            room.SetShouldDamagePlayer(true);
        }

        // Clear dictionaries as the transition is complete
        roomTargetColors.Clear();
        roomOriginalColors.Clear();
    }

    private IEnumerator LerpRoomColor(Room room, RoomStruct from, RoomStruct to, float duration, bool repeat = true)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            Color lerpedColor = Color.Lerp(from.RoomWallSpriteColor, to.RoomWallSpriteColor, t);
            room.SetRoomWallSpriteColor(lerpedColor);
            yield return null;
        }

        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            Color lerpedColor = Color.Lerp(to.RoomWallSpriteColor, from.RoomWallSpriteColor, t);
            room.SetRoomWallSpriteColor(lerpedColor);
            yield return null;
        }
    }

    // Utility method to shuffle a list
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
