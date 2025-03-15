using UnityEngine;

public class AngleShooter : MonoBehaviour
{
    [Header("Opcje wyboru k�ta")]
    [Tooltip("Czy tryb wyboru k�ta jest aktywny.")]
    public bool canSelectAngle = true;

    [Tooltip("Czas cyklu zmiany k�ta. Im mniejsza warto��, tym szybciej zmienia.")]
    public float angleCycleTime = 1f;

    [Header("Si�a skoku")]
    [Tooltip("Wielko�� si�y dodawanej do obiektu przy skoku.")]
    public float shootForce = 10f;

    // Aktualny k�t wyboru (w stopniach)
    private float currentAngle = 90f;
    // Czas rozpocz�cia przytrzymania spacji
    private float startTime;
    // Flaga sprawdzaj�ca, czy spacja jest przytrzymywana
    private bool isHoldingSpace = false;

    // Odniesienie do komponentu Rigidbody2D
    private Rigidbody2D rb;

    [SerializeField] private GameObject Arrow;
    [SerializeField] ArrowController playerArrow;

    [SerializeField] private GameObject Vineta;
    [SerializeField] SpriteRenderer VinetaMat;
    [SerializeField] private PlayerMove pmove; // Referencja do komponentu PlayerMove


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            //Debug.LogError("Brak komponentu Rigidbody2D na obiekcie!");
        }
        //Debug.Log("Skrypt AngleShooter zosta� uruchomiony. Naci�nij spacj�, aby rozpocz�� wyb�r k�ta (zakres: -90� do 90�).");
        if (pmove == null)
        {
            pmove = GetComponent<PlayerMove>();
        }
    }

    void Update()
    {
        if (canSelectAngle)
        {
            // Rozpocz�cie wyboru k�ta
            if (Input.GetKeyDown(KeyCode.Space) && pmove.isTouchDown == true)
            {
                isHoldingSpace = true;
                startTime = Time.time;
                Arrow.SetActive(true);
                //Debug.Log("Rozpocz�to trzymanie spacji. Start wyboru k�ta: " + startTime);
            }

            // Przytrzymywanie spacji powoduje zmian� k�ta w zakresie od -90 do 90 stopni
            if (isHoldingSpace && Input.GetKey(KeyCode.Space) && pmove.isTouchDown == true)
            {
                float elapsed = Time.time - startTime;
                // Mathf.PingPong generuje warto�� od 0 do 180, po czym odejmujemy 90 aby uzyska� zakres [-90, 90]
                currentAngle = Mathf.PingPong(elapsed / angleCycleTime * 180f, 180f) - 90f;
                float normalizedAngle = (1f - (currentAngle + 90f) / 180f);

                playerArrow.SetValue(normalizedAngle);
                //Debug.Log("Aktualny k�t: " + currentAngle + "� (czas trzymania: " + elapsed.ToString("F2") + " sekundy)");
                VinetaMat.material.SetFloat("_darknessStrength", normalizedAngle * 4f + 1f);
            }

            // Zwolnienie spacji powoduje wykonanie skoku
            if (isHoldingSpace && Input.GetKeyUp(KeyCode.Space) && pmove.isTouchDown == true)
            {
                isHoldingSpace = false;
                //Debug.Log("Zwolniono spacj�. Wybrany k�t: " + currentAngle + "�");
                ShootAtAngle(currentAngle);

                Arrow.SetActive(false);
                VinetaMat.material.SetFloat("_darknessStrength", 0f);
            }
        }
    }

    // Metoda odpowiedzialna za wykonanie skoku
    private void ShootAtAngle(float angle)
    {
        // Konwersja k�ta na radiany

        float angleRad = angle * Mathf.Deg2Rad;

        // Obliczenie kierunku dla pe�nego zakresu k�t�w.

        // Przyjmujemy, �e:

        // 0�   -> (0,1) czyli g�ra,

        // 90�  -> (1,0) czyli prawo,

        // 180� -> (0,-1) czyli d�,

        // 270� -> (-1,0) czyli lewo.

        Vector2 shootDirection = new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad)).normalized;

        // Dodajemy impuls do Rigidbody2D
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);

        //Debug.Log("Skok wykonany pod k�tem: " + angle + "�." +
        //          "\nKierunek wektorowy: " + shootDirection +
        //          "\nImpulse: " + shootForce);
    }
}