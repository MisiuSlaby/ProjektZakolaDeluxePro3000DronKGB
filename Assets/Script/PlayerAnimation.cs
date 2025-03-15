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
        // Sprawdzamy kierunek prêdkoœci w osi x i ustawiamy flipX
        if (rb.velocity.x < -0.1f)
        {
            // Poruszanie w lewo
            sprite.flipX = false;  // Dostosuj ustawienie, np. true lub false w zale¿noœci od tego, która strona to przód
        }
        else if (rb.velocity.x > 0.1f)
        {
            // Poruszanie w prawo
            sprite.flipX = true; // Dostosuj zgodnie z Twoimi preferencjami
        }
    }
}