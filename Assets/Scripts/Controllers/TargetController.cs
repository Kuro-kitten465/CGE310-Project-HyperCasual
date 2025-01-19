using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
}
