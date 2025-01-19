using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] private float throwSpeed = 10f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isThrown = false;

    public void Initialize(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        isMoving = true;
    }

    public void Throw()
    {
        isThrown = true;
        GetComponent<Rigidbody2D>().velocity = targetPosition.normalized * throwSpeed;
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
        
        if (transform.position == targetPosition)
        {
            isMoving = false;
            GameManager.Instance.OnKnifeHitTarget(this, null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Target"))
        {
            GameManager.Instance.OnKnifeHitTarget(this, collider);
        }
        else
        {
            GameManager.Instance.OnKnifeHitFailure(this);
        }
    }
}