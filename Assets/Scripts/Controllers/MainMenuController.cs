using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text appleText;

    private void Start()
    {
        bestScore.text = $"Best Score: {GameManager.Instance.HighestScore}";
        appleText.text = GameManager.Instance.Apple.ToString();
    }

    public void StartGame()
    {
        SceneManagement.LoadScene("Gameplay");
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(true);
    }

    public void OnQuit()
    {
        GameManager.Instance.SaveData();
        Application.Quit();
    }
}
