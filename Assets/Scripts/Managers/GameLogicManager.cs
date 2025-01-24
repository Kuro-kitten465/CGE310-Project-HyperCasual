using System.Collections.Generic;
using KuroNeko.Utilities.DesignPattern;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    [Header("Game Configuration")]
    [SerializeField] private float knifeSpeed = 10f;
    [SerializeField] private int maxKnifeCount = 10;
    [SerializeField] private float targetRotationSpeed = 100f;
    public float TargetRotationSpeed => targetRotationSpeed;
    [SerializeField] private float knifeStuckPosition = 0.65f;

    [Header("Audio")]
    [SerializeField] private AudioSource targetHitSource;
    [SerializeField] private AudioSource knifeThrowSource;
    [SerializeField] private AudioSource knifeHitSource;
    public AudioSource tapSource;

    [Header("Properties")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private TargetController target;

    private readonly Queue<KnifeController> knives = new();
    private int knifeIndex = 0;
    private int knifeCount = 0;

    private bool isGameOver = false;
    private bool isGameStarted = false;
    private int currentScore = 0;
    private int appleCount = 0;

    // Expose properties for external use (optional)
    public bool IsGameOver => isGameOver;
    public bool IsGameStarted
    {
        get => isGameStarted;
        set => isGameStarted = value;
    }
    public int CurrentScore => currentScore;

    private void Start()
    {
        InitializeGameplay();
    }

    private void InitializeGameplay()
    {
        isGameOver = false;
        currentScore = 0;
        knifeIndex = 0;
        isGameStarted = false;
        appleCount = GameManager.Instance.Apple;
        uiManager.UpdateApple(appleCount);
    }

    public KnifeController SpawnKnife()
    {
        var obj = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        obj.name = $"Knife {knifeIndex}";
        var knife = obj.GetComponent<KnifeController>();
        knife.Initialize(knifeSpeed, this);
        knifeThrowSource.Play();
        knives.Enqueue(knife);
        knifeIndex++;
        return knife;
    }

    public void OnKnifeHitTarget(KnifeController knife, Collider2D collider)
    {
        if (collider == null) return;

        knife.GetComponent<Rigidbody2D>().isKinematic = true;
        knife.gameObject.transform.SetParent(collider.gameObject.transform);
        knife.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        knife.transform.position = new Vector2(0, knifeStuckPosition);

        targetHitSource.Play();

        CheckLevelProgress();
        uiManager.UpdateScore(currentScore);
    }

    public void OnKnifeHitFailure(KnifeController knife)
    {
        isGameOver = true;

        knifeHitSource.Play();

        uiManager.ShowGameOver(currentScore);

        GameManager.Instance.Apple = appleCount;

        if (currentScore > GameManager.Instance.HighestScore)
        {
            GameManager.Instance.HighestScore = currentScore;
            GameManager.Instance.SaveData();
        }
    }

    private void CheckLevelProgress()
    {
        if (isGameOver) return;

        currentScore++;
        knifeCount++;
        if (knifeIndex >= maxKnifeCount)
        {
            var oldKnife = knives.Dequeue();
            Destroy(oldKnife.gameObject);
        }

        if (knifeCount >= maxKnifeCount * 2)
        {
            if (GameManager.Instance == null) return;

            appleCount++;
            uiManager.UpdateApple(appleCount);
            knifeCount = 0;
        }
    }
}
