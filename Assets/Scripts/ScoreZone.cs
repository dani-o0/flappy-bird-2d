using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool hasScored = false;

    [SerializeField] private AudioClip scoreClip;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!hasScored && collision.CompareTag("Player"))
        {
            hasScored = true;
            GameManager.Instance.AddScore(1);
            SoundManager.Instance.PlaySfx(scoreClip);
        }
    }
}