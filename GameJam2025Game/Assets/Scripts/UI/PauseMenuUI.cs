using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject root;

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = root.activeSelf ? 1f : 0f;
            root.SetActive(!root.activeSelf);
        }
    }
}
