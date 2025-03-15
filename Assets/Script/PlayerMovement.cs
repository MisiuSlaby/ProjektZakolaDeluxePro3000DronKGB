using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    // Ustawienia
    [SerializeField] private float moveSpeed = 5f;

    // Infromowanie o stanie dotkniêcia mo¿na debugowaæ ale ogólnie s¹ zbêdne w inspektorze
    [SerializeField] private bool isTouchUp = true;
    [SerializeField] private bool isTouchLeft = true;
    [SerializeField] private bool isTouchRight = true;
    [SerializeField] private bool isTouchDown = true;

    // Detektory
    [SerializeField] private Transform upTouchCheck;
    [SerializeField] private Transform leftTouchCheck;
    [SerializeField] private Transform rightTouchCheck;
    [SerializeField] private Transform downTouchCheck;

    // Elementy
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Typy powieszchni
    [SerializeField] private LayerMask surfaceLayers;
    public UnityEngine.Vector2 sideBoxSize = new UnityEngine.Vector2(1f, 2f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sprawdzenie czy istnieje fizyka
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Sprawdzenie kolizji w ró¿nych punktach
        //isTouchUp = Physics2D.OverlapCircle(upTouchCheck.position, 0.2f, surfaceLayers);
        isTouchLeft = Physics2D.OverlapBox(leftTouchCheck.position, sideBoxSize, 0f, surfaceLayers);
        // Detekcja po prawej stronie
        isTouchRight = Physics2D.OverlapBox(rightTouchCheck.position, sideBoxSize, 0f, surfaceLayers);
        isTouchDown = Physics2D.OverlapCircle(downTouchCheck.position, 0.2f, surfaceLayers);

        // Je¿eli gracz nie przytrzymuje spacji oraz postaæ dotyka pod³o¿a (nie ma w powietrzu)
        if (!Input.GetKey(KeyCode.Space) && isTouchDown)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                float moveInput = Input.GetAxis("Horizontal");
                rb.velocity = new UnityEngine.Vector2(moveInput * moveSpeed, rb.velocity.y);
                //Debug.Log("Postaæ wykonuje ruch!");
                if (Input.GetKey(KeyCode.A))
                {
                    sprite.flipX = false;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    sprite.flipX = true;
                }
            }
            //else
            //{
            //    rb.velocity = new UnityEngine.Vector2(0f, rb.velocity.y);
            //}
        }
        else if (!isTouchDown && isTouchLeft == true || isTouchRight == true)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x * -1f, rb.velocity.y);
        }
    }
}