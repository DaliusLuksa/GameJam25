using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DayProgressUI : MonoBehaviour
{
    private Slider _progressBarSlider = null;
    private GameManager _gameManager = null;

    private void Awake()
    {
        _progressBarSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (!_gameManager.IsGameInProgress()) { return; }

        float value = _gameManager.GetCurrentDayProgressNormalized();
        _progressBarSlider.value = value;
    }
}
