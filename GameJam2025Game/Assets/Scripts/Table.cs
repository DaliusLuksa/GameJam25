using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public void Interact(Player interactingPlayer)
    {
        Debug.Log($"Table started working... Started by - {interactingPlayer}");
    }
}
