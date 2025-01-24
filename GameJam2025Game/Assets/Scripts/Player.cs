using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerInteractPromptImage = null;
    [SerializeField] private KeyCode playerInteractKeycode = KeyCode.E;

    private IInteractable _currInteractableObject = null;

    private void Update()
    {
        HandleInteractablePrompt();
        HandlePlayerInteractInput();
    }

    private void HandleInteractablePrompt()
    {
        playerInteractPromptImage.gameObject.SetActive(_currInteractableObject != null);
    }

    private void HandlePlayerInteractInput()
    {
        if (_currInteractableObject != null)
        {
            if (Input.GetKeyDown(playerInteractKeycode))
            {
                _currInteractableObject.Interact(this);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"Player hit interactable object - {collision.gameObject}");
            _currInteractableObject = collision.gameObject.GetComponent<IInteractable>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"Player left interactable object - {collision.gameObject}");
            _currInteractableObject = null;
        }
    }
}
