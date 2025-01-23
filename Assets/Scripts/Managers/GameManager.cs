using UnityEngine;
using KuroNeko.Utilities.DesignPattern;

public class GameManager : MonoSingleton<GameManager>
{
    private int highestScore = 0;
    public int HighestScore
    {
        get => highestScore;
        set
        {
            highestScore = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        LoadSavedData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
    }

    public void LoadSavedData()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }
}