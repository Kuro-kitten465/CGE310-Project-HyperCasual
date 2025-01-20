using UnityEngine;

public class KnifeController : MonoBehaviour
{
    private float throwSpeed = 10f;

    public void Initialize(float throwSpeed)
    {
        this.throwSpeed = throwSpeed;
    }

    public void Throw()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, throwSpeed);
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