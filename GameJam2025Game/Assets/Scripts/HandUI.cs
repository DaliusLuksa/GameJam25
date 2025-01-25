using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Player _player = null;

    void Start()
    {
        this.gameObject.SetActive(false);
    }

public void onChangeHandUi(Sprite newHandImage)
{
    // Check if the GameObject has a SpriteRenderer component
    Image image = this.gameObject.GetComponent<Image>();
    if (newHandImage)
    {
        image.sprite = newHandImage;
        this.gameObject.SetActive(true);
    }
    else
    {
        this.gameObject.SetActive(false);
        Debug.LogWarning("SpriteRenderer component not found on the GameObject.");
    }
}
}
