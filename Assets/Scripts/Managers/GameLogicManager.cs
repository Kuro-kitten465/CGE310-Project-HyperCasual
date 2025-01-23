using System.Collections.Generic;
using KuroNeko.Utilities.DesignPattern;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    /*private int currentScore = 0;
    public int CurrentScore => currentScore;
    private int highestScore = 0;
    public int HighestScore => highestScore;

    [Header("Game Properties")]
    [SerializeField] private float knifeSpeed = 10f;
    [SerializeField] private int maxKnifeCount = 10;
    [SerializeField] private float targetRotationSpeed = 100f;
    public float TargetRotationSpeed => targetRotationSpeed;

    [Header("Config")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private TargetController target;
    [SerializeField] private float knifeStuckPosition = 0.65f;

    private readonly Queue<KnifeController> knives = new();
    private int knifeIndex = -1;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    public KnifeController SpawnKnife()
    {
        var obj = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        obj.name += $" {knifeIndex}";
        var knife = obj.GetComponent<KnifeController>();
        knife.Initialize(knifeSpeed);
        knives.Enqueue(knife);
        return knife;
    }

    public void OnKnifeHitTarget(KnifeController knife, Collider2D collider)
    {
        if (collider == null) return;

        knife.GetComponent<Rigidbody2D>().isKinematic = true;
        knife.gameObject.transform.SetParent(collider.gameObject.transform);
        knife.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        knife.transform.position = new Vector2(0, knifeStuckPosition);

        CheckLevelProgress();
        UIManager.Instance.UpdateScore();
    }

    public void OnKnifeHitFailure(KnifeController knife)
    {
        highestScore = currentScore;
        isGameOver = true;
        UIManager.Instance.ShowGameOver();
        Debug.Log("Game Over! Your score: " + highestScore);
    }

    private void CheckLevelProgress()
    {
        if (IsGameOver) return;
        currentScore++;
        knifeIndex++;

        if (knifeIndex >= maxKnifeCount)
        {
            var knife = knives.Dequeue();
            Destroy(knife.gameObject);
        }
    }*/

    [Header("Game Properties")]
    [SerializeField] private float knifeSpeed = 10f;
    [SerializeField] private int maxKnifeCount = 10;
    [SerializeField] private float targetRotationSpeed = 100f;
    public float TargetRotationSpeed => targetRotationSpeed;

    [Header("Config")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private TargetController target;
    [SerializeField] private float knifeStuckPosition = 0.65f;

    private readonly Queue<KnifeController> knives = new();
    private int knifeIndex = 0;

    private bool isGameOver = false;
    private int currentScore = 0;

    // Expose properties for external use (optional)
    public bool IsGameOver => isGameOver;
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
    }

    public KnifeController SpawnKnife()
    {
        var obj = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        obj.name = $"Knife {knifeIndex}";
        var knife = obj.GetComponent<KnifeController>();
        knife.Initialize(knifeSpeed, this);
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

        currentScore++;
        CheckLevelProgress();
        uiManager.UpdateScore(currentScore);
    }

    public void OnKnifeHitFailure(KnifeController knife)
    {
        isGameOver = true;

        uiManager.ShowGameOver(currentScore);

        if (GameManager.Instance != null)
            GameManager.Instance.HighestScore = currentScore;
    }

    private void CheckLevelProgress()
    {
        if (isGameOver) return;

        if (knifeIndex >= maxKnifeCount)
        {
            var oldKnife = knives.Dequeue();
            Destroy(oldKnife.gameObject);
        }
    }
}
