using UnityEngine;

public class KnifeController : MonoBehaviour
{
    private float throwSpeed = 10f;
    private GameLogicManager gameLogicManager;

    public void Initialize(float throwSpeed, GameLogicManager gameLogicManager)
    {
        this.throwSpeed = throwSpeed;
        this.gameLogicManager = gameLogicManager;
    }

    public void Throw()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, throwSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Target"))
        {
            gameLogicManager.OnKnifeHitTarget(this, collider);
        }
        else
        {
            gameLogicManager.OnKnifeHitFailure(this);
        }
    }
}