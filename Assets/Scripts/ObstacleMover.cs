using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento

    void Update()
    {
        if (!GameManager.Instance.IsGameOver())
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}