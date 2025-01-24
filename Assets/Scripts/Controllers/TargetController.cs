using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameLogicManager gameLogicManager;
    [Header("Rotation Settings")]
    public float baseRotationSpeed = 50f; // Initial rotation speed
    public float maxRotationSpeed = 300f; // Maximum rotation speed
    private float currentRotationSpeed;
    private void Start()
    {
        currentRotationSpeed = baseRotationSpeed;
    }

    private void Update()
    {
        if (gameLogicManager.IsGameOver) return;
        if (!gameLogicManager.IsGameStarted) return;
        
        UpdateRotationSpeed();
        Rotate();
    }
    private void UpdateRotationSpeed()
    {
        currentRotationSpeed = Mathf.Min(baseRotationSpeed * (1 + gameLogicManager.CurrentScore / 100f), maxRotationSpeed);
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.back, currentRotationSpeed * Time.deltaTime);
    }
}
