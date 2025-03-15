using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private void Awake()
    {
        // Pobranie komponentu SpriteRenderer
        sprite = GetComponent<SpriteRenderer>();

        // Pobranie komponentu Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Sprawdzamy kierunek pr�dko�ci w osi x i ustawiamy flipX
        if (rb.velocity.x < -0.1f)
        {
            // Poruszanie w lewo
            sprite.flipX = false;  // Dostosuj ustawienie, np. true lub false w zale�no�ci od tego, kt�ra strona to prz�d
        }
        else if (rb.velocity.x > 0.1f)
        {
            // Poruszanie w prawo
            sprite.flipX = true; // Dostosuj zgodnie z Twoimi preferencjami
        }
    }
}