using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        Invoke("DestroyInvoke", 8.0f);
    }
    
    void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    private void DestroyInvoke()
    {
        Destroy(gameObject);
    }
}