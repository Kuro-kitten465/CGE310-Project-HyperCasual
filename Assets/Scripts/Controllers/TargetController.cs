using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameLogicManager gameLogicManager;
    private void Update()
    {
        if (gameLogicManager.IsGameOver) return;
        
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.back, gameLogicManager.TargetRotationSpeed * Time.deltaTime);
    }
}
