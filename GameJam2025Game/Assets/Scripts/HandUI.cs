using UnityEngine;
using UnityEngine.UI;

public class HandUI : MonoBehaviour
{
    [SerializeField] private int playerIndex = 1;
    private Player _player = null;
    private Image _image = null;

    void Start()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        _player = GameManager.Instance.GetPlayer(playerIndex);
        if (_player != null)
        {
            if (_player.GetHeldItem() == null)
            {
                _image.gameObject.SetActive(false);
            }
            else
            {
                _image.sprite = _player.GetHeldItem().ItemSprite;
                _image.color = _player.GetHeldItem().ItemSpriteColor;
                _image.gameObject.SetActive(true);
            }
        }
    }
}
