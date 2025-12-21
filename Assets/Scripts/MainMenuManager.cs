using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Components")]
    public Button startButton;
    public Button quitButton;
    public TextMeshProUGUI recordText;

    [Header("Settings")]
    public string gameSceneName = "GameScene";

    void Start()
    {
        UpdateRecordDisplay();

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void UpdateRecordDisplay()
    {
        if (recordText != null)
        {
            int record = PlayerPrefs.GetInt("HighScore", 0);
            recordText.text = $"Record: {record}";
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}