using UnityEngine;

public class TargetController : MonoBehaviour
{
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.back, GameManager.Instance.TargetRotationSpeed * Time.deltaTime);
    }
}
