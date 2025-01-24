using UnityEngine;
using KuroNeko.Utilities.DesignPattern;

public class GameManager : MonoSingleton<GameManager>
{
    private int highestScore = 0;
    public int HighestScore
    {
        get => highestScore;
        set => highestScore = value;
    }

    private int apple = 0;
    public int Apple
    {
        get => apple;
        set => apple = value;
    }


    protected override void Awake()
    {
        base.Awake();
        LoadSavedData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.SetInt("Apple", apple);
        Debug.Log("Data saved!");
    }

    public void LoadSavedData()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        apple = PlayerPrefs.GetInt("Apple", 0);
        Debug.Log("Data loaded!");
    }
}