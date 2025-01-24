using UnityEngine;
using KuroNeko.Utilities.DesignPattern;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject tapText;
    [SerializeField] private TMP_Text appleText;
    [SerializeField] private TMP_Text scoreGameOverPanel;
    [SerializeField] private TMP_Text scorePanel;

    public void ShowGameOver(int score)
    {
        gameOverPanel.SetActive(true);
        scoreGameOverPanel.text = $"Score: {score}";
    }

    public void UpdateScore(int score)
    {
        scorePanel.text = $"Score: {score}";
    }

    public void UpdateApple(int apple)
    {
        if (GameManager.Instance == null) return;

        appleText.text = apple.ToString();
    }

    public void RestartGame()
    {
        SceneManagement.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        SceneManagement.LoadScene("MainMenu");
    }
}