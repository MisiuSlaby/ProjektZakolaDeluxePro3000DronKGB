using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator; // Referencja do Animatora
    [SerializeField] private PlayerMove pmove; // Referencja do komponentu PlayerMove

    private void Awake()
    {
        // Pobranie komponent�w
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Pobranie komponentu Animator (upewnij si�, �e komponent jest dodany do obiektu)

        // Je�li nie przypisali�my komponentu w Inspectorze, spr�bujemy go znale�� na tym samym obiekcie.
        if (pmove == null)
        {
            pmove = GetComponent<PlayerMove>();
        }
    }

    private void Update()
    {
        float horizontalVelocity = rb.velocity.x;
        float verticalVelocity = rb.velocity.y;

        // Obs�uga kierunku postaci
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

        // Ustawienie parametru animacji isWalking na podstawie pr�dko�ci w osi x
        bool isWalking = Mathf.Abs(horizontalVelocity) > 0.1f;
        bool isJumping = Mathf.Abs(verticalVelocity) > 0.1f;


        // Sprawdzenie, czy posta� si� porusza oraz czy isTouchDown jest false
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
