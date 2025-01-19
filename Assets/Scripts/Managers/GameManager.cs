using System.Collections.Generic;
using KuroNeko.Utilities.DesignPattern;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private TargetController target;

    private List<KnifeController> knives = new List<KnifeController>();

    public KnifeController SpawnKnife()
    {
        var obj = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        var knife = obj.GetComponent<KnifeController>();
        knife.Initialize(target.transform.position);
        knives.Add(knife);
        return knife;
    }

    public void OnKnifeHitTarget(KnifeController knife, Collider2D collider)
    {
        if (collider == null) return;

        knife.GetComponent<Rigidbody2D>().isKinematic = true;
        
        knife.gameObject.transform.SetParent(collider.gameObject.transform);
        CheckLevelProgress();
    }

    public void OnKnifeHitFailure(KnifeController knife)
    {
        Debug.Log("Game Over! Knife hit an obstacle.");
        // Restart or show ad logic
    }

    private void CheckLevelProgress()
    {
        // Logic for determining if the player completes the level
    }
}
