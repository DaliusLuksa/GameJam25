using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DayProgressUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText = null;
    [SerializeField] private TextMeshProUGUI finishedContractsText = null;
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

        finishedContractsText.text = $"Finished contracts: {_gameManager.FinishedContracts}";
        dayText.text = $"Day: {_gameManager.CurrentDay}";
        float value = _gameManager.GetCurrentDayProgressNormalized();
        _progressBarSlider.value = value;
    }
}
