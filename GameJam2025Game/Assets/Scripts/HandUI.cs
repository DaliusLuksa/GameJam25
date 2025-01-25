using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Player _player = null;
    [SerializeField] private Sprite _uiImage = null;

public void onChangeHandUi(Sprite newHandImage)
{
    // Check if the GameObject has a SpriteRenderer component
    SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        // Assign the new sprite to the SpriteRenderer
        spriteRenderer.sprite = newHandImage;
    }
    else
    {
        Debug.LogWarning("SpriteRenderer component not found on the GameObject.");
    }
}
}
