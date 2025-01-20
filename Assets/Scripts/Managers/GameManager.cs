using System.Collections.Generic;
using KuroNeko.Utilities.DesignPattern;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int currentScore = 0;
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

    public KnifeController SpawnKnife()
    {
        var obj = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        obj.gameObject.name += $" {knifeIndex}";
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
    }

    public void OnKnifeHitFailure(KnifeController knife)
    {
        highestScore = currentScore;
        Debug.Log("Game Over! Your score: " + highestScore);
    }

    private void CheckLevelProgress()
    {
        currentScore++;
        knifeIndex++;

        if (knifeIndex >= maxKnifeCount)
        {
            var knife = knives.Dequeue();
            Destroy(knife.gameObject);
        }
    }
}
