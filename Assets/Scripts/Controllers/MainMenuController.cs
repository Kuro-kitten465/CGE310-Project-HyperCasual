using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;

    public void StartGame()
    {
        SceneManagement.LoadScene("Gameplay");
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(true);
    }
}
