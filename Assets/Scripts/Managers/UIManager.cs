using UnityEngine;
using KuroNeko.Utilities.DesignPattern;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreGameOverPanel;
    [SerializeField] private TMP_Text scorePanel;

    public void ShowGameOver(int score)
    {
        gameOverPanel.SetActive(true);
        scoreGameOverPanel.text = $"{score}";
    }

    public void UpdateScore(int score)
    {
        scorePanel.text = $"Score: {score}";
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