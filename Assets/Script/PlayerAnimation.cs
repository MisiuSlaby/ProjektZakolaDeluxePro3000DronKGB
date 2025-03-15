using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator; // Referencja do Animatora
    [SerializeField] private PlayerMove pmove; // Referencja do komponentu PlayerMove

    private void Awake()
    {
        // Pobranie komponentów
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Pobranie komponentu Animator (upewnij siê, ¿e komponent jest dodany do obiektu)

        // Jeœli nie przypisaliœmy komponentu w Inspectorze, spróbujemy go znaleŸæ na tym samym obiekcie.
        if (pmove == null)
        {
            pmove = GetComponent<PlayerMove>();
        }
    }

    private void Update()
    {
        float horizontalVelocity = rb.velocity.x;
        float verticalVelocity = rb.velocity.y;

        // Obs³uga kierunku postaci
        if (horizontalVelocity < -0.5f)
        {
            // Poruszanie w lewo
            sprite.flipX = false;
        }
        else if (horizontalVelocity > 0.5f)
        {
            // Poruszanie w prawo
            sprite.flipX = true;
        }

        // Ustawienie parametru animacji isWalking na podstawie prêdkoœci w osi x
        bool isWalking = Mathf.Abs(horizontalVelocity) > 0.1f;
        bool isJumping = Mathf.Abs(verticalVelocity) > 0.1f;


        // Sprawdzenie, czy postaæ siê porusza oraz czy isTouchDown jest false
        if (isWalking && pmove.isTouchDown && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            // Ustawienie animacji "Jumping"
            animator.SetBool("Jumping", false);
            animator.SetBool("isWalking", isWalking);
            Debug.Log('1');
        }
        else if (isJumping)
        {
            Debug.Log('2');
            animator.SetBool("isWalking", false);
            animator.SetBool("Jumping", isJumping);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("Jumping", false);
        }
    }
}
