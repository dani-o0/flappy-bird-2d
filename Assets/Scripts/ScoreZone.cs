using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool hasScored = false; // Para evitar puntos duplicados

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasScored && collision.CompareTag("Player"))
        {
            hasScored = true;
            GameManager.Instance.AddScore(1);
        }
    }
}