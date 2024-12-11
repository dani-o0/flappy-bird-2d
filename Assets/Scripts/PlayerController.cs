using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    private Rigidbody2D rb;     // Referencia al Rigidbody2D
    private Animator animator;  // Referencia al Animator
    private bool isGameActive = true; // Estado del juego
    private float rotacionMaxima = 45f; // Ángulo máximo de rotación hacia arriba
    private float velocidadRotacion = 5f; // Velocidad de rotación al cambiar de ángulo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGameActive)
        {
            // Detectar toque en pantalla o clic para pruebas en Editor
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                Flap();
            }
            
            float rotacion = Mathf.LerpAngle(transform.eulerAngles.z, Mathf.Sign(rb.velocity.y) * rotacionMaxima, velocidadRotacion * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, rotacion);
        }
    }

    void Flap()
    {
        // Reiniciar la velocidad vertical y aplicar fuerza hacia arriba
        rb.velocity = Vector2.zero; // Detiene el movimiento vertical actual
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        Debug.Log("Flap!");
    }

    public void GotHit()
    {
        // Cambiar a la animación de "golpeado"
        animator.SetTrigger("isHit");

        // Desactivar controles y detener el juego
        isGameActive = false;
        rb.velocity = Vector2.zero; // Detener movimiento
        rb.simulated = false;       // Detener físicas
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameActive)
        {
            Debug.Log($"Colisión con: {collision.gameObject.name}");
            GotHit();
            GameManager.Instance.GameOver(); // Llamar al GameManager para manejar el fin del juego
        }
    }
}