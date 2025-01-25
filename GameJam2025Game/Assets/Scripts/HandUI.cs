using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Player _player = null;

public void onChangeHandUi(Sprite newHandImage)
{
    // Check if the GameObject has a SpriteRenderer component
    Image image = this.gameObject.GetComponent<Image>();
    if (newHandImage)
    {
        image.sprite = newHandImage;
    }
    else
    {
        Debug.LogWarning("SpriteRenderer component not found on the GameObject.");
    }
}
}
