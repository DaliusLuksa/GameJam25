using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    [SerializeField] private Item pickableItemPrefab = null;

    public void Interact(Player interactingPlayer)
    {
        Debug.Log($"Table gave item to the player... Started by - {interactingPlayer}");
        Item spawnedItem = Instantiate(pickableItemPrefab);
        interactingPlayer.GiveItem(spawnedItem);
    }
}
