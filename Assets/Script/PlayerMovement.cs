using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Ustawienia
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool snapJump = false;
    [SerializeField] private bool jumpUp = true;
    [SerializeField] private bool jumpLeft = false;
    [SerializeField] private bool jumpRight = false;
    [SerializeField] private bool jumpDown = false;

    // Infromowanie o stanie dotkniêcia mo¿na debugowaæ ale ogólnie s¹ zbêdne w inspektorze
    [SerializeField] private bool isTouchUp = true;
    [SerializeField] private bool isTouchLeft = true;
    [SerializeField] private bool isTouchRight = true;
    [SerializeField] private bool isTouchDown = true;
    [SerializeField] private int lastTouchSurfaceType = 0; // Przyda siê do double jumpa itp

    // Detektory
    [SerializeField] private Transform upTouchCheck;
    [SerializeField] private Transform leftTouchCheck;
    [SerializeField] private Transform rightTouchCheck;
    [SerializeField] private Transform downTouchCheck;

    // Elementy
    private Rigidbody2D rb;

    // Typy powieszchni
    [SerializeField] private LayerMask surfaceLayer;
    [SerializeField] private LayerMask iceLayer;
    [SerializeField] private LayerMask mudLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sprawdzenie czy istnieje fizyka
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new UnityEngine.Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Sprawdzenie z czym koliduje gracz
        isTouchUp = Physics2D.OverlapCircle(upTouchCheck.position, 0.2f, surfaceLayer);
        isTouchLeft = Physics2D.OverlapCircle(leftTouchCheck.position, 0.2f, surfaceLayer);
        isTouchRight = Physics2D.OverlapCircle(rightTouchCheck.position, 0.2f, surfaceLayer);
        isTouchDown = Physics2D.OverlapCircle(downTouchCheck.position, 0.2f, surfaceLayer);

        // SnapJump
        float jumpPower;
        if (snapJump == true)
        {
            jumpPower = 500f;
        }
        else
        {
            jumpPower = jumpForce;
        }

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && isTouchDown && jumpUp)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetKeyDown(KeyCode.A) && isTouchRight && jumpLeft)
        {
            rb.velocity = new UnityEngine.Vector2(jumpPower * -1f, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.D) && isTouchLeft && jumpRight)
        {
            rb.velocity = new UnityEngine.Vector2(jumpPower, rb.velocity.y);
        }
        //if (Input.GetKeyDown(KeyCode.S) && isTouchUp && jumpDown)
        //{
        //    rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower * -1f);
        //}
    }
}