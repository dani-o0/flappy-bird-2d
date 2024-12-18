using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private float rotacionMaxima = 45f;
    private float velocidadRotacion = 5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;
        
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }
        
        float rotacion = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Sign(rb.velocity.y) * rotacionMaxima, velocidadRotacion * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotacion);
    }

    void Flap()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void GotHit()
    {
        animator.SetTrigger("isHit");
        
        rb.velocity = Vector2.zero;
        rb.simulated = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsPlaying())
            return;
        
        GotHit();
        GameManager.Instance.SetGameState(GameState.STATE_GAMEOVER);
    }
}